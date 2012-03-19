using Omega.Client.Memory;
using Omega.Client.Formatting;
using NUnit.Framework;

namespace Omega.Client.Test.Examples
{
    [TestFixture]
    class TextFileExamples
    {
        // Example 7.6.6.3.1
        [Test]
        public void RotateHello()
        {
            var textFile = new TextFile('D') {
                {"HELLO", DisplayMode.Rotate, DisplayPosition.Bottom}
            };
            var bytes = textFile.GetBytes().PrettyPrint();

            Assert.AreEqual("D<ESC>&aHELLO", bytes);
        }
        
        [Test]
        public void CombiningTextAndGraphics()
        {
            var textFile = new TextFile('>') {
                {"Hello There", SpecialMode.Snow, DisplayPosition.Top},
                {"", DisplayMode.Rotate, DisplayPosition.Top},
                {"", SpecialMode.Welcome, DisplayPosition.Bottom}
            };
            var bytes = textFile.GetBytes().PrettyPrint();

            Assert.AreEqual("><ESC>\"n2Hello There<ESC>\"a<ESC>&n8", bytes);
        }
    }
}
