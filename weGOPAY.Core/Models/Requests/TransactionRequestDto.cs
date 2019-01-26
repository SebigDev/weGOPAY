using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Models.Requests
{
    public class TransactionRequestDto
    {
        public long Id { get; set; }
        public string UserId { get; set; }

        public int Currency { get; set; }

        public int TransactionType { get; set; }

        public int ReqCurrency { get; set; }

        public int ResCurrency { get; set; }


        public decimal Amount { get; set; }

        public decimal CurrentBalance { get; set; }


        public string RequestStatus { get; set; }


        public DateTime CreditRequestDate { get; set; }
    }
}
