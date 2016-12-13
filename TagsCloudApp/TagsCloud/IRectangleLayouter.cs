using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudApp.TagsCloud
{
    public interface IRectangleLayouter
    {
        List<Rectangle> Rectangles { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}