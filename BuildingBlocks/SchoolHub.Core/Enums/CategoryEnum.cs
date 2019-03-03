using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SchoolHub.Core.Enums
{
    public enum CategoryEnum
    {
        [Description("Staff")]
        Staff,

        [Description("Student")]
        Student,

        [Description("Pupil")]
        Pupil,
    }
}
