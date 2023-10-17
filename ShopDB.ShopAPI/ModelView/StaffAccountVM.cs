namespace ShopDB.ShopAPI.ModelView
{
    public class StaffAccountVM
    {
        public string? EmailAddres { get; set; }
        public string? GoogleId { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }

    public class StaffAccountUpdateVM
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
    }
}
