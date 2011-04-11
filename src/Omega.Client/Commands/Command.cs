using System.Collections.Generic;
using Omega.Client.Connection;

namespace Omega.Client.Commands
{
    public abstract class Command
    {
        public CommandCode CommandCode { get; private set; }

        protected Command(CommandCode commandCode)
        {
            CommandCode = commandCode;
        }

        public abstract IEnumerable<byte> GetDataField();

        public Checksum CalcChecksum()
        {
            var checksum = new Checksum();
            checksum.Add((byte) Ascii.STX);
            checksum.Add((byte) CommandCode);
            foreach(var b in GetDataField())
                checksum.Add(b);
            checksum.Add((byte) Ascii.ETX);

            return checksum;
        }
    }
}