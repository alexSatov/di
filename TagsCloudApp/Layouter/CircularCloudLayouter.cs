﻿using System;
using System.Linq;
using System.Drawing;
using TagsCloudApp.Config;
using System.Collections.Generic;

namespace TagsCloudApp.Layouter
{
    public class CircularCloudLayouter : IRectangleLayouter
    {
        public readonly Point CenterPoint;
        public List<Rectangle> Rectangles { get; }

        private readonly IEnumerable<Point> spiralPoints = new SpiralPoints();

        private static Point GetRectangleLocation(Point center, Size size)
        {
            return new Point(center.X - size.Width / 2, center.Y - size.Height / 2);
        }

        public CircularCloudLayouter(Settings settings)
        {
            CenterPoint = settings.CenterPoint;
            Rectangles = new List<Rectangle>();
        }

        private bool InFreePlace(Rectangle rectangle)
        {
            return !Rectangles.Any(rectangle.IntersectsWith);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = new Rectangle(0, 0, 0, 0);

            foreach (var nextPoint in spiralPoints)
            {
                var location = GetRectangleLocation(nextPoint + (Size)CenterPoint, rectangleSize);
                rectangle = new Rectangle(location, rectangleSize);

                if (InFreePlace(rectangle)) break;
            }

            rectangle = GetMovedToCenterRectangle(rectangle);
            Rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetMovedToCenterRectangle(Rectangle rectangle)
        {
            if (rectangle.GetCenter() == CenterPoint)
                return rectangle;

            var movedRect = rectangle;
            var vectorToCenter = CenterPoint - (Size)rectangle.GetCenter();
            vectorToCenter = new Point(Math.Sign(vectorToCenter.X), Math.Sign(vectorToCenter.Y));
            var cachedRect = rectangle;

            while (InFreePlace(movedRect))
            {
                cachedRect = movedRect;
                movedRect.Location += (Size)vectorToCenter;
            }

            return cachedRect;
        }
    }
}

