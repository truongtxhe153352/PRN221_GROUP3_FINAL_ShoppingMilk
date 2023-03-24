using System;
using System.Collections.Generic;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public long? MilkId { get; set; }
        public int? OrderId { get; set; }

        public virtual Milk? Milk { get; set; }
        public virtual Order? Order { get; set; }
    }
}
