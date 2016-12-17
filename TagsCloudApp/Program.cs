using Autofac;
using System.Drawing;
using System.Reflection;
using TagsCloudApp.Config;
using TagsCloudApp.BadWords;
using TagsCloudApp.Layouter;
using TagsCloudApp.Statistics;
using TagsCloudApp.TextReaders;
using TagsCloudApp.TagsCloudCreating;

namespace TagsCloudApp
{
    public class Program
    {
        public static Options UserOptions;

        public static void Main(string[] args)
        {
            UserOptions = Argparser.Parse(args);

            var container = GetContainer();
            var textFile = UserOptions.TextFile;
            var imageSaveFile = UserOptions.ImageSaveFile;

            var fileReader = container.Resolve<IFileReader>();
            var tagsCloudCreator = container.Resolve<TagsCloudCreator>();

            var text = fileReader.ReadTextFromFile(textFile);
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
            builder.RegisterType<BoringWordsFilter>().As<IWordsFilter>();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();

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
            
            settings.Font = UserOptions.Font ?? settings.Font;
            settings.TagColor = UserOptions.TagColor ?? settings.TagColor;
            settings.BackgrondColor = UserOptions.BackgroundColor ?? settings.BackgrondColor;
            settings.MinFontSize = UserOptions.MinFontSize != 0 ? UserOptions.MinFontSize : settings.MinFontSize;
            settings.MaxFontSize = UserOptions.MaxFontSize != 0 ? UserOptions.MaxFontSize : settings.MaxFontSize;
            settings.MaxTagsCount = UserOptions.MaxTagsCount != 0 ? UserOptions.MaxTagsCount : settings.MaxTagsCount;

            return settings;
        }
    }
}
