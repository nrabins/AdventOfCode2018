using AdventOfCode2018.Days;
using NUnit.Framework;
using System.IO;

namespace AdventOfCode2018.Test
{
    public class Day00Test
    {
        public Day00 Day { get; set; }

        [SetUp]
        public void Setup()
        {
            Day = new Day00();
        }

        [Test]
        public void Part1_1()
        {
            Assert.AreEqual(3, Day.Part1("1122"));
        }

        [Test]
        public void Part1_2()
        {
            Assert.AreEqual(4, Day.Part1("1111"));
        }

        [Test]
        public void Part1_3()
        {
            Assert.AreEqual(0, Day.Part1("1234"));
        }

        [Test]
        public void Part1_4()
        {
            Assert.AreEqual(9, Day.Part1("91212129"));
        }

    }
}