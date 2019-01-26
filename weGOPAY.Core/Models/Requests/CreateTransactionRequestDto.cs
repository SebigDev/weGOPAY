using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Enums;

namespace weGOPAY.weGOPAY.Core.Models.Requests
{
    public class CreateTransactionRequestDto
    {
        public long Id { get; set; }
        public string UserId { get; set; }

        public CurrencyEnum Currency { get; set; }

        public decimal Amount { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public TransactionStatus RequestStatus { get; set; }

    }
}
