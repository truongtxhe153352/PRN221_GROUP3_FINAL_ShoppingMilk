using System;
using System.Collections.Generic;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long OrderId { get; set; }
        public string? OrderDate { get; set; }
        public string? Username { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? CreateDate { get; set; }
        public long? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
