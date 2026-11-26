using Core.Abstract;
using Model;
using System.Collections.Generic;

namespace Dal.Abstract
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
