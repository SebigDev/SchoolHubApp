using SchoolHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Models.Classes
{
   public class ClassName
    {
        public int Id { get; set; }

        public string ClassCode { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }

        public CategoryEnum Category { get; set; }    }
}
