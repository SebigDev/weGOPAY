using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Enums
{
    public enum UserStatusEnum
    {
        [Description("Registered")]
        Registered = 1,

        [Description("Pending")]
        Pending,

        [Description("Activated")]
        Activated,

        [Description("De-Activated")]
        DeActivated

    }
}
