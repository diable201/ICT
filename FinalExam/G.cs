using System;

class Solution
{

    public static void Main(string[] args)
    {
        string[] nums = Console.ReadLine().Split();
        var a = int.Parse(nums[0]);
        var b = int.Parse(nums[1]);
        while(true)
        {
            if (b % a == 0) 
            {
                Console.WriteLine(b);
                return;
            }
            b--;
        }
    }
}