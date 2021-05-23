using System;


class Solution
{
    public static double CustomPower(double a, int b)
    {
        double tmp = a;
        for (var i = 2; i <= b; i++)
        {
            tmp *= a;
        }
        if (b == 0) 
        {
            return 1;
        }
        else 
        {
            return tmp;
        }
    }
    public static void Main(string[] args)
    {
        string[] nums = Console.ReadLine().Split();
        var a = double.Parse(nums[0]);
        var b = int.Parse(nums[1]);
        Console.WriteLine(CustomPower(a, b));
    }
}