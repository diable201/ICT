using System;

class Solution
{
    public static void Main(string[] args)
    {
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());
        int d = int.Parse(Console.ReadLine());
        for (int i = a; i <= b; i++)
        {
            if (i % d == c)
            {
                Console.Write(i + " ");
            }
        }
    }
}