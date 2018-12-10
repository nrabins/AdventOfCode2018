using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day10_Test
    {
        public Day10 Day10 { get; set; }

        [SetUp]
        public void Setup()
        {
            Day10 = new Day10();
        }

        [TestCase("position=< 9,  1> velocity=< 0,  2>",  9,  1,  0,  2)] 
        [TestCase("position=< 7,  0> velocity=<-1,  0>",  7,  0, -1,  0)] 
        [TestCase("position=< 3, -2> velocity=<-1,  1>",  3, -2, -1,  1)] 
        [TestCase("position=< 6, 10> velocity=<-2, -1>",  6, 10, -2, -1)] 
        [TestCase("position=< 2, -4> velocity=< 2,  2>",  2, -4,  2,  2)] 
        [TestCase("position=<-6, 10> velocity=< 2, -2>", -6, 10,  2, -2)] 
        [TestCase("position=< 1,  8> velocity=< 1, -1>",  1,  8,  1, -1)] 
        [TestCase("position=< 1,  7> velocity=< 1,  0>",  1,  7,  1,  0)] 
        [TestCase("position=<-3, 11> velocity=< 1, -2>", -3, 11,  1, -2)] 
        [TestCase("position=< 7,  6> velocity=<-1, -1>",  7,  6, -1, -1)] 
        [TestCase("position=<-2,  3> velocity=< 1,  0>", -2,  3,  1,  0)] 
        [TestCase("position=<-4,  3> velocity=< 2,  0>", -4,  3,  2,  0)] 
        [TestCase("position=<10, -3> velocity=<-1,  1>", 10, -3, -1,  1)] 
        [TestCase("position=< 5, 11> velocity=< 1, -2>",  5, 11,  1, -2)] 
        [TestCase("position=< 4,  7> velocity=< 0, -1>",  4,  7,  0, -1)] 
        [TestCase("position=< 8, -2> velocity=< 0,  1>",  8, -2,  0,  1)] 
        [TestCase("position=<15,  0> velocity=<-2,  0>", 15,  0, -2,  0)] 
        [TestCase("position=< 1,  6> velocity=< 1,  0>",  1,  6,  1,  0)] 
        [TestCase("position=< 8,  9> velocity=< 0, -1>",  8,  9,  0, -1)] 
        [TestCase("position=< 3,  3> velocity=<-1,  1>",  3,  3, -1,  1)] 
        [TestCase("position=< 0,  5> velocity=< 0, -1>",  0,  5,  0, -1)] 
        [TestCase("position=<-2,  2> velocity=< 2,  0>", -2,  2,  2,  0)] 
        [TestCase("position=< 5, -2> velocity=< 1,  2>",  5, -2,  1,  2)] 
        [TestCase("position=< 1,  4> velocity=< 2,  1>",  1,  4,  2,  1)] 
        [TestCase("position=<-2,  7> velocity=< 2, -2>", -2,  7,  2, -2)] 
        [TestCase("position=< 3,  6> velocity=<-1, -1>",  3,  6, -1, -1)] 
        [TestCase("position=< 5,  0> velocity=< 1,  0>",  5,  0,  1,  0)] 
        [TestCase("position=<-6,  0> velocity=< 2,  0>", -6,  0,  2,  0)] 
        [TestCase("position=< 5,  9> velocity=< 1, -2>",  5,  9,  1, -2)] 
        [TestCase("position=<14,  7> velocity=<-2,  0>", 14,  7, -2,  0)] 
        [TestCase("position=<-3,  6> velocity=< 2, -1>", -3,  6,  2, -1)] 

        public void Parse(string str, int expectedPosX, int expectedPosY, int expectedVelX, int expectedVelY)
        {
            var parsed = MovingPoint.Parse(str);
            Assert.AreEqual(expectedPosX, parsed.Position.Item1);
            Assert.AreEqual(expectedPosY, parsed.Position.Item2);
            Assert.AreEqual(expectedVelX, parsed.Velocity.Item1);
            Assert.AreEqual(expectedVelY, parsed.Velocity.Item2);
        }
    }
}
