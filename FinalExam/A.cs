using System;
using System.Collections.Generic;
using System.Collections;

namespace Final 
{
    class Solution
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ');
            var a = new int[10000];
            for (var i = 0; i < input.Length; i++)
            {
                a[i] = int.Parse(input[i]);
            }
            input = Console.ReadLine().Split(' ');
            var u = new int[10000];
            for (var i = 0; i < input.Length; i++)
            {
                u[i] = int.Parse(input[i]);
            }
            if ((int)a[2] < (int)u[2])
            {
                Console.WriteLine("Yes");
                return;
            }
            else if ((int)a[2] > (int)u[2])
            {
                Console.WriteLine("No");
                return;
            }
            else if ((int)a[2] == (int)u[2])
            {
                if ((int)a[1] < (int)u[1])
                {
                    Console.WriteLine("Yes");
                    return;
                }
                else if ((int)a[1] > (int)u[1])
                {
                    Console.WriteLine("No");
                    return;
                }
                else if ((int)a[1] == (int)u[1])
                {
                    if ((int)a[0] < (int)u[0])
                    {
                        Console.WriteLine("Yes");
                        return;
                    }
                    else if ((int)a[0] > (int)u[0])
                    {
                        Console.WriteLine("No");
                        return;
                    } 
                    else
                    {
                        Console.WriteLine("No");
                        return;
                    }
                }
            }
        }
    }
}