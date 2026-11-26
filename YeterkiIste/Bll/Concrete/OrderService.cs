using Bll.Abstract;
using Bll.Abstract.Repositories;
using Model;
using System;
using System.Collections.Generic;

namespace Bll.Concrete
{
    public class OrderService : IOrderService
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
            // Kaynak tutulmuyor; no-op.
        }

  
        public List<Order> GetOrders(string userId)
        {
            return repo.GetOrders(userId);
        }
    }
}
