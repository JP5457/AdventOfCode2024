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
            List<Dictionary<string, int>> changevalues = new List<Dictionary<string, int>>();
            foreach (string line in data)
            {
                Dictionary<string, int> changepairs = new Dictionary<string, int>();
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
                    if (changes.Count() >= 4)
                    {
                        string key = "";
                        List<int> recent = changes.GetRange(changes.Count() - 4, 4);
                        foreach(int rec in recent)
                        {
                            key = key + rec + ",";
                        }
                        if (!changepairs.ContainsKey(key)) { changepairs.Add(key, value); }
                    }
                }
                changevalues.Add(changepairs);
            }
            int best = 0;
            string beststr = "";
            for (int a = -9; a <= 9; a++)
            {
                for (int b = -9; b <= 9; b++)
                {
                    for (int c = -9; c <= 9; c++)
                    {
                        for (int d = -9; d <= 9; d++)
                        {
                            int bananas = 0;
                            string seqkey = a + "," + b + "," + c + "," + d + ",";
                            foreach(Dictionary<string, int> chvals in changevalues)
                            {
                                if (chvals.ContainsKey(seqkey)) 
                                {
                                    bananas += chvals[seqkey];
                                }
                            }
                            if (bananas > best) { 
                                best = bananas;
                                beststr = seqkey;
                                Console.WriteLine(beststr + " : " + best);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("best: " + best + " with " + beststr);
        }
    }
}
