using Core.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dal.Concrete.EfCore
{
    // Core'daki IBaseRepository'nin EF Core uygulaması. Tüm metotlar DI'dan gelen tek context'i kullanır.
    public class BaseEfRepository<T> : IBaseRepository<T> where T : class, IData, new()
    {
        protected readonly AppDbContext context;

        public BaseEfRepository(AppDbContext _context)
        {
            context = _context;
        }

        public virtual ICollection<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? context.Set<T>().ToList()
                : context.Set<T>().Where(filter).ToList();
        }

        public virtual T Get(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? context.Set<T>().FirstOrDefault()
                : context.Set<T>().Where(filter).SingleOrDefault();
        }

        public virtual void Create(T prm)
        {
            context.Set<T>().Add(prm);
        }

        public virtual T Add(T prm)
        {
            context.Entry(prm).State = EntityState.Added;
            return prm;
        }

        public async Task<T> AddAsync(T prm)
        {
            context.Entry(prm).State = EntityState.Added;
            await context.SaveChangesAsync();
            return prm;
        }

        public virtual T Update(T prm)
        {
            context.Entry(prm).State = EntityState.Modified;
            return prm;
        }

        public async Task<T> UpdateAsync(T prm)
        {
            context.Entry(prm).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return prm;
        }

        public virtual void Delete(T prm)
        {
            context.Entry(prm).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public async Task DeleteAsync(T prm)
        {
            context.Entry(prm).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public virtual T SoftDelete(T prm)
        {
            context.Entry(prm).State = EntityState.Modified;
            return prm;
        }

        public T GetbyName(string name)
        {
            // Name sütununa göre arar (birincil anahtar değil).
            return context.Set<T>().FirstOrDefault(x => EF.Property<string>(x, "Name") == name);
        }
    }
}
