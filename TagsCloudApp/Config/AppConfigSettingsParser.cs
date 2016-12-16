using System;
using System.Drawing;
using System.Configuration;

namespace TagsCloudApp.Config
{
    public class AppConfigSettingsParser : ISettingsParser
    {
        public Settings ParseSettings()
        {
            var config = (TagsCloudSection) ConfigurationManager.GetSection("tagsCloudGroup/tagsCloud");
            return new Settings
            {
                Font = config.Font.Name,
                MaxTagsCount = config.MaxTagsCount,
                FontSizeRange = new Tuple<int, int>(config.Font.MinSize, config.Font.MaxSize),
                ImageSize = new Size(config.ImageSize.Width, config.ImageSize.Height),
                CenterPoint = new Point(config.CenterPoint.X, config.CenterPoint.Y),
                BackgrondColor = ColorTranslator.FromHtml("#" + config.Color.Background),
                TagColor = ColorTranslator.FromHtml("#" + config.Color.Tag)
            };
        }
    }
}