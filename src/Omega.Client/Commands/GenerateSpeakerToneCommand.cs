using System;
using System.Collections.Generic;
using Omega.Client.Commands;

namespace Omega.Client.Speaker
{
    public class GenerateSpeakerToneCommand : WriteSpecialCommand
    {
        public GenerateSpeakerToneCommand(byte frequency, byte duration, byte repeat)
            : base("(")
        {
            
        }

        protected override IEnumerable<byte> GetSpecialFunctionData()
        {
            throw new NotImplementedException();
        }
    }
}
