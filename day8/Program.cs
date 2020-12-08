using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            Console.WriteLine($"Value of the accumulator before loop is {FindTheLoop(input)}");
            Console.WriteLine($"The accumulator's value after the program terminates is {BreakTheLoop(input)}");
        }

        static int BreakTheLoop(string[] input)
        {
            var parsed = input.Select(ParseLine).ToArray();
            for (var i = 0; i < parsed.Length; i++)
            {
                var line = parsed[i];
                if (line.command != "jmp" && line.command != "nop")
                    continue;

                parsed[i].command = line.command == "jmp" ? "nop" : "jmp";
                var acc = GetAccumulatorIfEnding();
                if (acc != -1)
                    return acc;

                parsed[i].command = line.command;
            }

            return -1;

            int GetAccumulatorIfEnding()
            {
                var accumulator = 0;
                var visited = new bool[parsed.Length];
                var currentPosition = 0;
                visited[currentPosition] = true;
                while (true)
                {
                    var line = parsed[currentPosition];
                    var newPosition = 0;
                    switch (line.command)
                    {
                        case "acc":
                            accumulator += line.counter;
                            newPosition = (currentPosition + 1) % parsed.Length;
                            break;
                        case "jmp":
                            newPosition = (currentPosition + line.counter) % parsed.Length;
                            break;
                        case "nop":
                            newPosition = (currentPosition + 1) % parsed.Length;
                            break;
                    }

                    if (currentPosition == parsed.Length - 1)
                        return accumulator;

                    if (visited[newPosition])
                        return -1;
                    else
                        visited[newPosition] = true;

                    currentPosition = newPosition;
                }
            }
        }

        static int FindTheLoop(string[] input)
        {
            var visited = new bool[input.Length];
            var currentPosition = 0;
            var accumulator = 0;
            visited[currentPosition] = true;
            while (true)
            {
                var line = input[currentPosition];
                var result = ParseLine(line);
                var newPosition = 0;
                switch (result.command)
                {
                    case "acc": 
                        accumulator += result.counter;
                        newPosition = (currentPosition + 1) % input.Length;
                        break;
                    case "jmp":
                        newPosition = (currentPosition + result.counter) % input.Length;
                        break;
                    case "nop":
                        newPosition = (currentPosition + 1) % input.Length;
                        break;
                }
                if (visited[newPosition])
                    return accumulator;
                else
                    visited[newPosition] = true;

                currentPosition = newPosition;
            }
        }

        private static (string command, int counter) ParseLine(string line)
        {
            var command = line.Substring(0, 3);
            var counter = int.Parse(line.Substring(4, line.Length - 4));
            return (command, counter);
        }
    }
}
