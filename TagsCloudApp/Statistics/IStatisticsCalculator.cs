using System.Collections.Generic;

namespace TagsCloudApp.Statistics
{
    public interface IStatisticsCalculator
    {
        Dictionary<string, int> Calculate(string text);
    }
}