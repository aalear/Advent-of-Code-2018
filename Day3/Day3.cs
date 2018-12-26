using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    public class Day3A : IPuzzle
    {
        public void Execute()
        {
            var fabric = Day3Helpers.BuildFabricMap();

            var overlappingArea = fabric.Cast<int>().Count(cell => cell == 2);

            Console.WriteLine(overlappingArea);
        }
    }

    public class Day3B : IPuzzle
    {
        public void Execute()
        {
            var claims = Day3Helpers.ParseClaims();
            var fabric = Day3Helpers.BuildFabricMap(claims);

            // Walk the fabric, checking for any overlaps per claim
            foreach(var claim in claims)
            {
                var isIndependent = true;
                for(var i = claim.X; i < claim.X + claim.Width; i++) 
                {
                    for(var j = claim.Y; j < claim.Y + claim.Height; j++) 
                    {
                        if(fabric[i, j] == 2) 
                        {
                            isIndependent = false;
                            break;
                        }
                    }
                }

                if(isIndependent)
                {
                    Console.WriteLine(claim.Id);
                    break;
                }
            }
        }
    }

    public class Day3Helpers
    {
        private static Regex ClaimParser = new Regex(@"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<width>\d+)x(?<height>\d+)", RegexOptions.Compiled);

        public static int[,] BuildFabricMap(List<Claim> claims = null)
        {
            claims = claims ?? ParseClaims();

            var fabric = new int[1000, 1000];

            foreach(var claim in claims)
            {
                // Mark up the area taken up by this claim
                for(var i = claim.X; i < claim.X + claim.Width; i++)
                {
                    for(var j = claim.Y; j < claim.Y + claim.Height; j++)
                    {
                        if(fabric[i, j] == 0)
                        {
                            // free space! let's claim it.
                            fabric[i, j] = 1;
                        }
                        else 
                        {
                            // uh oh, someone's already here. let's mark it as overlapping.
                            fabric[i, j] = 2;
                        }
                    }
                }
            }

            return fabric;
        }

        public static List<Claim> ParseClaims() 
        {
            var claims = new List<Claim>();
            using(var file = File.OpenText("day3/input.txt")) 
            {
                string claim = null;
                while((claim = file.ReadLine()) != null) 
                {
                    var parsedClaim = ClaimParser.Match(claim);
                    claims.Add(new Claim
                            {
                                Id = int.Parse(parsedClaim.Groups["id"].Value),
                                X = int.Parse(parsedClaim.Groups["x"].Value),
                                Y = int.Parse(parsedClaim.Groups["y"].Value),
                                Width = int.Parse(parsedClaim.Groups["width"].Value),
                                Height = int.Parse(parsedClaim.Groups["height"].Value) 
                            });
                }
            }

            return claims;
        }

        public struct Claim
        {
            public int Id;
            public int X;
            public int Y;
            public int Width;
            public int Height;
        }
    }
}