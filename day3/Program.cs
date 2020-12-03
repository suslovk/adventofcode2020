using System;
using System.IO;

namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            Console.WriteLine(GetTreesCount(input, 3, 1) + " trees traversed");
            var product = (long) GetTreesCount(input, 1, 1) * GetTreesCount(input, 3, 1) * GetTreesCount(input, 5, 1) *
                GetTreesCount(input, 7, 1) * GetTreesCount(input, 1, 2);
            Console.WriteLine("Product is " + product);
        }

        static int GetTreesCount(string[] input, int stepsRight, int stepsDown)
        {
            var rowLength = input[0].Length;
            var treesCount = 0;
            var horizontal = 0;
            for (var vertical = 0; vertical < input.Length; vertical += stepsDown)
            {
                if (input[vertical][horizontal] == '#')
                    treesCount++;
                
                horizontal = (horizontal + stepsRight) % rowLength;
            }

            return treesCount;
        }
    }
}
