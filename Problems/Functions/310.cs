using System;


class Solution
{
    public static bool IsPrime(int n)
    {
        if (n == 1) return false;
        for (int i = 2; i * i <= n; i++)
            if (n % i == 0)
                return false;
        return true;
    }
    public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        if (IsPrime(n)) Console.WriteLine("prime");
        else Console.WriteLine("composite");
    }
}