using Bll.Abstract;
using Dal.Abstract;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Concrete
{
   public class CategoryService : ICategoryService
    {
        ICategoryRepository repo;
        public CategoryService(ICategoryRepository prm)
        {
            repo = prm;
        }

        //public ICollection<Category> GetAll(Expression<Func<Category, bool>> filter = null)
        //{
        //    return repo.GetAll();
        //}
        public ICollection<Category> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            List<Category> data = repo.GetAll().ToList();
            if (data == null)
            {
                InitializeRepository();
            }

            return repo.GetAll();
        }
        public Category Get(Expression<Func<Category, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            return repo.Get(x => x.Id == id);
        }
        public DbSet<Category> Tablo()
        {
            return repo.Tablo();
        }
 

        public IQueryable<Category> GetAllWithQuery()
        {
            return repo.GetAllWithIQueryable();
        }


        public Category Add(Category prm)
        {
            throw new NotImplementedException();
        }

        public Task<Category> AddAsync(Category prm)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category prm)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Category prm)
        {
            throw new NotImplementedException();
        }

 
     

        public Category SoftDelete(Category prm)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category prm)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateAsync(Category prm)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Artık çağrıları algılama

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: yönetilen durumu (yönetilen nesneleri) bırakın.
                }

                // TODO: yönetilmeyen kaynakları (yönetilmeyen nesneleri) serbest bırakın ve aşağıda bir sonlandırıcıyı geçersiz kılın.
                // TODO: büyük alanları null olarak ayarlayın.

                disposedValue = true;
            }
        }

        // TODO: yalnızca yukarıdaki Dispose(bool disposing) ifadesi yönetilmeyen kaynakları serbest bırakacak koda sahipse sonlandırıcıyı geçersiz kılın.
        // ~CategoryService()
        // {
        //   // Bu kodu değiştirmeyin. Temizleme kodunu yukarıdaki Dispose(bool disposing) içine yerleştirin.
        //   Dispose(false);
        // }

        // Atılabilir deseni doğru uygulamak için bu kod eklendi.
        public void Dispose()
        {
            // Bu kodu değiştirmeyin. Temizleme kodunu yukarıdaki Dispose(bool disposing) içine yerleştirin.
            Dispose(true);
            // TODO: sonlandırıcı yukarıda geçersiz kılınırsa aşağıdaki satırın açıklamasını kaldırın.
            // GC.SuppressFinalize(this);
        }
        #endregion


        private List<Category> _category;
        private void InitializeRepository()
        {
            _category = new List<Category>
            {
                new Category{ Id=1,Name="Choco Chips",  IsActive=true,AddedTime= DateTime.Now,ImageUrl ="\\Images\\Chocochip.png"}, 
                new Category{ Id=2,Name="Choco Chips",  IsActive=true,AddedTime= DateTime.Now, ImageUrl ="\\Images\\ChocolateChipWalnut.png"},
                new Category{Id=3, Name="Choco Chips",  IsActive=true,AddedTime= DateTime.Now,ImageUrl ="\\Images\\Nuts.png"},
            };
        }

        public Category GetbyName(string name)
        {
            return repo.GetbyName(name);
        }

    }
}
