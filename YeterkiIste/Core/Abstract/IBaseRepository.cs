using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Abstract
{
    // Saf soyutlama: EF/DbSet/IQueryable tipleri içermez. Uygulama Dal katmanındadır.
    public interface IBaseRepository<T> where T : class, IData, new()
    {
        ICollection<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter = null);

        T Add(T prm);
        Task<T> AddAsync(T prm);

        T Update(T prm);
        Task<T> UpdateAsync(T prm);

        void Delete(T prm);
        T SoftDelete(T prm);
        Task DeleteAsync(T prm);

        T GetbyName(string name);

        void Create(T prm);
    }
}
