using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps.Domain.Models
{
    public class Type
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        public ICollection<UserType> UserType { get; set; } = new Collection<UserType>();
    }
}
