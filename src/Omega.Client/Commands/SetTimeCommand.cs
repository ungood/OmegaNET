using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return Encoding.UTF8.GetBytes(Time.ToString("HHmm"));
        }
    }
}
