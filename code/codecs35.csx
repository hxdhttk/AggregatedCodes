public class Solution {
    public int[] ShortestToChar(string S, char C) {
        var charIndices = new List<int>();
        for(var index = 0; index < S.Length; index++)
        {
            if(S[index] == C)
            {
                charIndices.Add(index);
            }
        }
        var res = new int[S.Length];
        for(var index = 0; index < S.Length; index++)
        {
            var value = charIndices.Select(charIndex => Math.Abs(charIndex - index)).Min();
            res[index] = value;
        }
        return res;
    }
}