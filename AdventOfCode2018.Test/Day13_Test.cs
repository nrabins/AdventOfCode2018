using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day13_Test
    {
        public Day13 Day13 { get; set; }

        [SetUp]
        public void Setup()
        {
            Day13 = new Day13();;
        }

        private string[] TestInputRaw => new[]
        {
            @"/->-\        ",
            @"|   |  /----\",
            @"| /-+--+-\  |",
            @"| | |  | v  |",
            @"\-+-/  \-+--/",
            @"  \------/   ",
        };

        private readonly MinecartState _testInputParsed = new MinecartState
        {
            Tracks = new[,]
            {
                {'/', '|', '|', '|', '\\', ' '},
                {'-', ' ', ' ', ' ', '-', ' '},
                {'-', ' ', '/', '|', '+', '\\'},
                {'-', ' ', '-', ' ', '-', '-'},
                {'\\', '|', '+', '|', '/', '-'},
                {' ', ' ', '-', ' ', ' ', '-'},
                {' ', ' ', '-', ' ', ' ', '-'},
                {' ', '/', '+', '|', '\\', '-'},
                {' ', '-', '-', ' ', '-', '-'},
                {' ', '-', '\\', '|', '+', '/'},
                {' ', '-', ' ', ' ', '-', ' '},
                {' ', '-', ' ', ' ', '-', ' '},
                {' ', '\\', '|', '|', '/', ' '},
            },
            Carts = new Dictionary<Tuple<int, int>, Cart>
            {
                {
                    Tuple.Create(2, 0),
                    new Cart(Direction.Right)
                },
                {
                    Tuple.Create(9, 3),
                    new Cart(Direction.Down)
                }
            }
        };

        [Test]
        public void Parse()
        {
            var parsed = Day13.Parse(TestInputRaw);
            Assert.AreEqual(_testInputParsed, parsed);
        }

        [Test]
        public void Part1()
        {
            var output = Day13.Part1(_testInputParsed);
            var expected = Tuple.Create(7, 3);
            Assert.AreEqual(expected, output);
        }

        private string[] Part2TestInput => new[]
        {
            @"/>-<\  ",
            @"|   |  ",
            @"| /<+-\",
            @"| | | v",
            @"\>+</ |",
            @"  |   ^",
            @"  \<->/",
        };

        [Test]
        public void Part2()
        {
            var parsed = Day13.Parse(Part2TestInput);
            var output = Day13.Part2(parsed);
            var expected = Tuple.Create(6, 4);
            Assert.AreEqual(expected, output);
        }
    }
}
