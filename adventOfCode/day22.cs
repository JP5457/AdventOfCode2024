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
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;
namespace Day22
{

    public class Day22
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

        static long prune(long num)
        {
            return num % 16777216;
        }

        static long mix(long num1, long num2)
        {
            return num1 ^ num2;
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            long total = 0;
            int depth = 2000;
            foreach (string line in data)
            {
                long secret = long.Parse(line);
                for (int i = 0; i < depth; i++)
                {
                    secret = mix(secret, secret * 64);
                    secret = prune(secret);
                    secret = mix(secret, (long)(secret/32));
                    secret = prune(secret);
                    secret = mix(secret, secret * 2048);
                    secret = prune(secret);
                }
                Console.WriteLine(secret);
                total += secret;
            }
            Console.WriteLine("total: " + total);
        }


        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            long total = 0;
            int depth = 2000;
            long best = 0;
            List<int> best_changes = new List<int> { -9, -9, -9, -9 };
            List<int> changesequ = new List<int> { -9, -9, -9, -9};
            bool doloop = true;
            while (doloop)
            {
                int bananas = 0;
                foreach (string line in data)
                {
                    long secret = long.Parse(line);
                    int value = (int)(secret % 10);
                    List<int> changes = new List<int>();
                    for (int i = 0; i < depth; i++)
                    {
                        secret = mix(secret, secret * 64);
                        secret = prune(secret);
                        secret = mix(secret, (long)(secret / 32));
                        secret = prune(secret);
                        secret = mix(secret, secret * 2048);
                        secret = prune(secret);
                        int newvalue = (int)(secret%10);
                        int change = newvalue - value;
                        changes.Add(change);
                        value = newvalue;
                        if (change == changesequ[3])
                        {
                            if (changes.Count() >= 4)
                            {
                                List<int> recent = changes.GetRange(changes.Count() - 4, 4);
                                if (Enumerable.SequenceEqual(recent, changesequ))
                                {
                                    bananas += value;
                                    break;
                                }
                            }
                        }
                    }
                    total += secret;
                }
                if(bananas > best)
                {
                    best = bananas;
                    best_changes = new List<int>(changesequ);
                    foreach (int j in changesequ) { Console.Write(j + ","); }
                    Console.WriteLine("had best: " + best);
                }
                for (int j = 3; j >= 0; j--)
                {
                    if (j == 0 && changesequ[j] == 9)
                    {
                        doloop = false;
                    }
                    else if (changesequ[j] < 9)
                    {
                        changesequ[j]++;
                        break;
                    }
                    else
                    {
                        changesequ[j] = -9;
                    }
                }
            }
            Console.WriteLine("total: " + best + " with: ");
            foreach (int j in best_changes) { Console.Write(j + ","); }
        }
    }
}
