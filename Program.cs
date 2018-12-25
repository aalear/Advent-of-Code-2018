using System;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Advent of Code Puzzles!");
            Console.WriteLine();

            Console.WriteLine("Enter puzzle selection by typing in the day and A for the first star, B for the second.");
            Console.WriteLine("e.g. for the first star of day 3, type in 3A.");

            var choice = Console.ReadLine();

            var puzzle = GetPuzzleImplementation(choice);
            puzzle.Execute();
        }

        static IPuzzle GetPuzzleImplementation(string id) 
        {
            const string Prefix = "Day";

            var assembly = Assembly.GetExecutingAssembly();

            var type = assembly.GetTypes().First(t => t.Name == Prefix + id.ToUpperInvariant());

            return Activator.CreateInstance(type) as IPuzzle;
        }
    }
}
