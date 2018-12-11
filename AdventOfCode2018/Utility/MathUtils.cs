using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Utility
{
	public static class MathUtils
	{
		public static int GetHundredsDigit(int num)
		{
			if (num < 100)
				return 0;

			return Math.Abs(num / 100 % 10);
		}
	}
}
