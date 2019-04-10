using SchoolHub.Core.Enums;
using System;

namespace SchoolHubProfiles.Core.Models.Payments
{
    public class Amount
    {
        public int Id { get; set; }
        public FeeType FeeType { get; set; }
        public long ClassId { get; set; }
        public decimal FeeAmount {get; set;}
        public DateTime DateCreated { get; set; } 
        public DateTime? UpdatedDate { get; set; }
    }
}
