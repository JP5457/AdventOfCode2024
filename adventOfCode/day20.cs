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
namespace Day20
{

    public class Day20
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

        static void writeWeights(List<List<int>> weights)
        {
            foreach (List<int> row in weights)
            {
                foreach (int weight in row)
                {
                    if (weight < 10)
                    {
                        Console.Write(" " + weight + " ");
                    }
                    else if (weight > (int.MaxValue - 100))
                    {
                        Console.Write("XX ");
                    }
                    else
                    {
                        Console.Write(weight + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        static List<List<int>> getWeights(List<string> map, List<List<int>> weights)
        {
            int endWeight = 0;
            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int y = 0; y < map.Count(); y++)
                {
                    for (int x = 0; x < map[y].Length; x++)
                    {
                        if (map[y][x] != '#')
                        {
                            if (y > 0)
                            {
                                if (map[y - 1][x] != '#')
                                {
                                    if (weights[y - 1][x] + 1 < weights[y][x])
                                    {
                                        weights[y][x] = weights[y - 1][x] + 1;
                                        changed = true;
                                    }
                                }
                            }
                            if (y < map.Count() - 1)
                            {
                                if (map[y + 1][x] != '#')
                                {
                                    if (weights[y + 1][x] + 1 < weights[y][x])
                                    {
                                        weights[y][x] = weights[y + 1][x] + 1;
                                        changed = true;
                                    }
                                }
                            }
                            if (x > 0)
                            {
                                if (map[y][x - 1] != '#')
                                {
                                    if (weights[y][x - 1] + 1 < weights[y][x])
                                    {
                                        weights[y][x] = weights[y][x - 1] + 1;
                                        changed = true;
                                    }
                                }
                            }
                            if (x < map[y].Length - 1)
                            {
                                if (map[y][x + 1] != '#')
                                {
                                    if (weights[y][x + 1] + 1 < weights[y][x])
                                    {
                                        weights[y][x] = weights[y][x + 1] + 1;
                                        changed = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return weights;
        }


        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<List<int>> weights = new List<List<int>>();
            int[] end = new int[2];
            Dictionary<int, int> cheats = new Dictionary<int, int>();
            List<int[]> directions = new List<int[]> { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            for (int y = 0; y < data.Count(); y++)
            {
                List<int> toweight = new List<int>();
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] == 'S')
                    {
                        toweight.Add(0);
                    }
                    else
                    {
                        toweight.Add(int.MaxValue - 10);
                    }
                    if (data[y][x] == 'E')
                    {
                        end[0] = y;
                        end[1] = x;
                    }
                }
                weights.Add(toweight);
            }
            weights = getWeights(data, weights);
            int endWeight = weights[end[0]][end[1]];
            for (int y = 1; y < data.Count()-1; y++)
            {
                Console.WriteLine("y: " + y + "out of " + (data.Count()-1));
                for (int x = 1; x < data[y].Length-1; x++)
                {
                    if (data[y][x] == '#')
                    {
                        List<int> nearweights = new List<int>();
                        foreach (int[] direction in directions)
                        {
                            int y_ = direction[0];
                            int x_ = direction[1];
                            if (data[y + y_][x + x_] != '#')
                            {
                                nearweights.Add(weights[y + y_][x + x_]);
                            }
                        }
                        nearweights.Sort();
                        nearweights.Reverse();
                        if (nearweights.Count() > 1)
                        {
                            for (int i = 0; i < nearweights.Count() - 1; i++)
                            {
                                for (int j = i + 1; j < nearweights.Count(); j++)
                                {
                                    int loss = nearweights[i] - nearweights[j] - 2;
                                    if (loss > 0)
                                    {
                                        if (cheats.Keys.Contains(loss))
                                        {
                                            cheats[loss]++;
                                        }
                                        else
                                        {
                                            cheats.Add(loss, 1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            List<int> keys = cheats.Keys.ToList();
            keys.Sort();
            foreach (int key in keys)
            {
                Console.WriteLine("saved: " + key + " count: " + cheats[key]);
                if (key >= 100) { total += cheats[key]; }
            }
            Console.WriteLine("total: " + total);
        }


        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<List<int>> weights = new List<List<int>>();
            int[] end = new int[2];
            Dictionary<int, int> cheats = new Dictionary<int, int>();
            List<int[]> directions = new List<int[]> { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            for (int y = 0; y < data.Count(); y++)
            {
                List<int> toweight = new List<int>();
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] == 'S')
                    {
                        toweight.Add(0);
                    }
                    else
                    {
                        toweight.Add(int.MaxValue - 10);
                    }
                    if (data[y][x] == 'E')
                    {
                        end[0] = y;
                        end[1] = x;
                    }
                }
                weights.Add(toweight);
            }
            weights = getWeights(data, weights);
            int endWeight = weights[end[0]][end[1]];
            for (int y = 1; y < data.Count() - 1; y++)
            {
                Console.WriteLine("y: " + y + "out of " + (data.Count() - 1));
                for (int x = 1; x < data[y].Length - 1; x++)
                {
                    if (data[y][x] != '#')
                    {
                        int ymax = 21;
                        int ymin = -21;
                        if (y + ymax > data.Count()) { ymax = data.Count()-y; }
                        if (y + ymin < 0) { ymin = 0 - y; }
                        for (int y_ = ymin; y_ < ymax; y_++)
                        {
                            int xmax = 21;
                            int xmin = -21;
                            if (x + xmax > data[y].Length) { xmax = data[y].Length-x; }
                            if (x + xmin < 0) { xmin = 0 - x; }
                            for (int x_ = xmin; x_ < xmax; x_++)
                            {
                                if (data[y + y_][x + x_] != '#')
                                {
                                    int distance = Math.Abs(x_) + Math.Abs(y_);
                                    if (weights[y][x] + distance < weights[y + y_][x + x_] && distance <= 20)
                                    {
                                        int loss = weights[y + y_][x + x_] - (weights[y][x] + distance);
                                        if (loss > 0)
                                        {
                                            if (cheats.Keys.Contains(loss))
                                            {
                                                cheats[loss]++;
                                            }
                                            else
                                            {
                                                cheats.Add(loss, 1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            List<int> keys = cheats.Keys.ToList();
            keys.Sort();
            foreach (int key in keys)
            {
                Console.WriteLine("there are : " + cheats[key] + " for key: " + key);
                if (key >= 100) { total += cheats[key]; }
            }
            Console.WriteLine("total: " + total);
        }
    }
}
