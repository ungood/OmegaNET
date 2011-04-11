using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        private void WriteBytes(IEnumerable<byte> bytes)
        {
            foreach(var b in bytes)
                WriteByte(b);
        }

        private void WriteEnum(Enum e, int repeat = 1)
        {
            for(int i = 0; i < repeat; i++)
                WriteByte((byte)(object)e);
        }

        private void Write(string s)
        {
            WriteBytes(Encoding.UTF8.GetBytes(s));
        }

        public void WriteHeader(SignType signType, SignAddress address)
        {
            WriteEnum(Ascii.NUL, 5);
            WriteEnum(Ascii.SOH);
            WriteEnum(signType);
            Write("00");
            Trace.WriteLine("");
        }

        public void WriteCommand(Command command)
        {
            WriteEnum(Ascii.STX);
            Thread.Sleep(100);

            WriteEnum(command.CommandCode);
            WriteBytes(command.GetDataField());
            WriteEnum(Ascii.ETX);
            Write(command.CalcChecksum().ToString());
            Trace.WriteLine("");
        }

        public void EndTransmission()
        {
            WriteEnum(Ascii.EOT);
            Trace.WriteLine("");
        }
    }
}