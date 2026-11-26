using Bll.Abstract;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using Web.Infastructure;
using Web.ViewModels;
namespace Web.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private const int MaxQuantityPerItem = 100;
        private readonly IyzicoSettings iyzico;

        public CartController(IUnitOfWork uow, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IOptions<IyzicoSettings> iyzicoOptions) : base(uow, userManager, signInManager, roleManager)
        {
            iyzico = iyzicoOptions.Value;
        }

        public IActionResult Index()
        {
            var cart = GetOrCreateCart(userManager.GetUserId(User));
            return View(ToCartVM(cart));
        }

        // Eskiden GET ile de çalışıyordu (CSRF) ve adet kontrolü yoktu: quantity=-5 ile sepet tutarı eksiye düşürülebiliyordu.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity)
        {
            if (quantity < 1 || quantity > MaxQuantityPerItem)
            {
                return BadRequest("Geçersiz adet.");
            }

            var product = uow.Productuow.GetById(productId);
            if (product == null || !product.IsActive)
            {
                return NotFound();
            }

            var userId = userManager.GetUserId(User);
            GetOrCreateCart(userId);
            uow.Cartuow.AddToCart(userId, productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFromCart(int productId)
        {
            uow.Cartuow.DeleteFromCart(userManager.GetUserId(User), productId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = GetOrCreateCart(userManager.GetUserId(User));
            if (cart.CartItems == null || cart.CartItems.Count == 0)
            {
                return RedirectToAction("Index");
            }

            return View(new OrderVM { CartVM = ToCartVM(cart) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(OrderVM prm)
        {
            var userId = userManager.GetUserId(User);
            var cart = GetOrCreateCart(userId);

            // Sepet bilgisi her zaman veritabanından kurulur (formdan gelen fiyata güvenilmez).
            // Eskiden hata durumunda CartVM boş kalıyordu ve sayfa patlıyordu.
            prm.CartVM = ToCartVM(cart);

            if (prm.CartVM.CartItems.Count == 0)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(prm);
            }

            if (string.IsNullOrWhiteSpace(iyzico.ApiKey) || string.IsNullOrWhiteSpace(iyzico.SecretKey))
            {
                ModelState.AddModelError("", "Ödeme sistemi yapılandırılmamış (Iyzico:ApiKey / Iyzico:SecretKey).");
                return View(prm);
            }

            var payment = PaymentProcess(prm, userId);
            if (payment.Status == "success")
            {
                SaveOrder(prm, payment, userId);
                ClearCart(cart.Id);
                return View("Success");
            }

            ModelState.AddModelError("", "Ödeme alınamadı: " + (payment.ErrorMessage ?? "bilinmeyen hata"));
            return View(prm);
        }

        private Cart GetOrCreateCart(string userId)
        {
            var cart = uow.Cartuow.GetCartByUserId(userId);
            if (cart == null)
            {
                // Sepeti olmayan kullanıcıda (ör. kayıt sırasında sepet oluşmadıysa) eskiden NullReferenceException alınıyordu.
                uow.Cartuow.InitializeCart(userId);
                uow.SaveChanges();
                cart = uow.Cartuow.GetCartByUserId(userId);
            }
            return cart;
        }

        private static CartVM ToCartVM(Cart cart)
        {
            return new CartVM()
            {
                CartId = cart.Id,
                CartItems = (cart.CartItems ?? new List<CartItem>())
                    .Select(x => new CartItemtVM() { CartItemId = x.Id, ProductId = x.Product.Id, Name = x.Product.Name, Price = x.Product.Price, ImageUrl = x.Product.ImageUrl, Quantity = x.Quantity })
                    .ToList()
            };
        }

        private void ClearCart(int cartId)
        {
            uow.Cartuow.ClearitemsFromCart(cartId);
        }

        private void SaveOrder(OrderVM prm, Payment payment, string userId)
        {
            var order = new Order();
            // Eskiden Random().Next(111111, 999999) kullanılıyordu; aynı numara tekrar gelebiliyordu.
            order.OrderNumber = DateTime.Now.ToString("yyMMddHHmmss") + RandomNumberGenerator.GetInt32(1000, 10000);
            order.OrderState = EnumOrderState.Tamamlandi;
            order.PaymentTypes = EnumPaymentTypes.CreditCard;
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.AddedTime = DateTime.Now;
            order.FirstName = prm.FirstName;
            order.LastName = prm.LastName;
            order.Address = prm.Address;
            order.City = prm.City;
            order.Email = prm.Email;
            order.Phone = prm.Phone;
            order.UserId = userId;

            foreach (var item in prm.CartVM.CartItems)
            {
                var orderitem = new OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };
                order.OrderItems.Add(orderitem);
            }

            uow.Orderuow.Create(order);
            uow.SaveChanges();
        }

        /// <summary>
        /// iyzico tutarları nokta ayraçlı metin bekler ("49.90").
        /// Eskiden ToString().Split(",")[0] ile kuruşlar atılıyordu (49,90 TL için 49 TL çekiliyordu)
        /// ve sonuç sunucunun dil ayarına göre değişiyordu.
        /// </summary>
        private static string ToIyzicoPrice(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private Payment PaymentProcess(OrderVM prm_model, string userId)
        {
            var options = new Iyzipay.Options();
            options.ApiKey = iyzico.ApiKey;
            options.SecretKey = iyzico.SecretKey;
            options.BaseUrl = iyzico.BaseUrl;

            // Sepet kalemi tutarı = birim fiyat x adet. Eskiden adet yok sayılıyordu; iyzico sepet toplamının
            // Price ile eşit olmasını istediği için 2 adetli siparişlerde ödeme reddediliyordu.
            List<BasketItem> basketItems = new List<BasketItem>();
            decimal basketTotal = 0;
            foreach (var item in prm_model.CartVM.CartItems)
            {
                var lineTotal = Math.Round(item.Price * item.Quantity, 2, MidpointRounding.AwayFromZero);
                basketTotal += lineTotal;

                basketItems.Add(new BasketItem
                {
                    Id = item.ProductId.ToString(),
                    Name = item.Name,
                    Category1 = "esya",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = ToIyzicoPrice(lineTotal)
                });
            }

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = ToIyzicoPrice(basketTotal);
            request.PaidPrice = ToIyzicoPrice(basketTotal);
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = prm_model.CartVM.CartId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.BasketItems = basketItems;

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = prm_model.CardName;
            paymentCard.CardNumber = new string(prm_model.CardNumber.Where(char.IsDigit).ToArray());
            paymentCard.ExpireMonth = prm_model.ExpirationMonth.PadLeft(2, '0');
            paymentCard.ExpireYear = prm_model.ExpirationYear.Length == 2 ? "20" + prm_model.ExpirationYear : prm_model.ExpirationYear;
            paymentCard.Cvc = prm_model.ExpirationCvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = userId;
            buyer.Name = prm_model.FirstName;
            buyer.Surname = prm_model.LastName;
            buyer.GsmNumber = prm_model.Phone;
            buyer.Email = prm_model.Email;
            // iyzico TC kimlik no zorunlu tutuyor; sitede bu alan toplanmadığı için iyzico dokümanındaki örnek değer kullanılıyor.
            buyer.IdentityNumber = "74300864791";
            buyer.RegistrationAddress = prm_model.Address;
            buyer.Ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            buyer.City = prm_model.City;
            buyer.Country = "Turkey";
            request.Buyer = buyer;

            // Eskiden teslimat/fatura adresi sabit "Jane Doe, Istanbul" gönderiliyordu.
            var contactName = prm_model.FirstName + " " + prm_model.LastName;
            request.ShippingAddress = new Address
            {
                ContactName = contactName,
                City = prm_model.City,
                Country = "Turkey",
                Description = prm_model.Address
            };
            request.BillingAddress = new Address
            {
                ContactName = contactName,
                City = prm_model.City,
                Country = "Turkey",
                Description = prm_model.Address
            };

            return Payment.Create(request, options);
        }

        public IActionResult GetOrders()
        {
            var orders = uow.Orderuow.GetOrders(userManager.GetUserId(User));
            var orderListVMs = new List<OrderListVM>();
            OrderListVM orderlistvm;

            foreach (var item in orders)
            {
                orderlistvm = new OrderListVM();
                orderlistvm.OrderId = item.Id;
                orderlistvm.OrderNumber = item.OrderNumber;
                orderlistvm.AddedTime = item.AddedTime;
                orderlistvm.Note = item.Note;
                orderlistvm.Phone = item.Phone;
                orderlistvm.FirstName = item.FirstName;
                orderlistvm.LastName = item.LastName;
                orderlistvm.Email = item.Email;
                orderlistvm.Address = item.Address;
                orderlistvm.City = item.City;

                orderlistvm.OrderItemVM = item.OrderItems.Select(x => new OrderItemVM()
                {
                    OrderItemId = x.Id,
                    Name = x.Product.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    ImageUrl = x.Product.ImageUrl
                }).ToList();

                orderListVMs.Add(orderlistvm);
            }
            return View(orderListVMs);
        }

    }
}
