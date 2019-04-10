using SchoolHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Payments
{
    public class CreateAmountDto
    {
        public FeeType FeeType { get; set; }
        public long ClassId { get; set; }
        public decimal FeeAmount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
