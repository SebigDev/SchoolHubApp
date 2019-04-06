using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Subjects
{
    public  class StudentSubjectsResponse
    {
        public long StudentId { get; set; }

        public List<SubjectDto> Subjects { get; set; }
    }
}
