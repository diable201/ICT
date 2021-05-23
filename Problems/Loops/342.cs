using System;

class Solution
{
    public static void Main(string[] args)
    {
        int sum = 0;
        for (int i = 1; i <= 100; i++)
        {
            int a = int.Parse(Console.ReadLine());
            sum += a;
        }
        Console.WriteLine(sum);
    }
}