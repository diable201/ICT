using System;
using System.Linq;

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
        for (var i = 0; i < n; i++)
        {
            if (i % 2 == 0) 
            {
                Console.Write(intArray[i] + " ");
            }
        }
        
    }
}