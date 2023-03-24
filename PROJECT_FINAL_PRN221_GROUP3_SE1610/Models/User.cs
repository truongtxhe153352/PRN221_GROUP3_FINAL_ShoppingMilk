using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PROJECT_FINAL_PRN221_GROUP3_SE1610.Models
{
    public partial class User
    {
        public User()
        {
            Milk = new HashSet<Milk>();
            Orders = new HashSet<Order>();
        }

        public long UserId { get; set; }
        public string? Username { get; set; }
        public string? Passwork { get; set; }
        public string? Address { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public long? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Milk> Milk { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
