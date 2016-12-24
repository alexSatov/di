using System.IO;

namespace TagsCloudApp.TextReaders
{
    public class TxtFileReader : IFileReader
    {
        public Result<string> ReadTextFromFile(string filename)
        {
            return Result.Of(() => File.ReadAllText(filename))
                .ReplaceError(e => "File with this name not exist");
        }
    }
}