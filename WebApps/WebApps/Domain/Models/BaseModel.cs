using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps.Domain.Models
{
    public class BaseModel
    {
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}
