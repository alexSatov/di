using System;
using System.Linq;
using TagsCloudApp.BadWords;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudApp.Statistics
{
    public class MostCommonWordsStatisticsCalculator : IStatisticsCalculator
    {
        private readonly IWordsFilter wordsFilter; 

        public MostCommonWordsStatisticsCalculator(IWordsFilter wordsFilter)
        {
            this.wordsFilter = wordsFilter;
        }

        public Dictionary<string, int> Calculate(string text)
        {
            return wordsFilter.Filter(Regex.Split(text, @"\W+"))
                .GroupBy(w => w, StringComparer.InvariantCultureIgnoreCase)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}