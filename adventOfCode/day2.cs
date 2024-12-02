using System.IO;
namespace Day2
{
    public class Day2
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
            List<List<int>> levels = new List<List<int>>();
            int safes = 0;
            foreach (string line in data)
            {
                string[] set = line.Split(' ');
                List<int> report = new List<int>();
                foreach (string level in set)
                {
                    report.Add(Int32.Parse(level));
                }
                levels.Add(report);
            }
            foreach (List<int> report in levels)
            {
                bool valid = true;
                if (report[0] > report[1] && (report[0] - report[1]) <= 3 && (report[0] - report[1]) > 0)
                {
                    for (int i = 1; i < report.Count; i++)
                    {
                        if (report[i-1] > report[i] && (report[i-1] - report[i]) <= 3 && (report[i-1] - report[i]) > 0)
                        {}
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid) { safes++; }
                }
                else if (report[1] > report[0] && (report[1] - report[0]) <= 3 && (report[1] - report[0]) > 0)
                {
                    for (int i = 1; i < report.Count; i++)
                    {
                        if (report[i] > report[i-1] && (report[i] - report[i-1]) <= 3 && (report[i] - report[i-1]) > 0)
                        {}
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid) { safes++; }
                } 
            }
            Console.WriteLine(safes);
        }

        static bool isValid(List<int> report, bool problem)
        {
            bool valid = true;
            if (report[0] > report[1] && (report[0] - report[1]) <= 3 && (report[0] - report[1]) > 0)
            {
                for (int i = 1; i < report.Count; i++)
                {
                    if (report[i - 1] > report[i] && (report[i - 1] - report[i]) <= 3 && (report[i - 1] - report[i]) > 0)
                    { }
                    else
                    {
                        if (problem)
                        {
                            valid = false;
                            break;
                        }
                        else
                        {
                            List<int> subReport1 = new List<int>(report);
                            List<int> subReport2 = new List<int>(report);
                            subReport1.RemoveAt(i);
                            subReport2.RemoveAt(i - 1);
                            return isValid(subReport1, true) || isValid(subReport2, true);
                        }
                    }
                }
                if (valid) { return true; }
            }
            else if (report[1] > report[0] && (report[1] - report[0]) <= 3 && (report[1] - report[0]) > 0)
            {
                for (int i = 1; i < report.Count; i++)
                {
                    if (report[i] > report[i - 1] && (report[i] - report[i - 1]) <= 3 && (report[i] - report[i - 1]) > 0)
                    { }
                    else
                    {
                        if (problem)
                        {
                            valid = false;
                            break;
                        }
                        else
                        {
                            List<int> subReport1 = new List<int>(report);
                            List<int> subReport2 = new List<int>(report);
                            subReport1.RemoveAt(i);
                            subReport2.RemoveAt(i - 1);
                            return isValid(subReport1, true) || isValid(subReport2, true);
                        }
                    }
                }
                if (valid) { return true; }
            }
            else
            {
                if (problem)
                {
                    return false;
                }
                else
                {
                    List<int> subReport1 = new List<int>(report);
                    List<int> subReport2 = new List<int>(report);
                    subReport1.RemoveAt(1);
                    subReport2.RemoveAt(0);
                    return isValid(subReport1, true) || isValid(subReport2, true);
                }
            }
            return false;
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            List<List<int>> levels = new List<List<int>>();
            int safes = 0;
            foreach (string line in data)
            {
                string[] set = line.Split(' ');
                List<int> report = new List<int>();
                foreach (string level in set)
                {
                    report.Add(Int32.Parse(level));
                }
                levels.Add(report);
            }
            foreach (List<int> report in levels)
            {
                safes += isValid(report, false) ? 1 : 0;
            }
            Console.WriteLine(safes);
        }
    }
}
