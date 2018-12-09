using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day08_Test
    {
        public Day08 Day08 { get; set; }

        public string RawInput => "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

        public int[] Input => new[] {2, 3, 0, 3, 10, 11, 12, 1, 1, 0, 1, 99, 2, 1, 1, 2};

        [SetUp]
        public void Setup()
        {
            Day08 = new Day08();
        }

        [Test]
        public void Parse()
        {
            var parsed = Day08.Parse(RawInput);
            Assert.AreEqual(Input, parsed);
        }

        [Test]
        public void Part1()
        {
            var output = Day08.Part1(Input);
            Assert.AreEqual(138, output);
        }

        [Test]
        public void Part2()
        {
            var output = Day08.Part2(Input);
            Assert.AreEqual(66, output);
        }
    }
}
