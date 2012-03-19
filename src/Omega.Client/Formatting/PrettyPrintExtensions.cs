using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.Client.Formatting
{
    /// <summary>
    /// Pretty-prints byte-strings.
    /// </summary>
    public static class PrettyPrintExtensions
    {
        /// <summary>
        /// Outputs a human-readable string from a collection of bytes.
        /// </summary>
        public static string PrettyPrint(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach(var b in bytes)
            {
                sb.Append(b.PrettyPrint());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Outputs a human-readable string for a single byte.
        /// </summary>
        public static string PrettyPrint(this byte b)
        {
            if(Enum.IsDefined(typeof(Ascii), b))
                return "<" + ((Ascii) b)+ ">";
            
            return ((char) b).ToString();
        }
    }
}
