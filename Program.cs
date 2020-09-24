using System;
using System.Diagnostics;
using System.IO;

namespace algorithm_lab1
{
    class Program
    {
        public static Stopwatch sw = new Stopwatch();

        static void Main(string[] args)
        {
            string path = @".\measures\";
            
            var f1 = 1;
            var f2 = 1;
            for (int i = 1; i < 100000000; i = f1 + f2)
            {
                //DoSmthAndMeasure(CreateArray(i), path, MyBubbleSort);
                DoSmthAndMeasure(CreateArray(i), path, Array.Sort);
                f1 = f2;
                f2 = i;
            }
        }

        static void DoSmthAndMeasure<T>(T[] array, string path, Action<T[]> act)
        {
            sw.Start();
            act(array);
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            File.AppendAllText(path + act.Method.Name + ".csv", array.Length + ";" + time + ";\n");
        }

        static void DoSmthAndMeasure<T>(T[] array, string path, Func<T[],T> funk)
        {
            sw.Start();
            funk(array);
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            File.AppendAllText(path + funk.Method.Name + ".csv", array.Length + ";" + time + ";\n");
        }

        static void MyBubbleSort<T>(T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[i].CompareTo(array[i + 1]) < 0)
                    {
                        var t = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = t;
                    }
                }
        }

        static T MyFindMax<T>(T[] array) where T : IComparable<T>
        {
            var max = array[0];
            foreach (var e in array)
            {
                if (max.CompareTo(e) > 0)
                    max = e;
            }
            return max;
        }
        static int[] CreateArray(int n)
        {
            var rnd = new Random();
            var array = new int[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next();
            }
            return array;
        }
    }
}
