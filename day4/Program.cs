using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            Console.WriteLine(ValidPassportsCount(input) + " valid passports");
            Console.WriteLine(ExtendedValidPassportsCount(input) + " valid (extended validation) passports");
        }

        static int ValidPassportsCount(string[] input)
        {
            var validPassportsCount = 0;
            var i = 0;
            while (i < input.Length)
            {
                const int byrMask = 0x01, iyrMask = 0x02, eyrMask = 0x04, 
                    hgtMask = 0x08, hclMask = 0x10, eclMask = 0x20, pidMask = 0x40,
                    validPassportMask = byrMask | iyrMask | eyrMask | hgtMask | hclMask | eclMask | pidMask;
                var claimsMask = 0;
                while (i < input.Length && !string.IsNullOrEmpty(input[i]))
                {
                    var line = input[i].Split(' ');
                    foreach (var el in line)
                    {
                        var split = el.Split(':');
                        switch (split[0])
                        {
                            case "byr": claimsMask |= byrMask; break;
                            case "iyr": claimsMask |= iyrMask; break;
                            case "eyr": claimsMask |= eyrMask; break;
                            case "hgt": claimsMask |= hgtMask; break;
                            case "hcl": claimsMask |= hclMask; break;
                            case "ecl": claimsMask |= eclMask; break;
                            case "pid": claimsMask |= pidMask; break;
                        }
                    }

                    i++;
                }

                if ((claimsMask & validPassportMask) == validPassportMask)
                    validPassportsCount++;
                
                i++;
            }

            return validPassportsCount;
        }

        static int ExtendedValidPassportsCount(string[] input)
        {
            var validPassportsCount = 0;
            var i = 0;
            while (i < input.Length)
            {
                const int byrMask = 0x01, iyrMask = 0x02, eyrMask = 0x04, 
                    hgtMask = 0x08, hclMask = 0x10, eclMask = 0x20, pidMask = 0x40,
                    validPassportMask = byrMask | iyrMask | eyrMask | hgtMask | hclMask | eclMask | pidMask;
                var claimsMask = 0;
                while (i < input.Length && !string.IsNullOrEmpty(input[i]))
                {
                    var line = input[i].Split(' ');
                    foreach (var el in line)
                    {
                        var split = el.Split(':');
                        switch (split[0])
                        {
                            case "byr":
                                var birthYear = int.Parse(split[1]);
                                if (birthYear >= 1920 && birthYear <= 2002) 
                                    claimsMask |= byrMask;
                                break;
                            case "iyr":
                                var issueYear = int.Parse(split[1]);
                                if (issueYear >= 2010 && issueYear <= 2020)
                                    claimsMask |= iyrMask;
                                break;
                            case "eyr":
                                var expirationYear = int.Parse(split[1]);
                                if (expirationYear >= 2020 && expirationYear <= 2030)
                                    claimsMask |= eyrMask;
                                break;
                            case "hgt":
                                string height = split[1];
                                if (height.EndsWith("cm"))
                                {
                                    var heightCm = int.Parse(height.Substring(0, height.Length - 2));
                                    if (heightCm >= 150 && heightCm <= 193)
                                        claimsMask |= hgtMask;
                                } else if (height.EndsWith("in"))
                                {
                                    var heightIn = int.Parse(height.Substring(0, height.Length - 2));
                                    if (heightIn >= 59 && heightIn <= 76)
                                        claimsMask |= hgtMask;
                                }
                                break;
                            case "hcl":
                                if (Regex.IsMatch(split[1], "^#[0-9a-f]{6}$"))
                                    claimsMask |= hclMask;
                                break;
                            case "ecl":
                                if (split[1] == "amb" || split[1] == "blu" || split[1] == "brn" || 
                                    split[1] == "gry" || split[1] == "grn" || split[1] == "hzl" || split[1] == "oth")
                                    claimsMask |= eclMask;
                                break;
                            case "pid":
                                if (Regex.IsMatch(split[1], "^[0-9]{9}$"))
                                    claimsMask |= pidMask;
                                break;
                        }
                    }

                    i++;
                }

                if ((claimsMask & validPassportMask) == validPassportMask)
                    validPassportsCount++;

                i++;
            }

            return validPassportsCount;
        }
    }
}