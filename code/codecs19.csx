using System.Linq;
using System.Collections.Generic;

public class Solution {
    private static readonly string[] _morseCodes = new string[] {
        ".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--","-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--.."
    };

    public int UniqueMorseRepresentations(string[] words) {
        var transcodes = new HashSet<string>(
            words.Select(word => string.Join("", word.Select(c => GetMorseCode(c))))
        );
        return transcodes.Count;
    }

    private string GetMorseCode(char c)
    {
        var index = (int)c - 97;
        return _morseCodes[index];
    }
}