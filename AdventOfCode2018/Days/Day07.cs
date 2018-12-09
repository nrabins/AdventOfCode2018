using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2018.Days
{
    public class Day07 : AdventProblem2<string[], Dictionary<char, Day07Node>, string, int>
    {
        protected override string InputFilePath => "Inputs/Day07.txt";
        protected override Dictionary<char, Day07Node> ParseInputFile()
        {
            return Parse(File.ReadAllLines(InputFilePath));
        }

        public bool Testing { get; set; }

        public override Dictionary<char, Day07Node> Parse(string[] input)
        {
            var dict = new Dictionary<char, Day07Node>();
            var reg = new Regex(@"^Step (\D) must be finished before step (\D) can begin\.");

            foreach (var line in input)
            {
                var parent = reg.Match(line).Groups[1].Value[0];
                var child = reg.Match(line).Groups[2].Value[0];

                if (!dict.ContainsKey(parent))
                    dict[parent] = new Day07Node(parent, Testing);
                if (!dict.ContainsKey(child))
                    dict[child] = new Day07Node(child, Testing);

                dict[parent].Children.Add(child);
                dict[child].Parents.Add(parent);
            }

            return dict;
        }

        public override string Part1(Dictionary<char, Day07Node> parsed)
        {
            var map = parsed.ToDictionary(pair => pair.Key,
                                        pair => (Day07Node)pair.Value.Clone());

            var start = map.First(pair => pair.Value.Parents.Count == 0);

            start.Value.State = NodeState.Complete;

            var visited = new List<Day07Node> {start.Value};
            
            var loopsLeft = 10000;
            while (loopsLeft > 0 && map.Any(pair => pair.Value.State != NodeState.Complete))
            {
                loopsLeft--;

                // find all nodes with parents that are all complete
                var ready = map.Where(pair => pair.Value.State != NodeState.Complete &&
                                              pair.Value.Parents.All(c => map[c].State ==
                                                                          NodeState.Complete));

                // complete the node that alphabetically comes first
                if (ready.Any())
                {
                    var next = ready.OrderBy(pair => pair.Key).First();
                    next.Value.State = NodeState.Complete;
                    visited.Add(next.Value);
                }
            }

            return string.Join("", visited.Select(node => node.Name));
        }

        public override int Part2(Dictionary<char, Day07Node> parsed)
        {
            var numWorkers = Testing ? 2 : 4;

            var workers = Enumerable.Range(0, numWorkers).Select(i => new Worker()).ToList();


            var map = parsed.ToDictionary(pair => pair.Key,
                                          pair => (Day07Node)pair.Value.Clone());


            var tick = 0;
            while (tick < 100000 && map.Any(pair => pair.Value.State != NodeState.Complete))
            {
                // Free up workers / complete tasks
                foreach (var worker in workers.Where(worker => worker.StepInProgress.HasValue))
                {
                    if (worker.FinishTick == tick)
                    {
                        map[worker.StepInProgress.Value].State = NodeState.Complete;
                        worker.StepInProgress = null;
                    }
                }
                
                // Find available nodes
                var available = map
                    .Where(pair => (pair.Value.State != NodeState.Complete && pair.Value.State != NodeState.InProgress)
                                   && pair.Value.Parents.All(c => map[c].State == NodeState.Complete))
                    .OrderBy(pair => pair.Key).ToList();


                // Assign free workers to available tasks
                foreach (var worker in workers.Where(worker => worker.StepInProgress == null))
                {
                    if (available.Any())
                    {
                        var task = available[0].Value;
                        task.State = NodeState.InProgress;
                        worker.StepInProgress = task.Name;
                        worker.FinishTick = task.TimeToComplete + tick;
                        available.RemoveAt(0);
                    }
                }

                tick++;
            }

            return tick - 1;
        }
    }

    public class Day07Node : IEquatable<Day07Node>, ICloneable
    {
        public char Name { get; set; }
        public NodeState State { get; set; }
        public HashSet<char> Parents { get; set; }
        public HashSet<char> Children { get; set; }
        public int TimeToComplete { get; set; }

        public Day07Node()
        {
        }

        public Day07Node(char name, bool Testing)
        {
            Name = name;
            State = NodeState.Unready;
            Parents = new HashSet<char>();
            Children = new HashSet<char>();
            TimeToComplete = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(name) + 1 + (Testing ? 0 : 60);
        }

        public Day07Node(char name, HashSet<char> parents, HashSet<char> children, int timeToComplete)
        {
            Name = name;
            State = NodeState.Unready;
            Parents = parents;
            Children = children;
            TimeToComplete = timeToComplete;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Day07Node);
        }

        public bool Equals(Day07Node other)
        {
            return other != null &&
                   Name == other.Name &&
                   State == other.State &&
                   Parents.SetEquals(other.Parents) &&
                   Children.SetEquals(other.Children) &&
                   TimeToComplete == other.TimeToComplete;
        }

        public override int GetHashCode()
        {
            var hashCode = -919211634;
            hashCode = hashCode * -1521134295 + Name.GetHashCode();
            hashCode = hashCode * -1521134295 + State.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<HashSet<char>>.Default.GetHashCode(Parents);
            hashCode = hashCode * -1521134295 + EqualityComparer<HashSet<char>>.Default.GetHashCode(Children);
            hashCode = hashCode * -1521134295 + TimeToComplete.GetHashCode();
            return hashCode;
        }

        public object Clone()
        {
            var clone = new Day07Node();
            clone.Name = Name;
            clone.State = State;
            clone.Parents = new HashSet<char>(Parents);
            clone.Children = new HashSet<char>(Children);
            clone.TimeToComplete = TimeToComplete;
            return clone;
        }
    }

    public enum NodeState
    {
        Unready,
        Ready,
        InProgress,
        Complete,
    }

    public class Worker
    {
        public char? StepInProgress { get; set; }
        public int FinishTick { get; set; }

        public Worker()
        {
            StepInProgress = null;
        }
    }
}
