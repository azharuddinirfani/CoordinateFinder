using System;
using System.Linq;

namespace CoordinateFinder
{
    class Program
    {

        static void Main(string[] args)
        {
            ParseAndPrintCoordinates(args[0]);
            Console.Read();
        }

        static void ParseAndPrintCoordinates(string filePath)
        {
            var coordinates = CoordinateParser
                .ParseAndGetCoordinatesFromFile(filePath)
                .Where(coordinate => coordinate.HasValue)
                .Select(x => x.HasValue ? (x.Value.Item1, x.Value.Item2, x.Value.Item3) :
                (x.Value.Item1, x.Value.Item2, x.Value.Item3));
            foreach ((double x, double y, double z) in coordinates)
            {
                Console.WriteLine($"{x} {y} {z}");
            }
        }
    }
}
