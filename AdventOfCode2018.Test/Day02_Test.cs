using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day02_Test
    {
        public Day02 Day { get; set; }

        [SetUp]
        public void Setup()
        {
            Day = new Day02();
        }

        [Test]
        public void Part1_1()
        {
            var input = new[]
            {
                "abcdef",
                "bababc",
                "abbcde",
                "abcccd",
                "aabcdd",
                "abcdee",
                "ababab",
            };
            Assert.AreEqual(12, Day.Part1(input));
        }

        [Test]
        public void CountChars_1()
        {
            var counts = Day.CountChars("abcdef");
            Assert.AreEqual(1, counts['a']);
            Assert.AreEqual(1, counts['b']);
            Assert.AreEqual(1, counts['c']);
            Assert.AreEqual(1, counts['d']);
            Assert.AreEqual(1, counts['e']);
            Assert.AreEqual(1, counts['f']);
        }

        [Test]
        public void CountChars_2()
        {
            var counts = Day.CountChars("bababc");
            Assert.AreEqual(2, counts['a']);
            Assert.AreEqual(3, counts['b']);
            Assert.AreEqual(1, counts['c']);
        }

        [Test]
        public void Part2_1()
        {
            var input = new[]
            {
                "abcde",
                "fghij",
                "klmno",
                "pqrst",
                "fguij",
                "axcye",
                "wvxyz",
            };
            Assert.AreEqual("fgij", Day.Part2(input));
        }

        [Test]
        public void CountCommonCharacters_1()
        {
            Assert.AreEqual(0, Day.CountCommonCharacters("abcde", "fghij"));
        }

        [Test]
        public void CountCommonCharacters_2()
        {
            Assert.AreEqual(5, Day.CountCommonCharacters("abcde", "abcde"));
        }

        [Test]
        public void CountCommonCharacters_3()
        {
            Assert.AreEqual(1, Day.CountCommonCharacters("abcde", "edcba"));
        }

        [Test]
        public void FindCommonCharacters_1()
        {
            Assert.AreEqual("abc", Day.FindCommonCharacters("abcde", "abced"));
        }

        [Test]
        public void FindCommonCharacters_2()
        {
            Assert.AreEqual("ace", Day.FindCommonCharacters("abcde", "axcxe"));
        }
    }
}
