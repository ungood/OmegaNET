using System.IO;
using Omega.Client.Commands;
using Omega.Client.Connection;
using NUnit.Framework;
using Omega.Client.Models;
using Omega.Client.Formatting;

namespace Omega.Client.Test.Examples
{
    [TestFixture]
    public class PacketExamples
    {
        private static string GetPacketData(Packet packet)
        {
            var stream = new MemoryStream();

            var writer = new StandardPacketFormat().CreateWriter(stream);
            writer.WritePacket(packet);

            return stream.ToArray().PrettyPrint();
        }

        [Test]
        // Example 7.6.2
        public void StandardTransmissionWithChecksum()
        {
            var packet = new Packet {
                new WriteTextCommand(new TextFile('A') {
                    "HELLO"
                })
            };

            var actual = GetPacketData(packet);

            Assert.AreEqual("<NUL><NUL><NUL><NUL><NUL><SOH>Z00<STX>AAHELLO<ETX>01FB<EOT>", actual);
        }
    }
}
