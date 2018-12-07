using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2018.Days;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            var day = new Day07();
            day.Run();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
