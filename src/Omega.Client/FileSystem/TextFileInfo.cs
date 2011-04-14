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
            return StartTime + StopTime.ToString();
        }
    }
}