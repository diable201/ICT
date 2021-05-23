using System;

class Solution
{
    public static void Main(string[] args)
    {
        var a = int.Parse(Console.ReadLine());
        var b = int.Parse(Console.ReadLine());
        var c = int.Parse(Console.ReadLine());
        var d = int.Parse(Console.ReadLine());
        if (a + b == c + d || a - b == c - d) Console.WriteLine("YES");
        else Console.WriteLine("NO");
    }
}