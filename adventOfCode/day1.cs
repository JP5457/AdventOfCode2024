using System.IO;
namespace Day1
{
    public class Day1
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
            List<int> group1 = new List<int>();
            List<int> group2 = new List<int>();
            int diffs = 0;
            foreach(string line in data)
            {
                string[] set = line.Split(' ');
                int val0 = Int32.Parse(set[0]);
                int val1 = Int32.Parse(set[3]);
                group1.Add(val0);
                group2.Add(val1);
            }
            group1.Sort();
            group2.Sort();
            for(int i = 0; i<group1.Count; i++)
            {
                diffs += Math.Abs(group1[i] - group2[i]);
            }
            Console.WriteLine(diffs);
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            List<int> group1 = new List<int>();
            List<int> group2 = new List<int>();
            int score = 0;
            foreach (string line in data)
            {
                string[] set = line.Split(' ');
                int val0 = Int32.Parse(set[0]);
                int val1 = Int32.Parse(set[3]);
                group1.Add(val0);
                group2.Add(val1);
            }
            for (int i = 0; i < group1.Count; i++)
            {
                int count = 0;
                foreach(int num in group2)
                {
                    if (num == group1[i])
                    {
                        count++;
                    }
                }
                score += (count * group1[i]);
            }
            Console.WriteLine(score);
        }
    }
}

