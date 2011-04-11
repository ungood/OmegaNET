using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Omega.Client.FileSystem;

namespace Omega.Client.Commands
{
    public class WriteTextCommand : Command
    {
        public TextFile File { get; set; }

        public WriteTextCommand(TextFile file)
            : base(CommandCode.WriteText)
        {
            Contract.Requires(file != null);
            File = file;
        }

        public override IEnumerable<byte> GetDataField()
        {
            return File.GetBytes();
        }
    }
}