using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day04_Test
    {
        public Day04 Day { get; set; }

        [SetUp]
        public void Setup()
        {
            Day = new Day04();
        }

        public string[] ProblemInput => new[]
        {
            "[1518 - 11 - 01 00:00] Guard #10 begins shift",
            "[1518 - 11 - 01 00:05] falls asleep",
            "[1518 - 11 - 01 00:25] wakes up",
            "[1518 - 11 - 01 00:30] falls asleep",
            "[1518 - 11 - 01 00:55] wakes up",
            "[1518 - 11 - 01 23:58] Guard #99 begins shift",
            "[1518 - 11 - 02 00:40] falls asleep",
            "[1518 - 11 - 02 00:50] wakes up",
            "[1518 - 11 - 03 00:05] Guard #10 begins shift",
            "[1518 - 11 - 03 00:24] falls asleep",
            "[1518 - 11 - 03 00:29] wakes up",
            "[1518 - 11 - 04 00:02] Guard #99 begins shift",
            "[1518 - 11 - 04 00:36] falls asleep",
            "[1518 - 11 - 04 00:46] wakes up",
            "[1518 - 11 - 05 00:03] Guard #99 begins shift",
            "[1518 - 11 - 05 00:45] falls asleep",
            "[1518 - 11 - 05 00:55] wakes up",
        };

        [Test]
        public void ParseLines()
        {
            // reverse times to test sorting
            var lines = new List<string>(ProblemInput);
            lines.Reverse();

            var expected = new[]
            {
                new GuardEvent(new DateTime(1518, 11, 01, 00, 00, 00), 10, GuardAction.BeginShift),
                new GuardEvent(new DateTime(1518, 11, 01, 00, 05, 00), 10, GuardAction.FallAsleep),
                new GuardEvent(new DateTime(1518, 11, 01, 00, 25, 00), 10, GuardAction.WakeUp),
                new GuardEvent(new DateTime(1518, 11, 01, 00, 30, 00), 10, GuardAction.FallAsleep),
                new GuardEvent(new DateTime(1518, 11, 01, 00, 55, 00), 10, GuardAction.WakeUp),
                new GuardEvent(new DateTime(1518, 11, 01, 23, 58, 00), 99, GuardAction.BeginShift),
                new GuardEvent(new DateTime(1518, 11, 02, 00, 40, 00), 99, GuardAction.FallAsleep),
                new GuardEvent(new DateTime(1518, 11, 02, 00, 50, 00), 99, GuardAction.WakeUp),
                new GuardEvent(new DateTime(1518, 11, 03, 00, 05, 00), 10, GuardAction.BeginShift),
                new GuardEvent(new DateTime(1518, 11, 03, 00, 24, 00), 10, GuardAction.FallAsleep),
                new GuardEvent(new DateTime(1518, 11, 03, 00, 29, 00), 10, GuardAction.WakeUp),
                new GuardEvent(new DateTime(1518, 11, 04, 00, 02, 00), 99, GuardAction.BeginShift),
                new GuardEvent(new DateTime(1518, 11, 04, 00, 36, 00), 99, GuardAction.FallAsleep),
                new GuardEvent(new DateTime(1518, 11, 04, 00, 46, 00), 99, GuardAction.WakeUp),
                new GuardEvent(new DateTime(1518, 11, 05, 00, 03, 00), 99, GuardAction.BeginShift),
                new GuardEvent(new DateTime(1518, 11, 05, 00, 45, 00), 99, GuardAction.FallAsleep),
                new GuardEvent(new DateTime(1518, 11, 05, 00, 55, 00), 99, GuardAction.WakeUp),
            };

            var parsed = Day.ParseLines(lines.ToArray());
            Assert.AreEqual(expected, parsed);
        }

        [Test]
        public void Parse()
        {
            var line = "[1518 - 11 - 01 00:00] Guard #10 begins shift";
            var parsed = GuardEvent.Parse(line);

            var expected = new GuardEvent(new DateTime(1518, 11, 01, 00, 00, 00), 10, GuardAction.BeginShift);
            Assert.AreEqual(expected, parsed);
        }

        [Test]
        public void Part1()
        {
            var input = Day.ParseLines(ProblemInput);
            var output = Day.Part1(input);
            Assert.AreEqual(240, output);
        }

        [Test]
        public void Part2()
        {
            var input = Day.ParseLines(ProblemInput);
            var output = Day.Part2(input);
            Assert.AreEqual(4455, output);
        }

        [Test]
        public void GetMetadata()
        {
            var guardEvents = Day.ParseLines(ProblemInput);
            var expectedMetadata = new Dictionary<int, Dictionary<int, int>>
            {
                {
                    10, new Dictionary<int, int>
                    {
                        {5, 1},
                        {6, 1},
                        {7, 1},
                        {8, 1},
                        {9, 1},
                        {10, 1},
                        {11, 1},
                        {12, 1},
                        {13, 1},
                        {14, 1},
                        {15, 1},
                        {16, 1},
                        {17, 1},
                        {18, 1},
                        {19, 1},
                        {20, 1},
                        {21, 1},
                        {22, 1},
                        {23, 1},
                        {24, 2},
                        {25, 1},
                        {26, 1},
                        {27, 1},
                        {28, 1},
                        {30, 1},
                        {31, 1},
                        {32, 1},
                        {33, 1},
                        {34, 1},
                        {35, 1},
                        {36, 1},
                        {37, 1},
                        {38, 1},
                        {39, 1},
                        {40, 1},
                        {41, 1},
                        {42, 1},
                        {43, 1},
                        {44, 1},
                        {45, 1},
                        {46, 1},
                        {47, 1},
                        {48, 1},
                        {49, 1},
                        {50, 1},
                        {51, 1},
                        {52, 1},
                        {53, 1},
                        {54, 1},
                    }
                    
                },
                {
                    99, new Dictionary<int, int>
                    {
                        { 36, 1},
                        { 37, 1},
                        { 38, 1},
                        { 39, 1},
                        { 40, 2},
                        { 41, 2},
                        { 42, 2},
                        { 43, 2},
                        { 44, 2},
                        { 45, 3},
                        { 46, 2},
                        { 47, 2},
                        { 48, 2},
                        { 49, 2},
                        { 50, 1},
                        { 51, 1},
                        { 52, 1},
                        { 53, 1},
                        { 54, 1},

                    }
                }
            };

            var actualMetadata = Day.GetMetadata(guardEvents);
            Assert.AreEqual(expectedMetadata, actualMetadata);
        }
    }
}
