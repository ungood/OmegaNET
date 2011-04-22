using System.Collections.Generic;
using Omega.Client;
using Omega.Client.Models;

namespace Omega.Web.Models
{
    public class TextFileModel
    {
        public string Label { get; set; }
        public string Description { get; set; }
        
        public int MaxLength { get; set; }
        
        public List<TextFileLineModel> Lines { get; set; }

        public TextFileModel()
        {
            Lines = new List<TextFileLineModel>();
        }
    }

    public class TextFileLineModel
    {
        public string Text { get; set; }

        public DisplayPosition Position { get; set; }
        public DisplayMode DisplayMode { get; set; }
        public SpecialMode SpecialMode { get; set; }
        public SpecialGraphic SpecialGraphic { get; set; }
    }
}