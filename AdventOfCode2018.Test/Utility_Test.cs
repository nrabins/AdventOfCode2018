using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2018.Utility;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace AdventOfCode2018.Test
{
    public class Utility_Test
    {

        [TestCase(0, new[] {1, 2, 3, 4, 5}, new[] {1, 2, 3, 4, 5})]
        [TestCase(1, new[] {1, 2, 3, 4, 5}, new[] {5, 1, 2, 3, 4})]
        [TestCase(-1, new[] {1, 2, 3, 4, 5}, new[] {2, 3, 4, 5, 1})]
        [TestCase(100, new[] {1, 2, 3, 4, 5}, new[] {1, 2, 3, 4, 5})]
        public void Rotate(int rotateAmount, int[] input, int[] expected)
        {
            var list = new LinkedList<int>(input);
            list.Rotate(rotateAmount);
            Assert.AreEqual(expected, list.ToArray());
        }
    }
}
