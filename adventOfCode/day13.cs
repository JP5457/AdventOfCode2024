using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Channels;
namespace Day13
{
    public class Day13
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

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<Dictionary<char, int[]>> claws = new List<Dictionary<char, int[]>>();
            Dictionary<char, int[]> newClaw = new Dictionary<char, int[]>();
            foreach (string line in data)
            {
                if (line == "")
                {
                    Console.WriteLine(newClaw['P'][1]);
                    claws.Add(newClaw);
                    newClaw = new Dictionary<char, int[]>();
                } else {
                    string[] split = line.Split(':');
                    if (split[0] == "Button A")
                    {
                        string[] split_ = line.Split(',');
                        newClaw.Add('A', new int[] { int.Parse(split_[0].Split('+')[1]), int.Parse(split_[1].Split('+')[1]) });
                    } else if (split[0] == "Button B")
                    {
                        string[] split_ = line.Split(',');
                        newClaw.Add('B', new int[] { int.Parse(split_[0].Split('+')[1]), int.Parse(split_[1].Split('+')[1]) });
                    } else if (split[0] == "Prize")
                    {
                        string[] split_ = line.Split(',');
                        newClaw.Add('P', new int[] { (int.Parse(split_[0].Split('=')[1])), int.Parse(split_[1].Split('=')[1]) });
                    }
                }
            }
            claws.Add(newClaw);
            foreach (Dictionary<char, int[]> claw in claws)
            {
                List<int> solutions = new List<int>();
                for (int a = 0; a <= 100; a++)
                {
                    for (int b = 0; b <= 100; b++)
                    {
                        if (claw['A'][0] * a + claw['B'][0] * b == claw['P'][0] && claw['A'][1] * a + claw['B'][1] * b == claw['P'][1])
                        {
                            solutions.Add((a*3) + b);
                        }
                    }
                }
                if (solutions.Count() > 0)
                {
                    total += solutions.Min();
                }
            }
            Console.WriteLine(total);
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            long total = 0;
            List<Dictionary<char, long[]>> claws = new List<Dictionary<char, long[]>>();
            Dictionary<char, long[]> newClaw = new Dictionary<char, long[]>();
            foreach (string line in data)
            {
                if (line == "")
                {
                    claws.Add(newClaw);
                    newClaw = new Dictionary<char, long[]>();
                }
                else
                {
                    string[] split = line.Split(':');
                    if (split[0] == "Button A")
                    {
                        string[] split_ = line.Split(',');
                        newClaw.Add('A', new long[] { long.Parse(split_[0].Split('+')[1]), long.Parse(split_[1].Split('+')[1]) });
                    }
                    else if (split[0] == "Button B")
                    {
                        string[] split_ = line.Split(',');
                        newClaw.Add('B', new long[] { long.Parse(split_[0].Split('+')[1]), int.Parse(split_[1].Split('+')[1]) });
                    }
                    else if (split[0] == "Prize")
                    {
                        string[] split_ = line.Split(',');
                        newClaw.Add('P', new long[] { (long.Parse(split_[0].Split('=')[1])+ 10000000000000), (long.Parse(split_[1].Split('=')[1])+ 10000000000000) });
                    }
                }
            }
            claws.Add(newClaw);
            foreach (Dictionary<char, long[]> claw in claws)
            {
                List<long> solutions = new List<long>();
                long x = claw['P'][0] * claw['A'][1];
                long y = claw['P'][1] * claw['A'][0];
                long xy = x - y;
                long x_ = claw['A'][1] * claw['B'][0];
                long y_ = claw['A'][0] * claw['B'][1];
                long xy_ = x_ - y_;
                if (xy % xy_ == 0)
                {
                    long B = xy / xy_;
                    long A = (claw['P'][1] - (claw['B'][1] * B)) / claw['A'][1];
                    total += (A * 3) + B;
                }
            }
            Console.WriteLine("total: " + total);
        }
    }
}
