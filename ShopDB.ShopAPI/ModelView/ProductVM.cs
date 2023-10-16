namespace ShopDB.ShopAPI.ModelView
{
    public class ProductVM
    {
        public Guid? CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int? UnitInStock { get; set; }
        public string Image { get; set; }
    }
}
