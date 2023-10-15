using Microsoft.EntityFrameworkCore;
using ShopDB.Repositories.EntityModel;
using ShopDB.Repositories.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Repositories.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ShopDBContext context;
        protected readonly DbSet<T> dbSet;  

        public GenericRepository(ShopDBContext context)
        {
            if(context == null)
            {
                this.context = context;
            }
            this.dbSet = this.context.Set<T>();
        }

        public async Task<bool> Add(T item)
        {
            try
            {
                dbSet.Add(item);
                await context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(object id)
        {
            try
            {
                var exist = dbSet.Find(id);
                if (exist != null)
                {
                    dbSet.Remove(exist);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
        public async Task<bool> Update(object id, T item)
        {
            try
            {
                var check = dbSet.Find(id);
                if (check != null)
                {
                    context.Entry(check).State = EntityState.Modified;
                    dbSet.Update(item);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public async Task<T> Get(object id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> GetAll(Func<T, bool> where, params Expression<Func<T, bool>>[] includes)
        {
            try
            {
                IQueryable<T> query = dbSet;
                foreach (var include in includes)
                {
                    query.Include(include);
                }
                var result = query.Where(where).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
