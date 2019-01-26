using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Enums
{
    public enum TransactionStatus
    {
        [Description("Requested")]
        Requested = 1,

        [Description("Processing")]
        Processing,

        [Description("Responded")]
        Responded
    }
}
