using System.IO;
using Omega.Client.Commands;
using Omega.Client.Connection;
using NUnit.Framework;
using Omega.Client.Memory;
using Omega.Client.Formatting;

namespace Omega.Client.Test.Examples
{
    [TestFixture]
    public class PacketExamples
    {
        [Test]
        // Example 7.6.2
        public void StandardTransmissionWithChecksum()
        {
            using(var conn = new MemoryStreamConnection())
            {
                conn.Open();

                var packet = new Packet {
                    new WriteTextCommand(new TextFile('A') {
                        "HELLO"
                    })
                };
                conn.Send(packet);

                Assert.AreEqual("<NUL><NUL><NUL><NUL><NUL><SOH>Z00<STX>AAHELLO<ETX>01FB<EOT>", conn.ToString());
            }
        }
    }
}
