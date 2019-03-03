using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SchoolHub.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attr.Length == 0 ? value.ToString() : (attr[0] as DescriptionAttribute).Description;
        }
    }
}
