using System;
using System.Collections.Generic;

namespace ShopDB.Repositories.EntityModel
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public Guid CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddres { get; set; }
        public string GoogleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
