using System;

class Solution
{
    public static void Main(string[] args)
    {
        var fm = Console.ReadLine().Split();
        var sm = Console.ReadLine().Split();
        var fr = Array.ConvertAll(fm, int.Parse);
        var sc = Array.ConvertAll(sm, int.Parse);
        var a = (fr[0]);
        var b = (fr[1]);
        var c = (sc[0]);
        var d = (sc[1]);

        if (a + d > b + c) Console.WriteLine("Barsenal");
        else if (a + d < b + c) Console.WriteLine("Arselona");
        else if (b > d) Console.WriteLine("Arselona");
        else if (d < b) Console.WriteLine("Barsenal");
        else Console.WriteLine("Friendship");
        var firstResult = a + d;
        var secondResult = b + c;
        Console.WriteLine("{0} {1}", firstResult, secondResult);
    }
}