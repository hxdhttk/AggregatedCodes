public class Solution
{
    public string MoveMCharsToHead(string str, int m)
    {
        var spiltIndex = str.Length - m;
        var newTail = str.Substring(0, str.Length - m);
        var newHead = str.Substring(spiltIndex);
        return newHead + newTail;
    }
}