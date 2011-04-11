using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Omega.Client.Formatting
{
    #region Enums

    #endregion

    /// <summary>
    /// Responsible for parsing human-friendly control codes from a string and transforming them into sign-readable format.
    /// </summary>
    public static class FormatParser
    {
        private static readonly Regex ExtendedCharsRegex = new Regex("([\x80-\xFF])");

        public static string ExpandExtendedChars(string s)
        {
            s = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(s));
            var matches = ExtendedCharsRegex.Matches(s);
            foreach(Match match in matches)
            {
                var c = match.Value[0];
                var b = ((byte) c) - 167;
                var replace = string.Format("{0}{1}",
                    (char) FormatCodes.ExtendedCharacterSet,
                    (char) b);
                s = s.Replace(c.ToString(), replace);
            }

            return s;
        }
    }
}
