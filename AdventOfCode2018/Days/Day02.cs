using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
    public class Day02 : AdventProblem<string[], int, string>
    {
        protected override string InputFilePath => "Inputs/Day02.txt";

        protected override string[] ParseInputFile()
        {
            return File.ReadAllLines(InputFilePath);
        }

        public override int Part1(string[] lines)
        {
            var count2 = 0;
            var count3 = 0;
            foreach (var line in lines)
            {
                var counts = CountChars(line);
                if (counts.ContainsValue(2))
                    count2++;
                if (counts.ContainsValue(3))
                    count3++;
            }

            return count2 * count3;
        }

        public Dictionary<char, int> CountChars(string line)
        {
            return line.ToCharArray().Aggregate(new Dictionary<char, int>(),
                                                (counts, c) =>
                                                {
                                                    if (!counts.ContainsKey(c))
                                                        counts[c] = 0;
                                                    counts[c]++;
                                                    return counts;
                                                });
        }

        public override string Part2(string[] parsed)
        {
            for (int i = 0; i < parsed.Length - 1; i++)
            {
                for (int j = i+1; j < parsed.Length; j++)
                {
                    if (i == j)
                        continue;
                    var charDiff = CountCommonCharacters(parsed[i], parsed[j]);
                    if (charDiff == parsed[i].Length - 1)
                        return FindCommonCharacters(parsed[i], parsed[j]);
                }
            }

            return "";
        }

        public int CountCommonCharacters(string a, string b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException($"{a} and {b} are different lengths");

            var commonCount = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                    commonCount++;
            }

            return commonCount;
        }

        public string FindCommonCharacters(string a, string b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException($"{a} and {b} are different lengths");

            var commonChars = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                    commonChars += a[i];
            }

            return commonChars;
        }
    }
}
