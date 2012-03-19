using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Omega.Client.Commands;
using Omega.Client.Memory;

namespace Omega.Client.Test.Examples
{
    [TestFixture]
    public class AutoPacketExamples
    {
        [Test]
        // Example 7.6.2
        public void StandardTransmissionWithChecksum()
        {
            using(var conn = new MemoryStreamConnection())
            {
                conn.Open();

                using(var packet = conn.CreatePacket())
                {
                    packet.WriteText(new [] {
                        new TextFile('A') { "HELLO" }, 
                    });
                }
                
                Assert.AreEqual("<NUL><NUL><NUL><NUL><NUL><SOH>Z00<STX>AAHELLO<ETX>01FB<EOT>", conn.ToString());
            }
        }
    }
}
