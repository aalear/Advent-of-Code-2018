using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day2A : IPuzzle
    {
        public void Execute() 
        {
            var twoLetterIdCount = 0;
            var threeLetterIdCount = 0;

            using(var file = File.OpenText("day2/input.txt")) 
            {
                string boxId = null;
                while((boxId = file.ReadLine()) != null) 
                {
                    var counts = boxId.GroupBy(c => c).ToDictionary(k => k, v => v.ToList().Count);

                    if(counts.Any(c => c.Value == 2)) 
                    {
                        twoLetterIdCount++;
                    }
                    if(counts.Any(c => c.Value == 3)) 
                    {
                        threeLetterIdCount++;
                    }
                }
            }

            Console.WriteLine(twoLetterIdCount * threeLetterIdCount);
        }
    }

    public class Day2B : IPuzzle
    {
        public void Execute() 
        {
            string[] boxIds = null;

            using(var file = File.OpenText("day2/input.txt")) 
            {
                boxIds = file.ReadToEnd().Split(Environment.NewLine);
            }

            var boxesFound = false;
            for(var i = 0; i < boxIds.Length; i++) 
            {
                var iBoxId = boxIds[i];
                for(var j = i + 1; j < boxIds.Length; j++) 
                {
                    var diffCount = 0;
                    var diffIndex = -1;
                    var jBoxId = boxIds[j];
                    for(var k = 0; k < iBoxId.Length; k++) 
                    {
                        if(iBoxId[k] != jBoxId[k]) 
                        {
                            diffCount++;
                            diffIndex = k;
                        } 
                    }

                    if(diffCount == 1)
                    {
                        // Jackpot!
                        Console.WriteLine(iBoxId.Remove(diffIndex, 1));
                        boxesFound = true;
                        break;
                    }
                }

                if(boxesFound)
                    break;
            }
        }
    }
}