using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Omega.Client.Commands
{
    public abstract class WriteSpecialCommand : Command
    {
        public string Function { get; private set; }

        protected WriteSpecialCommand(string function)
            : base(CommandCode.WriteSpecial)
        {
            Contract.Requires(function != null);
            Function = function;
        }

        public override IEnumerable<byte> GetDataField()
        {
            return Encoding.UTF8.GetBytes(Function)
                .Concat(GetSpecialFunctionData());
        }

        protected abstract IEnumerable<byte> GetSpecialFunctionData();
    }
}