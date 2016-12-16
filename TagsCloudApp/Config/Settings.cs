using System;
using System.Drawing;

namespace TagsCloudApp.Config
{
    public class Settings
    {
        public string Font { get; set; }
        public Size ImageSize { get; set; }
        public Color TagColor { get; set; }
        public int MaxTagsCount { get; set; }
        public Point CenterPoint { get; set; }
        public Color BackgrondColor { get; set; }
        public Tuple<int, int> FontSizeRange { get; set; }
    }
}
