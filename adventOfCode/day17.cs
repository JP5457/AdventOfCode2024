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
namespace Day17
{

    public class Day17
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
            List<int> program = new List<int>();

            // read file contents
            long A = 0;
            long B = 0;
            long C = 0;
            int pointer = 0;
            List<long> outs = new List<long>();
            foreach (string line in data)
            {
                if(line.Contains("Register A")){
                    A = long.Parse(line.Split(":")[1]);
                }
                if (line.Contains("Register B"))
                {
                    B = long.Parse(line.Split(":")[1]);
                }
                if (line.Contains("Register C"))
                {
                    C = long.Parse(line.Split(":")[1]);
                }
                if (line.Contains("Program"))
                {
                    string instruct = line.Split(":")[1];
                    string[] instructs = instruct.Split(",");
                    foreach (string i in instructs)
                    {
                        program.Add(int.Parse(i));
                    }
                }
            }

            while (true)
            {
                if (pointer >= program.Count - 1)
                {
                    break;
                }
                long op = program[pointer];
                long combo = program[pointer + 1];
                int literal = program[pointer + 1];
                if (combo == 4) { combo = A; }
                else if (combo == 5) { combo = B; }
                else if (combo == 6) { combo = C; }
                Console.WriteLine("op: " + op + " operand: " + literal);
                switch (op)
                {
                    case 0:
                        A = (long)(A / Math.Pow(2, combo));
                        break;
                    case 1:
                        B = B ^ literal;
                        break;
                    case 2:
                        B = combo % 8;
                        break;
                    case 3:
                        if (A != 0) { pointer = literal - 2; }
                        break;
                    case 4:
                        B = B ^ C;
                        break;
                    case 5:
                        outs.Add(combo % 8);
                        Console.WriteLine("A: " + A + " B: " + B + " C: " + C + " added " + (combo % 8));
                        break;
                    case 6:
                        B = (long)(A / Math.Pow(2, combo));
                        break;
                    case 7:
                        C = (long)(A / Math.Pow(2, combo));
                        break;
                }
                pointer += 2;
            }
            Console.WriteLine("====================================");
            Console.WriteLine(String.Join(",", outs.ToArray()));
            Console.WriteLine("A: " + A + " B: " + B + " C: " + C);
        }


        static List<int> testA(List<int> program, long A)
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            long B = 0;
            long C = 0;
            long pointer = 0;
            List<long> outs = new List<long>();

            while (true)
            {
                if (pointer >= program.Count - 1)
                {
                    break;
                }
                long op = program[(int)pointer];
                long combo = program[(int)(pointer + 1)];
                long literal = program[(int)(pointer + 1)];
                if (combo == 4) { combo = A; }
                else if (combo == 5) { combo = B; }
                else if (combo == 6) { combo = C; }
                switch (op)
                {
                    case 0:
                        A = (long)(A / Math.Pow(2, combo));
                        break;
                    case 1:
                        B = B ^ literal;
                        break;
                    case 2:
                        B = combo % 8;
                        break;
                    case 3:
                        if (A != 0) { pointer = literal - 2; }
                        break;
                    case 4:
                        B = B ^ C;
                        break;
                    case 5:
                        outs.Add(combo % 8);
                        break;
                    case 6:
                        B = (long)(A / Math.Pow(2, combo));
                        break;
                    case 7:
                        C = (long)(A / Math.Pow(2, combo));
                        break;
                }
                pointer += 2;
            }
            return outs.Select(x => (int)x).ToList();
        }

        static long getA(List<int> program, long A, int digits)
        {
            Console.WriteLine("A: " + A + " Digits: " + digits);
            List<int> trying = program.GetRange(program.Count()-digits, digits);
            //Console.WriteLine("trying: " + String.Join(",", trying.ToArray()));
            for (long a = 0; a < 8; a++)
            {
                List<int> test = testA(program, A + a);
                if (digits == 16) { Console.WriteLine("trying: " + String.Join(",", trying.ToArray()) + " with: " + String.Join(",", test.ToArray()) + "with A: " + (A+a)); }
                if (Enumerable.SequenceEqual(trying, test))
                {
                    long newA = A + a;
                    newA = newA << 3;
                    if (Enumerable.SequenceEqual(test, program)) { return A + a; }
                    if (getA(program, newA, digits + 1) != 0)
                    {
                        return A;
                    }
                }
            }
            return 0;
        }

        public static void Task2()
        {
            List<string> data = readFile("C:\\Users\\James\\source\\repos\\adventOfCode\\adventOfCode\\input.txt");
            int total = 0;
            List<int> program = new List<int>();
            List<long> outs = new List<long>();
            foreach (string line in data)
            {
                if (line.Contains("Program"))
                {
                    string instruct = line.Split(":")[1];
                    string[] instructs = instruct.Split(",");
                    foreach (string i in instructs)
                    {
                        program.Add(int.Parse(i));
                    }
                }
            }
            
            long A = getA(program, 0, 1);

            Console.WriteLine("A: " + A);
            Console.WriteLine(String.Join(",", testA(program, A).ToArray()));
        }
    }
}
