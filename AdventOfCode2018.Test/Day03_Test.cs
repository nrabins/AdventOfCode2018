using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2018.Test
{
    public class Day03_Test
    {
        public Day03 Day { get; set; }

        [SetUp]
        public void Setup()
        {
            Day = new Day03();
        }

        public class ParseClaimTestCase
        {
            public string Input { get; set; }
            public Claim Expected { get; set; }

            public ParseClaimTestCase(string input, Claim expected)
            {
                Input = input;
                Expected = expected;
            }
        }

        public static IEnumerable<ParseClaimTestCase> ParseClaimTestCases
        {
            get
            {
                yield return new ParseClaimTestCase("#1 @ 1,3: 4x4", new Claim(1, 1, 3, 4, 4));
                yield return new ParseClaimTestCase("#2 @ 3,1: 4x4", new Claim(2, 3, 1, 4, 4));
                yield return new ParseClaimTestCase("#3 @ 5,5: 2x2", new Claim(3, 5, 5, 2, 2));
                yield return new ParseClaimTestCase("#4 @ 0,0: 1x2", new Claim(4, 0, 0, 1, 2));
            }
        }

        [TestCaseSource(nameof(ParseClaimTestCases))]
        public void Claim_Parse(ParseClaimTestCase testCase)
        {
            var parsedClaim = Claim.Parse(testCase.Input);
            Assert.AreEqual(testCase.Expected, parsedClaim);
        }

        [Test]
        public void Claim_Right()
        {
            var claim = new Claim(1, 1, 2, 3, 4);
            Assert.AreEqual(3, claim.right);
        }

        [Test]
        public void Claim_Bottom()
        {
            var claim = new Claim(1, 1, 2, 3, 4);
            Assert.AreEqual(5, claim.bottom);
        }

        [Test]
        public void Bounds_GetBounds()
        {
            var claims = new[]
            {
                new Claim(1, 1, 3, 4, 4),
                new Claim(2, 3, 1, 4, 4),
                new Claim(3, 5, 5, 2, 2),
            };
            var bounds = Bounds.GetBounds(claims);
            var expected = new Bounds
            {
                xMin = 1,
                xMax = 6,
                yMin = 1,
                yMax = 6,
            };
            Assert.AreEqual(expected.xMin, bounds.xMin);
            Assert.AreEqual(expected.xMax, bounds.xMax);
            Assert.AreEqual(expected.yMin, bounds.yMin);
            Assert.AreEqual(expected.yMax, bounds.yMax);
        }

        [Test]
        public void GetField()
        {
            var claims = new[]
            {
                new Claim(1, 1, 3, 4, 4),
                new Claim(2, 3, 1, 4, 4),
                new Claim(3, 5, 5, 2, 2),
            };
            var bounds = new Bounds
            {
                xMin = 1,
                xMax = 6,
                yMin = 1,
                yMax = 6,
            };
            var expectedField = new[,]
            {
                {0, 0, 0, 0, 0, 0, 0,},
                {0, 0, 0, 1, 1, 1, 1,},
                {0, 0, 0, 1, 1, 1, 1,},
                {0, 1, 1, 2, 2, 1, 1,},
                {0, 1, 1, 2, 2, 1, 1,},
                {0, 1, 1, 1, 1, 1, 1,},
                {0, 1, 1, 1, 1, 1, 1,},
            };
            var actualField = Day.GetField(bounds, claims);
            Assert.AreEqual(expectedField, actualField);
        }

        [Test]
        public void Part1_Example()
        {
            var claims = new[]
            {
                new Claim(1, 1, 3, 4, 4),
                new Claim(2, 3, 1, 4, 4),
                new Claim(3, 5, 5, 2, 2),
            };
            Assert.AreEqual(4, Day.Part1(claims));
        }

        [Test]
        public void Part1_No_Overlap()
        {
            var claims = new[]
            {
                new Claim(1, 0, 0, 1, 1),
                new Claim(2, 1, 0, 1, 1),
            };
            Assert.AreEqual(0, Day.Part1(claims));
        }

        [Test]
        public void Part1_Single_Cell_Twice()
        {
            var claims = new[]
            {
                new Claim(1, 0, 0, 1, 1),
                new Claim(2, 0, 0, 1, 1),
            };
            Assert.AreEqual(1, Day.Part1(claims));
        }

        public class ClaimSharingTest
        {
            public int[,] Field { get; set; }
            public Claim Claim { get; set; }
            public bool Expected { get; set; }

            public ClaimSharingTest(int[,] field, Claim claim, bool expected)
            {
                Field = field;
                Claim = claim;
                Expected = expected;
            }
        }

        public static IEnumerable<ClaimSharingTest> ClaimSharingTests
        {
            get
            {
                yield return new ClaimSharingTest(new[,] { { 1, 1, 1 } }, new Claim(1, 0, 0, 1, 1), false);
                yield return new ClaimSharingTest(new[,] { { 2, 1, 1 } }, new Claim(1, 0, 0, 1, 1), true);
            }
        }

        [TestCaseSource(nameof(ClaimSharingTests))]
        public void IsClaimSharing(ClaimSharingTest testCase)
        {
            var result = Day.IsClaimSharing(testCase.Field, testCase.Claim);
            Assert.AreEqual(testCase.Expected, result);
        }

        [Test]
        public void Part2()
        {
            var claims = new[]
            {
                new Claim(1, 1, 3, 4, 4),
                new Claim(2, 3, 1, 4, 4),
                new Claim(3, 5, 5, 2, 2),
            };
            Assert.AreEqual(3, Day.Part2(claims));
        }


}
}
