using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Text.RegularExpressions;
namespace Day7
{
    public class Day7
    {

        static List<string> readFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            List<string> data = new List<string>();
            line = sr.ReadLine();
            while (line != null)
            {
                data.Add(line);
                line = sr.ReadLine();
            }
            return data;
        }

        static bool isMatch(Int64 start, Int64[] remains, Func<Int64, Int64, Int64> hauntedOp, Int64 expected)
        {
            if (remains.Length == 0)
            {
                return start == expected;
            }
            else
            {
                return (isMatch(hauntedOp(start, remains[0]), remains.Skip(1).ToArray(), ((x, y) => x + y), expected) ||
                              isMatch(hauntedOp(start, remains[0]), remains.Skip(1).ToArray(), ((x, y) => x * y), expected));
            }
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            Int64 total = 0;
            foreach(string line in data)
            {
                string[] parts = line.Split(":");
                Int64 expected = Int64.Parse(parts[0]);
                string[] textnums = parts[1].Split(" ");
                Int64[] numbers = textnums.Skip(1).ToArray().Select(x => Int64.Parse(x)).ToArray();
                bool match = (isMatch(numbers[0], numbers.Skip(1).ToArray(), ((x, y) => x + y), expected) ||
                              isMatch(numbers[0], numbers.Skip(1).ToArray(), ((x, y) => x * y), expected));
                if (match) { total += expected; }
            }
            Console.WriteLine(total);
        }

        public static Int64 concat(Int64 a, Int64 b)
        {
            return Int64.Parse(a.ToString() + b.ToString());
        }

        static bool isMatchP2(Int64 start, Int64[] remains, Func<Int64, Int64, Int64> hauntedOp, Int64 expected)
        {
            if (remains.Length == 0)
            {
                return start == expected;
            }
            else
            {
                return (isMatchP2(hauntedOp(start, remains[0]), remains.Skip(1).ToArray(), ((x, y) => x + y), expected) ||
                        isMatchP2(hauntedOp(start, remains[0]), remains.Skip(1).ToArray(), ((x, y) => x * y), expected) ||
                        isMatchP2(hauntedOp(start, remains[0]), remains.Skip(1).ToArray(), concat, expected));
            }
        }


        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            Int64 total = 0;
            foreach (string line in data)
            {
                string[] parts = line.Split(":");
                Int64 expected = Int64.Parse(parts[0]);
                string[] textnums = parts[1].Split(" ");
                Int64[] numbers = textnums.Skip(1).ToArray().Select(x => Int64.Parse(x)).ToArray();
                bool match = (isMatchP2(numbers[0], numbers.Skip(1).ToArray(), ((x, y) => x + y), expected) ||
                              isMatchP2(numbers[0], numbers.Skip(1).ToArray(), ((x, y) => x * y), expected) ||
                              isMatchP2(numbers[0], numbers.Skip(1).ToArray(), concat, expected));
                if (match) { total += expected; }
            }
            Console.WriteLine(total);
        }
    }
}
