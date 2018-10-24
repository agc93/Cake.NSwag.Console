using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cake.Core.IO;
using Cake.Core;

namespace Cake.NSwag.Console
{
    internal static class Extensions
    {
        internal static ProcessArgumentBuilder AddSwitch(this ProcessArgumentBuilder args, string key, string value, bool quoteValue = false)
        {
            if (string.IsNullOrWhiteSpace(value)) return args;
            value = quoteValue ? value.Quote() : value;
            return args.Append($"/{key.Trim('/', ':')}:{value}");
        }

        [DebuggerStepThrough]
        internal static KeyValuePair<string, string> SplitClassPath(this string s)
        {
            if (!s.Contains("."))
                return new KeyValuePair<string, string>("Generated", s);

            var segments = s.Split('.');
            return new KeyValuePair<string, string>(string.Join(".", segments.Take(segments.Length - 1)), segments.Last());
        }
    }
}
