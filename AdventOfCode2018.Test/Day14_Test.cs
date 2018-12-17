using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2018.Days;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Day14_Test
    {
        public Day14 Day14 { get; set; }

        [SetUp]
        public void Setup()
        {
            Day14 = new Day14();
        }

        [TestCase(9, "5158916779")]
        [TestCase(5, "0124515891")]
        [TestCase(5, "0124515891")]
        [TestCase(18, "9251071085")]
        [TestCase(2018, "5941429882")]
        public void Part1(long input, string expected)
        {
            var output = Day14.Part1(input);
            Assert.AreEqual(expected, output);
        }

        [TestCase("51589", 9)]
        [TestCase("01245", 5)]
        [TestCase("92510", 18)]
        [TestCase("59414", 2018)]
        public void Part2(string input, long expected)
        {
            var output = Day14.Part2(input);
            Assert.AreEqual(expected, output);
        }

        [TestCase(new[] {1, 2, 3}, new[] {1, 2}, false)]
        [TestCase(new[] {1, 2, 3}, new[] {1, 2, 3}, true)]
        [TestCase(new[] {1, 2, 3}, new[] {1, 2, 3, 4}, true)]
        [TestCase(new[] {1, 2, 3}, new[] {1, 2, 3, 4, 5}, true)]
        [TestCase(new[] {1, 2, 3}, new[] {3, 2, 1}, false)]
        [TestCase(new[] {1, 2, 3}, new[] {1, 1, 2, 3}, false)]
        public void StartsWith(int[] desiredDigits, int[] currentRecipe, bool expected)
        {
            var currentRecipeList = new List<int>(currentRecipe);
            var startsWith = Day14.StartsWith(currentRecipeList, desiredDigits);
            Assert.AreEqual(expected, startsWith);
        }

        [TestCase(new[] { 1, 2, 3 }, new[] { 1 }, true)]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 2 }, true)]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, true)]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 4 }, false)]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 4, 5 }, false)]
        [TestCase(new[] { 1, 2, 3 }, new[] { 2 }, false )]
        [TestCase(new[] { 1, 2, 3 }, new[] { 3, 2, 1 }, false)]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 1, 2, 3 }, false)]
        public void CouldMatch(int[] desiredDigits, int[] currentRecipe, bool expected)
        {
            var currentRecipeList = new List<int>(currentRecipe);
            var couldMatch = Day14.CouldMatch(currentRecipeList, desiredDigits);
            Assert.AreEqual(expected, couldMatch);
        }
    }
}
