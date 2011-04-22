#region License

// Copyright 2011 Jason Walker
// ungood@onetrue.name
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and 
// limitations under the License.

#endregion

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omega.Client.Formatting;

namespace Omega.Client.Models
{
    public class TextFile : SignFile, IEnumerable<TextFileLine>
    {
        private readonly List<TextFileLine> lines = new List<TextFileLine>();

        public TextFile(FileLabel label)
            : base(label) {}

        public void Add(TextFileLine line)
        {
            lines.Add(line);
        }

        public void Add(string text)
        {
            lines.Add(new TextFileLine(text));
        }

        public void Add(string text, DisplayMode display, DisplayPosition position = DisplayPosition.Middle)
        {
            lines.Add(new TextFileLine(text, display, position));
        }

        public void Add(string text, SpecialMode special, DisplayPosition position = DisplayPosition.Middle)
        {
            lines.Add(new TextFileLine(text, special, position));
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

        public override IEnumerable<byte> GetBytes()
        {
            yield return Label;
            foreach(var line in lines)
            {
                if(line.Mode != null)
                {
                    yield return (byte) Ascii.ESC;
                    yield return (byte) line.Mode.Position;
                    yield return (byte) line.Mode.DisplayMode;

                    if(line.Mode.SpecialMode != SpecialMode.None)
                        yield return (byte) line.Mode.SpecialMode;

                    if(line.Mode.SpecialGraphic != SpecialGraphic.None)
                        yield return (byte) line.Mode.SpecialGraphic;
                }

                if(line.Text == null)
                    yield break;

                var formatted = line.Text.Format();
                foreach(var b in Encoding.UTF8.GetBytes(formatted))
                    yield return b;
            }
        }

        #region IEnumerable<TextFileLine> Members

        public IEnumerator<TextFileLine> GetEnumerator()
        {
            return lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}