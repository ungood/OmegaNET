using System.Collections.Generic;
using Omega.Client.Models;

namespace Omega.Client.Commands
{
    public class WriteTextCommand : Command
    {
        public TextFile File { get; set; }

        public WriteTextCommand(TextFile file)
            : base(CommandCode.WriteText)
        {
            File = file;
        }

        public override IEnumerable<byte> GetDataField()
        {
            return File.GetBytes();
        }
    }
}