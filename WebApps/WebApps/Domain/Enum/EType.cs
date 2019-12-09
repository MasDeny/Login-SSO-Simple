using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps.Domain.Enum
{
    public enum EType : byte
    {
        [Description("Scale")]
        Scale = 1,
        [Description("Energy")]
        Energy = 2
    }
}
