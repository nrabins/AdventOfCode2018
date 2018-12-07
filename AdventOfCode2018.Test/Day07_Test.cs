using AdventOfCode2018.Days;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2018.Test
{
	public class Day07_Test
	{
		public Day07 Day07 { get; set; }

	    [SetUp]
	    public void Setup()
	    {
	        Day07 = new Day07();
	        Day07.Testing = true;
	    }

        private string[] TestInputRaw => new[]
	    {
	        "Step C must be finished before step A can begin.",
	        "Step C must be finished before step F can begin.",
	        "Step A must be finished before step B can begin.",
	        "Step A must be finished before step D can begin.",
	        "Step B must be finished before step E can begin.",
	        "Step D must be finished before step E can begin.",
	        "Step F must be finished before step E can begin.",
	    };

	    private Dictionary<char, Node> TestInputNodes => new Dictionary<char, Node>
	    {
	        {'A', new Node('A', new HashSet<char>(new List<char>{'C'}), new HashSet<char>(new List<char> {'B', 'D'}), 1)},
	        {'B', new Node('B', new HashSet<char>(new List<char>{'A'}), new HashSet<char>(new List<char> {'E'}), 2)},
	        {'C', new Node('C', new HashSet<char>(new List<char>{}), new HashSet<char>(new List<char> {'A', 'F'}), 3)},
	        {'D', new Node('D', new HashSet<char>(new List<char>{'A'}), new HashSet<char>(new List<char> {'E'}), 4)},
	        {'E', new Node('E', new HashSet<char>(new List<char>{'B', 'D', 'F'}), new HashSet<char>(new List<char> {}), 5)},
	        {'F', new Node('F', new HashSet<char>(new List<char>{'C'}), new HashSet<char>(new List<char> {'E'}), 6)},
	    };

	    [Test]
	    public void Parse()
	    {
	        var parsed = Day07.Parse(TestInputRaw);

	        bool areEqual = false;
	        if (parsed.Count == TestInputNodes.Count) // Require equal count.
	        {
	            areEqual = true;
	            foreach (var pair in parsed)
	            {
	                Node value;
	                if (TestInputNodes.TryGetValue(pair.Key, out value))
	                {
	                    // Require value be equal.
	                    if (!value.Equals(pair.Value))
	                    {
	                        areEqual = false;
	                        break;
	                    }
	                }
	                else
	                {
	                    // Require key be present.
	                    areEqual = false;
	                    break;
	                }
	            }
	        }
            Assert.IsTrue(areEqual);

            //Assert.AreEqual(TestInputNodes.OrderBy(pair => pair.Key), parsed.OrderBy(pair => pair.Key));
        }

	    [Test]
	    public void Part1()
	    {
	        var output = Day07.Part1(TestInputNodes);
            Assert.AreEqual("CABDFE", output);
        }

	    [Test]
	    public void Part2()
	    {
	        var output = Day07.Part2(TestInputNodes);
            Assert.AreEqual(15, output);
	    }
	}
}
