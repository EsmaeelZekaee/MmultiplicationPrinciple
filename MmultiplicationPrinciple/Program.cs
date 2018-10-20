using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace MmultiplicationPrinciple
{

    class Program
    {
        static void Main(string[] args)
        {
            var items = new List<List<char>>();
            items.Add("1234".ToList());
            items.Add("ABCDEFGH".ToList());
            items.Add("#$%^&*()".ToList());
            var arr = items.Select(x => x.ToArray()).ToArray();
            var sw = new Stopwatch();
            sw.Start();
            var output = MmultiplicationPrincipleGen.Generate(arr);
            sw.Stop();

            Console.WriteLine("All possible combination");
            foreach (var mp in output)
            {
                Console.Write(new string(mp));
                Console.Write("\t");
            }
            Console.WriteLine($"\nCount = {output.Length}\n");
            Console.WriteLine($"\nElapsed time = {sw.Elapsed.ToString("c")}\n");
            Console.ReadKey();
        }
    }

    public static class MmultiplicationPrincipleGen
    {
        public static T[][] Generate<T>(T[][] groups)
        {
            var gc = groups.Length;
            var k = 1;
            for (int i = 0; i < gc; i++)
                k = k * groups[i].Length;
            var mpList = new T[k][];
            int[] li = new int[gc];
            k = 0;
            void loop(int l)
            {
                if (l == gc)
                {
                    var group = new T[gc];
                    for (int i = 0; i < gc; i++)
                        group[i] = groups[i][li[i]];
                    mpList[k++] = group;
                    return;
                }
                for (int i = 0; i < groups[l].Length; i++, loop(l + 1))
                    li[l] = li[l] + 1 < groups[l].Length ? li[l] + 1 : 0;
            }
            loop(0);
            return mpList;
        }

        public static List<T[]> Generate<T>(List<List<T>> groups)
        {
            var gc = groups.Count;
            var mpList = new List<T[]>();
            int[] li = new int[gc];
            void loop(int l)
            {
                if (l == groups.Count)
                {
                    var group = new T[gc];
                    for (int i = 0; i < gc; i++)
                        group[i] = groups[i][li[i]];
                    mpList.Add(group);
                    return;
                }
                for (int i = 0; i < groups[l].Count; i++, loop(l + 1))
                    li[l] = li[l] + 1 < groups[l].Count ? li[l] + 1 : 0;
            }
            loop(0);
            return mpList;
        }
    }
}

