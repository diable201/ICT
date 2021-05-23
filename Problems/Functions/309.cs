using System;


class Solution
{
    public static bool Election(int x, int y, int z)
    {
        int sum = 0;
        sum = x + y + z;
        if (sum > 1) return true;
        else return false;
    }
    public static void Main(string[] args)
    {
        string[] nums = Console.ReadLine().Split();
        var a = int.Parse(nums[0]);
        var b = int.Parse(nums[1]);
        var c = int.Parse(nums[2]);
        if (Election(a ,b, c)) Console.WriteLine(1);
        else Console.WriteLine(0);
    }
}