using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
	public class Day01 : AdventProblem
	{
		public void Run()
		{
			var inputRaw = File.ReadAllLines("Inputs/Day01.txt");
			var commaDelimitedInput = string.Join(", ", inputRaw);
			
			var output1 = Part1(commaDelimitedInput);
			Console.WriteLine("Part 1: " + output1);

			var output2 = Part2(commaDelimitedInput);
			Console.WriteLine("Part 2: " + output2);
		}

		public int Part1(string input)
		{
			var instructions = input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

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

		public int Part2(string input)
		{
			var instructions = input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

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
