using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;

namespace AdventOfCode2018.Days
{
    public class Day06 : AdventProblem<Point[], int>
    {
        protected override string InputFilePath => "Inputs/Day06.txt";

        public bool Testing { get; set; }

        protected override Point[] ParseInputFile()
        {
            return Parse(File.ReadAllLines(InputFilePath));
        }

        public Point[] Parse(string[] strs)
        {
            return strs.Select(Point.Parse).ToArray();
        }

        public override int Part1(Point[] points)
        {
            // Approach:
            // Find the bounds of the shape
            // Create a dict from point to distance to each problem point
            // Find the min distance for each point
            // Use those counts to find minimum contiguous area (toss out points who have minimums on the edge -- they will extend forever)

            var minX = points.Min(point => point.x);
            var maxX = points.Max(point => point.x);
            var minY = points.Min(point => point.y);
            var maxY = points.Max(point => point.y);

            var pointDistances = new Dictionary<Point, Dictionary<Point, int>>();
            for (var x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    var cell = new Point(x, y);
                    pointDistances[cell] = new Dictionary<Point, int>(points.Length);
                    foreach (var point in points)
                    {
                        var manhattanDistance = cell.ManhattanDistanceTo(point);
                        pointDistances[cell][point] = manhattanDistance;
                    }
                }
            }

            // for each problem point, a list of (by definition) contiguous closest cells
            var closestCellsToPoint = new Dictionary<Point, List<Point>>(points.Length);

            foreach (var point in points)
            {
                closestCellsToPoint[point] = new List<Point>();
            }

            foreach (var cell in pointDistances)
            {
                var sorted = cell.Value.Select(pair => pair).ToList();
                sorted.Sort((a, b) => a.Value - b.Value);

                // Only add if there is no tie for first
                if (sorted[0].Value != sorted[1].Value)
                {
                    closestCellsToPoint[sorted[0].Key].Add(cell.Key);
                }
            }

            // find contained points by removing all points with cells on the boundary
            var containedPoints = closestCellsToPoint
                .Where(pair => !pair.Value.Any(point => point.x == maxX || point.x == minX ||
                                                        point.y == maxY || point.y == minY))
                .ToList();

            return containedPoints.Max(pair => pair.Value.Count);
        }

        public override int Part2(Point[] points)
        {
            var minX = points.Min(point => point.x);
            var maxX = points.Max(point => point.x);
            var minY = points.Min(point => point.y);
            var maxY = points.Max(point => point.y);

            var pointDistances = new Dictionary<Point, Dictionary<Point, int>>();
            for (var x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    var cell = new Point(x, y);
                    pointDistances[cell] = new Dictionary<Point, int>(points.Length);
                    foreach (var point in points)
                    {
                        var manhattanDistance = cell.ManhattanDistanceTo(point);
                        pointDistances[cell][point] = manhattanDistance;
                    }
                }
            }

            var cumulativeDistances = pointDistances
                .ToDictionary(pair => pair.Key,
                              pair => pair.Value.Sum(distance => distance.Value));

            var belowThreshold = cumulativeDistances
                .Where(pair => pair.Value < (Testing ? 32 : 10000));

            return belowThreshold.Count();
        }
    }
}

public struct Point : IEquatable<Point>
{
    public int x { get; set; }
    public int y { get; set; }

    public Point(int x, int y) : this()
    {
        this.x = x;
        this.y = y;
    }

    private static Regex ParseRegex = new Regex(@"(-?\d+), (-?\d+)");

    public static Point Parse(string str)
    {
        var match = ParseRegex.Match(str);
        return new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
    }

    public override bool Equals(object obj)
    {
        return obj is Point && Equals((Point)obj);
    }

    public bool Equals(Point other)
    {
        return x == other.x &&
               y == other.y;
    }

    public override int GetHashCode()
    {
        var hashCode = 1502939027;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + y.GetHashCode();
        return hashCode;
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }
}

public static class PointExtensionMethods
{
    public static int ManhattanDistanceTo(this Point point, Point other)
    {
        return Math.Abs(other.x - point.x) + Math.Abs(other.y - point.y);
    }
}