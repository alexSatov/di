using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudApp.Layouter
{
    public interface IRectangleLayouter
    {
        List<Rectangle> Rectangles { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}