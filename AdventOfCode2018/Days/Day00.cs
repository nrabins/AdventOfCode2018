using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
    public class Day00 : AdventProblem
    {
        public void Run()
        {
            var input = File.ReadAllText("Inputs/Day00.txt");
            var part1output = Part1(input);
            var part2output = Part2(input);
        }

        public int Part1(string input)
        {
            var sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var curr = (int)char.GetNumericValue(input[i]);
                var next = (int)char.GetNumericValue(input[(i + 1) % input.Length]);
                if (curr == next)
                    sum += curr;
            }

            return sum;
        }

        public int Part2(string input)
        {
            throw new NotImplementedException();
        }

    }
}
