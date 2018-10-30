public class Solution
{
    public string ReverseWords (string s)
    {
        return s.Split (' ')
            .Select (word => word.Reverse ())
            .Aggregate (new StringBuilder (), (sb, word) => sb.Append (new string (word.ToArray ())).Append (' '), sb => sb.ToString ().TrimEnd ());
    }
}