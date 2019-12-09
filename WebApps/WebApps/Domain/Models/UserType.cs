using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps.Domain.Models
{
    public class UserType
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }
    }
}
