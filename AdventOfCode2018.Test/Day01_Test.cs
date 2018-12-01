using AdventOfCode2018.Days;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2018.Test
{
	public class Day01_Test
	{
		public Day01 Day { get; set; }

		[SetUp]
		public void Setup()
		{
			Day = new Day01();
		}

		[Test]
		public void Part1_1()
		{
			Assert.AreEqual(3, Day.Part1("+1, -2, +3, +1"));
		}

		[Test]
		public void Part1_2()
		{
			Assert.AreEqual(3, Day.Part1("+1, +1, +1"));
		}

		[Test]
		public void Part1_3()
		{
			Assert.AreEqual(0, Day.Part1("+1, +1, -2"));
		}

		[Test]
		public void Part1_4()
		{
			Assert.AreEqual(-6, Day.Part1("-1, -2, -3"));
		}

		[Test]
		public void Part2_1()
		{
			Assert.AreEqual(0, Day.Part2("+1, -1"));
		}

		[Test]
		public void Part2_2()
		{
			Assert.AreEqual(10, Day.Part2("+3, +3, +4, -2, -4"));
		}

		[Test]
		public void Part2_3()
		{
			Assert.AreEqual(5, Day.Part2("-6, +3, +8, +5, -6"));
		}

		[Test]
		public void Part2_4()
		{
			Assert.AreEqual(14, Day.Part2("+7, +7, -2, -7, -4"));
		}
	}
}
