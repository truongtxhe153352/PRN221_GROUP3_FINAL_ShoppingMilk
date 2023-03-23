using System;
using System.Collections.Generic;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class Cart
    {
        public long CartId { get; set; }
        public long MilkId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? DateCreate { get; set; }

        public virtual Milk Milk { get; set; } = null!;
    }
}
