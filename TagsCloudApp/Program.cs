using Autofac;
using System.Reflection;
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
            var textFile = args[0];
            var imageSaveFile = args[1];
            var container = GetContainer();
            
            var tagsCloudCreator = container.Resolve<TagsCloudCreator>();
            var image = tagsCloudCreator.CreateTagsCloudImage(textFile);
            TagsCloudSaver.SaveTagsCloudImage(image, imageSaveFile);
        }

        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            builder.RegisterType<AppConfigSettingsParser>().As<ISettingsParser>();
            builder.Register(c => c.Resolve<ISettingsParser>().ParseSettings());

            builder.RegisterType<MostCommonWordsStatisticsCalculator>().As<IStatisticsCalculator>();
            builder.RegisterType<CircularCloudLayouter>().As<IRectangleLayouter>();

            return builder.Build();
        }
    }
}
