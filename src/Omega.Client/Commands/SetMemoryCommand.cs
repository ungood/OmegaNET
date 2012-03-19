using System.Collections.Generic;
using System.Text;
using Omega.Client.Formatting;
using Omega.Client.Models;

namespace Omega.Client.Commands
{
    public class SetMemoryCommand : WriteSpecialCommand
    {
        public FileTable Config { get; private set; }

        public SetMemoryCommand(FileTable config = null) : base("$")
        {
            Config = config ?? new FileTable();
        }

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            foreach(var kvp in Config)
            {
                yield return kvp.Key;
                yield return (byte) kvp.Value.Type;
                yield return kvp.Value.IsLocked ? (byte) 'L' : (byte) 'U';
                foreach(var b in kvp.Value.GetSizeField().ToAscii())
                    yield return b;
                foreach(var b in kvp.Value.GetDataField().ToAscii())
                    yield return b;
            }
        }
    }
}