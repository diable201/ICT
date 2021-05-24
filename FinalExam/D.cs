using System;
using System.Text.RegularExpressions;

class Solution
{

    public static bool CheckEmail(string email)
    {
        var pattern = @"^[a-z]+@[a-z]+\.[a-z]+$";
        return Regex.IsMatch(email, pattern);
    }
    public static void Main(string[] args)
    {
        var email = Console.ReadLine();
        if (CheckEmail(email)) Console.WriteLine("Yes");
        else Console.WriteLine("No");
    }
}