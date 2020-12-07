using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day4
    {
        public static void Do()
        {
            var fileStream = File.OpenRead("input4.txt");

            int count = 0;

            string line = null;
            using (var sr = new StreamReader(fileStream))
            {
                var fieldsDictionary = new Dictionary<string, string>();
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        if (IsValid(fieldsDictionary))
                            count++;

                        fieldsDictionary = new Dictionary<string, string>();
                    }
                    else
                    {
                        foreach (var f in line.Split(" "))
                        {
                            fieldsDictionary.Add(f.Substring(0, f.IndexOf(':')), f.Substring(f.IndexOf(':') + 1));
                        }
                    }
                }
                if (IsValid(fieldsDictionary))
                    count++;
            }

            Console.WriteLine($"{count}");
        }

        private static bool IsValid(Dictionary<string, string> fields)
        {
            if (YearNotInRange(fields, "byr", 1920, 2002))
                return false;

            if (YearNotInRange(fields, "iyr", 2010, 2020))
                return false;

            if (YearNotInRange(fields, "eyr", 2020, 2030))
                return false;

            if (!HeightCheck(fields)) 
                return false;

            if (!fields.TryGetValue("hcl", out var hairColor) || !Regex.IsMatch(hairColor, "^#[0-9a-f]{6}$"))
                return false;

            var validEyeColors = new HashSet<string>
            {
                "amb",
                "blu",
                "brn",
                "gry",
                "grn",
                "hzl",
                "oth"
            };
            if (!fields.TryGetValue("ecl", out var eyeColor) || !validEyeColors.Contains(eyeColor))
                return false;

            if (!fields.TryGetValue("pid", out var passport) || !Regex.IsMatch(passport, "^[0-9]{9}$"))
                return false;

            return true;
        }

        private static bool HeightCheck(Dictionary<string, string> fields)
        {
            if (!fields.TryGetValue("hgt", out var height))
                return false;

            var heightType = height.Substring(height.Length - 2);
            if (heightType != "cm" && heightType != "in")
                return false;

            var i = int.Parse(height.Substring(0, height.Length - 2));
            if (heightType == "cm")
            {
                if (i < 150 || i > 193)
                    return false;
            }
            else
            {
                if (i < 59 || i > 76)
                    return false;
            }

            return true;
        }

        private static bool YearNotInRange(Dictionary<string, string> fields, string field, int min, int max)
        {
            return !fields.TryGetValue(field, out var birthYear) || 
                   !Regex.IsMatch(birthYear, "^[0-9]{4}$") || 
                   !int.TryParse(birthYear, out int number) || 
                   number < min || 
                   number > max;
        }
    }
}