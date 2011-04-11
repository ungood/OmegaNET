using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return Encoding.UTF8.GetBytes(Date.ToString("MMddyy"));
        }
    }
}
