using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Omega.Client.Formatting
{
    public class TagFormatter : IFormatter
    {
        private static readonly Regex TagRegex
            = new Regex(@"<(?<closing>/?)(?<tag>[a-z]+)(?<arg>\s+[^\s<>/]+)*\s*(?<empty>/)?>",
            RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

        private static readonly IDictionary<string, MethodInfo> FormatMethods
            = typeof(TagFormatter)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Where(method => method.Name.StartsWith("FormatTag"))
            .ToDictionary(
                method => method.Name.Replace("FormatTag", "").ToLower(),
                method => method);

        private struct FormatOptions
        {
            public string Match { get; set; }
            public bool IsClosing { get; set; }
            public bool IsEmpty { get; set; }
            public string[] Args { get; set; }
        }

        public string Format(string input)
        {
            return TagRegex.Replace(input, match => {
                var tag = match.Groups["tag"].Value.ToLower();
                var options = new FormatOptions {
                    Match = match.Value,
                    IsClosing = match.Groups["closing"].Length > 0,
                    IsEmpty = match.Groups["empty"].Length > 0,
                    Args = match.Groups["arg"].Captures
                        .Cast<Capture>()
                        .Select(c => c.Value.Trim())
                        .ToArray()
                };
                
                var key = FormatMethods.Keys.SingleOrDefault(k => k.StartsWith(tag));
                if(key == null)
                    return match.Value;

                var method = FormatMethods[key];
                return (string)method.Invoke(null, new object[] { options });
            });
        }

        private static string FormatFromDictionary(IDictionary<string, char> dict, FormatOptions options, string prefix, string def)
        {
            if(options.Args.Length < 1)
                return def;

            var name = string.Join("-", options.Args).ToLower();
            
            char value;
            return dict.TryGetValue(name, out value)
                ? prefix + value
                : def;
        }

        #region Format Tags

        private static string FormatTagHigh(FormatOptions options)
        {
            return "\x05" + (options.IsClosing ? "0" : "1");
        }

        private static string FormatTagDescenders(FormatOptions options)
        {
            return "\x06" + (options.IsClosing ? "0" : "1");
        }
        
        private static string FormatTagFlash(FormatOptions options)
        {
            return "\x07" + (options.IsClosing ? "0" : "1");
        }
        
        private static string FormatTagWide(FormatOptions options)
        {
            return options.IsClosing ? "\x11" : "\x12";
        }

        private static string FormatTagFixed(FormatOptions options)
        {
            return "\x1e" + (options.IsClosing ? "0" : "1");
        }

        private static string FormatTagDouble(FormatOptions options)
        {
            return "\x1d\x31" + (options.IsClosing ? "0" : "1");
        }

        private static string FormatTagFancy(FormatOptions options)
        {
            return "\x1d\x35" + (options.IsClosing ? "0" : "1");
        }

        private static string FormatTagAuxiliary(FormatOptions options)
        {
            return "\x1d\x36" + (options.IsClosing ? "0" : "1");
        }

        private static string FormatTagShadows(FormatOptions options)
        {
            return "\x1d\x37" + (options.IsClosing ? "0" : "1");
        }

        #endregion

        #region Fonts

        public static IDictionary<string, char> Fonts = new Dictionary<string, char> {
            {"five",               '1'},
            {"five-standard",      '1'},
            {"five-stroke",        '2'},
            {"seven",              '3'},
            {"seven-standard",     '3'},
            {"seven-stroke",       '4'},
            {"seven-fancy",        '5'},
            {"ten",                '6'},
            {"ten-standard",       '6'},
            {"seven-shadow",       '7'},
            {"full-fancy",         '8'},
            {"full",               '9'},
            {"full-standard",      '9'},
            {"seven-shadow-fancy", ':'},
            {"seven-fancy-shadow", ':'},
            {"five-wide",          ';'},
            {"seven-wide",         '<'},
            {"seven-fancy-wide",   '='},
            {"seven-wide-fancy",   '='},
            {"five-wide-stroke",   '>'},
            {"five-stroke-wide",   '>'},
            {"five-custom",        'W'},
            {"seven-custom",       'X'},
            {"ten-custom",         'Y'},
            {"fifteen-custom",     'Z'},
        };

        private static string FormatTagFont(FormatOptions options)
        {
            return FormatFromDictionary(Fonts, options, "\x1A", options.Match);
        }

        #endregion

        #region Colors

        public static IDictionary<string, char> Colors = new Dictionary<string, char> {
            {"red",         '1'},
            {"green",       '2'},
            {"amber",       '3'},
            {"dim-red",     '4'},
            {"dim-green",   '5'},
            {"brown",       '6'},
            {"orange",      '7'},
            {"yellow",      '8'},
            {"rainbow-1",   '9'},
            {"rainbow-2",   'A'},
            {"color-mix",   'B'},
            {"autocolor",   'C'},
        };

        private static string FormatTagColor(FormatOptions options)
        {
            return FormatFromDictionary(Colors, options, "\x1C", options.Match);
        }

        private static string FormatTagRgb(FormatOptions options)
        {
            if(options.Args.Length < 1)
                return options.Match;

            var result = "\x1CZ" + options.Args[0];
            if(options.Args.Length > 1)
                result += "\x1CY" + options.Args[1];

            return result;
        }

        #endregion

        #region References

        public static IDictionary<string, char> DateFormats = new Dictionary<string, char> {
            {"mm/dd/yy",    '0'},
            {"dd/mm/yy",    '1'},
            {"mm-dd-yy",    '2'},
            {"dd-mm-yy",    '3'},
            {"mm.dd.yy",    '4'},
            {"dd.mm.yy",    '5'},
            {"mm dd yy",    '6'},
            {"dd mm yy",    '7'},
            {"mmm.dd,yyyy", '8'},
            {"day",         '9'},
        };

        // <date [format]/>
        private static string FormatTagDate(FormatOptions options)
        {
            return FormatFromDictionary(DateFormats, options, "\x0B", "\x0B\x38");
        }

        // <time/>
        private static string FormatTagTime(FormatOptions options)
        {
            return "\x13";
        }

        // <temperature [unit]/>
        private static string FormatTagTemperature(FormatOptions options)
        {
            return options.Args.Length > 0 && options.Args[0].ToLower()[0] == 'f'
                ? "\x08\x1C"
                : "\x08\x1D";
        }

        // <string label/>
        private static string FormatTagString(FormatOptions options)
        {
            if(options.Args.Length < 1)
                return options.Match;

            return "\x10" + options.Args[0];
        }

        // <picture label/>
        private static string FormatTagPicture(FormatOptions options)
        {
            if(options.Args.Length < 1)
                return options.Match;

            return "\x14" + options.Args[0];
        }

        // <animation [mode] files [holdtime]/>
        private static string FormatTagAnimation(FormatOptions options)
        {
            if(options.Args.Length < 1)
                return options.Match;

            var mode = "C";
            string filenames;
            var holdtime = "";

            switch(options.Args[0].ToLower())
            {
                case "fast":
                case "quick":
                    mode = "C";
                    filenames = options.Args[1];
                    break;
                case "faster":
                    mode = "G";
                    filenames = options.Args[1];
                    break;
                case "dots":
                    mode = "L";
                    filenames = options.Args[1];
                    break;
                default:
                    filenames = options.Args[0];
                    break;
            }

            var last = options.Args[options.Args.Length - 1];
            if(last != filenames)
                holdtime = last;

            int hold;
            if(!int.TryParse(holdtime, out hold))
                hold = 1;

            return "\x1F" + mode + filenames.PadLeft(9) + hold.ToString("X4");
        }

        private static IDictionary<string, char> Counters = new Dictionary<string, char> {
            {"1", 'z'},
            {"2", '{'},
            {"3", '|'},
            {"4", '}'},
            {"5", '~'},
        };

        // <counter 1-5/>
        private static string FormatTagCounter(FormatOptions options)
        {
            return FormatFromDictionary(Counters, options, "\x08", options.Match);
        }

        #endregion

        #region Misc

        private static IDictionary<string, char> Speeds = new Dictionary<string, char> {
            {"0", '\x09'},
            {"1", '\x15'},
            {"2", '\x16'},
            {"3", '\x17'},
            {"4", '\x18'},
            {"5", '\x19'},
        };

        // <speed 0-5/>
        private static string FormatTagSpeed(FormatOptions options)
        {
            return FormatFromDictionary(Speeds, options, "", options.Match);
        }

        // <page/>
        private static string FormatTagPage(FormatOptions options)
        {
            return "\x0C";
        }

        // <line/>
        private static string FormatTagLine(FormatOptions options)
        {
            return "\x0D";
        }

        #endregion
    }
}
