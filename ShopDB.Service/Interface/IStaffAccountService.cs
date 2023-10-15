﻿using ShopDB.Repositories.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Service.Interface
{
    public interface IStaffAccountService
    {
        IEnumerable<StaffAccount> GetAllStaffAccount();
        Task<StaffAccount> GetStaffAccountById(Guid id);
        Task<bool> AddStaffAccount(StaffAccount staffAccount);
        Task<bool> UpdateStaffAccount(Guid id, StaffAccount staffAccount);
        Task<bool> DeleteStaffAccount(Guid id);
    }
}