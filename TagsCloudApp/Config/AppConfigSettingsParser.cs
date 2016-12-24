using System.Drawing;
using System.Configuration;

namespace TagsCloudApp.Config
{
    public class AppConfigSettingsParser : ISettingsParser
    {
        public Result<Settings> ParseSettings()
        {
            return Result.Of(GetSettingsFromSection).ReplaceError(e => "Can't get settings section");
        }

        private static Settings GetSettingsFromSection()
        {
            var config = (TagsCloudSection)ConfigurationManager.GetSection("tagsCloud");
            return new Settings
            {
                Font = config.Font.Name,
                TagColor = config.Color.Tag,
                MinFontSize = config.Font.MinSize,
                MaxFontSize = config.Font.MaxSize,
                MaxTagsCount = config.MaxTagsCount,
                WordsFilterPath = config.WordFiltersPath,
                BackgrondColor = config.Color.Background,
                CenterPoint = new Point(config.CenterPoint.X, config.CenterPoint.Y),
                ImageSize = new Size(config.ImageSize.Width, config.ImageSize.Height)
            };
        }
    }
}