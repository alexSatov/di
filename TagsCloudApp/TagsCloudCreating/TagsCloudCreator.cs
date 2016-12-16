﻿using System;
using System.IO;
using System.Linq;
using System.Drawing;
using TagsCloudApp.Config;
using TagsCloudApp.TagsCloud;
using TagsCloudApp.Statistics;
using System.Collections.Generic;

namespace TagsCloudApp.TagsCloudCreating
{
    public class TagsCloudCreator
    {
        public Settings Settings;
        public TagsCloudPainter CloudPainter;
        public IRectangleLayouter CloudLayouter;
        public IStatisticsCalculator StatisticsCalculator;

        public TagsCloudCreator(IRectangleLayouter cloudLayouter, TagsCloudPainter cloudPainter, 
            IStatisticsCalculator statisticsCalculator, Settings settings)
        {
            Settings = settings;
            CloudPainter = cloudPainter;
            CloudLayouter = cloudLayouter;
            StatisticsCalculator = statisticsCalculator;
        }

        public Bitmap CreateTagsCloudImage(string textFile)
        {
            Dictionary<string, int> statistics;
            try
            {
                statistics = StatisticsCalculator.Calculate(File.ReadAllText(textFile));
            }
            catch (FileNotFoundException)
            {
                throw new ArgumentException("Incorrect filename with text");
            }

            var tags = new List<Tag>();
            var mostPopularWords = statistics.OrderByDescending(entry => entry.Value).Take(Settings.MaxTagsCount).ToArray();

            var maxTagWeight = mostPopularWords[0].Value;
            var minTagWeight = mostPopularWords.Last().Value;
            var tagWeightRange = Tuple.Create(minTagWeight, maxTagWeight);

            foreach (var pair in mostPopularWords)
            {
                var tag = new Tag(pair.Key, new Font(Settings.Font, GetFontSize(pair.Value, tagWeightRange, Settings.FontSizeRange)));
                tag.Area = CloudLayouter.PutNextRectangle(tag.TagSize);
                tags.Add(tag);
            }

            CloudPainter.DrawTags(tags);
            return CloudPainter.Image;
        }

        public int GetFontSize(int currentTagWeight, Tuple<int, int> tagWeightRange, Tuple<int, int> fontSizeRange)
        {
            return fontSizeRange.Item1 + (currentTagWeight - tagWeightRange.Item1) *
                (fontSizeRange.Item2 - fontSizeRange.Item1) / (tagWeightRange.Item2 - tagWeightRange.Item1);
        }
    }
}
