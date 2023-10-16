using System;
using System.Collections.Generic;

namespace ShopDB.Repositories.EntityModel
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid ProductId { get; set; }
        public Guid? CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int? UnitInStock { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
