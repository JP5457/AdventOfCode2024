using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Text.RegularExpressions;
namespace Day5
{
    public class Day5
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

        static bool checkRule(string[] rule, string[] update)
        {
            if (update.Contains(rule[0]) && update.Contains(rule[1]))
            {
                return (Array.IndexOf(update, rule[0]) < Array.IndexOf(update, rule[1]));
            } else
            {
                return true;
            }
        }

        static int getFailedRule(List<string[]> rules, string[] update)
        {
            int failPoint = -1;
            for (int i = 0; i<rules.Count; i++)
            {
                if (!checkRule(rules[i], update))
                {
                    failPoint = i;
                }
            }
            return failPoint;
        }

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<string[]> rules = new List<string[]>();
            List<string[]> updates = new List<string[]>();
            bool broken = false;
            foreach (string line in data)
            {
                if (line == "") { broken = true; }
                else if (!broken)
                {
                    rules.Add(line.Split('|'));
                }
                else
                {
                    updates.Add(line.Split(','));
                }
            }
            foreach (string[] update in updates)
            {
                bool valid = true;
                foreach (string[] rule in rules)
                {
                    if(!checkRule(rule, update))
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    int middle = (int)(update.Length / 2);
                    total += int.Parse(update[middle]);
                }
            }
            Console.WriteLine(total);
        }



        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<string[]> rules = new List<string[]>();
            List<string[]> updates = new List<string[]>();
            bool broken = false;
            foreach (string line in data)
            {
                if (line == "") { broken = true; }
                else if (!broken)
                {
                    rules.Add(line.Split('|'));
                }
                else
                {
                    updates.Add(line.Split(','));
                }
            }
            foreach (string[] update in updates)
            {
                bool valid = true;
                int failPoint;
                do
                {
                    failPoint = getFailedRule(rules, update);
                    if (failPoint != -1)
                    {
                        valid = false;
                        update[Array.IndexOf(update, rules[failPoint][0])] = rules[failPoint][1];
                        update[Array.IndexOf(update, rules[failPoint][1])] = rules[failPoint][0];
                    }
                } while (failPoint != -1);
                if (!valid)
                {
                    int middle = (int)(update.Length / 2);
                    total += int.Parse(update[middle]);
                }
            }
            Console.WriteLine(total);
        }
    }
}
