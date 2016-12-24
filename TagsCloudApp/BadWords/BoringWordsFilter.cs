using System.IO;
using System.Linq;
using TagsCloudApp.Config;
using TagsCloudApp.TextReaders;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudApp.BadWords
{
    public class BoringWordsFilter : IWordsFilter
    {
        public readonly HashSet<string> BoringWords;

        public BoringWordsFilter(IFileReader fileReader, Settings settings)
        {
            var path = settings.WordsFilterPath;
            //var filterFiles = GetFilterFiles(path).GetValueOrThrow();
            var boringWords = GetFilterFiles(path)
                .Then(f => GetAllBoringWords(f, fileReader));
            BoringWords = boringWords.IsSuccess ? new HashSet<string>(boringWords.Value) : new HashSet<string>();
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words
                .Select(w => w.ToLower())
                .Where(w => w.Length > 2 && !BoringWords.Contains(w));
        }

        private static IEnumerable<string> GetAllBoringWords(IEnumerable<string> files, IFileReader fileReader)
        {
            IEnumerable<string> boringWords = new List<string>();
            boringWords = files
                .Select(f => fileReader.ReadTextFromFile(f).GetValueOrThrow())
                .Aggregate(boringWords, (current, words) => current.Concat(Regex.Split(words, @"\W+")));
            return boringWords;
        }

        private static Result<List<string>> GetFilterFiles(string path)
        {
            var filterFiles = new List<string>();

            if (Directory.Exists(path))
                filterFiles.AddRange(Directory.GetFiles(path));
            if (File.Exists(path))
                filterFiles.Add(path);

            return filterFiles.Count != 0
                ? Result.Of(() => filterFiles)
                : Result.Fail<List<string>>("File / Dir with boring words isn't exist");
        }
    }
}