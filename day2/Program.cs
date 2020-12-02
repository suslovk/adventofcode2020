using System;
using System.IO;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            Console.WriteLine(ValidPasswordsCount(input) + " valid passwords");
            Console.WriteLine(ValidPasswordsV2Count(input) + " valid passwords");
        }

        static int ValidPasswordsCount(string[] input)
        {
            var validPasswordsCount = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var split = input[i].Split(' ');
                var times = split[0].Split('-');
                var fromTimes = int.Parse(times[0]);
                var toTimes = int.Parse(times[1]);
                var letter = split[1][0];
                var actualTimes = 0;
                string password = split[2];
                for (var j = 0; j < password.Length; j++)
                    if (password[j] == letter)
                        actualTimes++;

                if (actualTimes >= fromTimes && actualTimes <= toTimes)
                    validPasswordsCount++;
            }

            return validPasswordsCount;
        }

        static int ValidPasswordsV2Count(string[] input)
        {
            var validPasswordsCount = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var split = input[i].Split(' ');
                var positions = split[0].Split('-');
                var firstPosition = int.Parse(positions[0]);
                var secondPosition = int.Parse(positions[1]);
                var letter = split[1][0];
                string password = split[2];

                if (password[firstPosition - 1] == letter ^ password[secondPosition - 1] == letter)
                    validPasswordsCount++;
            }

            return validPasswordsCount;
        }
    }
}
