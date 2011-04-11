using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Client.FileSystem;
using Omega.Client.Formatting;
using NUnit.Framework;

namespace Omega.Client.Test
{
    [TestFixture]
    class TextFileExamples
    {
        // Example 7.6.6.3.1
        [Test]
        public void RotateHello()
        {
            var textFile = new TextFile('D');
            textFile.Add("HELLO", DisplayMode.Rotate, DisplayPosition.Bottom);
            var bytes = textFile.GetBytes().PrettyPrint();

            Assert.AreEqual("D<ESC>&aHELLO", bytes);
        }
        
        [Test]
        public void CombiningTextAndGraphics()
        {
            var textFile = new TextFile('>');
            textFile.Add("Hello There", SpecialMode.Snow, DisplayPosition.Top);
            textFile.Add("", DisplayMode.Rotate, DisplayPosition.Top);
            textFile.Add("", SpecialMode.Welcome, DisplayPosition.Bottom);
            var bytes = textFile.GetBytes().PrettyPrint();

            Assert.AreEqual("><ESC>\"n2Hello There<ESC>\"a<ESC>&n8", bytes);
        }
    }
}
