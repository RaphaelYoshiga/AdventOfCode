using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day2
    {
        public static void Do2()
        {
            var readAllLines = File.ReadAllLines("input2.txt");
            int totalCount = 0;
            foreach (var line in readAllLines)
            {
                var splitLine = line.Split(": ");
                var policy = splitLine[0];

                var strings = policy.Split(new char[] { '-', ' ' });
                var min = int.Parse(strings[0]);
                var max = int.Parse(strings[1]);
                var policyChar = strings[2][0];

                var password = splitLine[1];


                int correct = 0;

                var firstIndexChar = password[min - 1];
                var secondIndexChar = password.Length >= max ? password[max - 1] : ' ';

                if (firstIndexChar == policyChar)
                    correct++;

                if (secondIndexChar == policyChar)
                    correct++;

                if (correct == 1)
                    totalCount++;
            }

            Console.WriteLine($"Passwords: {totalCount}");
            Console.ReadLine();
        }

        private static void Do()
        {
            var readAllLines = File.ReadAllLines("input2.txt");
            int totalCount = 0;
            foreach (var line in readAllLines)
            {
                var splitLine = line.Split(": ");
                var policy = splitLine[0];

                var strings = policy.Split(new char[] { '-', ' ' });
                var min = int.Parse(strings[0]);
                var max = int.Parse(strings[1]);
                var policyChar = strings[2][0];

                var password = splitLine[1];

                var passwordDictionary = password.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());


                var valid = passwordDictionary.ContainsKey(policyChar) && min <= passwordDictionary[policyChar] &&
                            max >= passwordDictionary[policyChar];

                if (valid)
                    totalCount++;
            }

            Console.WriteLine($"Passwords: {totalCount}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Day2.Do2();
        }

        
    }
}
