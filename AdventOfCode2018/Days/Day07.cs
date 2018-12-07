using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
	public class Day07 : AdventProblem<Node, string>
	{
		protected override string InputFilePath => "Inputs/Day07.txt";

		public override string Part1(Node parsed)
		{
			throw new NotImplementedException();
		}

		public override string Part2(Node parsed)
		{
			throw new NotImplementedException();
		}

		protected override Node ParseInputFile()
		{
			throw new NotImplementedException();
		}
	}

	public class Node {
		public Node[] Parents { get; set; }
		public Node[] Children { get; set; }
	}
}
