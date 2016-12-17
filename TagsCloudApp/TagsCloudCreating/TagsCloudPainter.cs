using System.Linq;
using System.Drawing;
using TagsCloudApp.Config;
using System.Collections.Generic;

namespace TagsCloudApp.TagsCloudCreating
{
    public class TagsCloudPainter
    {
        public Pen Pen { get; set; }
        public Bitmap Image { get; private set; }
        public Color BackgroundColor { get; set; }
        public Graphics Painter { get; private set; }

        public TagsCloudPainter(Settings settings)
        {
            Pen = new Pen(ColorTranslator.FromHtml($"#{settings.TagColor}"), 3);
            BackgroundColor = ColorTranslator.FromHtml($"#{settings.BackgrondColor}");
            CreateNewImage(settings.ImageSize);
        }

        public void CreateNewImage(Size imageSize)
        {
            Image = new Bitmap(imageSize.Width, imageSize.Height);
            Painter = Graphics.FromImage(Image);
            Painter.FillRectangle(new SolidBrush(BackgroundColor), new Rectangle(new Point(0, 0), imageSize));
        }

        public void DrawRectangles(IEnumerable<Rectangle> rectangles)
        {
            Painter.DrawRectangles(Pen, rectangles.ToArray());
        }

        public void DrawTags(IEnumerable<Tag> tags)
        {
            var brush = new SolidBrush(Pen.Color);
            foreach (var tag in tags)
            {
                Painter.DrawString(tag.Text, tag.TagFont, brush, tag.Area.Location);
            }
        }
    }
}

