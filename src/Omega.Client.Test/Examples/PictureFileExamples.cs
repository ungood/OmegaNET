using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using NUnit.Framework;
using Omega.Client.FileSystem;
using Omega.Client.Formatting;

namespace Omega.Client.Test.FileSystem
{
    [TestFixture]
    public class PictureFileExamples
    {
        [Test]
        public void WriteDotsPicture()
        {
            var bmp = new BitmapImage(new Uri("Examples/example-arrow.png", UriKind.Relative));

            var picFile = new PictureFile('A', bmp, ColorFormat.TriColor);

            var actual = picFile.GetBytes().PrettyPrint();

            var expected = "A0F09000000000<CR>"
                              + "000000000<CR>"
                              + "000100000<CR>"
                              + "000110000<CR>"
                              + "000111000<CR>"
                              + "000111100<CR>"
                              + "111111110<CR>"
                              + "111111112<CR>"
                              + "111111110<CR>"
                              + "000111100<CR>"
                              + "000111000<CR>"
                              + "000110000<CR>"
                              + "000100000<CR>"
                              + "000000000<CR>"
                              + "000000000<CR>";

            Assert.AreEqual(expected, actual);
        }
    }
}
