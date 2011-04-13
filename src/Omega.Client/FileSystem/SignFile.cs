﻿using System.Collections.Generic;

namespace Omega.Client.FileSystem
{
    public abstract class SignFile
    {
        public FileLabel Label { get; set; }

        public abstract SignFileInfo CreateFileInfo();
        public abstract IEnumerable<byte> GetBytes();

        protected SignFile(FileLabel label)
        {
            Label = label;
        }
    }
}
