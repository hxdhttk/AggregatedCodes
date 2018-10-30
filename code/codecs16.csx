public class Solution
{
    public bool DetectCapitalUse (string word)
    {
        if (char.IsUpper (word[0]))
        {
            return IsCharsLower (word.Substring (1)) || IsCharsUpper (word.Substring (1));
        }

        return IsCharsLower (word);
    }
    private bool IsCharsLower (string word)
    {
        return word.ToLower () == word;
    }

    private bool IsCharsUpper (string word)
    {
        return word.ToUpper () == word;
    }
}