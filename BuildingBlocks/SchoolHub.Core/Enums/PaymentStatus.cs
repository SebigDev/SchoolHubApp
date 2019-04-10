using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SchoolHub.Core.Enums
{
    public enum PaymentStatus
    {
        [Description("Not Paid")]
        NotPaid = 1,

        [Description("Paid")]
        Paid,

        [Description("Processing")]
        Processing,

        [Description("Failed")]
        Failed

    }
}
