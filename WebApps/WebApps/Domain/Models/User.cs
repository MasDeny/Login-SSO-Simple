
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Domain.Enum;

namespace WebApps.Domain.Models
{
    public class User : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();
        public ICollection<UserType> UserTypes { get; set; } = new Collection<UserType>();
    }
}
