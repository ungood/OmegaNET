using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Omega.Client.Commands;

namespace Omega.Client.Connection
{
    public abstract class PacketWriter
    {
        protected Stream Stream { get; private set; }

        protected PacketWriter(Stream stream)
        {
            Stream = stream;
        }

        protected abstract void WriteByte(byte b);
        protected abstract void WriteHeader(SignType signType, SignAddress address);
        protected abstract void WriteCommand(Command command);
        protected abstract void EndTransmission();

        protected void WriteBytes(IEnumerable<byte> bytes)
        {
            foreach(var b in bytes)
                WriteByte(b);
        }

        protected void WriteEnum(Enum e, int repeat = 1)
        {
            for(int i = 0; i < repeat; i++)
                WriteByte((byte) (object) e);
        }

        protected void Write(string s)
        {
            WriteBytes(Encoding.ASCII.GetBytes(s));
        }

        public void WritePacket(Packet packet)
        {
            WriteHeader(packet.SignType, packet.Address);
            foreach(var command in packet)
                WriteCommand(command);
            EndTransmission();
        }
    }
}