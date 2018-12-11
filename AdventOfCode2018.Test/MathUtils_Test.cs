using AdventOfCode2018.Utility;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2018.Test
{
	public class MathUtils_Test
	{
		[TestCase(0, 0)]
		[TestCase(100, 1)]
		[TestCase(420, 4)]
		[TestCase(9001, 0)]
		[TestCase(8675309, 3)]
		public void GetHundredsDigit(int input, int expected)
		{
			var output = MathUtils.GetHundredsDigit(input);
			Assert.AreEqual(expected, output);
		}
	}
}
