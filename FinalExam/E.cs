using System;

namespace Final 
{
    class Solution
    {
        public static void Main(string[] args)
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var stringArray = Console.ReadLine().Split(' ');
            var intArray = new int[100005];
            for (var i = 0; i < n; i++)
            {
                intArray[i] = int.Parse(stringArray[i]);
            }
            bool flag = false;
            for (int i = 0; i <= n / 2; i++)
            {
                if (intArray[i] != intArray[n - i - 1])
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
                Console.WriteLine("Not palindrome");
            else
                Console.WriteLine("Palindrome");
        }
    }
}
