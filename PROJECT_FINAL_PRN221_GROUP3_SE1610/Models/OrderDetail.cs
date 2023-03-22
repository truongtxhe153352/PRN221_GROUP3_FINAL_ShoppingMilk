using System;
using System.Collections.Generic;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class OrderDetail
    {
        public long OrderDetailId { get; set; }
        public long? Quantity { get; set; }
        public long? MilkId { get; set; }
        public long? OrderId { get; set; }

        public virtual Milk? Milk { get; set; }
        public virtual Order? Order { get; set; }
    }
}
