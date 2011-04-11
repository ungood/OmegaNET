using System;
using System.IO.Ports;
using System.Text;
using Omega.Client.Connection;

namespace Omega.Client.Serial
{
    /// <summary>
    /// An implementation of IConnection that communicates through the serial port.
    /// </summary>
    public class SerialConnection : IConnection
    {
        private readonly SerialPort port;
        
        public SerialConnection(SerialPort port)
        {
            this.port = port;
        }

        public SerialConnection(string portName,
            int baudRate = 4800,
            Parity parity = Parity.Even,
            int dataBits = 7,
            StopBits stopBits = StopBits.Two)
        {
            port = new SerialPort(portName, baudRate, parity, dataBits, stopBits) {
                Encoding = Encoding.UTF8,
                Handshake = Handshake.RequestToSend
            };
        }

        public void Open()
        {
            port.Open();
        }

        public Packet CreatePacket(SignType type = SignType.AllSigns, SignAddress address = null, PacketFormat format = null)
        {
            if(address == null)
                address = SignAddress.Broadcast;

            if(format == null)
                format = PacketFormat.Standard;

            port.ReadTimeout  = format.ReadTimeout;
            port.WriteTimeout = format.WriteTimeout;

            return new Packet(port.BaseStream, format, type, address);
        }

        #region IDisposable

        private bool isDisposed;

        ~SerialConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (!isDisposed)
            {
                if(disposeManagedResources)
                {
                    port.Close();
                }
                isDisposed=true;
            }
        }

        #endregion
    }
}
