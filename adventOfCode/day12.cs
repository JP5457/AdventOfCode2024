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
namespace Day12
{
    public class Day12
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

        static bool containsArray(List<int[]> locations, int[] coords)
        {
            foreach (int[] location in locations)
            {
                if (location[0] == coords[0] && location[1] == coords[1])
                {
                    return true;
                }
            }
            return false;
        }

        static List<int[]> getRegion(List<string> data, int[] start)
        {
            List<int[]> locations = new List<int[]>();
            locations.Add(start);
            char val = data[start[0]][start[1]];
            bool added = true;
            while (added)
            {
                added = false;
                int locNum = locations.Count();
                for (int i = 0; i < locNum; i++)
                {
                    int y = locations[i][0];
                    int x = locations[i][1];
                    if (y > 0)
                    {
                        if (data[y - 1][x] == val && !containsArray(locations,[y - 1, x]))
                        {
                            locations.Add([y - 1, x]);
                            added = true;
                        }
                    }
                    if (y < data.Count() - 1)
                    {
                        if (data[y + 1][x] == val && !containsArray(locations, [y + 1, x]))
                        {
                            locations.Add([y + 1, x]);
                            added = true;
                        }
                    }
                    if (x > 0)
                    {
                        if (data[y][x - 1] == val && !containsArray(locations, [y, x - 1]))
                        {
                            locations.Add([y, x - 1]);
                            added = true;
                        }
                    }
                    if (x < data[y].Length - 1)
                    {
                        if (data[y][x + 1] == val && !containsArray(locations, [y, x+1]))
                        {
                            locations.Add([y, x + 1]);
                            added = true;
                        }
                    }
                }
            }
            return locations; 
        }

        static int getPerimiter(List<string> data, List<int[]> locations)
        {
            char val = data[locations[0][0]][locations[0][1]];
            int total = 0;
            foreach (int[] location in locations)
            {
                int y = location[0];
                int x = location[1];
                if (y > 0)
                {
                    if (data[y - 1][x] != val)
                    {
                        total++;
                    }
                }
                else { total++; }
                if (y < data.Count() - 1)
                {
                    if (data[y + 1][x] != val)
                    {
                        total++;
                    }
                }
                else { total++; }
                if (x > 0)
                {
                    if (data[y][x - 1] != val)
                    {
                        total++;
                    }
                }
                else { total++; }
                if (x < data[y].Length - 1)
                {
                    if (data[y][x + 1] != val)
                    {
                        total++;
                    }
                }
                else { total++; }
            }
            return total;
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            for (int y = 0; y < data.Count(); y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] != '#')
                    {
                        List<int[]> locations = getRegion(data, [y, x]);
                        int area = locations.Count();
                        int perimeter = getPerimiter(data, locations);
                        Console.WriteLine(data[y][x] + ": " + (area * perimeter));
                        foreach (int[] location in locations)
                        {
                            data[location[0]] = setChar(location[1], data[location[0]], '#');
                        }
                        total += area * perimeter;
                    }
                }
            }
            Console.WriteLine(total);
        }


        static int getSides(List<string> data, List<int[]> locations)
        {
            //make an array with all the locations being a . in the most inneficient way possible
            char val = data[locations[0][0]][locations[0][1]];
            List<string> thingy = new List<string>(data);
            int sides = 0;
            foreach (int[] location in locations)
            {
                thingy[location[0]] = setChar(location[1], thingy[location[0]], '.');
            }

            //count top sides
            for(int y = 0; y < thingy.Count; y++)
            {
                bool side = false;
                for(int x = 0; x < thingy[y].Length; x++)
                {
                    if(thingy[x][y] == '.')
                    {
                        if (y == 0) 
                        { 
                            if (!side) { sides++; side = true; }
                        } else if (thingy[x][y - 1] != '.')
                        {
                            if (!side) { sides++; side = true; }
                        } else
                        {
                            side = false;
                        }
                    } else
                    {
                        side = false;
                    }
                }
            }

            //count bottom sides
            for (int y = 0; y < thingy.Count; y++)
            {
                bool side = false;
                for (int x = 0; x < thingy[y].Length; x++)
                {
                    if (thingy[x][y] == '.')
                    {
                        if (y == data.Count()-1)
                        {
                            if (!side) { sides++; side = true; }
                        }
                        else if (thingy[x][y + 1] != '.')
                        {
                            if (!side) { sides++; side = true; }
                        }
                        else
                        {
                            side = false;
                        }
                    }
                    else
                    {
                        side = false;
                    }
                }
            }

            //count left? sides
            for (int x = 0; x < thingy[0].Length; x++)
            {
                bool side = false;
                for (int y = 0; y < thingy.Count(); y++)
                {
                    if (thingy[x][y] == '.')
                    {
                        if (x == 0)
                        {
                            if (!side) { sides++; side = true; }
                        }
                        else if (thingy[x - 1][y] != '.')
                        {
                            if (!side) { sides++; side = true; }
                        }
                        else
                        {
                            side = false;
                        }
                    }
                    else
                    {
                        side = false;
                    }
                }
            }

            //count the other sides
            for (int x = 0; x < thingy[0].Length; x++)
            {
                bool side = false;
                for (int y = 0; y < thingy.Count(); y++)
                {
                    if (thingy[x][y] == '.')
                    {
                        if (x == thingy[0].Length-1)
                        {
                            if (!side) { sides++; side = true; }
                        }
                        else if (thingy[x + 1][y] != '.')
                        {
                            if (!side) { sides++; side = true; }
                        }
                        else
                        {
                            side = false;
                        }
                    }
                    else
                    {
                        side = false;
                    }
                }
            }

            return sides;
        }


        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            for (int y = 0; y < data.Count(); y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (data[y][x] != '#')
                    {
                        List<int[]> locations = getRegion(data, [y, x]);
                        int area = locations.Count();
                        int sides = getSides(data, locations);
                        Console.WriteLine(data[y][x] + ": " + (area * sides));
                        foreach (int[] location in locations)
                        {
                            data[location[0]] = setChar(location[1], data[location[0]], '#');
                        }
                        total += area * sides;
                    }
                }
            }
            Console.WriteLine(total);
        }
    }
}
