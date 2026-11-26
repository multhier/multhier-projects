using Model;
using System;
using System.Collections.Generic;

namespace Bll.Abstract
{
    public interface IOrderService : IDisposable
    {
        void Add(Order prm);
        List<Order> GetOrders(string userId);
        void Create(Order prm);
    }
}
