using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2018.Days
{
    public abstract class AdventProblem2<TRawInput, TPart1Input, TPart1Output, TPart2Input, TPart2Output>
    {
        protected abstract string InputFilePath { get; }
        
		protected abstract TPart1Input Part1ParseInputFile();
        protected abstract TPart2Input Part2ParseInputFile();

        public abstract TPart1Input Part1Parse(TRawInput rawInput);
        public abstract TPart2Input Part2Parse(TRawInput rawInput);

        public abstract TPart1Output Part1(TPart1Input input);
        public abstract TPart2Output Part2(TPart2Input input);

        public void Run()
        {
            Console.WriteLine("Running program...");
            Console.WriteLine("");

            var sw = new Stopwatch();

            Console.WriteLine("Parsing input file...");
            sw.Start();
            var part1Input = Part1ParseInputFile();
            sw.Stop();
            Console.WriteLine($"    Parsed in {sw.ElapsedMilliseconds} ms");

            try
            {
                Console.WriteLine("Running part 1...");
                sw.Restart();
                var part1Output = Part1(part1Input);
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
            sw.Restart();
            var part2Input = Part2ParseInputFile();
            sw.Stop();
            Console.WriteLine($"    Parsed in {sw.ElapsedMilliseconds} ms");

            try
            {
                Console.WriteLine("Running part 2...");
                sw.Restart();
                var part2Output = Part2(part2Input);
                sw.Stop();
                Console.WriteLine($"    Finished in {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"    Result: {part2Output}");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("    Part 2 not yet implemented");
            }
        }
    }

    /// <summary>
    /// Helper subclass for when Part1 and Part2 have the same input type
    /// </summary>
    public abstract class AdventProblem2<TRawInput, TPartInput, TPart1Output, TPart2Output>
        : AdventProblem2<TRawInput, TPartInput, TPart1Output, TPartInput, TPart2Output>
    {
        protected override TPartInput Part1ParseInputFile()
        {
            return ParseInputFile();
        }

        protected override TPartInput Part2ParseInputFile()
        {
            return ParseInputFile();
        }

        protected abstract TPartInput ParseInputFile();

        public override TPartInput Part1Parse(TRawInput rawInput)
        {
            return Parse(rawInput);
        }

        public override TPartInput Part2Parse(TRawInput rawInput)
        {
            return Parse(rawInput);
        }

        public abstract TPartInput Parse(TRawInput input);
    }

    /// <summary>
    /// Helper subclass for then Part1 and Part2 have the same input and output types
    /// </summary>
    public abstract class AdventProblem2<TRawInput, TInput, TOutput> 
        : AdventProblem2<TRawInput, TInput, TOutput, TOutput>
    {
    }
}