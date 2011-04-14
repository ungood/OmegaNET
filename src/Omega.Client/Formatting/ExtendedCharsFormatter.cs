using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Omega.Client.Formatting
{
    public class ExtendedCharsFormatter : IFormatter
    {
        private static readonly Regex ExtendedCharsRegex
            = new Regex("([\u0080-\uFFFF])",
                RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

        private const string Chars = "ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜ¢£¥℞ƒáíóúñÑªº¿°¡ øØćĆčČđÐŠžŽΒšβÁÀÃãÊÍÕõ€☃↑↓←→";
                             // TODO: Figure out WTH a y-punctuation mark is.

        private static readonly Dictionary<char, byte> CharMap = Chars.ToCharArray().ToDictionary(
            c => c,
            c => (byte) (Chars.IndexOf(c) + 0x20));

        public string Format(string input)
        {
            return ExtendedCharsRegex.Replace(input, match => {
                var c = match.Value[0];
                if(!CharMap.ContainsKey(c))
                    return "?";

                var b = CharMap[c];
                return string.Format("\x08{0}", (char) b);
            });
        }
    }
}