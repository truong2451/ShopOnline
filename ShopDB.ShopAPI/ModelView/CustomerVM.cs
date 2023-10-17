namespace ShopDB.ShopAPI.ModelView
{
    public class CustomerVM
    {
        public string PhoneNumber { get; set; }
        public string? EmailAddres { get; set; }
        public string? GoogleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }

    public class CustomerUpdateVM
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
