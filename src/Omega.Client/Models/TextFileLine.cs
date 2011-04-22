namespace Omega.Client.Models
{
    public class TextMode
    {
        public DisplayPosition Position { get; set; }
        public DisplayMode DisplayMode { get; set; }
        public SpecialMode SpecialMode { get; set; }
        public SpecialGraphic SpecialGraphic { get; set; }

        public TextMode(DisplayMode display, DisplayPosition position = DisplayPosition.Middle)
        {
            DisplayMode = display;
            Position = position;
        }

        public TextMode(SpecialMode special, DisplayPosition position = DisplayPosition.Middle)
            : this(DisplayMode.Special, position)
        {
            SpecialMode = special;
        }

        public TextMode(SpecialGraphic graphic, DisplayPosition position = DisplayPosition.Middle)
            : this(DisplayMode.Special, position)
        {
            SpecialGraphic = graphic;
        }
    }

    public class TextFileLine
    {
        public TextMode Mode { get; set; }
        public string Text { get; set; }

        public TextFileLine() {}

        public TextFileLine(string text, TextMode mode = null)
        {
            Text = text;
            Mode = mode;
        }

        public TextFileLine(string text, DisplayMode display, DisplayPosition position = DisplayPosition.Middle)
            : this(text, new TextMode(display, position))
        {}

        public TextFileLine(string text, SpecialMode special, DisplayPosition position = DisplayPosition.Middle)
            : this(text, new TextMode(special, position))
        {}

        public TextFileLine(SpecialGraphic graphic, DisplayPosition position = DisplayPosition.Middle)
            : this(null, new TextMode(graphic, position))
        {}
    }
}