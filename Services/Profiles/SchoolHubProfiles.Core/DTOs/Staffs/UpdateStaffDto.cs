using System;

namespace SchoolHubProfiles.Core.DTOs.Staffs
{
    public class UpdateStaffDto
    {
        public UpdateStaffDto()
        {
            DateUpdated = DateTime.UtcNow;
        }

        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
    
       
        public DateTime DateEmployed { get; set; }

        public int Gender { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
