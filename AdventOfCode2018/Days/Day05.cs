using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Days
{
    public class Day05 : AdventProblem<string, int>
    {
        protected override string InputFilePath => "Inputs/Day05.txt";

        protected override string ParseInputFile()
        {
            return File.ReadAllText(InputFilePath);
        }

        public override int Part1(string polymer)
        {
            var reactionOccurred = true;
            while (polymer.Length > 0 && reactionOccurred)
            {
                reactionOccurred = false;
                for (int i = 0; i < polymer.Length-1; i++)
                {
                    var possibleReactants = new [] {polymer[i]+32, polymer[i]-32}; // 32 is the displacement between upper- and lowercase characters on the ASCII table
                    if (polymer[i + 1] == possibleReactants[0] || polymer[i + 1] == possibleReactants[1])
                    {
                        reactionOccurred = true;
                        polymer = polymer.Remove(i, 2);
                        break;
                    }
                }
            }

            return polymer.Length;
        }

        public override int Part2(string polymer)
        {
            var lengthAfterRemovalAndReaction = new Dictionary<char, int>(26);
            var unmodifiedReactionLength = Part1(polymer);

            for (var c = 'A'; c <= 'Z'; c++)
            {
                var withoutCharacter = polymer.Replace(c.ToString(), "").Replace(((char)(c + 32)).ToString(), "");
                if (withoutCharacter.Length == polymer.Length)
                {
                    lengthAfterRemovalAndReaction[c] = unmodifiedReactionLength;
                }
                else
                {
                    var reactionLength = Part1(withoutCharacter);
                    lengthAfterRemovalAndReaction[c] = reactionLength;
                }

            }

            return lengthAfterRemovalAndReaction.Aggregate((l, r) => l.Value < r.Value ? l : r).Value;
        }
    }
}