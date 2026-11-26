using Bll.Abstract;
using Dal.Abstract;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bll.Concrete
{
    public class BrandService : IBrandService
    {
        IBrandRepository repo;

        public BrandService(IBrandRepository _repo)
        {
            repo = _repo;
        }

        public Brand Get()
        {
            return repo.Get();
        }

        public ICollection<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            return repo.GetAll();
        }

        public IQueryable<Brand> GetAllWithQuery()
        {
            return repo.GetAllWithIQueryable();
        }

        public Brand GetById(int id)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BrandService()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
