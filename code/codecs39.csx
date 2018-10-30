public class Solution
{
    public IList<string> FindBrotherStrings (string str, string[] dict)
    {
        var res = new List<string>();
        foreach(var element in dict)
        {
            if(IsBrotherString(str, element))
            {
                res.Add(element);
            }
        }
        return res;
    }

    private bool IsBrotherString(string str, string target)
    {
        if(string.IsNullOrEmpty(str))
        {
            return false;
        }

        if(str.Length != target.Length)
        {
            return false;
        }

        var strArr = str.ToCharArray();
        var targetArr = target.ToCharArray();
        Array.Sort(strArr);
        Array.Sort(targetArr);
        for(var i  = 0; i < strArr.Length; i++)
        {
            if(strArr[i] != targetArr[i])
            {
                return false;
            }
        }
        return true;
    }
}