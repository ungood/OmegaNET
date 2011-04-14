using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Client.Commands
{
    public class GenerateSpeakerToneCommand : WriteSpecialCommand
    {
        public GenerateSpeakerToneCommand()
            : base("\x29") {}

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            throw new NotImplementedException();
        }
    }
}
