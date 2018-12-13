using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using AdventOfCode2018.Utility;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day12_Test
    {
        public Day12 Day12 { get; set; }

        [SetUp]
        public void Setup()
        {
            Day12 = new Day12();
        }

        private string[] RawInput => new[]
        {
            "initial state: #..#.#..##......###...###",
            "",
            "...## => #",
            "..#.. => #",
            ".#... => #",
            ".#.#. => #",
            ".#.## => #",
            ".##.. => #",
            ".#### => #",
            "#.#.# => #",
            "#.### => #",
            "##.#. => #",
            "##.## => #",
            "###.. => #",
            "###.# => #",
            "####. => #",
        };

        private Day12Config TestInput => new Day12Config
        {
            Plants = new Dictionary<long, bool>
            {
                {0, true},
                {3, true},
                {5, true},
                {8, true},
                {9, true},
                {16, true},
                {17, true},
                {18, true},
                {22, true},
                {23, true},
                {24, true},
            },
            Rules = new Dictionary<Tuple<bool, bool, bool, bool, bool>, bool>
            {
                {Tuple.Create(false, false, false, true, true), true},
                {Tuple.Create(false, false, true, false, false), true},
                {Tuple.Create(false, true, false, false, false), true},
                {Tuple.Create(false, true, false, true, false), true},
                {Tuple.Create(false, true, false, true, true), true},
                {Tuple.Create(false, true, true, false, false), true},
                {Tuple.Create(false, true, true, true, true), true},
                {Tuple.Create(true, false, true, false, true), true},
                {Tuple.Create(true, false, true, true, true), true},
                {Tuple.Create(true, true, false, true, false), true},
                {Tuple.Create(true, true, false, true, true), true},
                {Tuple.Create(true, true, true, false, false), true},
                {Tuple.Create(true, true, true, false, true), true},
                {Tuple.Create(true, true, true, true, false), true},
            }
        };


        [Test]
        public void Parse()
        {
            var parsed = Day12.Parse(RawInput);
            Assert.AreEqual(TestInput, parsed);
        }

        [Test]
        public void Part1()
        {
            var output = Day12.Part1(TestInput);
            Assert.AreEqual(325, output);
        }

        [Test]
        public void Day12Config_Advance()
        {
            var config = TestInput;
            config.Advance();

            var expected = new Dictionary<long, bool>
            {
                {0, true},
                {4, true},
                {9, true},
                {15, true},
                {18, true},
                {21, true},
                {24, true},
            };

            Assert.True(config.Plants.ContentEquals(expected));
        }
    }
}
