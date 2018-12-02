using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
	public class Day01 : AdventProblem<string, int>
	{
	    protected override string InputFilePath => "Inputs/Day01.txt";

	    protected override string ParseInputFile()
	    {
	        var inputRaw = File.ReadAllLines(InputFilePath);
	        return string.Join(", ", inputRaw);
	    }

		public override int Part1(string input)
		{
			var instructions = input.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

			var sum = 0;
			foreach (var instruction in instructions)
			{
				var delta = int.Parse(instruction.Substring(1));
				if (instruction[0] == '+')
					sum += delta;
				else
					sum -= delta;
			}
			return sum;
		}

		public override int Part2(string input)
		{
			var instructions = input.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

			var sum = 0;
			var visited = new List<int> { sum };
			
			var loopsRemaining = 10000;
			while (loopsRemaining > 0)
			{
				foreach (var instruction in instructions)
				{
					var delta = int.Parse(instruction.Substring(1));
					if (instruction[0] == '+')
						sum += delta;
					else
						sum -= delta;

					if (visited.Contains(sum))
						return sum;

					visited.Add(sum);
				}
				loopsRemaining--;
			}

			throw new Exception("10000 loops through input were not enough.");
		}
	}
}
