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
   public class ColorService :IColorService
    {
        IColorRepository repo;

        public ColorService(IColorRepository _repo)
        {
            repo = _repo;
        }

        public Color Get()
        {
            throw new NotImplementedException();
        }
 
        public ICollection<Color> GetAll(Expression<Func<Color, bool>> filter = null)
        {
            return repo.GetAll();
        }

        public IQueryable<Color> GetAllWithQuery()
        {
            throw new NotImplementedException();
        }

        public Color GetById(int id)
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
        // ~ColorService()
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
