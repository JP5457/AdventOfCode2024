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
namespace Day14
{
    public class Day14
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
            int sizex = 101;
            int sizey = 103;
            List<Dictionary<char, int[]>> robots = new List<Dictionary<char, int[]>>();
            //parses the input into a list of dicts
            foreach (string line in data)
            {
                Dictionary<char, int[]> newRobot = new Dictionary<char, int[]>();
                string[] split = line.Split(' ');
                string[] pos = split[0].Split('=');
                string[] coords = pos[1].Split(',');
                newRobot.Add('P', new int[] { int.Parse(coords[0]), int.Parse(coords[1]) });
                string[] vel = split[1].Split('=');
                string[] mags = vel[1].Split(',');
                newRobot.Add('V', new int[] { int.Parse(mags[0]), int.Parse(mags[1]) });
                robots.Add(newRobot);
            }
            foreach(Dictionary<char, int[]>  robot in robots)
            {
                for (int i = 0; i < 100; i++)
                {
                    robot['P'][0] += robot['V'][0];
                    robot['P'][1] += robot['V'][1];
                    if (robot['P'][0] >= sizex) { robot['P'][0] -= sizex; };
                    if (robot['P'][1] >= sizey) { robot['P'][1] -= sizey; };
                    if (robot['P'][0] < 0) { robot['P'][0] = sizex + robot['P'][0]; };
                    if (robot['P'][1] < 0) { robot['P'][1] = sizey + robot['P'][1]; };
                }
            }
            int q1, q2, q3, q4;
            q1 = q2 = q3 = q4 = 0;
            foreach (Dictionary<char, int[]> robot in robots)
            {
                if (robot['P'][0] < (sizex-1) / 2 && robot['P'][1] < (sizey-1) / 2) { q1++; }
                if (robot['P'][0] > (sizex - 1) / 2 && robot['P'][1] < (sizey - 1) / 2) { q2++; }
                if (robot['P'][0] < (sizex - 1) / 2 && robot['P'][1] > (sizey - 1) / 2) { q3++; }
                if (robot['P'][0] > (sizex - 1) / 2 && robot['P'][1] > (sizey - 1) / 2) { q4++; }
            }
            Console.WriteLine(q1 + " " + q2 + " " + q3 + " " + q4);
            Console.WriteLine(q1 * q2 * q3 * q4);
        }

        static string setChar(int position, string text, char toSet)
        {
            char[] temp = text.ToCharArray();
            temp[position] = toSet;
            return new string(temp);
        }

        public static List<string> writeRobots(List<Dictionary<char, int[]>> robots, int sizex, int sizey) 
        {
            List<string> grid = new List<string>();
            string line = "";
            for(int x =0; x < sizex; x++)
            {
                line = line + ".";
            }
            for (int y = 0; y < sizey; y++)
            {
                grid.Add(line);
            }
            foreach (Dictionary<char, int[]> robot in robots)
            {
                grid[robot['P'][1]] = setChar(robot['P'][0], grid[robot['P'][1]], '#');
            }
            return grid;
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            int sizex = 101;
            int sizey = 103;
            List<Dictionary<char, int[]>> robots = new List<Dictionary<char, int[]>>();
            //parses the input into a list of dicts
            foreach (string line in data)
            {
                Dictionary<char, int[]> newRobot = new Dictionary<char, int[]>();
                string[] split = line.Split(' ');
                string[] pos = split[0].Split('=');
                string[] coords = pos[1].Split(',');
                newRobot.Add('P', new int[] { int.Parse(coords[0]), int.Parse(coords[1]) });
                string[] vel = split[1].Split('=');
                string[] mags = vel[1].Split(',');
                newRobot.Add('V', new int[] { int.Parse(mags[0]), int.Parse(mags[1]) });
                robots.Add(newRobot);
            }
            int seconds = 0;
            while (true)
            {
                Console.WriteLine("seconds:" + seconds);
                List<string> grid = writeRobots(robots, sizex, sizey);
                foreach (string row in grid)
                {
                    if (row.Contains("##########"))
                    {
                        foreach (string row_ in grid)
                        {
                            Console.WriteLine(row_);
                        }
                        Console.ReadLine();
                        break;
                    }
                }
                foreach (Dictionary<char, int[]> robot in robots)
                {
                    robot['P'][0] += robot['V'][0];
                    robot['P'][1] += robot['V'][1];
                    if (robot['P'][0] >= sizex) { robot['P'][0] -= sizex; };
                    if (robot['P'][1] >= sizey) { robot['P'][1] -= sizey; };
                    if (robot['P'][0] < 0) { robot['P'][0] = sizex + robot['P'][0]; };
                    if (robot['P'][1] < 0) { robot['P'][1] = sizey + robot['P'][1]; };
                }
                seconds++;
            }
        }
    }
}
