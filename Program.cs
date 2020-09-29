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
            for (int i = 1; i < 1000000; i = f1 + f2)
            {
                DoSmthAndMeasure(CreateArray(i), path, MyBubbleSort);
                f1 = f2;
                f2 = i;
            }
        }

        static void DoSmthAndMeasure<T>(T[] array, string path, Action<T[]> act)
        {
            sw.Start();
            act(array);
            sw.Stop();
            var time = sw.ElapsedTicks;
            File.AppendAllText(path + act.Method.Name + ".csv", array.Length + ";" + time + ";\n");
        }

        static void DoSmthAndMeasure<T>(T[] array, string path, Func<T[], T> funk)
        {
            sw.Start();
            funk(array);
            sw.Stop();
            var time = sw.ElapsedTicks;
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

        static void MyReverse<T>(T[] array)
        {
            var rev = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                rev[i] = array[array.Length - 1 - i];
            }
        }

        static T MyBinarySearch<T>(T[] array)
        {
            var left = 0;
            var right = array.Length-1;
            var searchedValue = 5000;

            while (left <= right)
            {
                var middle = (left + right) / 2;
                if (searchedValue.CompareTo(array[middle]) == 0)
                {
                    return array[middle];
                }
                else if (searchedValue.CompareTo(array[middle]) > 0)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }
            return  default;
        }

        static int[] CreateArray(int n)
        {
            var rnd = new Random();
            var array = new int[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next()%10000;
            }
            return array;
        }
    }
}
