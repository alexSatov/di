using System;
using System.IO;

namespace TagsCloudApp.TextReaders
{
    public class TxtFileReader : IFileReader
    {
        public string ReadTextFromFile(string filename)
        {
            try
            {
                return File.ReadAllText(filename);
            }
            catch (FileNotFoundException)
            {
                throw new ArgumentException("File with this name not exist");
            }
        }
    }
}