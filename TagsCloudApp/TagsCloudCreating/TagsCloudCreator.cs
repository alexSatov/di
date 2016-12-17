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
            
            var mostPopularWords = statistics.OrderByDescending(entry => entry.Value).Take(Settings.MaxTagsCount).ToArray();
            var tagWeightRange = GetTagWeightRange(mostPopularWords);
            var tags = GetAllPuttedTags(mostPopularWords, tagWeightRange);

            CloudPainter.DrawTags(tags);
            return CloudPainter.Image;
        }

        public Tuple<int, int> GetTagWeightRange(KeyValuePair<string, int>[] statisticsWords)
        {
            try
            {
                var maxTagWeight = statisticsWords[0].Value;
                var minTagWeight = statisticsWords.Last().Value;
                return Tuple.Create(minTagWeight, maxTagWeight);
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentException("Text without words for statistics");
            }
        }

        public List<Tag> GetAllPuttedTags(KeyValuePair<string, int>[] statisticsWords, Tuple<int, int> tagWeightRange)
        {
            var tags = new List<Tag>();
            foreach (var pair in statisticsWords)
            {
                var tag = new Tag(pair.Key, new Font(Settings.Font, GetFontSize(pair.Value, tagWeightRange, Settings.FontSizeRange)));
                tag.Area = CloudLayouter.PutNextRectangle(tag.TagSize);
                tags.Add(tag);
            }
            return tags;
        }

        public int GetFontSize(int currentTagWeight, Tuple<int, int> tagWeightRange, Tuple<int, int> fontSizeRange)
        {
            if (tagWeightRange.Item1 == tagWeightRange.Item2) return fontSizeRange.Item1;
            return fontSizeRange.Item1 + (currentTagWeight - tagWeightRange.Item1) *
                (fontSizeRange.Item2 - fontSizeRange.Item1) / (tagWeightRange.Item2 - tagWeightRange.Item1);
        }
    }
}
