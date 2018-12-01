using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
	public class Day2016_01 : AdventProblem
	{
		public void Run()
		{
			var input = File.ReadAllText("Inputs/Day2016-01.txt");
			var part1output = Part1(input);
			Console.WriteLine("Part 1: " + part1output);

			var part2output = Part2(input);
			Console.WriteLine("Part 2: " + part2output);
		}

		public int Part1(string input) 
		{
			var x = 0;
			var y = 0;
			var direction = 0;

			var instructions = ParseInstructions(input);
			foreach (var instruction in instructions)
			{
				if (instruction[0] == 'R')
					direction = direction + 1;
				else
					direction = direction - 1;
				direction += 4;
				direction %= 4;

				var distance = int.Parse(instruction.Substring(1));

				switch (direction)
				{
					case 0: // north
						y += distance;
						break;
					case 1: // east
						x += distance;
						break;
					case 2:
						y -= distance;
						break;
					case 3:
						x -= distance;
						break;
				}
			}

			return Math.Abs(x) + Math.Abs(y);
		}

		public int Part2(string input)
		{
			var x = 0;
			var y = 0;
			var direction = 0;

			var visited = new List<Tuple<int, int>>
			{
				Tuple.Create(0, 0)
			};

			var instructions = ParseInstructions(input);
			foreach (var instruction in instructions)
			{
				if (instruction[0] == 'R')
					direction = direction + 1;
				else
					direction = direction - 1;
				direction += 4;
				direction %= 4;

				var distance = int.Parse(instruction.Substring(1));

				while (distance > 0)
				{
					switch (direction)
					{
						case 0: // north
							y++;
							break;
						case 1: // east
							x++;
							break;
						case 2:
							y--;
							break;
						case 3:
							x--;
							break;
					}

					var location = Tuple.Create(x, y);
					if (visited.Contains(location))
						return Math.Abs(x) + Math.Abs(y);

					visited.Add(location);

					distance--;
				}
			}

			throw new Exception("Didn't visit any location twice");
		}

		private string[] ParseInstructions(string input)
		{
			return input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
