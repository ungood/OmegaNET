using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Client.Commands
{
    public class ResetCommand : WriteSpecialCommand
    {
        public ResetCommand()
            : base(",")
        {}

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            return Enumerable.Empty<byte>();
        }
    }
}
