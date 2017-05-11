using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Cake.Core.IO;
using Cake.Core;

namespace Cake.NSwag.Console
{
    internal static class Extensions
    {
        internal static ProcessArgumentBuilder AddSwitch(this ProcessArgumentBuilder args, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return args;
            return args.Append($"/{key.Trim('/', ':')}:{value}");
        }

        [DebuggerStepThrough]
        internal static KeyValuePair<string, string> SplitClassPath(this string s)
        {
            if (s.Contains("."))
            {
                var segments = s.Split('.');
                return new KeyValuePair<string, string>(string.Join(".", segments.Take(segments.Length - 1)),
                    segments.Last());
            }
            return new KeyValuePair<string, string>("Generated", s);
        }
    }
}
