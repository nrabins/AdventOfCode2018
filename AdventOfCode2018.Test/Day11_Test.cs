using AdventOfCode2018.Days;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2018.Test
{
    public class Day11_Test
    {
        public Day11 Day11 { get; set; }

        [SetUp]
        public void Setup()
        {
            Day11 = new Day11();
        }

        [Test]
        public void Part1()
        {
            var output = Day11.Part1(42);
            Assert.AreEqual(Tuple.Create(21, 61), output);
        }

        public class Part2TestCase
        {
            public Tuple<int,int,int> Expected { get; set; }
            public int Serial { get; set; }

            public Part2TestCase(Tuple<int, int, int> expected, int serial)
            {
                Expected = expected;
                Serial = serial;
            }
        }

        public static IEnumerable<Part2TestCase> Part2TestCases
        {
            get
            {
                yield return new Part2TestCase(Tuple.Create(90, 269, 16), 18);
                yield return new Part2TestCase(Tuple.Create(232, 251, 12), 42);
            }
        }

        [TestCaseSource(nameof(Part2TestCases))]
        [Ignore("These tests take 12 minutes to run because I wrote tolerable-but-unoptimized code")]
        public void Part2(Part2TestCase testCase)
        {
            var output = Day11.Part2(testCase.Serial);
            Assert.AreEqual(testCase.Expected, output);
        }

        [TestCase(4, 8, 3, 5)]
        [TestCase(-5, 57, 122, 79)]
        [TestCase(0, 39, 217, 196)]
        [TestCase(4, 71, 101, 153)]
        public void GetPowerLevelAt(int expected, int serial, int x, int y)
        {
            var output = Day11.GetPowerLevelAt(serial, x, y);
            Assert.AreEqual(expected, output);
        }
    }
}
