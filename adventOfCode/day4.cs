using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
namespace Day4
{
    public class Day4
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
            List<string> words = new List<string>();
            for (int x = 0; x < data.Count; x++){
                for (int y = 0; y < data[0].Length; y++)
                {
                    if(x+3 < data.Count)
                    {
                        char[] chars = { data[x][y], data[x + 1][y], data[x + 2][y], data[x + 3][y] };
                        words.Add(new string(chars));
                    }
                    if (y + 3 < data[0].Length)
                    {
                        char[] chars = { data[x][y], data[x][y + 1], data[x][y + 2], data[x][y + 3] };
                        words.Add(new string(chars));
                    }
                    if (y + 3 < data[0].Length && x + 3 < data.Count)
                    {
                        char[] chars = { data[x][y], data[x + 1][y + 1], data[x + 2][y + 2], data[x + 3][y + 3] };
                        words.Add(new string(chars));
                    }
                    if (y > 2 && x + 3 < data.Count)
                    {
                        char[] chars = { data[x][y], data[x + 1][y - 1], data[x + 2][y - 2], data[x + 3][y - 3] };
                        words.Add(new string(chars));
                    }
                }
            }
            foreach (string word in words)
            {
                if(word == "XMAS" || word == "SAMX") { total++; }
            }
            Console.WriteLine(total);
        }



        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<string[]> word_pairs = new List<string[]>();
            string[] allowed = new string[] { "MAS", "SAM"};
            for (int x = 0; x < data.Count; x++)
            {
                for (int y = 0; y < data[0].Length; y++)
                {
                    if (x + 2 < data.Count && y+2 < data[0].Length)
                    {
                        char[] diag1 = { data[x][y], data[x + 1][y + 1], data[x + 2][y + 2]};
                        char[] diag2 = { data[x][y + 2], data[x + 1][y + 1], data[x + 2][y] };
                        string[] pair = { new string(diag1), new string(diag2) };
                        word_pairs.Add(pair);
                    }
                }
            }
            foreach (string[] pair in word_pairs)
            {
                if (allowed.Contains(pair[0]) && allowed.Contains(pair[1])) { total++;}
            }
            Console.WriteLine(total);
        }
    }
}
