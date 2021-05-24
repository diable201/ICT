using System;
using System.Collections.Generic;
using System.Collections;

namespace Final 
{
    class Solution
    {
        public static void Main(string[] args)
        {
            ArrayList a = new ArrayList();
            var input = Console.ReadLine();
            foreach (char letter in input)
                if (!a.Contains(letter))
                    a.Add(letter);
            foreach (char letter in a)
                Console.Write(letter);
        }
    }
}