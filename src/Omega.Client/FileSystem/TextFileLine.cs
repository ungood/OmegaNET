namespace Omega.Client.FileSystem
{
    public class TextFileLine
    {
        public DisplayPosition Position { get; set; }
        public DisplayMode Mode { get; set; }
        public SpecialMode SpecialMode { get; set; }
        public SpecialGraphic SpecialGraphic { get; set; }
        public string Text { get; set; }

        public TextFileLine() {}

        public TextFileLine(string text, DisplayMode mode = DisplayMode.AutoMode,
            DisplayPosition position = DisplayPosition.Middle)
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