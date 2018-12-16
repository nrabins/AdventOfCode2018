using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2018.Days
{
    public abstract class AdventProblem2<TRawInput, TPartInput, TPart1Output, TPart2Output>
    {
        protected abstract string InputFilePath { get; }
        
		protected abstract TPartInput ParseInputFile();
        public abstract TPartInput Parse(TRawInput input);
        

        public void Run()
        {
            Console.WriteLine("Running program...");
            Console.WriteLine("");

            var sw = new Stopwatch();

            Console.WriteLine("Parsing input file...");
            sw.Start();
            var input = ParseInputFile();
            sw.Stop();
            Console.WriteLine($"    Parsed in {sw.ElapsedMilliseconds} ms");

            try
            {
                Console.WriteLine("Running part 1...");
                sw.Restart();
                var part1Output = Part1(input);
                sw.Stop();
                Console.WriteLine($"    Finished in {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"    Result: {part1Output}");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("    Part 1 not yet implemented");
            }

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Parsing input file...");
            sw.Start();
            input = ParseInputFile();
            sw.Stop();
            Console.WriteLine($"    Parsed in {sw.ElapsedMilliseconds} ms");

            try
            {
                Console.WriteLine("Running part 2...");
                sw.Restart();
                var part2Output = Part2(input);
                sw.Stop();
                Console.WriteLine($"    Finished in {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"    Result: {part2Output}");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("    Part 2 not yet implemented");
            }
        }

        public abstract TPart1Output Part1(TPartInput parsed);
        public abstract TPart2Output Part2(TPartInput parsed);
    }

    public abstract class AdventProblem2<TRawInput, TPartInput, TOutput> : AdventProblem2<TRawInput, TPartInput, TOutput, TOutput>
    {
    }
}