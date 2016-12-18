using System;
using CommandLine;
using CommandLine.Text;

namespace TagsCloudApp
{
    public static class Argparser
    {
        public static Options Parse(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                throw new ArgumentException("Incorrect command line arguments");
            }
            return options;
        }
    }

    public class Options
    {
        [Option('t', "textfile", Required = true, HelpText = "File name with words for statistics calculating")]
        public string TextFile { get; set; }

        [Option('i', "imagesavefile", Required = true, HelpText = "File name in which image will be saved")]
        public string ImageSaveFile { get; set; }

        [Option('p', "wordFiltersPath", HelpText = "File/Dir path to file/files with words to filter")]
        public string WordFiltersPath { get; set; }

        [Option('w', "wigth", HelpText = "Width of image")]
        public int Width { get; set; }

        [Option('h', "height", HelpText = "Height of image")]
        public int Height { get; set; }

        [Option('x', HelpText = "X coordinate of central point")]
        public int X { get; set; }

        [Option('y', HelpText = "Y coordinate of central point")]
        public int Y { get; set; }

        [Option('n', "minfontsize", HelpText = "Min font size")]
        public int MinFontSize { get; set; }

        [Option('m', "maxfontsize", HelpText = "Max font size")]
        public int MaxFontSize { get; set; }

        [Option('f', "font", HelpText = "Font name")]
        public string Font { get; set; }

        [Option('c', "tagscount", HelpText = "Max tags count on image")]
        public int MaxTagsCount { get; set; }

        [Option('b', "backgroundcolor", HelpText = "Background color in hex format")]
        public string BackgroundColor { get; set; }

        [Option('g', "tagcolor", HelpText = "Tag color in hex format")]
        public string TagColor { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}