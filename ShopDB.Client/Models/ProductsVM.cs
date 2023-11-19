using ShopDB.Repositories.EntityModel;

namespace ShopDB.Client.Models
{
    public class ProductsVM
    {
        public string Search { get; set; }
        public List<Product> Products { get; set; }
    }
}
