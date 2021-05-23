using System;

class Solution
{
    public static void Main(string[] args)
    {
        int a = Convert.ToInt32(Console.ReadLine());
        Console.Write(a + 2 - (a % 2));
    }
}