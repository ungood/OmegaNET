using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Omega.Client.Test
{
    [TestFixture]
    public class TimeFixture
    {
        private void AssertTimeSpan(int hour, int minute, byte value)
        {
            var time1 = new StartStopTime(value);
            var time2 = new StartStopTime(new TimeSpan(hour, minute, 0));
            Assert.AreEqual(time1.Value, time2.Value);
            Assert.AreEqual(time1.Time, time2.Time);
            Assert.AreEqual(time1, time2);
        }

        [Test]
        public void TimeSpanByteConversion()
        {
            AssertTimeSpan( 1, 40, 0x0A);
            AssertTimeSpan(20, 20, 0x7A);
            AssertTimeSpan( 6, 00, 0x24);
        }

    }
}
