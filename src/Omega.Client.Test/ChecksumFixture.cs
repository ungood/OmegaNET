using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Client.Connection;
using NUnit.Framework;

namespace Omega.Client.Test
{
    [TestFixture]
    public class ChecksumFixture
    {
        [Test]
        public void ChecksumExample()
        {
            var checksum = new Checksum();
            checksum.Add(0x02);
            checksum.Add((byte) 'A');
            checksum.Add((byte) 'A');
            checksum.Add((byte) 'H');
            checksum.Add((byte) 'E');
            checksum.Add((byte) 'L');
            checksum.Add((byte) 'L');
            checksum.Add((byte) 'O');
            checksum.Add(0x03);

            Assert.AreEqual("01FB", checksum.ToString());
        }
    }
}
