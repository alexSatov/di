using System;
using System.Drawing;
using System.Configuration;

namespace TagsCloudApp.Config
{
    public class AppConfigSettingsParser : ISettingsParser
    {
        public Settings ParseSettings()
        {
            var config = (TagsCloudSection) ConfigurationManager.GetSection("tagsCloud");
            return new Settings
            {
                Font = config.Font.Name,
                TagColor = config.Color.Tag,
                MinFontSize = config.Font.MinSize,
                MaxFontSize = config.Font.MaxSize,
                MaxTagsCount = config.MaxTagsCount,
                BackgrondColor = config.Color.Background,
                CenterPoint = new Point(config.CenterPoint.X, config.CenterPoint.Y),
                ImageSize = new Size(config.ImageSize.Width, config.ImageSize.Height)
            };
        }
    }
}