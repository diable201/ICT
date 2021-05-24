using System;

class Solution
{

    public static bool CheckRectangle(int a, int b, int c, int d)
    {
        if (a > c)
            return false;
        if (b > d)
            return false;
        return true;
    }
    public static void Main(string[] args)
    {
        string[] nums = Console.ReadLine().Split();
        var a = int.Parse(nums[0]);
        var b = int.Parse(nums[1]);
        var c = int.Parse(nums[2]);
        var d = int.Parse(nums[3]);
        if (CheckRectangle(a, b, c, d) || CheckRectangle(a, b, d, c)) Console.WriteLine("Thanks, Nurbek");
        else Console.WriteLine("Impossible");
    }
}