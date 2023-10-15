using ShopDB.Repositories.EntityModel;
using ShopDB.Repositories.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Repositories.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ShopDBContext context) : base(context)
        {
        }
    }
}
