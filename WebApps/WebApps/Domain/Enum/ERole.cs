using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps.Domain.Enum
{
    public enum ERole : byte
    {
        [Description("Admin")]
        Admin = 1,
        [Description("User")]
        User = 2,
    }
}
