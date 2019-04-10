using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SchoolHub.Core.Enums
{
    public enum CategoryEnum
    {
        [Description("Nusery")]
        Nusery = 1,

        [Description("Primary")]
        Primary,

        [Description("Secondary")]
        Secondary,
    }
}
