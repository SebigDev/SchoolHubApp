using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Payments
{
    public class PaymentResponse
    {
        public PayChannel PayChannel { get; set; }
        public PaymentReport PaymentReport { get; set; }
       
    }

    public class PayChannel
    {
        public long ClassId { get; set; }
        public long StudentId { get; set; }
    }

    public class PaymentReport
    {
        public string PaymentStatus { get; set; }
        public string PaymentRefrenceId { get; set; }
        public string FeeType { get; set; }

        public decimal Amount { get; set; }
    }
}
