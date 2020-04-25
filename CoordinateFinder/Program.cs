using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoordinateFinder
{
    class Program
    {

        static void Main(string[] args)
        {
            var coordinates = StringParser.ParseAndGetCoordinates("N13 G98 G81 X12.2156 Y21.6652 Z.335 I-.0394 F19.685");
            Console.Write(coordinates);
            var coordinates1 = StringParser.ParseAndGetCoordinatesFromMultipleLines("N13 G98 G81 X12.2156 Y21.6652 Z.335 I-.0394 F19.685"+Environment.NewLine +
                "N14 X14.0382 Y3.6672");
            Console.Write(coordinates1);

            Console.Read();
        }
    }


    class StringParser
    {
        static readonly Regex xpattern = new Regex(@"(X(\d)*(.)?\d+)");
        static readonly Regex ypattern = new Regex(@"(Y(\d)*(.)?\d+)");
        static readonly Regex zpattern = new Regex(@"(Z(\d)*(.)?\d+)");
        public static (double, double, double) ParseAndGetCoordinates(string line)
        {
            GetPatternValue(xpattern.Match(line), out double xcorrdinate);
            GetPatternValue(ypattern.Match(line), out double ycorrdinate);
            GetPatternValue(zpattern.Match(line), out double zcorrdinate);
            return (xcorrdinate, ycorrdinate, zcorrdinate);
        }

        public static IEnumerable<(double, double, double)> ParseAndGetCoordinatesFromMultipleLines(string lines)
        {
            var coordinates = new List<(double, double, double)>();
            foreach (var line in Regex.Split(lines,Environment.NewLine))
            {
                coordinates.Add(ParseAndGetCoordinates(line));
            }
            return coordinates;
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
