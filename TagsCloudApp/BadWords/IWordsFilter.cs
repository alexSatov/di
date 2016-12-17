namespace TagsCloudApp.BadWords
{
    public interface IWordsFilter
    {
        bool IsCorrectWord(string word);
    }
}