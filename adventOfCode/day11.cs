using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using static System.Formats.Asn1.AsnWriter;
namespace Day11
{
    public class Day11
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

        static List<Int64> blink(List<Int64> stones)
        {
            List<Int64> newStones = new List<Int64>();
            foreach (Int64 stone in stones)
            {
                if(stone == 0)
                {
                    newStones.Add(1);
                } else if ((stone.ToString().Length % 2) == 0) {
                    newStones.Add(Int64.Parse(stone.ToString().Substring(0, (int)(stone.ToString().Length / 2))));
                    newStones.Add(Int64.Parse(stone.ToString().Substring((int)(stone.ToString().Length / 2), (int)(stone.ToString().Length / 2))));
                } else
                {
                    newStones.Add(stone * 2024);
                }
            }
            return newStones;
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            string[] stones = data[0].Split(' ');
            List<Int64> nums = stones.Select(stone => Int64.Parse(stone)).ToList();
            for(int i = 0; i < 25; i++)
            {
                Console.WriteLine(i);
                nums = blink(nums);
            }
            Console.WriteLine(nums.Count());
        }

      
        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            Int64 total = 0;

            string[] stones = data[0].Split(' ');
            List<Int64> nums = stones.Select(stone => Int64.Parse(stone)).ToList();
            Dictionary<Int64, Int64[]> splits = new Dictionary<Int64, Int64[]>();
            Dictionary<Int64, Int64> stoneCount = new Dictionary<Int64, Int64>();
            foreach (Int64 stone in nums)
            {
                stoneCount.Add(stone, 1);
            }
            if (!stoneCount.ContainsKey(1)) { stoneCount.Add(1, 0); }
            for (int i = 0; i < 75; i++)
            {
                Console.WriteLine(i);
                Dictionary<Int64, Int64> newStoneCount = new Dictionary<Int64, Int64>(stoneCount);
                newStoneCount[1] = newStoneCount[1] + stoneCount[0];
                newStoneCount[0] = 0;
                foreach (Int64 stone in stoneCount.Keys)
                {
                    if ((stone.ToString().Length % 2) == 0)
                    {
                        if (splits.ContainsKey(stone))
                        {
                            newStoneCount[splits[stone][0]] = newStoneCount[splits[stone][0]] + stoneCount[stone];
                            newStoneCount[splits[stone][1]] = newStoneCount[splits[stone][1]] + stoneCount[stone];
                        }
                        else
                        {
                            Int64[] split = new Int64[2];
                            split[0] = Int64.Parse(stone.ToString().Substring(0, (int)(stone.ToString().Length / 2)));
                            split[1] = Int64.Parse(stone.ToString().Substring((int)(stone.ToString().Length / 2), (int)(stone.ToString().Length / 2)));
                            if (!newStoneCount.ContainsKey(split[0])) { newStoneCount.Add(split[0], 0); }
                            if (!newStoneCount.ContainsKey(split[1])) { newStoneCount.Add(split[1], 0); }
                            splits.Add(stone, split);
                            newStoneCount[split[0]] = newStoneCount[split[0]] + stoneCount[stone];
                            newStoneCount[split[1]] = newStoneCount[split[1]] + stoneCount[stone];
                        }
                        newStoneCount[stone] = newStoneCount[stone] - stoneCount[stone] ;
                    }
                    else
                    {
                        if (!newStoneCount.ContainsKey(stone * 2024)) { newStoneCount.Add(stone * 2024, stoneCount[stone]); } else
                        {
                            newStoneCount[stone * 2024] = newStoneCount[stone * 2024] + stoneCount[stone];
                        }
                        newStoneCount[stone] = newStoneCount[stone] - stoneCount[stone];
                    }
                }
                stoneCount = newStoneCount;
            }
            foreach(Int64 count in stoneCount.Values)
            {
                total += count;
            }
            Console.WriteLine(total);
        }
    }
}
