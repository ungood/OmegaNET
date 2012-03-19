#region License
// Copyright 2012 Jason Walker
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
    /// <summary>
    /// A wrapper around Packet that calls an IConnection's Send() method on dispose.
    /// </summary>
    public class DisposablePacket : ICommandHandler, IDisposable
    {
        private readonly IConnection connection;
        private readonly Packet wrappedPacket;

        public DisposablePacket(IConnection connection, SignType signType, SignAddress signAddress)
        {
            this.connection = connection;
            wrappedPacket = new Packet(signType, signAddress);
        }

        public void Handle(Command command)
        {
            wrappedPacket.Handle(command);
        }

        public void Dispose()
        {
            connection.Send(wrappedPacket);
        }
    }
}
