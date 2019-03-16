using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Classes
{
    public class StaffClassAssIgnedResponse
    {
        public long StaffId { get; set; }

        public List<ClassDto> Classes { get; set; }
    }
}
