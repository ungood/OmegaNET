﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Omega.Client.Formatting;

namespace Omega.Client.Memory
{
    public enum ColorFormat : byte 
    {
        Monochrome = 1,
        TriColor   = 2,
        EightColor  = 4,
    }

    public class PictureFile : SignFile
    {
        public ColorFormat Format { get; set; }
        public bool IsLarge { get; private set; }

        private readonly byte[,] bytes;

        public int Width
        {
            get { return bytes.GetLength(0); }
        }

        public int Height
        {
            get { return bytes.GetLength(1); }
        }

        public PictureFile(FileLabel label, BitmapSource bitmap, ColorFormat format)
            : base(label)
        {
            Format = format;
            var swapped = BitmapHelper.Dither(bitmap, format);
            bytes = BitmapHelper.GetBytes(swapped);
        }

        public PictureFile(FileLabel label, string filename, ColorFormat format)
            : this(label, new BitmapImage(new Uri(filename, UriKind.Relative)), format)
        {}

        public override SignFileInfo CreateFileInfo()
        {
            return new PictureFileInfo(bytes.GetLength(0), bytes.GetLength(1), Format);
        }

        public override IEnumerable<byte> GetBytes()
        {
            yield return Label;
            var sb = new StringBuilder();

            sb.Append(Height.ToString("X2"));
            sb.Append(Width.ToString("X2"));
            for(int y = 0; y < Height; y++)
            {
                for(int x = 0; x < Width; x++)
                    sb.Append(bytes[x, y]);
                sb.Append("\x0D");
            }

            foreach(var b in sb.ToString().ToAscii())
                yield return b;
        }
    }

    public class PictureFileInfo : SignFileInfo
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public ColorFormat Format { get; set; }

        public PictureFileInfo(int width, int height, ColorFormat format, bool isLocked = false)
            : base(FileType.Picture, isLocked)
        {
            Width = width;
            Height = height;
            Format = format;
        }

        public override string GetSizeField()
        {
            return Height.ToString("X2") + Width.ToString("X2");
        }

        public override string GetDataField()
        {
            return (((int) Format) * 1000).ToString();
        }
    }
}
