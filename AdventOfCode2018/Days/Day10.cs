using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdventOfCode2018.Days
{
    public class Day10 : AdventProblem2<string[], List<MovingPoint>, int>
    {
        protected override string InputFilePath => "Inputs/Day10.txt";

        protected override List<MovingPoint> ParseInputFile()
        {
            return Parse(File.ReadAllLines(InputFilePath));
        }

        public override List<MovingPoint> Parse(string[] input)
        {
            return input.Select(MovingPoint.Parse).ToList();
        }

        public override int Part1(List<MovingPoint> points)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Day10Form(points);
            form.StartSimulation();
           
            return 0;
        }
        
        public override int Part2(List<MovingPoint> parsed)
        {
            throw new NotImplementedException();
        }
    }

    public class Day10Form : Form
    {
        public List<MovingPoint> Points { get; set; }


        public Day10Form(List<MovingPoint> points)
        {
            Points = points;
            
            Size = new Size(1200, 800);
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void StartSimulation()
        {
            var pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            var graphics = CreateGraphics();


            // I guessed and checked starting with the 10000 increment (noticing the input seemed to be multiples of 10000 away from a condensed region).
            // I know, I know, not graceful. :(
            var steps = 10117; 
            var iteratedPoints = Points.Select(p => Tuple.Create(p.Position.Item1 + p.Velocity.Item1 * steps,
                                                                 p.Position.Item2 + p.Velocity.Item2 * steps)).ToList();

            // find max bounds (used for debugging)
            var xMin = iteratedPoints.Min(p => p.Item1);
            var xMax = iteratedPoints.Max(p => p.Item1);
            var yMin = iteratedPoints.Min(p => p.Item2);
            var yMax = iteratedPoints.Max(p => p.Item2);

            Show();
            foreach (var iteratedPoint in iteratedPoints)
            {
                // draw current position
                graphics.DrawRectangle(pen, iteratedPoint.Item1 + 400, iteratedPoint.Item2 + 400, 1, 1);
            }


            while (true)
            {
                // I know this is hacky and there must be a better way to do it :\
            }
            
        }
    }

    public struct MovingPoint : IEquatable<MovingPoint>
    {
        public Tuple<int, int> Position { get; set; }
        public Tuple<int, int> Velocity { get; set; }

        // position=< 10273,  10227> velocity=<-1, -1>
        private static readonly Regex Pattern = new Regex(@"-?\d+");
        public static MovingPoint Parse(string rawStr)
        {
            var matches = Pattern.Matches(rawStr);
            var posX = int.Parse(matches[0].Value);
            var posY = int.Parse(matches[1].Value);
            var velX = int.Parse(matches[2].Value);
            var velY = int.Parse(matches[3].Value);

            return new MovingPoint
            {
                Position = Tuple.Create(posX, posY),
                Velocity = Tuple.Create(velX, velY),
            };
        }

        public override bool Equals(object obj)
        {
            return obj is MovingPoint && Equals((MovingPoint)obj);
        }

        public bool Equals(MovingPoint other)
        {
            return EqualityComparer<Tuple<int, int>>.Default.Equals(Position, other.Position) &&
                   EqualityComparer<Tuple<int, int>>.Default.Equals(Velocity, other.Velocity);
        }

        public override int GetHashCode()
        {
            var hashCode = -1595683060;
            hashCode = hashCode * -1521134295 + EqualityComparer<Tuple<int, int>>.Default.GetHashCode(Position);
            hashCode = hashCode * -1521134295 + EqualityComparer<Tuple<int, int>>.Default.GetHashCode(Velocity);
            return hashCode;
        }
    }
}
