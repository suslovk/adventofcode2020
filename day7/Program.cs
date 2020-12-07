using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var parsed = ParseInput(input);
            Console.WriteLine($"{CountBags(parsed)} can eventually contain at least one shiny gold bag");
            Console.WriteLine($"{CountBagsInside(parsed)} individual bags required inside single shiny gold bag");
        }

        static int CountBagsInside(Dictionary<string, Bag> input) 
        {
            return Count("shiny gold");

            int Count(string color)
            {
                var bag = input[color];
                var sum = 0;
                foreach (var bagInside in bag.Contains)
                    sum += bagInside.count * (Count(bagInside.color) + 1);

                return sum;
            }
        }

        static int CountBags(Dictionary<string, Bag> dict)
        {
            return Count(dict, "shiny gold").ToHashSet().Count;

            List<string> Count(Dictionary<string, Bag> dict, string color)
            {
                var bags = dict[color];
                var list = new List<string>();
                foreach (var bag in bags.ContainsIn)
                {
                    list.Add(bag.color);
                    list.AddRange(Count(dict, bag.color));
                }

                return list;
            }
        }

        static Dictionary<string, Bag> ParseInput(string[] input)
        {
            var dict = new Dictionary<string, Bag>(input.Length);
            for (var j = 0; j < input.Length; j++)
            {
                var bagColor = input[j].Substring(0, input[j].IndexOf("bags") - 1);
                if (!dict.ContainsKey(bagColor))
                    dict.Add(bagColor, new Bag());
                var containsColorsStrings = input[j].Substring(input[j].IndexOf("contain") + 8).Split(", ");

                if (!containsColorsStrings[0].StartsWith("no"))
                {
                    for (var i = 0; i < containsColorsStrings.Length; i++)
                    {
                        var count = int.Parse(containsColorsStrings[i].Substring(0, containsColorsStrings[i].IndexOf(" ")));
                        var indexOfColor = containsColorsStrings[i].IndexOf(" ") + 1;
                        var indexOfColorEnd = containsColorsStrings[i].IndexOf("bag") - indexOfColor - 1;
                        var containsColor = containsColorsStrings[i].Substring(indexOfColor, indexOfColorEnd);
                        dict[bagColor].Contains.Add((containsColor, count));
                        if (!dict.ContainsKey(containsColor))
                            dict.Add(containsColor, new Bag());

                        dict[containsColor].ContainsIn.Add((bagColor, count));
                    }
                }
            }

            return dict;
        }

        class Bag
        {
            public List<(string color, int count)> Contains = new List<(string, int)>();
            public List<(string color, int count)> ContainsIn = new List<(string, int)>();
        }
    }
}
