using System.IO;
using TagsCloudApp.TextReaders;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudApp.BadWords
{
    public class BoringWordsFilter : IWordsFilter
    {
        public readonly HashSet<string> Prepositions;
        public readonly HashSet<string> Pronouns;
        public readonly HashSet<string> Unions;

        public BoringWordsFilter(IFileReader fileReader)
        {
            string[] prepositions;
            string[] pronouns;
            string[] unions;

            try
            {
                prepositions = Regex.Split(fileReader.ReadTextFromFile("BoringWords/prepositions.txt"), @"\W+");
                pronouns = Regex.Split(fileReader.ReadTextFromFile("BoringWords/pronouns.txt"), @"\W+");
                unions = Regex.Split(fileReader.ReadTextFromFile("BoringWords/unions.txt"), @"\W+");
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("File with boring words is missing");
            }

            Unions = new HashSet<string>(unions);
            Pronouns = new HashSet<string>(pronouns);
            Prepositions = new HashSet<string>(prepositions);
        }

        public bool IsCorrectWord(string word)
        {
            return !Prepositions.Contains(word) && !Pronouns.Contains(word) && !Unions.Contains(word);
        }
    }
}