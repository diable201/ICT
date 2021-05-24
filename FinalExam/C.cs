using System;

namespace Final 
{
    class Solution
    {
        public static void Main(string[] args)
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            for (var i = 0; i < n; i++)
                Console.Write(arr[i] + i + 1 + " ");
        }
    }
}
