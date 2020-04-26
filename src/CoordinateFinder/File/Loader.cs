using System.Collections.Generic;
using System.IO;

namespace CoordinateFinder.File
{
    public class Loader
    {
        public static IEnumerable<string> ParseAndGetLine(string pathToFile)
        {
            using (var fileReader = new StreamReader(pathToFile))
            {
                string currentLine;
                while ((currentLine = fileReader.ReadLine()) != null)
                {
                    yield return currentLine;
                }
            }
        }
    }
}
