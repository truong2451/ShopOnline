using System;
using System.Collections.Generic;

namespace ShopDB.Repositories.EntityModel
{
    public partial class OrderDetail
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }
        public int Quantity { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
