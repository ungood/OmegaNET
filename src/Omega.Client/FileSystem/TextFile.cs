using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Omega.Client.Formatting;

namespace Omega.Client.FileSystem
{
    public class TextFileInfo : SignFileInfo
    {
        public StartStopTime StartTime { get; set; }
        public StartStopTime StopTime { get; set; }
        public int Size { get; set; }

        public TextFileInfo(int size = 0)
            : base(FileType.Text)
        {
            Size = size;
            StartTime = StartStopTime.Always;
            StopTime = StartStopTime.Always;
        }
        
        public override string GetSizeField()
        {
            return Size.ToString("X4");
        }

        public override string GetDataField()
        {
            return StartTime.ToString() + StopTime.ToString();
        }
    }

    public class TextFile : SignFile, IEnumerable<TextFileLine>
    {
        private List<TextFileLine> lines = new List<TextFileLine>();
        
        public TextFile(FileLabel label)
            : base(label) {}

        public void Add(TextFileLine line)
        {
            Contract.Requires(line != null);
            
            lines.Add(line);
        }

        public void Add(string text, DisplayMode mode = DisplayMode.AutoMode, DisplayPosition position = DisplayPosition.Middle)
        {
            lines.Add(new TextFileLine(text, mode, position));
        }

        public void Add(string text, SpecialMode mode, DisplayPosition position = DisplayPosition.Middle)
        {
            lines.Add(new TextFileLine(text, mode, position));
        }

        public void Add(SpecialGraphic graphic, DisplayPosition position = DisplayPosition.Middle)
        {
            lines.Add(new TextFileLine(graphic, position));
        }

        public override SignFileInfo CreateFileInfo()
        {
            var bytes = GetBytes();
            return new TextFileInfo(bytes.Count());
        }

        public IEnumerator<TextFileLine> GetEnumerator()
        {
            return lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override IEnumerable<byte> GetBytes()
        {
            yield return Label;
            foreach(var line in lines)
            {
                yield return (byte) Ascii.ESC;
                yield return (byte) line.Position;
                yield return (byte) line.Mode;

                if(line.SpecialMode != SpecialMode.None)
                    yield return (byte) line.SpecialMode;

                if(line.SpecialGraphic != SpecialGraphic.None)
                    yield return (byte) line.SpecialGraphic;

                if(line.Text == null)
                    yield break;

                var formatted = FormatParser.Format(line.Text);
                foreach(var b in Encoding.UTF8.GetBytes(formatted))
                    yield return b;
            }
        }
    }

    public class TextFileLine
    {
        public DisplayPosition Position { get; set; }
        public DisplayMode Mode { get; set; }
        public SpecialMode SpecialMode { get; set; }
        public SpecialGraphic SpecialGraphic { get; set; }
        public string Text { get; set; }
        
        public TextFileLine(string text, DisplayMode mode = DisplayMode.AutoMode, DisplayPosition position = DisplayPosition.Middle)
        {
            Text = text;
            Position = position;
            Mode = mode;
        }

        public TextFileLine(string text, SpecialMode mode, DisplayPosition position = DisplayPosition.Middle)
        {
            Text = text;
            Position = position;
            Mode = DisplayMode.Special;
            SpecialMode = mode;
        }

        public TextFileLine(SpecialGraphic graphic, DisplayPosition position = DisplayPosition.Middle)
        {
            Position = position;
            Mode = DisplayMode.Special;
            SpecialGraphic = graphic;
        }
    }
}
