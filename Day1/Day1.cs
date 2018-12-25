using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    public class Day1A : IPuzzle
    {
        public void Execute() 
        {
            var frequency = 0;

            using(var file = File.OpenText("day1/input.txt")) 
            {
                string input = null;
                while((input = file.ReadLine()) != null) 
                {
                    var adjustment = int.Parse(input);

                    frequency += adjustment;
                }
            }

            Console.WriteLine(frequency);
        }
    }

    public class Day1B : IPuzzle
    {
        private int frequency = 0;
        private HashSet<int> seenFrequencies = new HashSet<int> { 0 };
        private bool duplicateFound = false;

        public void Execute() 
        {
            using(var file = File.OpenText("day1/input.txt")) 
            {
                while(duplicateFound != true) 
                {
                    file.DiscardBufferedData();
                    file.BaseStream.Seek(0, SeekOrigin.Begin);
                    DoSearch(file);
                }
            }
        }

        private void DoSearch(StreamReader file) 
        {
            string input = null;

            while((input = file.ReadLine()) != null)
            {
                var adjustment = int.Parse(input);

                frequency += adjustment;

                if(seenFrequencies.Contains(frequency)) {
                    Console.WriteLine(frequency);
                    duplicateFound = true;
                    break;
                }
                else 
                {
                    seenFrequencies.Add(frequency);
                }
            }
        }
    }
}