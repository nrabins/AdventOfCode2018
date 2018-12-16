using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2018.Utility;

namespace AdventOfCode2018.Days
{
    public class Day13 : AdventProblem2<string[], MinecartState, Tuple<int, int>>
    {
        protected override string InputFilePath => "Inputs/Day13.txt";

        protected override MinecartState ParseInputFile()
        {
            return Parse(File.ReadAllLines(InputFilePath));
        }

        public override MinecartState Parse(string[] lines)
        {
            var width = lines.Max(line => line.Length);
            var height = lines.Length;

            var tracks = new char[width, height];
            var carts = new Dictionary<Tuple<int, int>, Cart>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var c = lines[y][x];
                    var cParsed = c;

                    switch (c)
                    {
                        case '<':
                            carts.Add(Tuple.Create(x, y), new Cart(Direction.Left));
                            cParsed = '-';
                            break;
                        case '>':
                            carts.Add(Tuple.Create(x, y), new Cart(Direction.Right));
                            cParsed = '-';
                            break;
                        case '^':
                            carts.Add(Tuple.Create(x, y), new Cart(Direction.Up));
                            cParsed = '|';
                            break;
                        case 'v':
                            carts.Add(Tuple.Create(x, y), new Cart(Direction.Down));
                            cParsed = '|';
                            break;
                    }

                    tracks[x, y] = cParsed;
                }
            }

            return new MinecartState
            {
                Tracks = tracks,
                Carts = carts
            };
        }

        public override Tuple<int, int> Part1(MinecartState state)
        {
            var iterations = 10000;
            while (iterations-- > 0)
            {
                // Get list of carts sorted from top left to bottom right
                var cartAndPositions = state.Carts.ToList();
                cartAndPositions.Sort((o1, o2) =>
                {
                    // if on same row (y), compare x
                    if (o1.Key.Item2 == o2.Key.Item2)
                        return o1.Key.Item1 - o2.Key.Item1;

                    // otherwise compare y
                    return o1.Key.Item2 - o2.Key.Item2;
                });

                foreach (var cartAndPosition in cartAndPositions)
                {
                    var cart = cartAndPosition.Value;
                    var pos = cartAndPosition.Key;

                    var x = pos.Item1;
                    var y = pos.Item2;

                    // Find the next square
                    var xNext = x;
                    var yNext = y;

                    switch (cart.CurrentDirection)
                    {
                        case Direction.Up:
                            yNext--;
                            break;
                        case Direction.Right:
                            xNext++;
                            break;
                        case Direction.Down:
                            yNext++;
                            break;
                        case Direction.Left:
                            xNext--;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    var posNext = Tuple.Create(xNext, yNext);
                    if (state.Carts.ContainsKey(posNext))
                    {
                        // collision!
                        return posNext;
                    }
                    else
                    {
                        // remove the cart's old position, add the new position
                        state.Carts.Remove(pos);
                        state.Carts.Add(posNext, cart);
                    }

                    var cNext = state.Tracks[xNext, yNext];
                    switch (cNext)
                    {
                        case '-':
                        case '|':
                            // continue going current direction
                            break;
                        case '/':
                            switch (cart.CurrentDirection)
                            {
                                case Direction.Up:
                                    cart.CurrentDirection = Direction.Right;
                                    break;
                                case Direction.Right:
                                    cart.CurrentDirection = Direction.Up;
                                    break;
                                case Direction.Down:
                                    cart.CurrentDirection = Direction.Left;
                                    break;
                                case Direction.Left:
                                    cart.CurrentDirection = Direction.Down;
                                    break;
                            }

                            break;
                        case '\\':
                            switch (cart.CurrentDirection)
                            {
                                case Direction.Up:
                                    cart.CurrentDirection = Direction.Left;
                                    break;
                                case Direction.Right:
                                    cart.CurrentDirection = Direction.Down;
                                    break;
                                case Direction.Down:
                                    cart.CurrentDirection = Direction.Right;
                                    break;
                                case Direction.Left:
                                    cart.CurrentDirection = Direction.Up;
                                    break;
                            }

                            break;
                        case '+':
                            switch (cart.NextTurn)
                            {
                                case Turn.Left:
                                    switch (cart.CurrentDirection)
                                    {
                                        case Direction.Up:
                                            cart.CurrentDirection = Direction.Left;
                                            break;
                                        case Direction.Right:
                                            cart.CurrentDirection = Direction.Up;
                                            break;
                                        case Direction.Down:
                                            cart.CurrentDirection = Direction.Right;
                                            break;
                                        case Direction.Left:
                                            cart.CurrentDirection = Direction.Down;
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }

                                    break;
                                case Turn.Straight:
                                    // do nothing!
                                    break;
                                case Turn.Right:
                                    switch (cart.CurrentDirection)
                                    {
                                        case Direction.Up:
                                            cart.CurrentDirection = Direction.Right;
                                            break;
                                        case Direction.Right:
                                            cart.CurrentDirection = Direction.Down;
                                            break;
                                        case Direction.Down:
                                            cart.CurrentDirection = Direction.Left;
                                            break;
                                        case Direction.Left:
                                            cart.CurrentDirection = Direction.Up;
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }

                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            cart.NextTurn = cart.NextTurn.Next();

                            break;
                        default:
                            Debug.Assert(false, $"Cart went off the track at {xNext},{yNext}");
                            break;
                    }
                }
            }

            throw new Exception("Exceeded current max threshold without collision");
        }

        public override Tuple<int, int> Part2(MinecartState state)
        {
            var iterations = 1000000;
            var lastTick = false;
            while (iterations-- > 0 && !lastTick)
            {
                // Get list of carts sorted from top left to bottom right
                var cartAndPositions = state.Carts.ToList();
                cartAndPositions.Sort((o1, o2) =>
                {
                    // if on same row (y), compare x
                    if (o1.Key.Item2 == o2.Key.Item2)
                        return o1.Key.Item1 - o2.Key.Item1;

                    // otherwise compare y
                    return o1.Key.Item2 - o2.Key.Item2;
                });

                var collided = new List<Tuple<int, int>>();

                foreach (var cartAndPosition in cartAndPositions)
                {
                    var cart = cartAndPosition.Value;
                    var pos = cartAndPosition.Key;

                    if (collided.Contains(pos))
                        continue;

                    var x = pos.Item1;
                    var y = pos.Item2;

                    // Find the next square
                    var xNext = x;
                    var yNext = y;

                    switch (cart.CurrentDirection)
                    {
                        case Direction.Up:
                            yNext--;
                            break;
                        case Direction.Right:
                            xNext++;
                            break;
                        case Direction.Down:
                            yNext++;
                            break;
                        case Direction.Left:
                            xNext--;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    var posNext = Tuple.Create(xNext, yNext);
                    // only collide if the other cart isn't already collided
                    if (state.Carts.ContainsKey(posNext) && !collided.Contains(posNext))
                    {
                        // add to collided
                        collided.Add(pos);
                        collided.Add(posNext);
                    }
                    else
                    {
                        // remove the cart's old position, add the new position
                        state.Carts.Remove(pos);
                        state.Carts.Add(posNext, cart);
                    }

                    var cNext = state.Tracks[xNext, yNext];
                    switch (cNext)
                    {
                        case '-':
                        case '|':
                            // continue going current direction
                            break;
                        case '/':
                            switch (cart.CurrentDirection)
                            {
                                case Direction.Up:
                                    cart.CurrentDirection = Direction.Right;
                                    break;
                                case Direction.Right:
                                    cart.CurrentDirection = Direction.Up;
                                    break;
                                case Direction.Down:
                                    cart.CurrentDirection = Direction.Left;
                                    break;
                                case Direction.Left:
                                    cart.CurrentDirection = Direction.Down;
                                    break;
                            }

                            break;
                        case '\\':
                            switch (cart.CurrentDirection)
                            {
                                case Direction.Up:
                                    cart.CurrentDirection = Direction.Left;
                                    break;
                                case Direction.Right:
                                    cart.CurrentDirection = Direction.Down;
                                    break;
                                case Direction.Down:
                                    cart.CurrentDirection = Direction.Right;
                                    break;
                                case Direction.Left:
                                    cart.CurrentDirection = Direction.Up;
                                    break;
                            }

                            break;
                        case '+':
                            switch (cart.NextTurn)
                            {
                                case Turn.Left:
                                    switch (cart.CurrentDirection)
                                    {
                                        case Direction.Up:
                                            cart.CurrentDirection = Direction.Left;
                                            break;
                                        case Direction.Right:
                                            cart.CurrentDirection = Direction.Up;
                                            break;
                                        case Direction.Down:
                                            cart.CurrentDirection = Direction.Right;
                                            break;
                                        case Direction.Left:
                                            cart.CurrentDirection = Direction.Down;
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }

                                    break;
                                case Turn.Straight:
                                    // do nothing!
                                    break;
                                case Turn.Right:
                                    switch (cart.CurrentDirection)
                                    {
                                        case Direction.Up:
                                            cart.CurrentDirection = Direction.Right;
                                            break;
                                        case Direction.Right:
                                            cart.CurrentDirection = Direction.Down;
                                            break;
                                        case Direction.Down:
                                            cart.CurrentDirection = Direction.Left;
                                            break;
                                        case Direction.Left:
                                            cart.CurrentDirection = Direction.Up;
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }

                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            cart.NextTurn = cart.NextTurn.Next();

                            break;
                        default:
                            Debug.Assert(false, $"Cart went off the track at {xNext},{yNext}");
                            break;
                    }
                }

                if (collided.Any())
                {
                    foreach (var cart in collided)
                    {
                        state.Carts.Remove(cart);
                    }

                    if (state.Carts.Count == 1)
                        lastTick = true;
                }
            }

            return state.Carts.First().Key;
        }

    }

    public class MinecartState : IEquatable<MinecartState>
    {
        public char[,] Tracks { get; set; }

        public Dictionary<Tuple<int, int>, Cart> Carts { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as MinecartState);
        }

        public bool Equals(MinecartState other)
        {
            return other != null &&
                   Tracks.Rank == other.Tracks.Rank &&
                   Enumerable.Range(0, Tracks.Rank).All(dimension =>
                                                            Tracks.GetLength(dimension) ==
                                                            other.Tracks.GetLength(dimension)) &&
                   Tracks.Cast<char>().SequenceEqual(other.Tracks.Cast<char>()) &&
                   Carts.ContentEquals(other.Carts);
        }

        public override int GetHashCode()
        {
            var hashCode = 1938324569;
            hashCode = hashCode * -1521134295 + EqualityComparer<char[,]>.Default.GetHashCode(Tracks);
            hashCode = hashCode * -1521134295 +
                       EqualityComparer<Dictionary<Tuple<int, int>, Cart>>.Default.GetHashCode(Carts);
            return hashCode;
        }
    }

    public class Cart : IEquatable<Cart>
    {
        public Direction CurrentDirection { get; set; }

        public Turn NextTurn { get; set; }

        public Cart(Direction currentDirection)
        {
            CurrentDirection = currentDirection;
            NextTurn = Turn.Left;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Cart);
        }

        public bool Equals(Cart other)
        {
            return other != null &&
                   CurrentDirection == other.CurrentDirection &&
                   NextTurn == other.NextTurn;
        }

        public override int GetHashCode()
        {
            var hashCode = -493353346;
            hashCode = hashCode * -1521134295 + CurrentDirection.GetHashCode();
            hashCode = hashCode * -1521134295 + NextTurn.GetHashCode();
            return hashCode;
        }
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
    }

    public enum Turn
    {
        Left,
        Straight,
        Right,
    }
}
