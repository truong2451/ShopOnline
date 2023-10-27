﻿using ShopDB.Repositories.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Service.Interface
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomer();
        Task<Customer> GetCustomerById(Guid id);
        Task<bool> SignUp(Customer customer);
        Task<bool> UpdateCustomer(Guid id, Customer customer);
        Task<bool> DeleteCustomer(Guid id);
        Task<bool> ChangePassword(Guid id, string oldPassword, string newPassword);
        Customer CheckLogin(string username);
    }
}
