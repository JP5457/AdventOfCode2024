using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
namespace Day9
{
    public class Day9
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

        public static void Task1()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            Int64 total = 0;
            string map = data[0];
            List<Int64> disk = new List<Int64>();
            int id= 0;
            bool space = false;
            foreach (char c in map)
            {
                int todata = int.Parse(c.ToString());
                for (int i = 0; i < todata; i++)
                {
                    if (space)
                    {
                        disk.Add(-1);
                    }
                    else
                    {
                        disk.Add(id);
                    } 
                }
                if (!space) { id++; }
                space = !space;
            }
            for (int i = disk.Count()-1; i >= 0; i--){
                int moveto = disk.IndexOf(-1);
                if (moveto > i) { break; }
                if (disk[i] != -1)
                {
                    disk[moveto] = disk[i];
                    disk[i] = -1;
                }
            }
            Console.WriteLine(String.Join("; ", disk));
            for (int i = 0; i<disk.Count(); i++)
            {
                if (disk[i] > 0)
                {
                    total += i * disk[i];
                }
            }
            Console.WriteLine(total);
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            Int64 total = 0;
            string map = data[0];
            List<int> disk = new List<int>();
            int id = 0;
            bool space = false;
            foreach (char c in map)
            {
                int todata = int.Parse(c.ToString());
                for (int i = 0; i < todata; i++)
                {
                    if (space)
                    {
                        disk.Add(0-id);
                    }
                    else
                    {
                        disk.Add(id);
                    }
                }
                if (!space) { id++; }
                space = !space;
            }
            int miny = 0 - disk[disk.Count - 1];
            Dictionary<int, int> gaps = new Dictionary<int, int>();
            for (int y = -1; y > miny; y--)
            {
                List<int> indexes2 = Enumerable.Range(0, disk.Count).Where(i => disk[i] == y).ToList();
                gaps.Add(y, indexes2.Count());
            }

                for (int x = disk[disk.Count - 1]; x > 0; x--)
            {
                List<int> indexes = Enumerable.Range(0, disk.Count).Where(i => disk[i] == x).ToList();
                for (int y = -1; y > miny; y--)
                {
                    if (gaps[y] >= indexes.Count)
                    {
                        List<int> indexes2 = Enumerable.Range(0, disk.Count).Where(i => disk[i] == y).ToList();
                        if (indexes2[0] > indexes[0])
                        {
                            break;
                        }
                        for (int j = 0; j < indexes.Count; j++)
                        {
                            disk[indexes2[j]] = disk[indexes[j]];
                            disk[indexes[j]] = miny - 1;
                        }
                        gaps[y] = gaps[y] - indexes.Count();
                        break;
                    }
                }
            }
            for (int i = 0; i < disk.Count(); i++)
            {
                if (disk[i] > 0)
                {
                    total += i * disk[i];
                }
            }
            Console.WriteLine(total);
        }
    }
}
