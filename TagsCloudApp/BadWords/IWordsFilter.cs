using System.Collections.Generic;

namespace TagsCloudApp.BadWords
{
    public interface IWordsFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> words);
    }
}