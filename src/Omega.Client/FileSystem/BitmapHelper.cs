using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Omega.Client.FileSystem
{
    public static class BitmapHelper
    {
        private static readonly IList<Color> ColorList = new List<Color> {
            Colors.Black,
            Colors.Red,
            Colors.Green,
            Color.FromRgb(255, 191, 0),
            Colors.DarkRed,
            Colors.DarkGreen,
            Colors.Brown,
            Colors.Orange,
            Colors.Yellow
        };

        public static BitmapSource SwapPalette(BitmapSource source, ColorFormat format)
        {
            var dest = new FormatConvertedBitmap();
            dest.BeginInit();

            dest.Source = source;

            var colors = ColorList.Take((int)format).ToList();
            dest.DestinationPalette = new BitmapPalette(colors);

            dest.DestinationFormat = PixelFormats.Indexed8;
            dest.EndInit();

            return dest;
        }

        public static byte[,] GetBytes(BitmapSource source)
        {
            var stride = (source.PixelWidth * source.Format.BitsPerPixel + 7) / 8;
            var buffer = new byte[stride * source.PixelHeight];
            source.CopyPixels(buffer, stride, 0);

            var bytes = new byte[source.PixelWidth, source.PixelHeight];
            for(int x = 0; x < source.PixelWidth; x++)
            {
                for(int y = 0; y < source.PixelHeight; y++)
                    bytes[x, y] = buffer[(y * source.PixelHeight) + x];
            }

            return bytes;
        }
    }
}