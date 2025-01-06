using System;
namespace LinqDemo
{
    public class class1
    {
        public static void Main(string[] args)
        {
            int[] arr = { 23, 44, 12, 123, 44, 55, 213, 53, 1, 12, 3, 54, 12, 55, 11, 33, 42, 09 };
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i]>50)
                count++;
            }
            int[] brr= new int[count];
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i]>50)
                brr[index++] = arr[i];
            }
            Array.Sort(brr);
            Array.Reverse(brr);
            foreach(int i in brr)
                Console.Write(i+" ");
        }
    }
}