#region License

// Copyright 2011 Jason Walker
// ungood@onetrue.name
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and 
// limitations under the License.

#endregion

using System.Diagnostics;
using System.IO;
using System.Threading;
using Omega.Client.Commands;
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

        #region Nested type: Writer

        public class Writer : PacketWriter
        {
            public Writer(Stream stream) : base(stream) {}

            protected override void EndTransmission()
            {
                WriteEnum(Ascii.EOT);
                Trace.WriteLine("");
            }

            protected override void WriteByte(byte b)
            {
                Trace.Write(b.PrettyPrint());
                Stream.WriteByte(b);
            }

            protected override void WriteHeader(SignType signType, SignAddress address)
            {
                WriteEnum(Ascii.NUL, 5);
                WriteEnum(Ascii.SOH);
                WriteEnum(signType);
                Write(address.ToString());
                Trace.WriteLine("");
            }

            protected override void WriteCommand(Command command)
            {
                WriteEnum(Ascii.STX);
                Thread.Sleep(100);

                WriteEnum(command.CommandCode);
                WriteBytes(command.GetDataField());
                WriteEnum(Ascii.ETX);
                Write(command.CalcChecksum().ToString());
                Trace.WriteLine("");
            }
        }

        #endregion
    }
}