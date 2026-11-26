using Bll.Abstract;
using Dal.Abstract;
using Model;
using System;
using System.Collections.Generic;

namespace Bll.Concrete
{
    class OrderService : IOrderService
    {
        IOrderRepository repo;
        public OrderService(IOrderRepository prm)
        {
            repo = prm;
        }

        public void Add(Order prm)
        {
            repo.Add(prm);
            
        }

        public void Create(Order prm)
        {
            repo.Create(prm);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

  
        public List<Order> GetOrders(string userId)
        {
            return repo.GetOrders(userId);
        }
    }
}
