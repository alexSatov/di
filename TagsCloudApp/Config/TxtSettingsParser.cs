using System;
using System.IO;
using System.Linq;
using System.Drawing;
namespace TagsCloudApp.Config
{
    public class TxtSettingsParser : ISettingsParser
    {
        public Settings ParseSettings(string settingsFile)
        {
            var settingsData = File.ReadAllLines("settings.txt")
                .Select(line => line.Split(' '))
                .ToDictionary(pair => pair[0], pair => pair[1]);

            var font = settingsData["font"];
            var textFile = settingsData["textFile"];
            var maxTagsCount = int.Parse(settingsData["maxTagsCount"]);

            var coordinates = settingsData["centerPoint"].Split(',').Select(int.Parse).ToArray();
            var centerPoint = new Point(coordinates[0], coordinates[1]);

            var size = settingsData["imageSize"].Split(',').Select(int.Parse).ToArray();
            var imageSize = new Size(size[0], size[1]);

            var fontSize = settingsData["fontSizeRange"].Split(',').Select(int.Parse).ToArray();
            var fontSizeRange = Tuple.Create(fontSize[0], fontSize[1]);

            return new Settings
            {
                CenterPoint = centerPoint,
                Font = font,
                FontSizeRange = fontSizeRange,
                ImageSize = imageSize,
                MaxTagsCount = maxTagsCount,
                TextFile = textFile
            };
        }
    }
}