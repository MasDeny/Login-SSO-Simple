using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApps.Domain.Enum;

namespace WebApps.Resources
{
    public class SaveUserResource
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(125)]
        public string Username { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        public ERole[] RoleUser { get; set; }

        public EType[] TypeUser { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    }
}
