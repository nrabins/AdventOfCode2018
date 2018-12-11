using AdventOfCode2018.Days;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2018.Test
{
	public class Day11_Test
	{
		public Day11 Day11 { get; set; }

		[SetUp]
		public void Setup()
		{
			Day11 = new Day11();
		}

		[Test]
		public void Part1()
		{
			var output = Day11.Part1(42);
			Assert.AreEqual(Tuple.Create(21, 61), output);
		}

		[TestCase(4, 8, 3, 5)]
		[TestCase(-5, 57, 122, 79)]
		[TestCase(0, 39, 217, 196)]
		[TestCase(4, 71, 101, 153)]
		public void GetPowerAt(int expected, int serial, int x, int y)
		{
			var output = Day11.GetPowerAt(serial, x, y);
			Assert.AreEqual(expected, output);
		}
	}
}
