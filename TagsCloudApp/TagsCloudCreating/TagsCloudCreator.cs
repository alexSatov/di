using System;
using System.Linq;
using System.Drawing;
using TagsCloudApp.Config;
using TagsCloudApp.Layouter;
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

        public Bitmap CreateTagsCloudImage(string text)
        {
            var statistics = StatisticsCalculator.Calculate(text);
            if (statistics.Count == 0)
                throw new ArgumentException("Text without words for statistics");

            var mostPopularWords = statistics.OrderByDescending(entry => entry.Value).Take(Settings.MaxTagsCount).ToArray();
            var tags = GetAllTags(mostPopularWords);
            CloudPainter.DrawTags(tags);
            return CloudPainter.Image;
        }

        public List<Tag> GetAllTags(KeyValuePair<string, int>[] statisticsWords)
        {
            var tags = new List<Tag>();
            var maxTagWeight = statisticsWords[0].Value;
            var minTagWeight = statisticsWords.Last().Value;

            foreach (var pair in statisticsWords)
            {
                var font = new Font(Settings.Font, GetFontSize(pair.Value, minTagWeight, maxTagWeight, 
                    Settings.MinFontSize, Settings.MaxFontSize));

                var tag = new Tag(pair.Key, font);
                tag.Area = CloudLayouter.PutNextRectangle(tag.TagSize);
                tags.Add(tag);
            }
            return tags;
        }

        public int GetFontSize(int currentTagWeight, int minTagWeight, int maxTagWeight, int minFontSize, int maxFontSize)
        {
            if (minTagWeight == maxTagWeight)
                return minFontSize;

            return minFontSize + (currentTagWeight - minTagWeight) * (maxFontSize - minFontSize) / (maxTagWeight - minTagWeight);
        }
    }
}
