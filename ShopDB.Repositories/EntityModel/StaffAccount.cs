using System;
using System.Collections.Generic;

namespace ShopDB.Repositories.EntityModel
{
    public partial class StaffAccount
    {
        public Guid StaffId { get; set; }
        public string PhoneNumber { get; set; }
        public string? EmailAddres { get; set; }
        public string? GoogleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
