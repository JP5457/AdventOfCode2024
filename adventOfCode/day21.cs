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
using System.Runtime.CompilerServices;
namespace Day21
{

    public class Day21
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

        static string multChar(char tomult, int tomultint)
        {
            string result = "";
            for (int i = 0; i < tomultint; i++)
            {
                result += tomult;
            }
            return result;
        }

        static string getInputs(string code, List<string> numpad, char current)
        {
            string inputs = "";
            foreach (char inp in code)
            {
                int current_x = 0;
                int current_y = 0;
                int new_x = 0;
                int new_y = 0;
                for (int y = 0; y < numpad.Count(); y++)
                {
                    for (int x = 0; x < numpad[y].Length; x++)
                    {
                        if (numpad[y][x] == current)
                        {
                            current_x = x;
                            current_y = y;
                        }
                        if (numpad[y][x] == inp)
                        {
                            new_x = x;
                            new_y = y;
                        }
                    }
                }
                int xdif = new_x - current_x;
                int ydif = new_y - current_y;
                bool hasblankx = false;
                bool hasblanky = false;
                if (xdif < 0)
                {
                    hasblankx = numpad[current_y].Substring(new_x, (current_x - new_x)).Contains('#');
                }
                else
                {
                    hasblankx = numpad[current_y].Substring(current_x, (new_x - current_x)).Contains('#');
                }
                if (ydif < 0)
                {
                    if (numpad[new_y][current_x] == '#') { hasblanky = true; }
                }
                if (hasblankx)
                {
                    if (current_y < new_y)
                    {
                        inputs += multChar('v', new_y - current_y);
                    }
                    if (current_y > new_y)
                    {
                        inputs += multChar('^', current_y - new_y);
                    }
                    if (current_x < new_x)
                    {
                        inputs += multChar('>', new_x - current_x);
                    }
                    if (current_x > new_x)
                    {
                        inputs += multChar('<', current_x - new_x);
                    }
                }
                else if (hasblanky)
                {
                    if (current_x > new_x)
                    {
                        inputs += multChar('<', current_x - new_x);
                    }
                    if (current_x < new_x)
                    {
                        inputs += multChar('>', new_x - current_x);
                    }
                    if (current_y < new_y)
                    {
                        inputs += multChar('v', new_y - current_y);
                    }
                    if (current_y > new_y)
                    {
                        inputs += multChar('^', current_y - new_y);
                    }
                } else
                {
                    if (current_x > new_x)
                    {
                        inputs += multChar('<', current_x - new_x);
                    }
                    if (current_y < new_y)
                    {
                        inputs += multChar('v', new_y - current_y);
                    }
                    if (current_y > new_y)
                    {
                        inputs += multChar('^', current_y - new_y);
                    }
                    if (current_x < new_x)
                    {
                        inputs += multChar('>', new_x - current_x);
                    }
                }
                inputs += "A";
                current = inp;
            }
            return inputs;
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<string> numpad = new List<string> { "789", "456", "123", "#0A" };
            List<string> inputpad = new List<string> { "#^A", "<v>"};
            foreach (string line in data)
            {
                Console.WriteLine("input: " + line);
                string numinputs = getInputs(line, numpad, 'A');
                Console.WriteLine(numinputs);
                string inputinputs = getInputs(numinputs, inputpad, 'A');
                Console.WriteLine(inputinputs);
                string inputinputinputs = getInputs(inputinputs, inputpad, 'A');
                Console.WriteLine(inputinputinputs);
                int inputNum = int.Parse(line.Substring(0, 3));
                int length = inputinputinputs.Length;
                int complexity = length * inputNum;
                Console.WriteLine("inputNum: " + inputNum + " length: " + length);
                Console.WriteLine("complexity: " + complexity);
                total += complexity;
            }
            Console.WriteLine(total);
        }


        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            long total = 0;
            List<string> numpad = new List<string> { "789", "456", "123", "#0A" };
            List<string> inputpad = new List<string> { "#^A", "<v>" };
            Dictionary<string, Dictionary<string, long>> patterns = new Dictionary<string, Dictionary<string, long>>();
            string inps = "^<>vA";
            int depth = 25;
            foreach (string line in data)
            {
                Dictionary<string, long> inppairs = new Dictionary<string, long>();
                string pat = getInputs(line, numpad, 'A');
                string[] pats = pat.Split("A");
                pats = pats.Take(pats.Count() - 1).ToArray();
                foreach (string p in pats)
                {
                    string toadd = p + "A";
                    if (inppairs.ContainsKey(toadd))
                    {
                        inppairs[toadd] += 1;
                    }
                    else
                    {
                        inppairs.Add(toadd, 1);
                    }
                }
                long length = 0;
                long inputNum = long.Parse(line.Substring(0, 3));
                for (int i = 0; i < depth; i++)
                {
                    long templength = 0;
                    foreach (KeyValuePair<string, long> len in inppairs)
                    {
                        templength += len.Key.Length * len.Value;
                    }
                    Console.WriteLine(i + " : " + templength);
                    Dictionary<string, long> pairs = new Dictionary<string, long>(inppairs);
                    Console.WriteLine("depth: " + i);
                    Dictionary<string, long> newpairs = new Dictionary<string, long>();
                    foreach (KeyValuePair<string, long> pair in pairs)
                    {
                        if (!patterns.ContainsKey(pair.Key))
                        {
                            pat = getInputs(pair.Key, inputpad, 'A');
                            pats = pat.Split("A");
                            pats = pats.Take(pats.Count() - 1).ToArray();
                            Dictionary<string, long> forpattern = new Dictionary<string, long>();
                            foreach (string p in pats)
                            {
                                string toadd = p + "A";
                                if (forpattern.ContainsKey(toadd))
                                {
                                    forpattern[toadd] += 1;
                                }
                                else
                                {
                                    forpattern.Add(toadd, 1);
                                }
                            }
                            patterns.Add(pair.Key, forpattern);
                        }
                        foreach (KeyValuePair<string, long> bob in patterns[pair.Key])
                        {
                            if (newpairs.ContainsKey(bob.Key))
                            {
                                newpairs[bob.Key] += bob.Value * pair.Value;
                            }
                            else
                            {
                                newpairs.Add(bob.Key, bob.Value * pair.Value);
                            }
                        }
                    }
                    inppairs = new Dictionary<string, long>(newpairs);
                }
                foreach (KeyValuePair<string, long> len in inppairs)
                {
                    length += len.Key.Length * len.Value;
                }
                long complexity = length * inputNum;
                Console.WriteLine("length: " + length + " number: " + inputNum + " complexity: " + complexity);
                total += complexity;
            }
            Console.WriteLine(total);
        }
    }
}
