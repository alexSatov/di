namespace TagsCloudApp.TextReaders
{
    public interface IFileReader
    {
        Result<string> ReadTextFromFile(string filename);
    }
}