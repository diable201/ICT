using System;

class Solution
{

    public static int GreatestMultiplier(int a, int b)
    {
        while(true)
        {
            if (b % a == 0) 
            {
                return b;
            }
            b--;
        }
    }
    public static void Main(string[] args)
    {
        string[] nums = Console.ReadLine().Split();
        var a = int.Parse(nums[0]);
        var b = int.Parse(nums[1]);
        Console.WriteLine(GreatestMultiplier(a, b));
    }
}