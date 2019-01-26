using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Enums
{
    public enum SettlementStatus
    {
        [Description("Success")]
        Success = 1,

        [Description("Failed")]
        Failed
    }
}
