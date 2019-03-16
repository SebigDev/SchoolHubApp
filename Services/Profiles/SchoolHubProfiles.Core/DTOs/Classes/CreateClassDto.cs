using SchoolHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Classes
{
    public class CreateClassDto
    {
        public string ClassCode { get; set; }
        public string Name { get; set; }

        public CategoryEnum Category { get; set; }
    }
}
