using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Omega.Client.Formatting
{
    #region Enums

    #endregion

    // BOOLEANS: 0/1, on/off, true/false, yes/no

    // Call
    //   <date format />
    //   <time/>
    //   <temperature format />
    //   <string +file />
    //   <picture +file />
    //   <animimation mode +files hold />
    //   <counter +id />
    
    // Format
    //   <flash></flash>
    //   <high></high>
    //   <descenders></descenders>
    //   <wide></wide>
    //   <double></double>
    //   <fixed></fixed>
    //   <fancy></fancy>
    //   <shadow></shadow>
    // 
    //   <font +name />
    //   <color +name />
    //   <rgb +font shade />
    
    // Extended character sets
    //    ÇüéâäàåçêëèïîìÅÉæÆô
    //    <extended decimal/hex/name />
    //    euro
    //    up,down,left,right
    //    pacman, boat, ball, telephone, heart, car, handicap, rhino,
    //    mug, satellite, copyright, male, female, bottle, diskette, printer, music, infinity
    
    /// <summary>
    /// Responsible for parsing human-friendly control codes from a string and transforming them into sign-readable format.
    /// </summary>
    public static class FormatParser
    {
        public static string Format(string input)
        {
            return ExpandExtendedChars(input);
        }

        #region Extended Characters

        private static readonly Regex ExtendedCharsRegex = new Regex("([\u0080-\uFFFF])");

        private const string ExtendedChars = "ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜ¢£¥℞ƒáíóúñÑªº¿°¡ øØćĆčČđÐŠžŽΒšβÁÀÃãÊÍÕõ€ ↑↓←→";

        private static readonly Dictionary<char, byte> CharMap = new Dictionary<char, byte>();
            
        static FormatParser()
        {
            for(int c = 0; c < ExtendedChars.Length; c++)
            {
                if(ExtendedChars[c] == ' ')
                    continue;
                CharMap.Add(ExtendedChars[c], (byte) (c + 0x20));
            }
        }

        public static string ExpandExtendedChars(string input)
        {
            return ExtendedCharsRegex.Replace(input, match => {
                var c = match.Value[0];
                if(!CharMap.ContainsKey(c))
                    return "?";

                var b = CharMap[c];
                return string.Format("\x08{0}", (char)b);
            });
        }

        #endregion
    }
}
