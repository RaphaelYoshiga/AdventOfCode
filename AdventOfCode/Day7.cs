using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Bag
    {
        public List<BagCount> BagCounts;

        public Bag(string color)
        {
            Color = color;
        }

        public string Color { get; }

        public void Add(List<BagCount> bagCounts)
        {
            BagCounts = bagCounts;
        }
    }

    internal class Day7
    {
        private const string ShinyGold = "shiny gold";
        private static Dictionary<string, Bag> _bags;
        private static int count = 0;

        public static void Do()
        {
            _bags = GetBags();

            int totalBags = 0;
            foreach (var bagCount in _bags[ShinyGold].BagCounts)
            {
                totalBags += CountTotalInsideBags(bagCount);
            };
            Console.WriteLine($"Total: {totalBags}");

        }

        private static int CountTotalInsideBags(BagCount bagCount)
        {
            var childrenBags = _bags[bagCount.Color].BagCounts.Sum(CountTotalInsideBags);
            return bagCount.Number + bagCount.Number * childrenBags;
        }

        private static void Part1()
        {
            foreach (var bag in _bags.Where(p => p.Key != ShinyGold))
            {
                if (LookAt(bag.Value))
                    count++;
            }

            Console.WriteLine($"Count: {count}");
        }

        private static bool LookAt(Bag bag)
        {
            if (bag.Color == ShinyGold)
            {
                return true;
            }

            foreach (var insideBag in bag.BagCounts)
            {
                if (LookAt(_bags[insideBag.Color]))
                    return true;
            }

            return false;
        }

        private static Dictionary<string, Bag> GetBags()
        {
            var fileStream = File.OpenRead("input7.txt");

            var bags = new Dictionary<string, Bag>();
            using (var sr = new StreamReader(fileStream))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    var bag = CastBag(line);
                    bags.Add(bag.Color, bag);
                }
            }

            return bags;
        }

        private static Bag CastBag(string line)
        {
            var splitLine = line.Split(" bags contain ");

            var bag = new Bag(splitLine[0]);

            var contents = splitLine[1].Split(", ");

            var bagCounts = contents
                .Select(Parse)
                .Where(p => p != null)
                .ToList();

            bag.Add(bagCounts);
            return bag;
        }

        private static BagCount Parse(string content)
        {
            if (!int.TryParse(content.Substring(0, content.IndexOf(' ')), out var number))
            {
                return null;
            }

            var preColor = content.Substring(content.IndexOf(" ") + 1);
            var color = preColor.Substring(0, preColor.IndexOf(" bag"));

            return new BagCount(number, color);
        }
    }

    public class BagCount
    {
        public int Number { get; }
        public string Color { get; }

        public BagCount(in int number, string color)
        {
            Number = number;
            Color = color;
        }
    }
}