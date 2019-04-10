using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Payments
{
    public class AmountDto
    {
        public int Id { get; set; }
        public string FeeType { get; set; }
        public long ClassId { get; set; }
        public decimal FeeAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
