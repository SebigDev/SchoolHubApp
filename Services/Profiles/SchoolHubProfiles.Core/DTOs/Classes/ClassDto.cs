using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Classes
{
   public class ClassDto
    {
        public int Id { get; set; }

        public string ClassCode { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string Category { get; set; }
    }
}
