using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Omega.Client.Models
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

        public static BitmapSource Dither(BitmapSource source, ColorFormat format)
        {
            if(source.Format == PixelFormats.Indexed8)
            {
                var rgb = new FormatConvertedBitmap();
                rgb.BeginInit();
                rgb.Source = source;
                rgb.DestinationFormat = PixelFormats.Rgb24;
                rgb.EndInit();
                source = rgb;
            }

            var dest = new FormatConvertedBitmap();
            dest.BeginInit();

            dest.Source = source;
            var numColors = (int)Math.Pow(2, (int) format);
            var colors = ColorList.Take(numColors).ToList();
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
            for(int y = 0; y < source.PixelHeight; y++)
            {
                for(int x = 0; x < source.PixelWidth; x++)
                {
                    bytes[x, y] = buffer[(y * stride) + x];
                }
            }

            return bytes;
        }
    }
}