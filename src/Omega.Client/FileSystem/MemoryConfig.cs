using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omega.Client.FileSystem
{
    public class MemoryConfig : IEnumerable<KeyValuePair<FileLabel, SignFileInfo>>
    {
        private readonly Dictionary<FileLabel, SignFileInfo> files
            = new Dictionary<FileLabel, SignFileInfo>();
        
        public void Add(SignFile file)
        {
            files.Add(file.Label, file.CreateFileInfo());
        }

        public void Add(FileLabel label, SignFileInfo info)
        {
            files.Add(label, info);
        }

        public IEnumerator<KeyValuePair<FileLabel, SignFileInfo>> GetEnumerator()
        {
            return files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
