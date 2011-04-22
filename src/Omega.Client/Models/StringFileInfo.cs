namespace Omega.Client.Models
{
    public class StringFileInfo : SignFileInfo
    {
        public int Size { get; set; }

        public StringFileInfo(int size = 0) : base(FileType.String)
        {
            Size = size;
            IsLocked = true;
        }

        public override string GetSizeField()
        {
            return Size.ToString("X4");
        }

        public override string GetDataField()
        {
            return "0000";
        }
    }
}