using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day09_Test
    {
        public Day09 Day09 { get; set; }

        [SetUp]
        public void Setup()
        {
            Day09 = new Day09();
        }

        [Test]
        public void Parse()
        {
            var raw = "448 players; last marble is worth 71628 points";
            var parsed = Day09.Parse(raw);
            var expected = Tuple.Create(448, 71628);
            Assert.AreEqual(expected, parsed);
        }

        [TestCase(9, 25, 32)]
        [TestCase(10, 1618, 8317)]
        [TestCase(13, 7999, 146373)]
        [TestCase(17, 1104, 2764)]
        [TestCase(21, 6111, 54718)]
        [TestCase(30, 5807, 37305)]
        public void Part1(int numPlayers, int numMarbles, int expected)
        {
            var output = Day09.Part1(Tuple.Create(numPlayers, numMarbles));
            Assert.AreEqual(expected, output);
        }
    }
}
