using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Enums
{
    public enum TransactionTypeEnum
    {
        [Description("Credit")]
        Credit = 1,
        [Description("Debit")]
        Debit
    }
}
