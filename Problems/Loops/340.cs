using System;

class Solution
{
    public static void Main(string[] args)
    {
        int a = int.Parse(Console.ReadLine());
        for (int i = 1; i <= a; i++)
        {
            if (a % i == 0)
            {
                Console.Write(i + " ");
            }
        }
    }
}