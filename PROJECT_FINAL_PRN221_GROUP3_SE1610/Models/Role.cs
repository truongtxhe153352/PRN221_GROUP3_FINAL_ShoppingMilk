using System;
using System.Collections.Generic;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string? RoleName { get; set; }
        public long RoleId { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
