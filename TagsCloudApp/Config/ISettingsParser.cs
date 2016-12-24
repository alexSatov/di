namespace TagsCloudApp.Config
{
    public interface ISettingsParser
    {
        Result<Settings> ParseSettings();
    }
}