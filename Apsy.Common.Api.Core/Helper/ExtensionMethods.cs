using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Apsy.Common.Api.Core.Helper
{
    public static class ExtensionMethods
    {
        public static string ToDescription(this Enum en)
        {
            if (en == null)
            {
                return "null";
            }

            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

                if (attrs != null && attrs.Length > 0)
                {
                }
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return en.ToString();
        }
    }
}
