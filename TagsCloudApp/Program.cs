using System;
using Autofac;
using TagsCloudApp.Config;
using TagsCloudApp.Statistics;
using TagsCloudApp.TagsCloud;
using TagsCloudApp.TagsCloudCreating;

namespace TagsCloudApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите название файла с изображением");
            var imageName = Console.ReadLine();
            var settings = GetSettings();
            var container = GetContainer(settings);

            using (var scope = container.BeginLifetimeScope())
            {
                var tagsCloudCreator = scope.Resolve<TagsCloudCreator>();
                var image = tagsCloudCreator.CreateTagsCloudImage();
                TagsCloudSaver.SaveTagsCloudImage(image, imageName);
            }
        }

        public static IContainer GetContainer(Settings settings)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MostCommonWordsStatistics>().As<IStatistics>();
            builder.RegisterType<CircularCloudLayouter>().As<IRectangleLayouter>();
            builder.RegisterType<Tag>().AsSelf();
            builder.RegisterType<TagsCloudPainter>().AsSelf();
            builder.RegisterType<TagsCloudCreator>().AsSelf();

            return builder.Build();
        }

        public static Settings GetSettings()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TxtSettingsParser>().As<ISettingsParser>();
            builder.Register(c =>
            {
                var result = new TxtSettingsParser().ParseSettings("settings.txt");
                return result;
            });


            var container = builder.Build();
            Settings settings;

            using (var scope = container.BeginLifetimeScope())
            {
                var settingsParser = scope.Resolve<ISettingsParser>();
                settings = settingsParser.ParseSettings("settings.txt");
            }

            return settings;
        }
    }
}
