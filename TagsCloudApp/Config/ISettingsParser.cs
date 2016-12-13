namespace TagsCloudApp.Config
{
    public interface ISettingsParser
    {
        Settings ParseSettings(string settingsFile);
    }
}