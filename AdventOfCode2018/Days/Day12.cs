using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventOfCode2018.Utility;

namespace AdventOfCode2018.Days
{
    public class Day12 : AdventProblem2<string[], Day12Config, long>
    {
        protected override string InputFilePath => "Inputs/Day12.txt";

        protected override Day12Config ParseInputFile()
        {
            return Parse(File.ReadAllLines(InputFilePath));
        }

        public override Day12Config Parse(string[] input)
        {
            return Day12Config.Parse(input);
        }

        public override long Part1(Day12Config config)
        {
            var generationCount = 20;
            for (var generation = 0; generation < generationCount; generation++)
            {
                config.Advance();
            }

            return config.Plants.Keys.Sum();
        }

        public override long Part2(Day12Config config)
        {
            // 300 generations is enough to plop this in a spreadsheet and see that (as suspected)
            // the delta between generations converges (in this case, to 55).
            // A bit of math later, and we get our result.

            var generationCount = 300;
            Console.WriteLine($"0, {config.TotalPlantValue()}");

            for (var generation = 0L; generation < generationCount; generation++)
            {
                config.Advance();
                Console.WriteLine($"{generation + 1}, {config.TotalPlantValue()}");
            }

            return config.TotalPlantValue();
        }
    }

    public class Day12Config : IEquatable<Day12Config>
    {
        public Dictionary<long, bool> Plants { get; set; }
        public Dictionary<Tuple<bool, bool, bool, bool, bool>, bool> Rules { get; set; }

        public Day12Config()
        {
            Plants = new Dictionary<long, bool>();
            Rules = new Dictionary<Tuple<bool, bool, bool, bool, bool>, bool>();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Day12Config);
        }

        public bool Equals(Day12Config other)
        {
            return other != null &&
                   Plants.ContentEquals(other.Plants) &&
                   Rules.ContentEquals(other.Rules);
        }

        public override int GetHashCode()
        {
            var hashCode = 1500223435;
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<long, bool>>.Default.GetHashCode(Plants);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Tuple<bool, bool, bool, bool, bool>, bool>>.Default.GetHashCode(Rules);
            return hashCode;
        }

        private static Regex HeaderRegex = new Regex(@"^initial state: ((?:#|\.)+)");
        private static Regex RuleRegex = new Regex(@"^((?:#|\.){5}) => (#|\.)");

        public static Day12Config Parse(string[] input)
        {
            var config = new Day12Config();

            var headerStr = input[0];
            var plantStr = HeaderRegex.Match(headerStr).Groups[1].Value;

            for (var i = 0; i < plantStr.Length; i++)
            {
                if (plantStr[i] == '#')
                    config.Plants[i] = true;
            }

            var rulesStrs = input.Skip(2);
            foreach (var rulesStr in rulesStrs)
            {
                var groups = RuleRegex.Match(rulesStr).Groups;
                var plantInputStr = groups[1].Value;
                var plantOutputStr = groups[2].Value;

                if (plantOutputStr == "#")
                {
                    var rulesBools = plantInputStr.ToCharArray().Select(c => c == '#').ToArray();
                    var key = Tuple.Create(rulesBools[0], rulesBools[1], rulesBools[2], rulesBools[3], rulesBools[4]);
                    config.Rules[key] = true;
                }
            }

            return config;
        }

        public void Advance()
        {
            // we check two away from the left- and right-most plants since the affect plant is the center of a group of 5
            var min = Plants.Keys.Min() - 2;
            var max = Plants.Keys.Max() + 2;

            // we use a new dictionary because altering values while we iterate through would be incorrect
            var newPlants = new Dictionary<long, bool>();
            for (var i = min; i <= max; i++)
            {
                var inputs = Tuple.Create(Plants.ContainsKey(i - 2),
                                          Plants.ContainsKey(i - 1),
                                          Plants.ContainsKey(i),
                                          Plants.ContainsKey(i + 1),
                                          Plants.ContainsKey(i + 2)
                                         );
                if (Rules.ContainsKey(inputs))
                {
                    newPlants[i] = true;
                }
            }

            Plants = newPlants;
        }

        public long TotalPlantValue()
        {
            return Plants.Keys.Sum();
        }
    }
}
