using ShopDB.Repositories.EntityModel;
using ShopDB.Repositories.Repository.Interface;
using ShopDB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
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
                    cus.IsActive = false;
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

        public Customer CheckLogin(string username)
        {
            try
            {
                var account = repository.GetAll(x => x.Username == username).FirstOrDefault();
                if (account != null)
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

        public async Task<bool> VerifyEmail(string _from, string _to, string _subject, string _body, SmtpClient client)
        {
            try
            {
                MailMessage message = new MailMessage(
                    from: _from,
                    to: _to,
                    subject: _subject,
                    body: _body
                );

                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.ReplyToList.Add(new MailAddress(_from));
                message.Sender = new MailAddress(_from);

                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ForgetPwd(string email, string _from, string _subject, SmtpClient client)
        {
            try
            {
                var checkEmail = repository.GetAll(x => x.EmailAddres == email).FirstOrDefault();
                if (checkEmail != null)
                {
                    checkEmail.Password = GenerateRandomPwd();
                    await repository.Update(checkEmail.CustomerId, checkEmail);

                    MailMessage message = new MailMessage(
                        from: _from,
                        to: email,
                        subject: _subject,
                        body: "Your's New Password Is: " + checkEmail.Password
                    );

                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.SubjectEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.ReplyToList.Add(new MailAddress(_from));
                    message.Sender = new MailAddress(_from);

                    await client.SendMailAsync(message);
                    return true;
                }
                else
                {
                    throw new Exception("Invalid Email");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GenerateRandomPwd()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = 8;
            Random random = new Random();
            char[] randomArray = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomArray[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomArray);
        }
    }
}
