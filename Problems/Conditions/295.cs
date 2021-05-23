using System;

class Solution
{
    public static void Main(string[] args)
    {
        var a = int.Parse(Console.ReadLine());
        var b = int.Parse(Console.ReadLine());
        var c = int.Parse(Console.ReadLine());
        if (a >= (b + c) || b >= (a + c) || c >= (a + b)) Console.WriteLine("NO");
        else Console.WriteLine("YES");
    }
}