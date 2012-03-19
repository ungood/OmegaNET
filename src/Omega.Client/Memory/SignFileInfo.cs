namespace Omega.Client.Memory
{
    public enum FileType : byte
    {
        Text    = 0x41,
        String  = 0x42,
        Picture = 0x44,
    }

    public enum FileProtection : byte
    {
        Unlocked = 0x55,
        Locked   = 0x4c,
    }

    public abstract class SignFileInfo
    {
        public FileType Type { get; private set; }
        public bool IsLocked { get; set; }

        public abstract string GetSizeField();
        public abstract string GetDataField();
        
        protected SignFileInfo(FileType type, bool isLocked = false)
        {
            Type = type;
            IsLocked = isLocked;
        }
    }
}