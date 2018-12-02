using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
    public class Day00 : AdventProblem<string, int>
    {
        protected override string InputFilePath => "Inputs/Day00.txt";

        protected override string ParseInputFile()
        {
            throw new NotImplementedException();
        }


        public override int Part1(string input)
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

        public override int Part2(string input)
        {
            throw new NotImplementedException();
        }

    }
}
