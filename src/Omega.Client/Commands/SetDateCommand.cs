using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Client.Formatting;

namespace Omega.Client.Commands
{
    public class SetDateCommand : WriteSpecialCommand
    {
        public DateTime Date { get; private set; }

        public SetDateCommand(DateTime date)
            : base(";")
        {
            Date = date;
        }

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            return Date.ToString("MMddyy").ToAscii();
        }
    }
}
