using System;

class Solution
{
    public static void Main(string[] args)
    {
        int a = Convert.ToInt32(Console.ReadLine());
        int b = a / 100;
        int c = a / 10 % 10;
        int d = a % 10;
        Console.Write(b + c + d);
    }
}