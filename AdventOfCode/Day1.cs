using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day1
    {
        private static void Do()
        {
            var readAllLines = File.ReadAllLines("input.txt");
            var nums = readAllLines.Select(int.Parse).ToList();

            for (int i = 0; i < nums.Count; i++)
            {
                var num = nums[i];
                for (int y = i; y < nums.Count; y++)
                {
                    if (y == i)
                        continue;

                    var otherNum = nums[y];


                    for (int j = 0; j < nums.Count; j++)
                    {
                        if (j == y || j == i)
                            continue;

                        var anotherNum = nums[j];

                        if (otherNum + num + anotherNum == 2020)
                        {
                            Console.WriteLine($"{num} + {otherNum} + {anotherNum} = {num * otherNum * anotherNum}");
                        }
                    }
                }
            }
        }
    }
}