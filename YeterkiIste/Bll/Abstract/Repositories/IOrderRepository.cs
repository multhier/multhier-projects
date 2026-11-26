using Core.Abstract;
using Model;
using System.Collections.Generic;

namespace Bll.Abstract.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
