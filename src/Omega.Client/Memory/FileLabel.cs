using System;

namespace Omega.Client.Memory
{
    public struct FileLabel
    {
        private readonly byte value;

        public FileLabel(byte value)
        {
            if(value < 0x20 || value > 0x7E)
                throw new ArgumentException("Valid file labels are between 0x20 (space) and 0x7E (half-space)");

            this.value = value;
        }

        public FileLabel(char value)
            : this((byte)value) {}

        public static implicit operator FileLabel(byte value)
        {
            return new FileLabel(value);
        }

        public static implicit operator byte(FileLabel label)
        {
            return label.value;
        }

        public static implicit operator FileLabel(char value)
        {
            return new FileLabel(value);
        }

        public static implicit operator char(FileLabel label)
        {
            return (char) label.value;
        }

        public static readonly FileLabel Priority = new FileLabel('0');
        public static readonly FileLabel Default = new FileLabel('A');
    }
}
