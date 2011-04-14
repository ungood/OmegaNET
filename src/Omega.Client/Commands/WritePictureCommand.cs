using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Client.FileSystem;

namespace Omega.Client.Commands
{
    public class WritePictureCommand : Command
    {
        public PictureFile File { get; set; }

        public WritePictureCommand(PictureFile file)
            : base(CommandCode.WriteSmallDotsPicture)
        {
            File = file;
        }

        public override IEnumerable<byte> GetDataField()
        {
            return File.GetBytes();
        }
    }
}
