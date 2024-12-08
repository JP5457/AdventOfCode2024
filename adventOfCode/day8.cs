using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
namespace Day8
{
    public class Day8
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

        static string setChar(int position, string text, char toSet)
        {
            char[] temp = text.ToCharArray();
            temp[position] = toSet;
            return new string(temp);
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<string> antinodes = new List<string>(data);
            Dictionary<char, List<int[]>> locations = new Dictionary<char, List<int[]>>();
            for (int y = 0; y < data.Count(); y++)
            {
                for (int x = 0; x < data[0].Length; x++)
                {
                    if (data[y][x] != '.')
                    {
                        if (!locations.ContainsKey(data[y][x]))
                        {
                            locations.Add(data[y][x], new List<int[]>());
                        }
                        locations[data[y][x]].Add(new int[] { y, x });
                    }
                }
            }
            foreach (KeyValuePair<char, List<int[]>> location in locations)
            {
                for (int i = 0; i < location.Value.Count(); i++)
                {
                    for (int j = i+1; j < location.Value.Count(); j++)
                    {
                        int diffy = location.Value[i][0] - location.Value[j][0];
                        int diffx = location.Value[i][1] - location.Value[j][1];
                        int[] anode1 = new int[] { location.Value[i][0] + diffy, location.Value[i][1] + diffx };
                        int[] anode2 = new int[] { location.Value[j][0] - diffy, location.Value[j][1] - diffx };
                        if (anode1[0] >= 0 && anode1[0] < antinodes.Count() && anode1[1] >= 0 && anode1[1] < antinodes[0].Length)
                        {
                            antinodes[anode1[0]] = setChar(anode1[1], antinodes[anode1[0]], '#');
                        }
                        if (anode2[0] >= 0 && anode2[0] < antinodes.Count() && anode2[1] >= 0 && anode2[1] < antinodes[0].Length)
                        {
                            antinodes[anode2[0]] = setChar(anode2[1], antinodes[anode2[0]], '#');
                        }
                    }
                }
            }

            foreach (string line in antinodes)
            {
                Console.WriteLine(line);
                total += line.Count(x => x == '#');
            }
            Console.WriteLine(total);
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<string> antinodes = new List<string>(data);
            Dictionary<char, List<int[]>> locations = new Dictionary<char, List<int[]>>();
            for (int y = 0; y < data.Count(); y++)
            {
                for (int x = 0; x < data[0].Length; x++)
                {
                    if (data[y][x] != '.')
                    {
                        if (!locations.ContainsKey(data[y][x]))
                        {
                            locations.Add(data[y][x], new List<int[]>());
                        }
                        locations[data[y][x]].Add(new int[] { y, x });
                    }
                }
            }
            foreach (KeyValuePair<char, List<int[]>> location in locations)
            {
                for (int i = 0; i < location.Value.Count(); i++)
                {
                    for (int j = i + 1; j < location.Value.Count(); j++)
                    {
                        int diffy = location.Value[i][0] - location.Value[j][0];
                        int diffx = location.Value[i][1] - location.Value[j][1];
                        int an1 = 0;
                        int an2 = 0;
                        while (an1 >= 0)
                        {
                            int[] anode1 = new int[] { location.Value[i][0] + (diffy * an1), location.Value[i][1] + (diffx*an1) };
                            if (anode1[0] >= 0 && anode1[0] < antinodes.Count() && anode1[1] >= 0 && anode1[1] < antinodes[0].Length)
                            {
                                antinodes[anode1[0]] = setChar(anode1[1], antinodes[anode1[0]], '#');
                                an1++;
                            }
                            else { an1 = -1; }
                        }
                        while (an2 >= 0)
                        {
                            int[] anode2 = new int[] { location.Value[j][0] - (diffy*an2), location.Value[j][1] - (diffx*an2) };
                            if (anode2[0] >= 0 && anode2[0] < antinodes.Count() && anode2[1] >= 0 && anode2[1] < antinodes[0].Length)
                            {
                                antinodes[anode2[0]] = setChar(anode2[1], antinodes[anode2[0]], '#');
                                an2++;
                            }
                            else { an2 = -1; }
                        }
                    }
                }
            }

            foreach (string line in antinodes)
            {
                Console.WriteLine(line);
                total += line.Count(x => x == '#');
            }
            Console.WriteLine(total);
        }
    }
}
