using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Omega.Client.Formatting
{
    /// <summary>
    /// Responsible for parsing human-friendly control codes from a string and transforming them into sign-readable format.
    /// </summary>
    public static class FormatExtensions
    {
        private static readonly IEnumerable<IFormatter> Formatters = new List<IFormatter> {
            new ExtendedCharsFormatter(),
            new TagFormatter()
        };

        public static string Format(this string input)
        {
            return Formatters.Aggregate(input, (s, formatter) => formatter.Format(s));
        }

        public static byte[] ToAscii(this string input)
        {
            return Encoding.ASCII.GetBytes(input);
        }
    }
}
