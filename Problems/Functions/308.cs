using System;


class Solution
{
    public static bool Xor(int a, int b)
    {
        if (a == 1 && b == 1) return false;
        else if (a == 1 || b == 1) return true;
        else return false;
    }
    public static void Main(string[] args)
    {
        string[] nums = Console.ReadLine().Split();
        var a = int.Parse(nums[0]);
        var b = int.Parse(nums[1]);
        if (Xor(a ,b)) Console.WriteLine(1);
        else Console.WriteLine(0);
    }
}