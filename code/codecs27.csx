public class Solution
{
    public string ConvertToTitle (int n)
    {
        var buffer = new Stack<char> ();
        while (n > 0)
        {
            var remainder = n % 26;
            if (remainder == 0)
            {
                buffer.Push ('Z');
                n = n / 26 - 1;
            }
            else
            {
                buffer.Push ((char) ('A' + (remainder - 1)));
                n = n / 26;
            }
        }
        return new string (buffer.ToArray ());
    }
}