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
namespace Day10
{
    public class Day10
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

        static int valueOfHead(List<string> data, int[] head)
        {
            List<string> reached = new List<string>(data);
            reached[head[0]] = setChar(head[1], reached[head[0]], '#');
            int score = 0;
            bool changed = true;
            while (changed) {
                changed = false;
                for (int y = 0; y < data.Count(); y++)
                {
                    for (int x = 0; x < data[y].Length; x++)
                    {
                        if (reached[y][x] == '#')
                        {
                            int val = int.Parse(data[y][x].ToString());
                            if (y > 0)
                            {
                                if (data[y - 1][x] != '-' && reached[y - 1][x] != '#')
                                {
                                    if (int.Parse(data[y - 1][x].ToString()) == val + 1)
                                    {
                                        reached[y - 1] = setChar(x, reached[y - 1], '#');
                                        changed = true;
                                    }
                                }
                            }
                            if (y < data.Count() - 1)
                            {
                                if (data[y + 1][x] != '-' && reached[y + 1][x] != '#')
                                {
                                    if (int.Parse(data[y + 1][x].ToString()) == val + 1)
                                    {
                                        reached[y + 1] = setChar(x, reached[y + 1], '#');
                                        changed = true;
                                    }
                                }
                            }
                            if (x > 0)
                            {
                                if (data[y][x - 1] != '-' && reached[y][x - 1] != '#')
                                {
                                    if (int.Parse(data[y][x - 1].ToString()) == val + 1)
                                    {
                                        reached[y] = setChar(x - 1, reached[y], '#');
                                        changed = true;
                                    }
                                }
                            }
                            if (x < data[y].Length - 1)
                            {
                                if (data[y][x + 1] != '-' && reached[y][x + 1] != '#')
                                {
                                    if (int.Parse(data[y][x + 1].ToString()) == val + 1)
                                    {
                                        reached[y] = setChar(x + 1, reached[y], '#');
                                        changed = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int y = 0; y < data.Count(); y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (reached[y][x] == '#' && data[y][x] == '9')
                    {
                        score++;
                    }
                }
            }
            return score;
        }


        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            for (int y = 0; y < data.Count(); y++)
            {
                while (data[y].IndexOf('0') >= 0)
                {
                    int x = data[y].IndexOf('0');
                    total += valueOfHead(data, [y, x]);
                    data[y] = setChar(x, data[y], '-');
                }
            }
            Console.WriteLine(total);
        }

        static int ratingOfHead(List<string> data, int[] point)
        {
            int total = 0;
            int y = point[0];
            int x = point[1];
            int val = int.Parse(data[y][x].ToString());
            if (y > 0)
            {
                if (data[y - 1][x] != '-')
                {
                    if (int.Parse(data[y - 1][x].ToString()) == val + 1)
                    {
                        if (val == 8) { total += 1; }
                        else
                        {
                            total += ratingOfHead(data, [y - 1, x]);
                        }
                    }
                }
            }
            if (y < data.Count() - 1)
            {
                if (data[y + 1][x] != '-')
                {
                    if (int.Parse(data[y + 1][x].ToString()) == val + 1)
                    {
                        if (val == 8) { total += 1; }
                        else
                        {
                            total += ratingOfHead(data, [y + 1, x]);
                        }
                    }
                }
            }
            if (x > 0)
            {
                if (data[y][x - 1] != '-')
                {
                    if (int.Parse(data[y][x - 1].ToString()) == val + 1)
                    {
                        if (val == 8) { total += 1; }
                        else
                        {
                            total += ratingOfHead(data, [y, x - 1]);
                        }
                    }
                }
            }
            if (x < data[y].Length - 1)
            {
                if (data[y][x + 1] != '-')
                {
                    if (int.Parse(data[y][x + 1].ToString()) == val + 1)
                    {
                        if (val == 8) { total += 1; }
                        else
                        {
                            total += ratingOfHead(data, [y, x+1]);
                        }
                    }
                }
            }
            return total;
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            for (int y = 0; y < data.Count(); y++)
            {
                while (data[y].IndexOf('0') >= 0)
                {
                    int x = data[y].IndexOf('0');
                    total += ratingOfHead(data, [y, x]);
                    data[y] = setChar(x, data[y], '-');
                }
            }
            Console.WriteLine(total);
        }
    }
}
