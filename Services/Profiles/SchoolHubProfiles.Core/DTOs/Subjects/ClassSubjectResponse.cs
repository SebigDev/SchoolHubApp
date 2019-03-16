using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Subjects
{
    public class ClassSubjectResponse
    {
        public long ClassId { get; set; }

        public List<SubjectDto> Subjects { get; set; }
    }
}
