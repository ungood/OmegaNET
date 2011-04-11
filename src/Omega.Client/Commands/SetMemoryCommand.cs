using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Omega.Client.FileSystem;

namespace Omega.Client.Commands
{
    public class SetMemoryCommand : WriteSpecialCommand
    {
        public MemoryConfig Config { get; private set; }

        public SetMemoryCommand(MemoryConfig config = null) : base("$")
        {
            Config = config ?? new MemoryConfig();
        }

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            foreach(var kvp in Config)
            {
                yield return kvp.Key;
                yield return (byte) kvp.Value.Type;
                yield return kvp.Value.IsLocked ? (byte) 'L' : (byte) 'U';
                foreach(var b in Encoding.UTF8.GetBytes(kvp.Value.GetSizeField()))
                    yield return b;
                foreach(var b in Encoding.UTF8.GetBytes(kvp.Value.GetDataField()))
                    yield return b;
            }
        }
    }
}