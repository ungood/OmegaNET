using System.Diagnostics;
using System.IO;
using Omega.Client.Formatting;

namespace Omega.Client.Connection
{
    public class StandardPacketFormat : PacketFormat
    {
        public override int ReadTimeout
        {
            get { return 1000; }
        }

        public override int WriteTimeout
        {
            get { return 1000; }
        }

        public override PacketWriter CreateWriter(Stream stream)
        {
            return new Writer(stream);
        }

        public class Writer : PacketWriter
        {
            public Writer(Stream stream) : base(stream) {}

            protected override void WriteByte(byte b)
            {
                Trace.Write(b.PrettyPrint());
                Stream.WriteByte(b);
            }
        }
    }
}