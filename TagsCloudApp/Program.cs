using System;
using Autofac;
using System.IO;
using System.Drawing;
using System.Reflection;
using TagsCloudApp.Config;
using TagsCloudApp.Layouter;
using TagsCloudApp.Statistics;
using TagsCloudApp.TagsCloudCreating;

namespace TagsCloudApp
{
    public class Program
    {
        public static Options UserOptions;

        public static void Main(string[] args)
        {
            UserOptions = Argparser.Parse(args);

            var textFile = UserOptions.TextFile;
            var imageSaveFile = UserOptions.ImageSaveFile;
            var container = GetContainer();
            var tagsCloudCreator = container.Resolve<TagsCloudCreator>();

            string text;
            try
            {
                text = File.ReadAllText(textFile);
            }
            catch (FileNotFoundException)
            {
                throw new ArgumentException("File with this name not exist");
            }

            var image = tagsCloudCreator.CreateTagsCloudImage(text);
            TagsCloudSaver.SaveTagsCloudImage(image, imageSaveFile);
        }

        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            builder.RegisterType<AppConfigSettingsParser>().As<ISettingsParser>();
            builder.Register(c => ConcatWithUserOptions(c.Resolve<ISettingsParser>().ParseSettings()));

            builder.RegisterType<MostCommonWordsStatisticsCalculator>().As<IStatisticsCalculator>();
            builder.RegisterType<CircularCloudLayouter>().As<IRectangleLayouter>();

            return builder.Build();
        }

        public static Settings ConcatWithUserOptions(Settings settings)
        {
            var width = UserOptions.Width != 0 ? UserOptions.Width : settings.ImageSize.Width;
            var height = UserOptions.Height != 0 ? UserOptions.Height : settings.ImageSize.Height;
            settings.ImageSize = new Size(width, height);

            var x = UserOptions.X != 0 ? UserOptions.X : settings.CenterPoint.X;
            var y = UserOptions.Y != 0 ? UserOptions.Y : settings.CenterPoint.Y;
            settings.CenterPoint = new Point(x, y);

            var minFontSize = UserOptions.MinFontSize != 0 ? UserOptions.MinFontSize : settings.FontSizeRange.Item1;
            var maxFontSize = UserOptions.MaxFontSize != 0 ? UserOptions.MaxFontSize : settings.FontSizeRange.Item2;
            settings.FontSizeRange = new Tuple<int, int>(minFontSize, maxFontSize);
            
            settings.BackgrondColor = UserOptions.BackgroundColor != null
                ? ColorTranslator.FromHtml("#" + UserOptions.BackgroundColor)
                : settings.BackgrondColor;

            settings.TagColor = UserOptions.TagColor != null
                ? ColorTranslator.FromHtml("#" + UserOptions.TagColor)
                : settings.TagColor;

            settings.Font = UserOptions.Font ?? settings.Font;
            settings.MaxTagsCount = UserOptions.MaxTagsCount != 0 ? UserOptions.MaxTagsCount : settings.MaxTagsCount;

            return settings;
        }
    }
}
