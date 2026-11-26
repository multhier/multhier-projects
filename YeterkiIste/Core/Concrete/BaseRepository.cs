using Core.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Concrete
{
    // TContext artık "new()" ile oluşturulmuyor: tüm metotlar DI'dan gelen (istek başına tek) context'i kullanır.
    // Böylece okunan ve güncellenen entity'ler aynı context'te kalır ve bağlantı ayarı tek yerden gelir.
    public class BaseRepository<T, TContext> : IBaseRepository<T> where T : class, IData, new() where TContext : DbContext
    {
        protected TContext context;  //Repository ye açılsın
        public BaseRepository(TContext _context)
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
        public virtual IQueryable<T> GetAllWithIQueryable()
        {
            return context.Set<T>();
        }

        public virtual void Create(T prm)
        {
            context.Set<T>().Add(prm);
        }

        public virtual T Add(T prm)
        {
            context.Entry(prm).State = Microsoft.EntityFrameworkCore.EntityState.Added; 
            return prm;
        }
        public async Task<T> AddAsync(T prm)
        { 
                var addedEntity = context.Entry(prm);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
                return prm; 
        }




        public virtual T Update(T prm)
        {
            context.Entry(prm).State = Microsoft.EntityFrameworkCore.EntityState.Modified; 
            return prm;
        }

        public async Task<T> UpdateAsync(T prm)
        { 
                var updatedEntity = context.Entry(prm);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
                return prm; 
        }
        public virtual void Delete(T prm)
        {
            // Eskiden state "Modified" yapılıyordu, yani kayıt hiç silinmiyordu.
            context.Entry(prm).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();
        }

        public async Task DeleteAsync(T prm)
        { 
                var deletedEntity = context.Entry(prm);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync(); 
        }


        public virtual T SoftDelete(T prm)
        {
            context.Entry(prm).State = Microsoft.EntityFrameworkCore.EntityState.Modified; 
            return prm;
        }

        public DbSet<T> Tablo()
        {
            return context.Set<T>(); 
        }

        public T GetbyName(string name)
        {
            // Find(name) birincil anahtarla arar; int Id'li tablolarda hata veriyordu.
            // "Name" sütununa göre arar (Name sütunu olmayan entity'lerde kullanılmamalı).
            return context.Set<T>().FirstOrDefault(x => EF.Property<string>(x, "Name") == name);
        }


    }
}
