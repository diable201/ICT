using System;

class Solution
{

    public static int GetDivisor(int a)
    {
        var res = 1;
        for (var i = 2; i <= a; i++)
        {
            if (a % i == 0)
            {
                res = i; 
                break;
            }
        }
        return res;
    }
    public static void Main(string[] args)
    {
        string[] nums = Console.ReadLine().Split();
        var a = int.Parse(nums[0]);
        Console.WriteLine(GetDivisor(a));   
    }
}