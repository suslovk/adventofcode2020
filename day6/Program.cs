using System;
using System.Collections.Generic;
using System.IO;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            Console.WriteLine("Sum of aggrees from all groups is " + SumAgreesInAllGroups(input));
            Console.WriteLine("Sum of consensuses from all groups is " + SumConsensusesInAllGroups(input));
        }

        static int SumConsensusesInAllGroups(string[] input) =>
            IterateAllGroups(input, GetConsensusInAGroup);

        static int SumAgreesInAllGroups(string[] input) =>
            IterateAllGroups(input, GetAggreesInAGroup);

        static int IterateAllGroups(string[] input, Func<ArraySegment<string>, int> func)
        {
            var sum = 0;
            int startOfGroup = 0, endOfGroup = 0;
            while (endOfGroup < input.Length)
            {
                while (endOfGroup < input.Length && !string.IsNullOrEmpty(input[endOfGroup]))
                    endOfGroup++;
                
                sum += func(new ArraySegment<string>(input, startOfGroup, endOfGroup - startOfGroup));
                startOfGroup = ++endOfGroup;
            }

            return sum;
        }

        static int GetAggreesInAGroup(ArraySegment<string> group)
        {
            var hashset = new HashSet<char>(26);
            for (var i = 0; i < group.Count; i++)
            for (var j = 0; j < group[i].Length; j++)
                hashset.Add(group[i][j]);
            
            return hashset.Count;
        }

        static int GetConsensusInAGroup(ArraySegment<string> group)
        {
            var agreesCount = new int[26];
            for (var i = 0; i < group.Count; i++)
            for (var j = 0; j < group[i].Length; j++)
                agreesCount[group[i][j] - 'a']++;
            
            var sum = 0;
            for (var i = 0; i < agreesCount.Length; i++)
                if (agreesCount[i] == group.Count)
                    sum++;
            
            return sum;
        }
    }
}
