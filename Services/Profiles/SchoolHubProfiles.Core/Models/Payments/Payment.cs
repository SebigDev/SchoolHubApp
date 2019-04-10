using SchoolHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Models.Payments
{
    public class Payment
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long  ClassId { get; set; }
        public decimal Amount { get; set; }

        public FeeType FeeType { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentRefernceId { get; set; }
    }
}
