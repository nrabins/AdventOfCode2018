using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day06_Test
    {
        public Day06 Day06 { get; set; }

        [SetUp]
        public void Setup()
        {
            Day06 = new Day06();
            Day06.Testing = true;
        }

        public string[] TestInput => new[]
        {
            "1, 1",
            "1, 6",
            "8, 3",
            "3, 4",
            "5, 5",
            "8, 9",
        };

        public class ParsePointTestCase
        {
            public string Input { get; set; }
            public Point Expected { get; set; }

            public ParsePointTestCase(string input, Point expected)
            {
                Input = input;
                Expected = expected;
            }
        }

        private static IEnumerable<ParsePointTestCase> ParsePointTestCases
        {
            get
            {
                yield return new ParsePointTestCase("1, 1", new Point(1, 1));
                yield return new ParsePointTestCase("1, 6", new Point(1, 6));
                yield return new ParsePointTestCase("8, 3", new Point(8, 3));
                yield return new ParsePointTestCase("3, 4", new Point(3, 4));
                yield return new ParsePointTestCase("5, 5", new Point(5, 5));
                yield return new ParsePointTestCase("8, 9", new Point(8, 9));
            }
        }

        [TestCaseSource(nameof(ParsePointTestCases))]
        public void Point_Parse(ParsePointTestCase testCase)
        {
            var parsed = Point.Parse(testCase.Input);
            Assert.AreEqual(testCase.Expected, parsed);
        }

        [Test]
        public void Point_ManhattanDistanceTo()
        {
             Assert.AreEqual(0, new Point(0, 0).ManhattanDistanceTo(new Point(0, 0)));
             Assert.AreEqual(5, new Point(0, 0).ManhattanDistanceTo(new Point(0, 5)));
             Assert.AreEqual(5, new Point(0, 0).ManhattanDistanceTo(new Point(5, 0)));
             Assert.AreEqual(5, new Point(0, 0).ManhattanDistanceTo(new Point(0, -5)));
             Assert.AreEqual(5, new Point(0, 0).ManhattanDistanceTo(new Point(-5, 0)));
             Assert.AreEqual(4, new Point(1, 1).ManhattanDistanceTo(new Point(-1, -1)));
             Assert.AreEqual(4, new Point(1, 2).ManhattanDistanceTo(new Point(3, 4)));
             Assert.AreEqual(4, new Point(2, 2).ManhattanDistanceTo(new Point(0, 0)));
             Assert.AreEqual(16, new Point(-3, 5).ManhattanDistanceTo(new Point(5, -3)));
        }

        [Test]
        public void Part1()
        {
            var parsed = Day06.Parse(TestInput);
            var output = Day06.Part1(parsed);
            Assert.AreEqual(17, output);
        }

        [Test]
        public void Part2()
        {
            var parsed = Day06.Parse(TestInput);
            var output = Day06.Part2(parsed);
            Assert.AreEqual(16, output);

        }
    }
}
