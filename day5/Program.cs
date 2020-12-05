using System;
using System.IO;
using System.Linq;

namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            Console.WriteLine("Highest seat ID is " + GetHighestSeatId(input));
            Console.WriteLine("Missing pass is " + GetMissingBoardingPass(input));
        }

        static int GetMissingBoardingPass(string[] input)
        {
            var grouped = input.GroupBy(k => k.Substring(0, 7)).Where(g => g.Count() == 7).First();
            var occupied = new bool[8];
            foreach (var key in grouped)
                occupied[ParseSeat(key)] = true;

            var desiredSeat = 0;
            for (var i = 0; i < occupied.Length; i++)
            {
                if (!occupied[i])
                {
                    desiredSeat = i;
                    break;
                }
            }

            return ParseRow(grouped.Key) * 8 + desiredSeat;
        }

        static int GetHighestSeatId(string[] input)
        {
            var maxSeat = input[0];
            for (var i = 1; i < input.Length; i++)
                maxSeat = Max(maxSeat, input[i]);

            return Decode(maxSeat);
        }

        static string Max(string seat1, string seat2)
        {
            for (var i = 0; i < 7; i++)
            {
                if (seat1[i] == seat2[i])
                    continue;

                return seat1[i] == 'B' ? seat1 : seat2;
            }

            for (var i = 7; i < 10; i++)
            {
                if (seat1[i] == seat2[i])
                    continue;

                return seat1[i] == 'R' ? seat1 : seat2;
            }

            return seat1;
        }

        static int Decode(string seat)
        {
            var rowNo = ParseRow(seat);
            var seatNo = ParseSeat(seat);

            return rowNo * 8 + seatNo;
        }

        static int ParseRow(string seat)
        {
            var result = 0;
            for (var i = 6; i >= 0; i--)
            {
                var power = 1 << (6 - i);
                result += power * (seat[i] == 'B' ? 1 : 0);
            }

            return result;
        }

        static int ParseSeat(string seat)
        {
            var result = 0;
            for (var i = 9; i >= 7; i--)
            {
                var power = 1 << (9 - i);
                result += power * (seat[i] == 'R' ? 1 : 0);
            }

            return result;
        }
    }
}
