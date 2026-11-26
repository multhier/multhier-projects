using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Abstract
{
   public interface ICategoryService :IDisposable
    {
        ICollection<Category> GetAll(Expression<Func<Category, bool>> filter = null);
        IQueryable<Category> GetAllWithQuery();
        Category Get(Expression<Func<Category, bool>> filter = null);
        Category GetById(int id);

        Category Add(Category prm);
        Task<Category> AddAsync(Category prm);

        Category Update(Category prm);
        Task<Category> UpdateAsync(Category prm);

        void Delete(Category prm);
        Category SoftDelete(Category prm);
        Task DeleteAsync(Category prm);
        DbSet<Category> Tablo();
         Category GetbyName(string name);

    }
}
