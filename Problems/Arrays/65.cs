using System;


class Solution
{
    public static void Main(string[] args)
    {
        var n = Convert.ToInt32(Console.ReadLine());
        var tmp = 0;
        var stringArray = Console.ReadLine().Split(' ');
        var intArray = new int[100];
        for (var i = 0; i < n; i++)
        {
            intArray[i] = int.Parse(stringArray[i]);
        }
        for (var i = 0; i < n; i++)
        {
            if (intArray[i] > 0) 
            {
                tmp++;
                // Console.Write(intArray[i] + " ");
            }
        }
        Console.WriteLine(tmp);
    }
}