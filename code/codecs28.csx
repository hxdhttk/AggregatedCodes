using System.Text.RegularExpressions;

public class Solution {
    public bool IsNumber(string s) {
        s = s.Trim();
        var regex = new Regex(@"^[-+]*([0-9]+\.?[0-9]*|\.[0-9]+|([0-9]+\.?[0-9]*|\.[0-9]+)e[-+]*[0-9]+)$");
        return regex.IsMatch(s);
    }
}