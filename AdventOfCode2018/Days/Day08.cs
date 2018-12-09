using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
    public class Day08 : AdventProblem2<string, int[], int>
    {
        protected override string InputFilePath => "Inputs/Day08.txt";

        protected override int[] ParseInputFile()
        {
            return Parse(File.ReadAllText(InputFilePath));
        }

        public override int[] Parse(string input)
        {
            return input.Split(' ').Select(int.Parse).ToArray();
        }

        public override int Part1(int[] parsed)
        {
            var remaining = new List<int>(parsed);
            var topNode = GetNode(remaining);

            var metaDataSum = SumMetadata(topNode);
            return metaDataSum;
        }

        public override int Part2(int[] parsed)
        {
            var remaining = new List<int>(parsed);
            var topNode = GetNode(remaining);

            var valueSum = SumValues(topNode);
            return valueSum;
        }

        private int SumMetadata(Day08Node node)
        {
            return node.Metadata.Sum() + node.Children.Sum(SumMetadata);
        }

        private int SumValues(Day08Node node)
        {
            if (node.Children.Any())
            {
                return node.Metadata.Sum(childIndex =>
                {
                    if (childIndex > 0 && childIndex <= node.Children.Count)
                        return SumValues(node.Children[childIndex - 1]);
                    return 0;
                });
            }

            return node.Metadata.Sum();
        }

        public static Day08Node GetNode(List<int> remaining)
        {
            // Approach -- consume left to right, recursing on children
            
            // 1. Consume header
            Debug.Assert(remaining.Count >= 2);
            var node = new Day08Node(remaining[0], remaining[1]);
            remaining.RemoveRange(0, 2);

            // 2. Consume children (recursion here)
            for (int childIndex = 0; childIndex < node.NumChildren; childIndex++)
            {
                var childNode = GetNode(remaining);
                node.Children.Add(childNode);
            }

            // 3. Consume metadata
            node.Metadata = remaining.Take(node.NumMetadata).ToList();
            remaining.RemoveRange(0, node.NumMetadata);

            return node;
        }



    }

    public class Day08Node
    {
        public int NumChildren { get; set; }
        public int NumMetadata { get; set; }
        public List<Day08Node> Children { get; set; }
        public List<int> Metadata { get; set; }

        public Day08Node(int numChildren, int numMetadata, List<Day08Node> children, List<int> metadata)
        {
            NumChildren = numChildren;
            NumMetadata = numMetadata;
            Children = children;
            Metadata = metadata;
        }

        public Day08Node(int numChildren, int numMetadata) : this(numChildren, numMetadata, new List<Day08Node>(), new List<int>())
        {
        }
    }
}
