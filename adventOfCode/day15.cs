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
namespace Day15
{
    public class Day15
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

        static List<string> moveRobot(List<string> map, char move)
        {
            Dictionary<char, int[]> moves = new Dictionary<char, int[]>()
            {
                { '^', new int[] {-1, 0} },
                { '>', new int[] { 0, 1 } },
                { '<', new int[] { 0, -1 } },
                { 'v', new int[] { 1, 0 } }
            };

            for (int y = 0; y < map.Count; y++)
            {
                if (map[y].Contains("@"))
                {
                    int x = map[y].IndexOf("@");
                    int y_ = moves[move][0];
                    int x_ = moves[move][1];
                    int xmove = x;
                    int ymove = y;
                    string tomove = ".";
                    while (true)
                    {
                        tomove = tomove + map[ymove][xmove].ToString();
                        ymove += y_;
                        xmove += x_;
                        if (map[ymove][xmove] == '#')
                        {
                            tomove = "";
                            break;
                        }
                        if (map[ymove][xmove] == '.') { break; }
                    }
                    xmove = x;
                    ymove = y;
                    if (tomove != "")
                    {
                        foreach (char push in tomove)
                        {
                            map[ymove] = setChar(xmove, map[ymove], push);
                            ymove += y_;
                            xmove += x_;
                        }
                    }
                    break;
                }
            }
            return map;
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<string> map = new List<string>();
            string instruction = "";
            bool addMap = true;
            foreach (string line in data)
            {
                if (addMap) { map.Add(line); } else { instruction = instruction + line; };
                if (line == "") { addMap = false; }
            }
            foreach (char move in instruction)
            {
                map = moveRobot(map, move);
                //Console.WriteLine("move:" + move);
                //foreach (string row in map)
                //{
                //    Console.WriteLine(row);
                //}
                //Console.WriteLine("");
                //Thread.Sleep(1000);

            }
            foreach (string row in map)
            {
                Console.WriteLine(row);
            }
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 'O')
                    {
                        total += (100 * y) + x;
                    }
                }
            }
            Console.WriteLine(total);
        }

        static List<string> moveRobot2(List<string> map, char move)
        {
            Dictionary<char, int[]> moves = new Dictionary<char, int[]>()
            {
                { '^', new int[] {-1, 0} },
                { '>', new int[] { 0, 1 } },
                { '<', new int[] { 0, -1 } },
                { 'v', new int[] { 1, 0 } }
            };

            for (int y = 0; y < map.Count; y++)
            {
                if (map[y].Contains("@"))
                {
                    int x = map[y].IndexOf("@");
                    int y_ = moves[move][0];
                    int x_ = moves[move][1];
                    if (y_ == 0)
                    {
                        int xmove = x;
                        int ymove = y;
                        string tomove = ".";
                        while (true)
                        {
                            tomove = tomove + map[ymove][xmove].ToString();
                            ymove += y_;
                            xmove += x_;
                            if (map[ymove][xmove] == '#')
                            {
                                tomove = "";
                                break;
                            }
                            if (map[ymove][xmove] == '.') { break; }
                        }
                        xmove = x;
                        ymove = y;
                        if (tomove != "")
                        {
                            foreach (char push in tomove)
                            {
                                map[ymove] = setChar(xmove, map[ymove], push);
                                ymove += y_;
                                xmove += x_;
                            }
                        }
                        break;
                    } else
                    {
                        int xmove = x;
                        int ymove = y;
                        List<List<ArrayList>> slices = new List<List<ArrayList>>();
                        List<ArrayList> slice = new List<ArrayList>();
                        // current value, to set value, y, x
                        slice.Add(new ArrayList() { '@', '.', ymove, xmove });
                        slices.Add(slice);
                        bool hitWall = false;
                        bool hitBox = true;
                        while (hitBox)
                        {
                            hitBox = false;
                            slice = new List<ArrayList>();
                            List<ArrayList> LastSlice = slices[slices.Count - 1];
                            foreach (ArrayList last in LastSlice)
                            {
                                if ((char)last[0] == '.')
                                {
                                    continue;
                                }
                                int thisy = (int)last[2] + y_;
                                int thisx = (int)last[3] + x_;
                                slice.Add(new ArrayList() { map[thisy][thisx], last[0], thisy, thisx });
                                if (map[thisy][thisx] == '#')
                                {
                                    hitWall = true;
                                    break;
                                }
                                if (map[thisy][thisx] == '[')
                                {
                                    hitBox = true;
                                    if (map[thisy][thisx+1] == ']')
                                    {
                                        slice.Add(new ArrayList() { map[thisy][thisx+1], '.', thisy, thisx+1 });
                                    }
                                }
                                if (map[thisy][thisx] == ']')
                                {
                                    hitBox = true;
                                    if (map[thisy][thisx - 1] == '[')
                                    {
                                        slice.Add(new ArrayList() { map[thisy][thisx - 1], '.', thisy, thisx - 1 });
                                    }
                                }
                            }
                            slices.Add(slice);
                        }
                        if (!hitWall)
                        {
                            foreach (List<ArrayList> doslice in slices)
                            {
                                foreach (ArrayList doarray in doslice)
                                {
                                    if ((char)doarray[1] == '.')
                                    {
                                        map[(int)doarray[2]] = setChar((int)doarray[3], map[(int)doarray[2]], (char)doarray[1]);
                                    }
                                }
                                foreach (ArrayList doarray in doslice)
                                {
                                    if ((char)doarray[1] != '.')
                                    {
                                        map[(int)doarray[2]] = setChar((int)doarray[3], map[(int)doarray[2]], (char)doarray[1]);
                                    }
                                }
                            }
                        }
                    }
                    break;
                }
            }
            return map;
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<string> map = new List<string>();
            string instruction = "";
            bool addMap = true;
            foreach (string line in data)
            {
                if (line == "") { addMap = false; }
                if (addMap) {
                    string newLine = "";
                    foreach (char c in line)
                    {
                        switch (c)
                        {
                            case '#':
                                newLine = newLine + "##";
                                break;
                            case '.':
                                newLine = newLine + "..";
                                break;
                            case 'O':
                                newLine = newLine + "[]";
                                break;
                            case '@':
                                newLine = newLine + "@.";
                                break;
                        }
                    }
                    map.Add(newLine);
                } else { instruction = instruction + line; };
            }
            foreach (char move in instruction)
            {
                map = moveRobot2(map, move);
            }
            foreach (string row in map)
            {
                Console.WriteLine(row);
            }
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == '[')
                    {
                        total += (100 * y) + x;
                    }
                }
            }
            Console.WriteLine(total);
        }
    }
}
