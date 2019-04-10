using SchoolHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Payments
{
   public class PaymentRequest
    {
        public long StudentId { get; set; }
        public long ClassId { get; set; }
        public decimal Amount { get; set; }

        public FeeType FeeType { get; set; }

        public int PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentRefernceId { get; set; }
    }
}
