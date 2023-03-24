using System;
using System.Collections.Generic;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class Category
    {
        public Category()
        {
            Milk = new HashSet<Milk>();
        }

        public long CategoryId { get; set; }
        public long? BrandId { get; set; }
        public string? Name { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual ICollection<Milk> Milk { get; set; }
    }
}
