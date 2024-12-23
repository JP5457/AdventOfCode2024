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
using System.Xml.Linq;
using System.Collections.Immutable;
namespace Day23
{

    public class Day23
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
            long total = 0;
            List<string> lans = new List<string>();
            Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
            foreach (string line in data)
            {
                string[] strings = line.Split('-');
                if (connections.ContainsKey(strings[0]))
                {
                    connections[strings[0]].Add(strings[1]);
                }
                else { connections.Add(strings[0], new List<string> { strings[1] }); }
                if (connections.ContainsKey(strings[1]))
                {
                    connections[strings[1]].Add(strings[0]);
                }
                else { connections.Add(strings[1], new List<string> { strings[0] }); }
            }
            foreach (KeyValuePair<string, List<string>> connection in connections)
            {
                if (connection.Key[0] == 't')
                {
                    foreach (string device1 in connection.Value)
                    {
                        var common = connection.Value.Intersect(connections[device1]);
                        foreach(var final in common)
                        {
                            List<string> toadd = new List<string> { connection.Key, device1, final.ToString() };
                            toadd.Sort();
                            string foradd = toadd[0] + "," + toadd[1] + "," + toadd[2];
                            if (!lans.Contains(foradd)) { lans.Add(foradd); }
                        }
                    }
                }
            }
            foreach(string lan in lans)
            {
                Console.WriteLine(lan);
            }
            Console.WriteLine("total: " + lans.Count());
        }

        static List<string> getmaximal(Dictionary<string, List<string>> connections, List<string> clique, List<string> totest)
        {
            clique.Sort();
            //Console.WriteLine(String.Join(",", clique));
            List<string> myclique = new List<string>(clique);
            List<string> newtotest = new List<string>(totest);
            foreach (string node in totest)
            {
                var sims = connections[node].Intersect(clique).ToList();
                sims.Sort();
                newtotest.Remove(node);
                if (Enumerable.SequenceEqual(clique, sims))
                {
                    List<string> newclique = new List<string>(clique);
                    newclique.Add(node);
                    List<string> maximal = getmaximal(connections, newclique, newtotest);
                    if (maximal.Count() > myclique.Count) { 
                        myclique = new List<string>(maximal);
                    }
                }
            }
            return myclique;
        }


        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            long total = 0;
            List<string> lans = new List<string>();
            Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
            foreach (string line in data)
            {
                string[] strings = line.Split('-');
                if (connections.ContainsKey(strings[0]))
                {
                    connections[strings[0]].Add(strings[1]);
                }
                else { connections.Add(strings[0], new List<string> { strings[1] }); }
                if (connections.ContainsKey(strings[1]))
                {
                    connections[strings[1]].Add(strings[0]);
                }
                else { connections.Add(strings[1], new List<string> { strings[0] }); }
            }
            foreach (string node1 in connections.Keys)
            {
                Console.WriteLine(node1);
                List<string> clique = new List<string> { node1 };
                clique = getmaximal(connections, clique, connections[node1]);
                clique.Sort();
                lans.Add(string.Join(",", clique));
            }
            string longest = "";
            foreach (string lan in lans)
            {
                if (lan.Length >= longest.Length) { Console.WriteLine(lan); }
                if (lan.Length > longest.Length) { longest = lan; }
            }
            Console.WriteLine("longest: " + longest);
        }
    }
}
