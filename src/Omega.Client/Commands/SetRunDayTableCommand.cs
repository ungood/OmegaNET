using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Client.Commands
{
    public class SetRunDayTableCommand : WriteSpecialCommand
    {
        public SetRunDayTableCommand() : base("2")
        {

        }

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            yield return (byte) 'A';
            yield return (byte) 'A';
            yield return (byte) '1';
        }
    }
}
