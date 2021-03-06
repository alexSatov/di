﻿using System;
using System.Linq;
using FluentAssert;
using System.Drawing;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudApp.Config;
using TagsCloudApp.Layouter;
using TagsCloudApp.TagsCloudCreating;

namespace TagsCloudApp.Tests
{
    [TestFixture]
    public class CircularCloudLayouter_should
    {
        private CircularCloudLayouter cloudLayouter;

        [SetUp]
        public void CreateCircularCloudLayouter()
        {
            cloudLayouter = new CircularCloudLayouter(new Settings { CenterPoint = new Point(500, 500) });
        }

        [TestCase(0, 0, ExpectedResult = new[] { -25, -25 })]
        [TestCase(100, 60, ExpectedResult = new[] { 75, 35 })]
        [TestCase(500, 500, ExpectedResult = new[] { 475, 475 })]
        public int[] alwaysPutFirstRectangle_atCenter(int pointX, int pointY)
        {
            cloudLayouter = new CircularCloudLayouter(new Settings { CenterPoint = new Point(pointX, pointY) });
            var rectangle = cloudLayouter.PutNextRectangle(new Size(50, 50));
            return new[] { rectangle.Location.X, rectangle.Location.Y };
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(8, ExpectedResult = 8)]
        [TestCase(1000, ExpectedResult = 1000)]
        public int savePuttedRectangles(int rectanglesCount)
        {
            for (var i = 0; i < rectanglesCount; i++)
                cloudLayouter.PutNextRectangle(new Size(50, 20));
            return cloudLayouter.Rectangles.Count;
        }

        [Test]
        public void buildCircularCloud()
        {
            for (var i = 0; i < 50; i++)
                cloudLayouter.PutNextRectangle(new Size(50, 20));

            var leftmostRect = Math.Abs(cloudLayouter.Rectangles.Min(rect => rect.Location.X) - cloudLayouter.CenterPoint.X);
            var rightmostRect = Math.Abs(cloudLayouter.Rectangles.Max(rect => rect.Location.X) - cloudLayouter.CenterPoint.X);
            var upmostRect = Math.Abs(cloudLayouter.Rectangles.Max(rect => rect.Location.Y) - cloudLayouter.CenterPoint.Y);
            var downmostRect = Math.Abs(cloudLayouter.Rectangles.Min(rect => rect.Location.Y) - cloudLayouter.CenterPoint.Y);

            Assert.AreEqual(leftmostRect, rightmostRect, 60);
            Assert.AreEqual(upmostRect, downmostRect, 30);
        }

        [Test]
        public void buildCloud_withNotIntersectingRectangles()
        {
            for (var i = 0; i < 30; i++)
                cloudLayouter.PutNextRectangle(new Size(15, 15));
            var intersectingRectangles = cloudLayouter.Rectangles.SelectMany(
                rect => cloudLayouter.Rectangles.Where(
                    otherRect => rect.IntersectsWith(otherRect) && rect != otherRect));

            intersectingRectangles.Count().ShouldBeEqualTo(0);
        }

        [TearDown]
        public void CheckOnFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var settings = new AppConfigSettingsParser().ParseSettings().Value;
                settings.ImageSize = new Size(1000, 1000);
                var cloudPainter = new TagsCloudPainter(settings);
                var dir = TestContext.CurrentContext.TestDirectory + "\\FailedTests\\";
                var testName = TestContext.CurrentContext.Test.Name;
                var path = dir + testName + "_cloud.png";
                cloudPainter.DrawRectangles(cloudLayouter.Rectangles);
                TagsCloudSaver.SaveTagsCloudImage(cloudPainter.Image, path);
                Console.WriteLine("Tag cloud visualization saved to file " + path);
            }
        }
    }
}

