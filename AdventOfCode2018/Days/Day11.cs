using AdventOfCode2018.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
    public class Day11 : AdventProblem2<string, int, Tuple<int, int>, Tuple<int, int, int>>
    {
        protected override string InputFilePath => "Inputs/Day11.txt";

        protected override int ParseInputFile()
        {
            return Parse(File.ReadAllText(InputFilePath));
        }

        public override int Parse(string input)
        {
            return int.Parse(input);
        }

        public override Tuple<int, int> Part1(int serial)
        {
            var powerLevels = GetPowerLevels(serial);

            // calculate the 3x3 square with the highest power level sum
            var maxSum = int.MinValue;
            Tuple<int, int> maxCoords = null;
            for (var x = 0; x < powerLevels.GetLength(0) - 2; x++)
            {
                for (var y = 0; y < powerLevels.GetLength(1) - 2; y++)
                {
                    var sum = 0;
                    for (var dx = 0; dx < 3; dx++)
                    {
                        for (var dy = 0; dy < 3; dy++)
                        {
                            sum += powerLevels[x + dx, y + dy];
                        }
                    }
                    if (sum > maxSum)
                    {
                        maxSum = sum;
                        maxCoords = Tuple.Create(x + 1, y + 1);
                    }
                }
            }
            return maxCoords;
        }

        public override Tuple<int, int, int> Part2(int serial)
        {
            var powerLevels = GetPowerLevels(serial);

            // calculate the 3x3 square with the highest power level sum
            var maxSum = int.MinValue;
            Tuple<int, int, int> maxCoords = null;

            for (var size = 1; size <= powerLevels.GetLength(0); size++)
            {
                for (var x = 0; x < powerLevels.GetLength(0) + 1 - size; x++)
                {
                    for (var y = 0; y < powerLevels.GetLength(1) + 1 - size; y++)
                    {
                        var sum = 0;
                        for (var dx = 0; dx < size; dx++)
                        {
                            for (var dy = 0; dy < size; dy++)
                            {
                                sum += powerLevels[x + dx, y + dy];
                            }
                        }
                        if (sum > maxSum)
                        {
                            maxSum = sum;
                            maxCoords = Tuple.Create(x + 1, y + 1, size);
                        }
                    }
                }
                Console.WriteLine($"  Size {size}: max sum = {maxSum} at {maxCoords}");
            }
            
            return maxCoords;
        }



        private int[,] GetPowerLevels(int serial)
        {
            var powerLevels = new int[300, 300];

            // fill out power level array
            for (int x = 0; x < powerLevels.GetLength(0); x++)
            {
                for (int y = 0; y < powerLevels.GetLength(1); y++)
                {
                    powerLevels[x, y] = GetPowerLevelAt(serial, x + 1, y + 1);
                }
            }

            return powerLevels;
        }

        public int GetPowerLevelAt(int serial, int x, int y)
        {
            var rackId = x + 10;
            var powerLevel = rackId * y;
            powerLevel += serial;
            powerLevel *= rackId;
            powerLevel = MathUtils.GetHundredsDigit(powerLevel);
            powerLevel -= 5;
            return powerLevel;
        }
    }
}
