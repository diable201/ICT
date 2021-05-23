using System;

class Solution
{
    public static void Main(string[] args)
    {
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());
        int d = int.Parse(Console.ReadLine());
        for (int i = 0; i <= 1000; i++)
        {
            if (a * Math.Pow(i, 3) + b * Math.Pow(i, 2) + c * Math.Pow(i, 1) + d == 0)
                Console.Write(i + " ");
        }
    }
}