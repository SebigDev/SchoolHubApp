﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SchoolHub.Core.Enums
{
    public enum UserTypeEnum
    {
       
        [Description("Admin")]
        Admin = 1,

        [Description("Staff")]
        Staff,

        [Description("Student")]
        Student

    }
}
