using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day05_Test
    {
        public Day05 Day05 { get; set; }

        [SetUp]
        public void Setup()
        {
            Day05 = new Day05();
        }

        [TestCase(0, "aA")]
        [TestCase(0, "abBA")]
        [TestCase(4, "abAB")]
        [TestCase(6, "aabAAB")]
        [TestCase(10, "dabAcCaCBAcCcaDA")]
        public void Part1(int expected, string input)
        {
            var output = Day05.Part1(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Part2()
        {
            var input = "dabAcCaCBAcCcaDA";
            var output = Day05.Part2(input);
            var expected = 4;
            Assert.AreEqual(expected, output);
        }
    }
}
