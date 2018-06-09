using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleMessagingApp.Core.Utils
{
    public static class StringUtils
    {
        public static string FormatString(string source, IDictionary<string, object> parameters)
        {
            if (source == null)
            {
                return null;
            }

            return parameters.Aggregate(source, (current, parameter) => current.Replace(parameter.Key, parameter.Value.ToString()));
        }
    }
}
