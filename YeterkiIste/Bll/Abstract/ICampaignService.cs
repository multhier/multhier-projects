using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Abstract
{
    public interface ICampaignService : IDisposable
    {
        ICollection<Campaign> GetAll(Expression<Func<Campaign, bool>> filter = null);
        Campaign Get(Expression<Func<Campaign, bool>> filter = null);
        Campaign GetById(int id);

        Campaign Add(Campaign prm);
        Task<Campaign> AddAsync(Campaign prm);

        Campaign Update(Campaign prm);
        Task<Campaign> UpdateAsync(Campaign prm);

        void Delete(Campaign prm);
        Campaign SoftDelete(Campaign prm);
        Task DeleteAsync(Campaign prm);

    }
}
