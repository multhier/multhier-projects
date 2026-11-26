using Bll.Abstract;
using Dal.Abstract;
using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Concrete
{
    public class CampaignService : ICampaignService
    {
        ICampaignRepository repo;
        public CampaignService(ICampaignRepository _repo)
        {
            repo = _repo;
        }
        public ICollection<Campaign> GetAll(Expression<Func<Campaign, bool>> filter = null)
        {
            return repo.GetAll();
        }
        public Campaign Get(Expression<Func<Campaign, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Campaign GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Campaign Add(Campaign prm)
        {
            throw new NotImplementedException();
        }

        public Task<Campaign> AddAsync(Campaign prm)
        {
            throw new NotImplementedException();
        }

        public void Delete(Campaign prm)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Campaign prm)
        {
            throw new NotImplementedException();
        }




        public Campaign SoftDelete(Campaign prm)
        {
            throw new NotImplementedException();
        }

        public Campaign Update(Campaign prm)
        {
            throw new NotImplementedException();
        }

        public Task<Campaign> UpdateAsync(Campaign prm)
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
        // ~CampaignService()
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
    }
}
