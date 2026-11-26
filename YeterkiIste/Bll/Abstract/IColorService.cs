using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bll.Abstract
{
    public interface IColorService : IDisposable
    {
        ICollection<Color> GetAll(Expression<Func<Color, bool>> filter = null);
        IQueryable<Color> GetAllWithQuery();
        Color Get();
        Color GetById(int id);
         
    }
}
