using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudApp.Statistics
{
    public class MostCommonWordsStatisticsCalculator : IStatisticsCalculator
    {
        public Dictionary<string, int> Calculate(string text)
        {
            return Regex.Split(text, @"\W+")
            .Where(w => w.Length > 3)
            .GroupBy(w => w, StringComparer.InvariantCultureIgnoreCase)
            .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}