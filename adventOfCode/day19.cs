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
namespace Day19
{

    public class Day19
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

        static bool canmake(string pattern, List<string> alex, List<string> seen)
        {
            if ( seen.Contains(pattern))
            {
                return false;
            }
            else
            {
                seen.Add(pattern);
            }
            foreach (string towel in alex)
            {
                if (pattern == towel)
                {
                    return true;
                }
                if (pattern.Length >= towel.Length)
                {
                    if (pattern.Substring(0, towel.Length) == towel)
                    {
                        if (canmake(pattern.Substring(towel.Length), alex, seen)) { return true; }
                    }
                }
            }
            return false;
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<string> alex = new List<string>();
            List<string> patterns = new List<string>();
            bool ispattern = false;
            foreach (string line in data)
            {
                if (line == "") { ispattern = true; }
                else if (ispattern)
                {
                    patterns.Add(line);
                }
                else
                {
                    string[] temp = line.Split(",");
                    foreach (string s in temp)
                    {
                        alex.Add(Regex.Replace(s, @"\s+", ""));
                    }
                }
            }
            foreach (string pattern in patterns)
            {
                List<string> possible = new List<string>();
                foreach (string towel in alex)
                {
                    if (pattern.Contains(towel)) { possible.Add(towel); }
                }
                List<string> seen = new List<string>();
                if (canmake(pattern, possible, seen))
                {
                    total++;
                } else
                {
                }
            }
            Console.WriteLine(total);
        }

        static long countmake(string pattern, List<string> alex, Dictionary<string, long> seen)
        {
            if (seen.ContainsKey(pattern))
            {
                return seen[pattern];
            }
            else
            {
                seen.Add(pattern, 0);
            }
            long count = 0;
            foreach (string towel in alex)
            {
                if (pattern == towel)
                {
                    count++;
                }
                if (pattern.Length >= towel.Length)
                {
                    if (pattern.Substring(0, towel.Length) == towel)
                    {
                        count += countmake(pattern.Substring(towel.Length), alex, seen);
                    }
                }
            }
            seen[pattern] = count;
            return count;
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            long total = 0;
            List<string> alex = new List<string>();
            List<string> patterns = new List<string>();
            bool ispattern = false;
            foreach (string line in data)
            {
                if (line == "") { ispattern = true; }
                else if (ispattern)
                {
                    patterns.Add(line);
                }
                else
                {
                    string[] temp = line.Split(",");
                    foreach (string s in temp)
                    {
                        alex.Add(Regex.Replace(s, @"\s+", ""));
                    }
                }
            }
            foreach (string pattern in patterns)
            {
                List<string> possible = new List<string>();
                foreach (string towel in alex)
                {
                    if (pattern.Contains(towel)) { possible.Add(towel); }
                }
                Dictionary<string, long> seen = new Dictionary<string, long>();
                total += countmake(pattern, possible, seen);
            }
            Console.WriteLine(total);
        }
    }
}
