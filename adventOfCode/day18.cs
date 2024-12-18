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
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace Day18
{

    public class Day18
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

        static List<string> ComputeDrops(List<string> data, int x, int y, int drops)
        {
            List<string> map = new List<string>();
            for (int i = 0; i <= y; i++)
            {
                string row = "";
                for (int j = 0; j <= x; j++)
                {
                    row += ".";
                }
                map.Add(row);
            }
            for (int i = 0; i < drops; i++)
            {
                string line = data[i];
                string[] strings = line.Split(',');
                map[int.Parse(strings[1])] = setChar(int.Parse(strings[0]), map[int.Parse(strings[1])], '#');
            }
            return map;
        }

        static List<string> addDrop (List<string> data, List<string> map, int drop)
        {
            string line = data[drop];
            string[] strings = line.Split(',');
            map[int.Parse(strings[1])] = setChar(int.Parse(strings[0]), map[int.Parse(strings[1])], '#');
            return map;
        }

        static bool LoopWeight (List<string> map, List<List<int>> weight)
        {
            List<List<int>> newWeight = new List<List<int>>(weight);
            bool changed = false;
            for (int y = 0; y < weight.Count(); y++)
            {
                for(int x = 0; x < weight[y].Count(); x++)
                {
                    if (map[y][x] == '.')
                    {
                        if (y > 0) 
                        {
                            if (map[y - 1][x] == '.' && weight[y - 1][x]+1 < weight[y][x])
                            {
                                newWeight[y][x] = weight[y - 1][x]+1;
                                changed = true;
                            }
                        }
                        if (y < weight.Count()-1)
                        {
                            if (map[y + 1][x] == '.' && weight[y + 1][x]+1 < weight[y][x])
                            {
                                newWeight[y][x] = weight[y + 1][x] + 1;
                                changed = true;
                            }
                        }
                        if (x > 0)
                        {
                            if (map[y][x-1] == '.' && weight[y][x-1]+1 < weight[y][x])
                            {
                                newWeight[y][x] = weight[y][x-1] + 1;
                                changed = true;
                            }
                        }
                        if (x < weight[y].Count()-1)
                        {
                            if (map[y][x + 1] == '.' && weight[y][x + 1]+1 < weight[y][x])
                            {
                                newWeight[y][x] = weight[y][x + 1] + 1;
                                changed = true;
                            }
                        }
                    }
                }
            }
            weight = new List<List<int>>(newWeight);
            return changed;
        }



        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int x = 70;
            int y = 70;
            int drops = 1024;
            List<string> map = ComputeDrops(data, x, y, drops);
            foreach (string line in map)
            {
                Console.WriteLine(line);
            }
            List<List<int>> weight = new List<List<int>>();
            for (int i = 0; i <= y; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j <= x; j++)
                {
                    row.Add(2147483641);
                }
                weight.Add(row);
            }
            weight[0][0] = 0;
            while (true)
            {
                if (!LoopWeight(map, weight))
                {
                    break;
                }
            }
            foreach (List<int> row in weight)
            {
                foreach (int w in row)
                {
                    if (w == 2147483641)
                    {
                        Console.Write("# ");
                    }
                    else { Console.Write(w + " "); }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Steps: " + weight[x][y]);
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int x = 70;
            int y = 70;
            int drops = 1024;
            List<string> map = ComputeDrops(data, x, y, drops);
            foreach (string line in map)
            {
                Console.WriteLine(line);
            }
            bool ispath = true;
            while (ispath)
            {
                Console.WriteLine("drop: " + data[drops]);
                List<List<int>> weight = new List<List<int>>();
                for (int i = 0; i <= y; i++)
                {
                    List<int> row = new List<int>();
                    for (int j = 0; j <= x; j++)
                    {
                        row.Add(2147483641);
                    }
                    weight.Add(row);
                }
                weight[0][0] = 0;
                while (true)
                {
                    if (!LoopWeight(map, weight))
                    {
                        break;
                    }
                }
                if (weight[x][y] == 2147483641)
                {
                    ispath = false;
                    Console.WriteLine("final drop: " + data[drops-1]);
                }
                else
                {
                    map = addDrop(data, map, drops);
                    drops++;
                }
            }
            foreach (string line in map)
            {
                Console.WriteLine(line);
            }
        }
    }
}
