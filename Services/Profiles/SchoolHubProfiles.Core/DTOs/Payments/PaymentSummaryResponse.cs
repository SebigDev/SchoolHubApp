using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Payments
{
    public class PaymentSummaryResponse
    {
        public string StudentFullname { get; set; }
        public string Classname { get; set; }
        public List<PaymentType> PaymentType { get; set; }
        public decimal SumTotal { get; set; }
    }


    public class PaymentType
    {
        public string PayName { get; set; }
        public decimal PayAmount { get; set; }
    }

 
}
