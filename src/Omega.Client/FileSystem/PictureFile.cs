using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Omega.Client.FileSystem
{
    public enum ColorFormat : byte 
    {
        Monochrome = 2,
        TriColor   = 4,
        EightColor  = 8,
    }

    public class PictureFile : SignFile
    {
        public bool IsLarge { get; private set; }

        public PictureFile(FileLabel label, BitmapSource bitmap, ColorFormat format)
            : base(label)
        {
            //bitmap.
            //bitmap.PixelWidth
        }
        
        public override SignFileInfo CreateFileInfo()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<byte> GetBytes()
        {
            throw new NotImplementedException();
        }
    }
}
