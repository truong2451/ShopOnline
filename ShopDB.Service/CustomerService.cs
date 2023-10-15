using ShopDB.Repositories.EntityModel;
using ShopDB.Repositories.Repository.Interface;
using ShopDB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repository;

        public CustomerService(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            try
            {
                return repository.GetAll(x => x.IsDelete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Customer> GetCustomerById(Guid id)
        {
            try
            {
                return await repository.Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SignUp(Customer customer)
        {
            try
            {
                customer.IsActive = true;
                customer.IsDelete = false;
                return await repository.Add(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            try
            {
                var cus = await repository.Get(id);
                if (cus != null)
                {
                    cus.IsDelete = true;
                    return await repository.Update(id, cus);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateCustomer(Guid id, Customer customer)
        {
            try
            {
                var cus = await repository.Get(id);
                if (cus != null)
                {
                    cus.PhoneNumber = customer.PhoneNumber;
                    cus.FullName = customer.FullName;
                    cus.Address = customer.Address;
                    return await repository.Update(cus.CustomerId, cus);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            try
            {
                var cus = await repository.Get(id);
                if (cus != null)
                {
                    if (cus.Password == oldPassword)
                    {
                        cus.Password = newPassword;
                        return await repository.Update(id, cus);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
