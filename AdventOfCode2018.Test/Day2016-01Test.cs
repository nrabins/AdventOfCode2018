using AdventOfCode2018.Days;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;


namespace AdventOfCode2018.Test
{
	public class Day2016_01Test
	{
		public Day2016_01 Day { get; set; }

		[SetUp]
		public void Setup()
		{
			Day = new Day2016_01();
		}

		[Test]
		public void Part1_1()
		{
			Assert.AreEqual(5, Day.Part1("R2, L3"));
		}

		[Test]
		public void Part1_2()
		{
			Assert.AreEqual(2, Day.Part1("R2, R2, R2"));
		}

		[Test]
		public void Part1_3()
		{
			Assert.AreEqual(12, Day.Part1("R5, L5, R5, R3"));
		}

		[Test]
		public void Part2_1()
		{
			Assert.AreEqual(4, Day.Part2("R8, R4, R4, R8"));
		}
	}
}
