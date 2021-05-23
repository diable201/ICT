using System;


class Solution
{
    public static void Main(string[] args)
    {
        var n = Convert.ToInt32(Console.ReadLine());
        var stringArray = Console.ReadLine().Split(' ');
        var intArray = new int[100];
        for (var i = 0; i < n; i++)
        {
            intArray[i] = int.Parse(stringArray[i]);
        }
        for (var i = 0; i < n - 1; i++)
        {
            if (intArray[i] > 0 && intArray[i + 1] > 0 ||
                intArray[i] < 0 && intArray[i + 1] < 0) 
            {
                Console.WriteLine("YES");
                return;
            }
        }
        Console.WriteLine("NO");
    }
}