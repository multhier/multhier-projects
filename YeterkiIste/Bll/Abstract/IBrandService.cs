using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bll.Abstract
{
    public interface IBrandService : IDisposable
    {
        ICollection<Brand> GetAll(Expression<Func<Brand, bool>> filter = null);
        IQueryable<Brand> GetAllWithQuery();
        Brand Get();
        Brand GetById(int id);

    }
}
