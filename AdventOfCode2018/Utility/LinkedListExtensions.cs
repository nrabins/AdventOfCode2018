using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Utility
{
    public static class LinkedListExtensions
    {
        public static void Rotate<T>(this LinkedList<T> list, int amount)
        {
            // rotate to the right
            if (amount > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    var last = list.Last;
                    list.RemoveLast();
                    list.AddFirst(last);
                }
            }
            else
            {
                for (int i = 0; i > amount; i--)
                {
                    var first = list.First;
                    list.RemoveFirst();
                    list.AddLast(first);
                }
            }
        }
    }
}
