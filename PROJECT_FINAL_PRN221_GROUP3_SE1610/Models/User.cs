using System;
using System.Collections.Generic;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class User
    {
        public User()
        {
            Milk = new HashSet<Milk>();
            Orders = new HashSet<Order>();
            Roles = new HashSet<Role>();
        }

        public long UserId { get; set; }
        public string? Name { get; set; }
        public string? Passwork { get; set; }
        public string? Address { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Milk> Milk { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
