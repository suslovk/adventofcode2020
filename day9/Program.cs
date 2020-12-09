using System;
using System.IO;
using System.Linq;

namespace day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();
            long invalidNumber = input[GetInvalidNumber(input)];
            Console.WriteLine($"The first number that does not have satisfy criteria is {invalidNumber}");
            Console.WriteLine($"Encryption weakness in XMAS-encrypted list of numbers is {GetSubarrayWithSum(input, invalidNumber)}");
        }

        static int GetInvalidNumber(long[] input)
        {
            for (var i = 25; i < input.Length; i++)
            {
                var buffer = input.Skip(i - 25).Take(25).ToArray();
                Array.Sort(buffer);
                int start = 0, stop = buffer.Length - 1;
                while (start < stop)
                {
                    var sum = buffer[start] + buffer[stop];
                    if (sum < input[i])
                        start++;
                    else if (sum > input[i])
                        stop--;
                    else
                        break;
                }

                if (start == stop)
                    return i;
            }

            return -1;
        }

        static long GetSubarrayWithSum(long[] input, long desiredSum)
        {
            int start = 0, end = 1;
            var sum = input[start] + input[end];
            while (sum != desiredSum)
            {
                if (sum < desiredSum)
                {
                    end++;
                    sum += input[end];
                }
                else if (sum > desiredSum)
                {
                    sum -= input[start];
                    start++;
                }
                else
                    break;
            }

            var subarray = input[start..(end + 1)];
            Array.Sort(subarray);
            return subarray[0] + subarray[subarray.Length - 1];
        }
    }
}
