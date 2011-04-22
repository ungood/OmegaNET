using System.Collections.Generic;
using Omega.Client.Models;

namespace Omega.Client.Commands
{
    public class WriteStringCommand : Command
    {
        public StringFile File { get; private set; }

        public WriteStringCommand(StringFile file)
            : base(CommandCode.WriteString)
        {
            File = file;
        }

        public override IEnumerable<byte> GetDataField()
        {
            return File.GetBytes();
        }
    }
}