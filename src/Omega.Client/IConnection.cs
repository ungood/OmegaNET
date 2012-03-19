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
using Omega.Client.Commands;

namespace Omega.Client
{
    public interface IConnection : ICommandHandler, IDisposable
    {
        void Open();

        void Send(Packet packet);

        DisposablePacket CreatePacket(SignType type = SignType.AllSigns, SignAddress address = null);
    }

    public abstract class ConnectionBase : IConnection
    {
        public abstract void Dispose();
        public abstract void Open();
        public abstract void Send(Packet packet);

        public void Handle(Command command)
        {
            Send(new Packet {command});
        }

        public DisposablePacket CreatePacket(SignType type = SignType.AllSigns, SignAddress address = null)
        {
            return new DisposablePacket(this, type, address ?? SignAddress.Broadcast);
        }
    }
}