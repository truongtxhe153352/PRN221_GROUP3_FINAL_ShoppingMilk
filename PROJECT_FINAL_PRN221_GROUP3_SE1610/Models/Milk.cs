using System;
using System.Collections.Generic;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class Milk
    {
        public Milk()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }
        public string? Price { get; set; }
        public string? Decription { get; set; }
        public string? Published { get; set; }
        public string? ImageUrl { get; set; }
        public long? CateId { get; set; }
        public long? Userid { get; set; }

        public virtual Category? Cate { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
