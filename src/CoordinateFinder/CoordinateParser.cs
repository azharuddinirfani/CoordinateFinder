using CoordinateFinder.File;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CoordinateFinder
{
    public class CoordinateParser
    {
        static readonly Regex xpattern = new Regex(@"(X(\d)*(.)?\d+)");
        static readonly Regex ypattern = new Regex(@"(Y(\d)*(.)?\d+)");
        static readonly Regex zpattern = new Regex(@"(Z(\d)*(.)?\d+)");
        public static (double, double, double)? ParseAndGetCoordinates(string line)
        {
            var hasPatternFound = GetPatternValue(xpattern.Match(line), out double xcorrdinate);
            hasPatternFound |= GetPatternValue(ypattern.Match(line), out double ycorrdinate);
            hasPatternFound |= GetPatternValue(zpattern.Match(line), out double zcorrdinate);
            if (!hasPatternFound)
            {
                return (null);
            }
            return (xcorrdinate, ycorrdinate, zcorrdinate);
        }

        public static IEnumerable<(double, double, double)> ParseAndGetCoordinatesFromMultipleLines(string lines)
        {
            var coordinatesList = new List<(double, double, double)>();
            foreach (var line in Regex.Split(lines, Environment.NewLine))
            {
                var coordinates = ParseAndGetCoordinates(line);
                if (coordinates.HasValue)
                {
                    coordinatesList.Add(coordinates.Value);
                }
            }
            return coordinatesList;
        }

        public static IEnumerable<(double, double, double)?> ParseAndGetCoordinatesFromFile(string pathToFile)
        {
            foreach (var line in Loader.ParseAndGetLine(pathToFile))
            {
                yield return ParseAndGetCoordinates(line);
            }
        }

        private static bool GetPatternValue(Match match, out double xcorrdinate)
        {
            xcorrdinate = 0d;
            if (match.Success)
            {
                double.TryParse(match.Value.Substring(1), out xcorrdinate);
                return true;
            }
            return false;
        }



    }
}
