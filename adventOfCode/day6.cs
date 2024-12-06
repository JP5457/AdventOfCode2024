using System.Collections;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Text.RegularExpressions;
namespace Day6
{
    public class Day6
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

        static void printData(List<string> data)
        {
            foreach (string line in data)
            {
                Console.WriteLine(line);
            }
        }

        public static List<string> Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            bool left = false;
            List<ArrayList> movement = new List<ArrayList>();
            movement.Add(new ArrayList() { '^', -1, 0, '>' });
            movement.Add(new ArrayList() { '>', 0, 1 , 'v' });
            movement.Add(new ArrayList() { 'v', 1, 0 , '<'});
            movement.Add(new ArrayList() { '<', 0, -1, '^' });
            while (!left)
            {
                for (int y = 0; y < data.Count; y++)
                {
                    foreach (ArrayList direction in movement)
                    {
                        if (data[y].Contains((char)direction[0]))
                        {
                            int x = data[y].IndexOf((char)direction[0]);
                            int y_ = (int)direction[1];
                            int x_ = (int)direction[2];
                            data[y] = setChar(x, data[y], 'X');
                            if (y + y_ >= 0 && y + y_ < data.Count && x + x_ >= 0 && x + x_ < data[0].Length)
                            {
                                if (data[y + y_][x + x_] == '#')
                                {
                                    data[y] = setChar(x, data[y], (char)direction[3]);
                                    break;
                                } else
                                {
                                    data[y + y_] = setChar(x + x_, data[y + y_], (char)direction[0]);
                                    data[y] = setChar(x, data[y], 'X');
                                    break;
                                }
                            }
                            else
                            {
                                data[y] = setChar(x, data[y], 'X');
                                left = true;
                            }
                        }
                    }
                }
            }
            foreach (string line in data)
            {
                total += line.Count(x => x == 'X');
            }
            Console.WriteLine(total);
            return data;
        }

        static bool isLoop(List<string> data)
        {
            List<ArrayList> movement = new List<ArrayList>();
            movement.Add(new ArrayList() { '^', -1, 0, '>', 'w'});
            movement.Add(new ArrayList() { '>', 0, 1, 'v', 'd'});
            movement.Add(new ArrayList() { 'v', 1, 0, '<', 's'});
            movement.Add(new ArrayList() { '<', 0, -1, '^', 'a'});
            int steps = 0;
            int steplim = data.Count * data[0].Length;
            while (true)
            {
                for (int y = 0; y < data.Count; y++)
                {
                    foreach (ArrayList direction in movement)
                    {
                        if (data[y].Contains((char)direction[0]))
                        {
                            steps++;
                            if (steps > steplim)
                            {
                                return true;
                            }
                            int x = data[y].IndexOf((char)direction[0]);
                            int y_ = (int)direction[1];
                            int x_ = (int)direction[2];
                            data[y] = setChar(x, data[y], 'X');
                            if (y + y_ >= 0 && y + y_ < data.Count && x + x_ >= 0 && x + x_ < data[0].Length)
                            {
                                if (data[y + y_][x + x_] == '#')
                                {
                                    data[y] = setChar(x, data[y], (char)direction[3]);
                                    break;
                                } else if (data[y + y_][x + x_] == (char)direction[4])
                                {
                                    return true;
                                } else
                                {
                                    data[y + y_] = setChar(x + x_, data[y + y_], (char)direction[0]);
                                    data[y] = setChar(x, data[y], (char)direction[4]);
                                    break;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }



        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            List<string> path = Task1();
            int total = 0;
            for (int y = 0; y < data.Count; y++)
            {
                for (int x = 0; x < data[0].Length; x++)
                {
                    if (data[y][x] == '.' && path[y][x] == 'X') { 
                        List<string> temp = new List<string>(data);
                        temp[y] = setChar(x, temp[y], '#');
                        bool looped = isLoop(temp);
                        if (looped) {
                            Console.WriteLine("found loop at " + y + " " + x);
                        }
                        total += looped ? 1 : 0;
                    }
                }
            }
            Console.WriteLine(total);
        }
    }
}
