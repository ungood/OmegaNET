using System;
using System.Collections.Generic;
using Omega.Client.Formatting;

namespace Omega.Client.Commands
{
    public class SetTimeCommand : WriteSpecialCommand
    {
        public DateTime Time { get; set; }

        public SetTimeCommand(DateTime time)
            : base(" ")
        {
            Time = time;
        }

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            return Time.ToString("HHmm").ToAscii();
        }
    }
}
