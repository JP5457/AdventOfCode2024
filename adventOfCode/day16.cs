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
namespace Day16
{

    public class Max
    { 
        public static int max = 2147483647;
    }
    public class Day16
    {

        Dictionary<string, int> visited = new Dictionary<string, int>();

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

        static int loopweights(List<List<Dictionary<char, int>>> weights, List<string> data, Dictionary<char, int[]> moves)
        {
            int changes = 0;
            for (int y = 0; y < data.Count(); y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] != '#')
                    {
                        foreach (KeyValuePair<char, int[]> pair in moves)
                        {
                            char direction = pair.Key;
                            int y_ = pair.Value[0];
                            int x_ = pair.Value[1];

                            if (y + y_ >= 0 && y + y_ < data.Count() && x + x_ >= 0 && x + x_ < data[y].Length)
                            {
                                if (data[y + y_][x + x_] != '#')
                                {
                                    if (weights[y + y_][x + x_][direction] + 1 < weights[y][x][direction])
                                    {
                                        weights[y][x][direction] = weights[y + y_][x + x_][direction] + 1;
                                        changes++;
                                    }
                                }
                            }
                        }
                        int lowest = 2147483641;
                        string lowest_direction = "";
                        foreach (KeyValuePair<char, int> vals in weights[y][x])
                        {
                            if (vals.Value < lowest)
                            {
                                lowest = vals.Value;
                                lowest_direction = lowest_direction + vals.Key.ToString();
                            }
                        }
                        foreach (KeyValuePair<char, int> vals in weights[y][x])
                        {
                            int mod = 2000;
                            if ((vals.Key == '^' || vals.Key == 'v') && (lowest_direction.Contains(">") || (lowest_direction.Contains("<")))) { mod = 1000; }
                            if ((vals.Key == '<' || vals.Key == '>') && (lowest_direction.Contains("^") || (lowest_direction.Contains("v")))) { mod = 1000; }
                            if (vals.Value > lowest + mod)
                            {
                                weights[y][x][vals.Key] = lowest + mod;
                                changes++;
                            }
                        }
                    }
                }
            }
            return changes;
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            Dictionary<char, int[]> moves = new Dictionary<char, int[]>()
            {
                { '^', new int[] {-1, 0} },
                { '>', new int[] { 0, 1 } },
                { '<', new int[] { 0, -1 } },
                { 'v', new int[] { 1, 0 } }
            };
            List<List<Dictionary<char, int>>> weights = new List<List<Dictionary<char, int>>>();
            for (int y = 0; y < data.Count(); y++)
            {
                List<Dictionary<char, int>> forweights = new List<Dictionary<char, int>>();
                for (int x = 0; x < data[y].Length; x++)
                {
                    Dictionary<char, int> newdict = new Dictionary<char, int>() { { '^', 2100000000 }, { '>', 2100000000 }, { 'v', 2100000000 }, { '<', 2100000000 }, };
                    if (data[y][x] == 'S')
                    {
                        newdict['<'] = 0;
                    }
                    forweights.Add(newdict);
                }
                weights.Add(forweights);
            }
            int changes = 1;
            while (changes > 0)
            {
                changes = loopweights(weights, data, moves);
                Console.WriteLine("changes: " + changes);
            }
            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] == 'E')
                    {
                        total = weights[y][x].Values.Min();
                    }
                }
            }
            Console.WriteLine(total);
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            Dictionary<char, int[]> moves = new Dictionary<char, int[]>()
            {
                { '^', new int[] {-1, 0} },
                { '>', new int[] { 0, 1 } },
                { '<', new int[] { 0, -1 } },
                { 'v', new int[] { 1, 0 } }
            };
            List<List<Dictionary<char, int>>> weights = new List<List<Dictionary<char, int>>>();
            for (int y = 0; y < data.Count(); y++)
            {
                List<Dictionary<char, int>> forweights = new List<Dictionary<char, int>>();
                for (int x = 0; x < data[y].Length; x++)
                {
                    Dictionary<char, int> newdict = new Dictionary<char, int>() { { '^', 2100000000 }, { '>', 2100000000 }, { 'v', 2100000000 }, { '<', 2100000000 }, };
                    if (data[y][x] == 'S')
                    {
                        newdict['<'] = 0;
                    }
                    forweights.Add(newdict);
                }
                weights.Add(forweights);
            }
            int changes = 1;
            while (changes > 0)
            {
                changes = loopweights(weights, data, moves);
                Console.WriteLine("changes: " + changes);
            }

            int lowest = 2147483641;
            char lowest_direction = '"';
            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] == 'E')
                    {
                        foreach (KeyValuePair<char, int> vals in weights[y][x])
                        {
                            if (vals.Value < lowest)
                            {
                                lowest = vals.Value;
                                lowest_direction = vals.Key;
                            }
                        }
                    }
                }
            }

            int best = lowest + 1000;
            Console.WriteLine(best);

            int seats = 0;

            List<List<Dictionary<char, int>>> eweights = new List<List<Dictionary<char, int>>>();
            for (int y = 0; y < data.Count(); y++)
            {
                List<Dictionary<char, int>> forweights = new List<Dictionary<char, int>>();
                for (int x = 0; x < data[y].Length; x++)
                {
                    Dictionary<char, int> newdict = new Dictionary<char, int>() { { '^', 2100000000 }, { '>', 2100000000 }, { 'v', 2100000000 }, { '<', 2100000000 }, };
                    if (data[y][x] == 'E')
                    {
                        newdict[lowest_direction] = 0;
                    }
                    forweights.Add(newdict);
                }
                eweights.Add(forweights);
            }
            changes = 1;
            while (changes > 0)
            {
                changes = loopweights(eweights, data, moves);
                Console.WriteLine("changes: " + changes);
            }
            List<string> seatmap = new List<string>(data);
            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] != '#')
                    {
                        int wlowest = 2147483641;
                        char wlowest_direction = '"';
                        int elowest = 2147483641;
                        char elowest_direction = '"';
                        foreach (KeyValuePair<char, int> vals in weights[y][x])
                        {
                            if (vals.Value < wlowest)
                            {
                                wlowest = vals.Value;
                                wlowest_direction = vals.Key;
                            }
                        }
                        foreach (KeyValuePair<char, int> vals in eweights[y][x])
                        {
                            if (vals.Value < elowest)
                            {
                                elowest = vals.Value;
                                elowest_direction = vals.Key;
                            }
                        }

                        bool oppositeup = (elowest_direction == '^' && wlowest_direction == 'v') || (elowest_direction == 'v' && wlowest_direction == '^');
                        bool oppositedown = (elowest_direction == '>' && wlowest_direction == '<') || (elowest_direction == '<' && wlowest_direction == '>');
                        bool oppositedir = oppositeup || oppositedown;

                        total = wlowest + elowest;

                        if (total <= best && elowest_direction != wlowest_direction)
                        {
                            Console.WriteLine("total: " + total + " weight: " + wlowest + " eweight:" + elowest + " dir:" + wlowest_direction + " edir:" + elowest_direction);

                            seats++;
                            seatmap[y] = setChar(x, seatmap[y], 'X');
                        }
                    }
                }
            }
            int oops = 0;
            List<string> tooutseatmap = new List<string>(seatmap);
            for (int y = 0; y < seatmap.Count; y++)
            {
                for (int x = 0; x < seatmap[y].Length; x++)
                {
                    if (seatmap[y][x] == 'X')
                    {
                        int neighbours = 0;
                        foreach (int[] i in moves.Values)
                        {
                            if (seatmap[y + i[0]][x + i[1]] == 'X')
                            {
                                neighbours++;
                            }
                        }
                        if (neighbours < 2)
                        {
                            tooutseatmap[y] = setChar(x, tooutseatmap[y], 'O');
                            oops++;
                        }
                    }
                }
            }
            foreach (string s in tooutseatmap)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("seats: " + seats);
            Console.WriteLine("oops: " + oops);
            Console.WriteLine("estimate: " + (seats - oops + 3));
        }
    }
}
