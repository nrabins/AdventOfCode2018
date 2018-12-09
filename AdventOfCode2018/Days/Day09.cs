using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventOfCode2018.Utility;

namespace AdventOfCode2018.Days
{
    public class Day09 : AdventProblem2<string, Tuple<int, int>, long>
    {
        protected override string InputFilePath => "Inputs/Day09.txt";

        protected override Tuple<int, int> ParseInputFile()
        {
            return Parse(File.ReadAllText(InputFilePath));
        }

        public override Tuple<int, int> Parse(string input)
        {
            var regex = new Regex(@"(\d+) players; last marble is worth (\d+) points");
            var matches = regex.Match(input).Groups;
            var numPlayers = int.Parse(matches[1].Value);
            var numMarbles = int.Parse(matches[2].Value);
            return Tuple.Create(numPlayers, numMarbles);
        }

        public override long Part1(Tuple<int, int> parsed)
        {
            var numPlayers = parsed.Item1;
            var numMarbles = parsed.Item2;

            var scores = new long[numPlayers];

            var marbles = new LinkedList<int>(new[] { 0 });


            for (int marble = 1; marble <= numMarbles; marble++)
            {
                if (marble % 23 == 0)
                {
                    var player = marble % numPlayers;
                    scores[player] += marble;
                    marbles.Rotate(7);
                    scores[player] += marbles.First.Value;
                    marbles.RemoveFirst();
                }
                else
                {
                    marbles.Rotate(-2);
                    marbles.AddFirst(marble);
                }
            }

            return scores.Max();
        }

        

        public override long Part2(Tuple<int, int> parsed)
        {
            var newParams = Tuple.Create(parsed.Item1, 100 * parsed.Item2);
            return Part1(newParams);
        }
    }
}
