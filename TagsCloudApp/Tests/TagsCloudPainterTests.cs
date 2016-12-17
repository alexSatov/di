using System;
using FluentAssert;
using System.Drawing;
using NUnit.Framework;
using TagsCloudApp.Config;
using TagsCloudApp.Layouter;
using TagsCloudApp.TagsCloudCreating;

namespace TagsCloudApp.Tests
{
    [TestFixture]
    public class TagsCloudPainter_should
    {
        private static Settings settings = new AppConfigSettingsParser().ParseSettings();

        [Test]
        public void drawingOnlyPositivePoints()
        {
            settings.CenterPoint = new Point(0, 0);
            settings.ImageSize = new Size(50, 50);
            var cloudLayouter = new CircularCloudLayouter(settings);
            cloudLayouter.PutNextRectangle(new Size(50, 50));
            var visualizator = new TagsCloudPainter(settings);
            visualizator.DrawRectangles(cloudLayouter.Rectangles);
            for (var i = 0; i < 50; i++)
            {
                if (i < 24)
                    visualizator.Image.GetPixel(i, 24).ShouldBeEqualTo(Color.FromArgb(255, 255, 140, 0));
                else
                    visualizator.Image.GetPixel(24, Math.Abs(i - 24)).ShouldBeEqualTo(Color.FromArgb(255, 255, 140, 0));
            }
        }
    }
}
