using System;
using System.Globalization;

namespace Algorithm
{
    class Program
    {


        public static void /*int[]*/ BubbleSort(int[] ArrayNotSort)
        {
          

            for (int i = 0; i < ArrayNotSort.Length; i++ )
            {
                for (int j = 0; j < ArrayNotSort.Length - i - 1; j++)
                {
                    if (ArrayNotSort[j] > ArrayNotSort[j + 1])
                    {
                        var temp = ArrayNotSort[j];
                        ArrayNotSort[j] = ArrayNotSort[j + 1];
                        ArrayNotSort[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine($"Массив отсортирован");
            //return ArrayNotSort;
        }

        static void Main(string[] args)
        {

            int[] a = new[] {-5675, 1000, 009, 4, 45, 7, 8, 9};
            BubbleSort(a);
            var b = a;
            foreach (var q in a)
            {
                Console.WriteLine($"{q}");
            }
        }
    }
}
