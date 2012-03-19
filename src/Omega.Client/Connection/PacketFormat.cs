using System.IO;
using Omega.Client.Commands;

namespace Omega.Client.Connection
{
    public abstract class PacketFormat
    {
        public abstract int ReadTimeout { get; }
        public abstract int WriteTimeout { get; }

        public abstract PacketWriter CreateWriter(Stream stream);
    }
}
