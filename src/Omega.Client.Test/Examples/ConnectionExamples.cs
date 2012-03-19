using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Omega.Client.Test.Examples
{
    [TestFixture]
    public class ConnectionExamples
    {
        [Test]
        public void SimpleSendPriorityMessageExample()
        {
            using(var conn = new MemoryStreamConnection())
            {
                conn.Open();
                conn.WritePriorityMessage("Hello World!");

                Assert.AreEqual("<NUL><NUL><NUL><NUL><NUL><SOH>Z00<STX>A0<ESC> oHello World!<ETX>055D<EOT>", conn.ToString());
            }
        }
    }
}
