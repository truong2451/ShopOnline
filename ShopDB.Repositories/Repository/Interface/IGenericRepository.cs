using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Repositories.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Func<T, bool> where, params Expression<Func<T, bool>>[] includes);
        Task<T> Get(object id);
        Task<bool> Delete(object id);
        Task<bool> Add(T item);
        Task<bool> Update(T item);
    }
}
