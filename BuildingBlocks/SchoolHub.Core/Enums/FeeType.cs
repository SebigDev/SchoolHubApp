using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SchoolHub.Core.Enums
{
    public enum FeeType
    {
        [Description("School Fee")]
        SchoolFee = 1,

        [Description("Exams Fee")]
        ExamsFee,

        [Description("PTA Levy")]
        PTALevy,

        [Description("Other Fee")]
        OtherFee
    }
}
