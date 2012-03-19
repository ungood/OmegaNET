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

using System;
using System.IO.Ports;
using System.Text;

namespace Omega.Client.Connection
{
    /// <summary>
    /// An implementation of IConnection that communicates through the serial port.
    /// </summary>
    public class SerialConnection : ConnectionBase
    {
        private readonly SerialPort port;
        
        public SerialConnection(SerialPort port)
        {
            this.port = port;

            PacketFormat = new StandardPacketFormat();
        }

        public SerialConnection(string portName,
            int baudRate = 4800,
            Parity parity = Parity.Even)
        {
            var dataBits = parity == Parity.None ? 8 : 7;
            var stopBits = parity == Parity.None ? StopBits.One : StopBits.Two;

            port = new SerialPort(portName, baudRate, parity, dataBits, stopBits) {
                Encoding = Encoding.ASCII,
                Handshake = Handshake.None,
            };

            PacketFormat = new StandardPacketFormat();
        }

        public PacketFormat PacketFormat { get; set; }
        
        public override void Open()
        {
            port.Open();
        }

        public override void Send(Packet packet)
        {
            port.ReadTimeout = PacketFormat.ReadTimeout;
            port.WriteTimeout = PacketFormat.WriteTimeout;

            var writer = PacketFormat.CreateWriter(port.BaseStream);
            writer.WritePacket(packet);
        }

        #region IDisposable

        private bool isDisposed;

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SerialConnection()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposeManagedResources)
        {
            if(!isDisposed)
            {
                if(disposeManagedResources)
                {
                    port.Close();
                }
                isDisposed = true;
            }
        }

        #endregion
    }
}