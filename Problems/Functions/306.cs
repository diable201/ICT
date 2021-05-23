using System;


class Solution
{
    public static int MinOfFourNumbers(int a, int b, int c, int d)
    {
        return Math.Min(Math.Min(a, b), Math.Min(c ,d));
    }
    public static void Main(string[] args)
    {
        string[] nums = Console.ReadLine().Split();
        var a = int.Parse(nums[0]);
        var b = int.Parse(nums[1]);
        var c = int.Parse(nums[2]);
        var d = int.Parse(nums[3]);
        Console.WriteLine(MinOfFourNumbers(a, b, c, d));
    }
}