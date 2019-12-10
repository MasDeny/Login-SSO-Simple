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
    public class UserResource
    {
        public int Id { get; set; }
        
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(255)]
        public string Url { get; set; }
        //public IEnumerable<string> Url { get; set; }

        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> Types { get; set; }
    }
}
