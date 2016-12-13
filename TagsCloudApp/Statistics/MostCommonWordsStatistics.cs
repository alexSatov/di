using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudApp.Statistics
{
    public class MostCommonWordsStatistics : IStatistics
    {
        public Dictionary<string, int> GetStatistics(string filename)
        {
            var statistics = new Dictionary<string, int>();

            var text = File.ReadAllLines(filename);
            var words = text
                .SelectMany(line => Regex.Split(line, @"\W+"))
                .Where(word => word.Length > 3)
                .Select(word => word.ToLower())
                .ToArray();
            var uniqueWords = words.Distinct();

            foreach (var uniqueWord in uniqueWords)
            {
                var count = words.Count(word => word == uniqueWord);
                statistics.Add(uniqueWord, count);
            }

            return statistics;
        }
    }
}