using System;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace algorithm_lab1
{
    class Program
    {
        public static Stopwatch sw = new Stopwatch();

        static void Main(string[] args)
        {
            string path = @".\measures\";

            //var f1 = 1;
            //var f2 = 1;
            //for (int i = 1; i < 1000000; i = f1 + f2)
            //{
            //    DoSmthAndMeasure(CreateArray(i), path, MyBubbleSort);
            //    f1 = f2;
            //    f2 = i;
            //}
            Console.WriteLine(DoSmthAndMeasure(CreateArray(500), path, MyBubbleSort));
            Console.WriteLine(DoSmthAndMeasure(CreateArray(5000), path, MyBubbleSort));
            Console.WriteLine(DoSmthAndMeasure(CreateArray(50000), path, MyBubbleSort));
        }

        static long DoSmthAndMeasure<T>(T[] array, string path, Action<T[]> act) where T : IComparable
        {
            //Array.Sort(array);
            //Array.Reverse(array);
            sw.Start();
            act(array);
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            //File.AppendAllText(path + act.Method.Name + ".csv", array.Length + ";" + time + ";\n");
            return time;
        }

        static long DoSmthAndMeasure<T>(T[] array, string path, Func<T[], T> funk) where T : IComparable
        {
            
            sw.Start();
            funk(array);
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            //File.AppendAllText(path + funk.Method.Name + ".csv", array.Length + ";" + time + ";\n");
            return time;
        }

        static void MyBubbleSort<T>(T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        var t = array[j];
                        array[j] = array[j+1];
                        array[j+1] = t;
                    }
                }
        }

        static void BubbleSortFromStackOverflow<T>(T[] array) where T : IComparable
        {
            var temp = default(T);

            for (int write = 0; write < array.Length; write++)
            {
                for (int sort = 0; sort < array.Length - 1; sort++)
                {
                    if (array[sort].CompareTo(array[sort + 1]) > 0)
                    {
                        temp = array[sort + 1];
                        array[sort + 1] = array[sort];
                        array[sort] = temp;
                    }
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
            var rev = array;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rev[array.Length - 1 - i];
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
