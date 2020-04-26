using System;
using System.Linq;
using Xunit;


namespace CoordinateFinder.Tests
{
    public class CoordinateParserTests
    {
        [Fact]
        public void GivenALineWithXYZCoordinatesPresent_ReturnsXYZCordinates()
        {
            var line = @"N13 G98 G81 X12.2156 Y21.6652 Z.335 I-.0394 F19.685";
            var parseCoordinates = CoordinateParser.ParseAndGetCoordinatesFromMultipleLines(line);
            foreach ((double x, double y, double z) in parseCoordinates)
            {
                Assert.Equal(12.2156, x);
                Assert.Equal(21.6652, y);
                Assert.Equal(.335, z);
            }
        }

        [Fact]
        public void GivenALineWithXCoordinatesPresent_ReturnsXCordinates()
        {
            var line = @"N13 G98 G81 X12.2156 I-.0394 F19.685";
            var parseCoordinates = CoordinateParser.ParseAndGetCoordinatesFromMultipleLines(line);
            foreach ((double x, double y, double z) in parseCoordinates)
            {
                Assert.Equal(12.2156, x);
                Assert.Equal(0d, y);
                Assert.Equal(0d, z);
            }
        }

        [Fact]
        public void GivenALineWithNoCoordinatesPresent_ReturnsEmptyLists()
        {
            var line = @"N13 G98 G81 I-.0394 F19.685";
            var parseCoordinates = CoordinateParser.ParseAndGetCoordinatesFromMultipleLines(line);
            Assert.Empty(parseCoordinates);
        }

        [Fact]
        public void GivenMultipleLinesWithXYZCoordinatesPresent_ReturnsListXYZCordinates()
        {
            var lines = @"N13 G98 G81 X12.2156 Y21.6652 Z.335 I-.0394 F19.685"
                        + Environment.NewLine +
                        "N14 G98 G81 X1.2156 Y2.6652 Z3.335 I - .0394 F19.685";
            var parseCoordinates = CoordinateParser.ParseAndGetCoordinatesFromMultipleLines(lines);
            Assert.Equal(parseCoordinates.First(), (12.2156, 21.6652, .335));
            Assert.Equal(parseCoordinates.Last(), (1.2156, 2.6652, 3.335));
        }

        [Fact]
        public void GivenFileWithCoordinates_ReturnsListXYZCordinates()
        {
            var pathToFile = "TestFile\\TestInput.txt";
            var parseCoordinates = CoordinateParser
                .ParseAndGetCoordinatesFromFile(pathToFile)
                .Where(x => x.HasValue)
                ;
            Assert.NotNull(parseCoordinates);
        }
    }
}
