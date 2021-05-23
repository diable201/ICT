using System;

class Solution
{
    public static void Main(string[] args)
    {
        var a = Console.ReadLine();
        var b = Console.ReadLine();
        var c = Console.ReadLine();
        var d = Console.ReadLine();
        if (a == c || b == d) Console.WriteLine("YES");
        else Console.WriteLine("NO");
    }
}