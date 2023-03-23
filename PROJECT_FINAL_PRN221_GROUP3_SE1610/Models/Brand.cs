using System;
using System.Collections.Generic;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Categories = new HashSet<Category>();
        }

        public long Id { get; set; }
        public string? BrandName { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
