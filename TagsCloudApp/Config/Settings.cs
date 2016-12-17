using System.Drawing;

namespace TagsCloudApp.Config
{
    public class Settings
    {
        public string Font { get; set; }
        public Size ImageSize { get; set; }
        public string TagColor { get; set; }
        public int MinFontSize { get; set; }
        public int MaxFontSize { get; set; }
        public int MaxTagsCount { get; set; }
        public Point CenterPoint { get; set; }
        public string BackgrondColor { get; set; }
    }
}
