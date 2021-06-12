using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinimumTrianglePaths
{
    public class Program
    {
        static void Main(string[] args)
        {
            string inputFile = null;

            List<List<int>> triangle = new List<List<int>>();
            try
            {
                inputFile = System.IO.File.ReadAllText(@"file.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No file passed in command line");
                return;
            }


            triangle = GetValues(inputFile);

            List<int> result = MinimumTotal(triangle);
            string res = "Minimal path is: ";
            for (int i = 1; i < result.Count; i++)
            {
                res += (result[i] - result[i - 1]) + " ";
                if (i == result.Count - 1)
                {
                    res += "= " + result[result.Count - 1];
                }
                else
                {
                    res += "+ ";
                }
            }
            Console.WriteLine(res);
            Console.ReadLine();

        }


        public static List<List<int>> GetValues(string inputfile)
        {
            List<List<int>> triangle = new List<List<int>>();
            try
            {
                try
                {
                    //1. row
                    int first = Convert.ToInt32(inputfile.Split("\r")[0]);
                    List<int> firstRow = new List<int>();
                    firstRow.Add(first);
                    triangle.Add(firstRow);

                    List<int> twoRow = new List<int>()
                     {
                         Convert.ToInt32(inputfile.Split("\r")[1].Split("\n")[1].Split(" ")[0]),
                         Convert.ToInt32(inputfile.Split("\r")[1].Split("\n")[1].Split(" ")[1])
                     };
                    triangle.Add(twoRow);

                    List<int> threeRow = new List<int>()
                     {
                         Convert.ToInt32(inputfile.Split("\r")[2].Split("\n")[1].Split(" ")[0]),
                         Convert.ToInt32(inputfile.Split("\r")[2].Split("\n")[1].Split(" ")[1]),
                         Convert.ToInt32(inputfile.Split("\r")[2].Split("\n")[1].Split(" ")[2])
                     };

                    triangle.Add(threeRow);

                    List<int> fourRow = new List<int>()
                    {
                        Convert.ToInt32(inputfile.Split("\r")[3].Split("\n")[1].Split(" ")[0]),
                        Convert.ToInt32(inputfile.Split("\r")[3].Split("\n")[1].Split(" ")[1]),
                        Convert.ToInt32(inputfile.Split("\r")[3].Split("\n")[1].Split(" ")[2]),
                        Convert.ToInt32(inputfile.Split("\r")[3].Split("\n")[1].Split(" ")[3])
                    };

                    triangle.Add(fourRow);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Format error.");
                    return null;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return triangle;
        }

        public static List<int> MinimumTotal(List<List<int>> triangle)
        {
            int[] preRow = new int[triangle.Count];
            string s = "";

            List<int> nlist = new List<int>();
            foreach (IList<int> row in triangle)
            {
                int last = row.Count - 1;
                if (last > 0)
                {
                    preRow[last] = preRow[last - 1] + row[last];
                }
                int pre = preRow[0];
                preRow[0] += row[0];
                nlist.Add(pre);
                for (int i = 1; i < last; ++i)
                {

                    int temp = preRow[i];
                    s += temp + " ";
                    preRow[i] = Math.Min(preRow[i] + row[i], pre + row[i]);
                    pre = temp;
                }
            }


            int res = preRow[0];
            foreach (int num in preRow)
            {
                if (num < res)
                {
                    res = num;
                }
            }

            nlist.Add(res);
            return nlist;
        }


    }
}
