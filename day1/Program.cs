using System;
using System.IO;
using System.Linq;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
            const int desiredSum = 2020;
            int firstChallenge = GetProductOfElementsWithSum(input, desiredSum);
            Console.WriteLine($"Product of pair with sum {desiredSum} is {firstChallenge}");
            int secondChallenge = GetProductOfTripletWithSum(input, desiredSum);
            Console.WriteLine($"Product of triplet with sum {desiredSum} is {secondChallenge}");
        }

        static int GetProductOfElementsWithSum(int[] arr, int desiredSum)
        {
            Array.Sort(arr);
            int left = 0, right = arr.Length - 1;
            while (right > left)
            {
                var sum = arr[left] + arr[right];
                if (sum > desiredSum)
                    right--;
                else if (sum < desiredSum)
                    left++;
                else
                    return arr[left] * arr[right];
            }

            return -1;
        }

        static int GetProductOfTripletWithSum(int[] arr, int desiredSum)
        {
            Array.Sort(arr);
            
            for (var i = 0; i < arr.Length - 2; i++)
            {
                int left = i + 1, right = arr.Length - 1;
                while (left < right)
                {
                    var sum = arr[i] + arr[left] + arr[right];

                    if (sum < desiredSum)
                        left++;
                    else if (sum > desiredSum)
                        right--;
                    else
                        return arr[i] * arr[left] * arr[right];
                }
            }

            return -1;
        }
    }
}
