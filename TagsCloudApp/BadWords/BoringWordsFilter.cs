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
            var filterFiles = GetFilterFiles(path);
            var boringWords = GetAllBoringWords(filterFiles, fileReader);
            BoringWords = new HashSet<string>(boringWords);
        }

        private static IEnumerable<string> GetAllBoringWords(IEnumerable<string> files, IFileReader fileReader)
        {
            IEnumerable<string> boringWords = new List<string>();
            boringWords = files
                .Select(fileReader.ReadTextFromFile)
                .Aggregate(boringWords, (current, words) => current.Concat(Regex.Split(words, @"\W+")));
            return boringWords;
        }

        private static IEnumerable<string> GetFilterFiles(string path)
        {
            var filterFiles = new List<string>();

            if (Directory.Exists(path))
                filterFiles.AddRange(Directory.GetFiles(path));
            else if (File.Exists(path))
                filterFiles.Add(path);
            else
                throw new FileNotFoundException("File/Dir with boring words is missing");

            return filterFiles;
        }

        public bool IsCorrectWord(string word)
        {
            return !BoringWords.Contains(word);
        }
    }
}