using SchoolHubProfiles.Core.Models.Subjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Models.Mapping
{
    public class ClassSubjectMap
    {
        public long Id { get; set; }
        public long ClassId { get; set; }

        public long SubjectId { get; set; }

        public DateTime? MappedOn { get; set; }
    }
}
