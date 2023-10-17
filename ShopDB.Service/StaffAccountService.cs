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
    public class StaffAccountService : IStaffAccountService
    {
        private readonly IStaffAccountRepository repository;

        public StaffAccountService(IStaffAccountRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<StaffAccount> GetAllStaffAccount()
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

        public async Task<StaffAccount> GetStaffAccountById(Guid id)
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

        public async Task<bool> AddStaffAccount(StaffAccount staffAccount)
        {
            try
            {
                staffAccount.Role = 1;
                staffAccount.IsActive = true;
                staffAccount.IsDelete = false;
                return await repository.Add(staffAccount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteStaffAccount(Guid id)
        {
            try
            {
                var account = await repository.Get(id);
                if (account != null)
                {
                    account.IsDelete = true;
                    return await repository.Update(account.StaffId, account);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateProfileStaff(Guid id, StaffAccount staffAccount)
        {
            try
            {
                var account = await repository.Get(id);
                if (account != null)
                {
                    account.PhoneNumber = staffAccount.PhoneNumber;
                    account.FullName = staffAccount.FullName;
                    return await repository.Update(account.StaffId, account);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteStaff(Guid id)
        {
            try
            {
                var account = await repository.Get(id);
                if (account != null)
                {
                    account.IsActive = false;
                    account.IsDelete = true;
                    return await repository.Update(account.StaffId, account);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ChangePasswordStaff(Guid id, string oldPassword, string newPassword)
        {
            try
            {
                var account = await repository.Get(id);
                if (account != null)
                {
                    if (account.Password == oldPassword)
                    {
                        account.Password = newPassword;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public StaffAccount CheckLogin(string username)
        {
            try
            {
                var account = repository.GetAll(x => x.Username == username).FirstOrDefault();
                if(account != null)
                {
                    return account;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
