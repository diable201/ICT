using System;

class Solution
{
    public static void Main(string[] args)
    {
        int year = int.Parse(Console.ReadLine());
        if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
        {
            Console.WriteLine("YES");
        }
        else 
        {
            Console.WriteLine("NO");
        }
    }
}