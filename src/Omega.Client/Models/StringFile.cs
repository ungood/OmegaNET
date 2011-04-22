﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Client.Formatting;

namespace Omega.Client.Models
{
    public class StringFile : SignFile
    {
        public string Text { get; private set; }

        public StringFile(FileLabel label, string text)
            : base(label)
        {
            Text = text;
        }

        public override SignFileInfo CreateFileInfo()
        {
            return new StringFileInfo(GetBytes().Count());
        }

        public override IEnumerable<byte> GetBytes()
        {
            yield return Label;

            var formatted = Text.Format();
            foreach(var b in Encoding.UTF8.GetBytes(formatted))
                yield return b;
        }
    }
}