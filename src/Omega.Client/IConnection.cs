using System;
using Omega.Client.Connection;

namespace Omega.Client
{
    public interface IConnection : IDisposable
    {
        void Open();
        Packet CreatePacket(SignType type = SignType.AllSigns, SignAddress address = null, PacketFormat format = null);
    }
}
