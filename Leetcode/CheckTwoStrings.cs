public class Solution 
{
    public bool ArrayStringsAreEqual(string[] word1, string[] word2)
    {
        string s1 = string.Empty;
        string s2 = string.Empty;
        foreach (string word in word1)
            s1 += word;
        foreach (string word in word2)
            s2 += word;
        return s1 == s2;
    }
}