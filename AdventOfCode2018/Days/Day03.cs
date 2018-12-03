﻿using System;
using System.Collections.Generic;
using AdventOfCode2018.Days;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    public class Day03 : AdventProblem<Claim[], int>
    {
        protected override string InputFilePath => "Inputs/Day03.txt";

        protected override Claim[] ParseInputFile()
        {
            var lines = File.ReadAllLines(InputFilePath);
            var claims = lines.Select(Claim.Parse).ToArray();
            return claims;
        }

        public override int Part1(Claim[] claims)
        {
            var bounds = Bounds.GetBounds(claims);
            var field = new int[bounds.xMax+1, bounds.yMax+1];

            foreach (var claim in claims)
            {
                for (int x = claim.x; x <= claim.right; x++)
                {
                    for (int y = claim.y; y <= claim.bottom; y++)
                    {
                        field[x, y]++;
                    }
                }
            }

            var overlappingCellCount = 0;
            for (int x = 0; x <= bounds.xMax; x++)
            {
                for (int y = 0; y <= bounds.yMax; y++)
                {
                    if (field[x, y] > 1)
                        overlappingCellCount++;
                }
            }
            return overlappingCellCount;
        }

        public override int Part2(Claim[] parsed)
        {
            throw new System.NotImplementedException();
        }
    }


    public struct Bounds
    {
        public int xMin;
        public int xMax;
        public int yMin;
        public int yMax;

        public static Bounds GetBounds(Claim[] claims)
        {
            return new Bounds
            {
                xMin = claims.Aggregate(int.MaxValue, (min, claim) => claim.x < min ? claim.x : min),
                xMax = claims.Aggregate(int.MinValue, (max, claim) => claim.right > max ? claim.right : max),
                yMin = claims.Aggregate(int.MaxValue, (min, claim) => claim.y < min ? claim.y : min),
                yMax = claims.Aggregate(int.MinValue, (max, claim) => claim.bottom > max ? claim.bottom : max),
            };
        }
    }

    public struct Claim
    {
        public int id;
        public int x;
        public int y;
        public int width;
        public int height;
        public int right => x + width - 1;
        public int bottom => y + height - 1;

        public Claim(int id, int x, int y, int width, int height)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public static Claim Parse(string str)
        {
            var digitStrs = Regex.Split(str, @"\D+").Where(split => split != string.Empty);
            var digits = digitStrs.Select(int.Parse).ToArray();
            if (digits.Length != 5)
                throw new ArgumentException($"Error parsing this line: {str}");

            return new Claim()
            {
                id = digits[0],
                x = digits[1],
                y = digits[2],
                height = digits[3],
                width = digits[4],
            };
        }

    }

}