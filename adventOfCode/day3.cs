using System.Diagnostics.Metrics;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
namespace Day3
{
    public class Day3
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
            Regex rg = new Regex("mul\\([0-9]{1,3},[0-9]{1,3}\\)+");
            foreach (string line in data)
            {
                MatchCollection sets = rg.Matches(line);
                for (int i = 0; i < sets.Count; i++)
                {
                    string[] nums = Regex.Replace(sets[i].Value, @"[^0-9,]+", "").Split(',');
                    total += (int.Parse(nums[0]) * int.Parse(nums[1]));
                }
            }
            Console.WriteLine(total);
        }



        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            Regex rg = new Regex("mul\\([0-9]{1,3},[0-9]{1,3}\\)|do\\(\\)|don't\\(\\)+");
            bool doing = true;
            foreach (string line in data)
            {
                MatchCollection sets = rg.Matches(line);
                for (int i = 0; i < sets.Count; i++)
                {
                    if (sets[i].Value == "do()")
                    {
                        doing = true;
                    }
                    else if (sets[i].Value == "don't()")
                    {
                        doing = false;
                    }
                    else if (doing)
                    {
                        Console.WriteLine(sets[i]);
                        string[] nums = Regex.Replace(sets[i].Value, @"[^0-9,]+", "").Split(',');
                        total += (int.Parse(nums[0]) * int.Parse(nums[1]));
                    }
                }
            }
            Console.WriteLine(total);
        }
    }
}
