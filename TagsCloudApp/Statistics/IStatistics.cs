using System.Collections.Generic;

namespace TagsCloudApp.Statistics
{
    public interface IStatistics
    {
        Dictionary<string, int> GetStatistics(string filename);
    }
}