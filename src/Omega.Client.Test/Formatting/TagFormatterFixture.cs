using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Omega.Client.Formatting;

namespace Omega.Client.Test.Formatting
{
    [TestFixture]
    public class TagFormatterFixture
    {
        private static readonly TagFormatter Formatter = new TagFormatter();

        [Test]
        public void WideCharactersTest()
        {
            Assert.AreEqual("\x12", Formatter.Format("<wide>"));
            Assert.AreEqual("\x12", Formatter.Format("<w>"));

            Assert.AreEqual("\x11", Formatter.Format("</w>"));
        }
    }
}
