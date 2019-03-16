using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Subjects
{
    public class SubjectDto
    {
        public long Id { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public DateTime? CreatedOn { get; set; }

        public long CreatedBy { get; set; }
    }
}
