using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2018.Days
{
    public abstract class AdventProblem<TInput, TPart1Output, TPart2Output>
    {
        protected abstract string InputFilePath { get; }

        protected abstract TInput ParseInputFile();
        

        public void Run()
        {
            Console.WriteLine("Running program...");

            var sw = new Stopwatch();

            Console.WriteLine("Parsing input file...");
            sw.Start();
            var input = ParseInputFile();
            sw.Stop();
            Console.WriteLine($"Input file parsed in {sw.ElapsedMilliseconds} ms");

            Console.WriteLine("Running part 1...");
            sw.Restart();
            var part1Output = Part1(input);
            sw.Stop();
            Console.WriteLine($"Part 1 finished in {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"    Result: {part1Output}");

            Console.WriteLine("Running part 2...");
            sw.Restart();
            var part2Output = Part2(input);
            sw.Stop();
            Console.WriteLine($"Part 2 finished in {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"    Result: {part2Output}");
        }

        public abstract TPart1Output Part1(TInput parsed);
        public abstract TPart2Output Part2(TInput parsed);
    }

    public abstract class AdventProblem<TInput, TOutput> : AdventProblem<TInput, TOutput, TOutput>
    {
    }
}