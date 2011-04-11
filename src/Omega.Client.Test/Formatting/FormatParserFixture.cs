using Omega.Client.Formatting;
using NUnit.Framework;

namespace Omega.Client.Test.Formatting
{
    [TestFixture]
    public class FormatParserFixture
    {
        private void AssertExtended(string extendedChar, byte expected)
        {
            var expStr = string.Format("{0}{1}", (char) FormatCodes.ExtendedCharacterSet, (char) expected);
            var actual = FormatParser.ExpandExtendedChars(extendedChar);
            Assert.AreEqual(expStr, actual);
        }

        [Test]
        public void ExpandExtendedCharsTest()
        {
            AssertExtended("Ç", 0x20);
            AssertExtended("ü", 0x21);
            AssertExtended("é", 0x22);
            AssertExtended("â", 0x23);
            AssertExtended("ä", 0x24);
            AssertExtended("à", 0x25);
            AssertExtended("å", 0x26);
            AssertExtended("ç", 0x27);
            AssertExtended("ê", 0x28);
            AssertExtended("ë", 0x29);
            AssertExtended("è", 0x2A);
            AssertExtended("ï", 0x2B);
            AssertExtended("î", 0x2C);
            AssertExtended("ì", 0x2D);
            AssertExtended("Ä", 0x2E);
            AssertExtended("Å", 0x2F);
            AssertExtended("É", 0x30);
            AssertExtended("æ", 0x31);
            AssertExtended("Æ", 0x32);
            AssertExtended("ô", 0x33);
            AssertExtended("ö", 0x34);
            AssertExtended("ò", 0x35);
            AssertExtended("û", 0x36);
            AssertExtended("ù", 0x37);
            AssertExtended("ÿ", 0x38);
            AssertExtended("Ö", 0x39);
            AssertExtended("Ü", 0x3A);
            AssertExtended("¢", 0x3B);
            AssertExtended("£", 0x3C);
            AssertExtended("¥", 0x3D);
            AssertExtended("ƒ", 0x3F);
            AssertExtended("á", 0x40);
            AssertExtended("í", 0x41);
            AssertExtended("ó", 0x42);
            AssertExtended("ú", 0x43);
            AssertExtended("ñ", 0x44);
            AssertExtended("Ñ", 0x45);
            AssertExtended("ª", 0x46);
            AssertExtended("º", 0x47);
            AssertExtended("¿", 0x48);

            AssertExtended("¡", 0x4A);
            AssertExtended(" ", 0x4B);
            AssertExtended("ø", 0x4C);
            AssertExtended("Ø", 0x4D);
            AssertExtended("Ð", 0x53);
            AssertExtended("Š", 0x54);
            AssertExtended("ž", 0x55);
            AssertExtended("Ž", 0x56);

            AssertExtended("š", 0x58);
            AssertExtended("β", 0x59);
            AssertExtended("Á", 0x5A);
            AssertExtended("À", 0x5B);
            AssertExtended("Ã", 0x5C);
            AssertExtended("ã", 0x5D);
            AssertExtended("Ê", 0x5E);
            AssertExtended("Í", 0x5F);
            AssertExtended("Õ", 0x60);
            AssertExtended("õ", 0x61);

            AssertExtended("↑", 0x64);
            AssertExtended("↓", 0x65);
            AssertExtended("←", 0x66);
            AssertExtended("→", 0x67);
        }
    }
}