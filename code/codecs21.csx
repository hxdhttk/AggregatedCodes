using System.Linq;

public class Solution
{
    public IList<int> SelfDividingNumbers (int left, int right)
    {
        return Range(left, right).Where(num => IsSelfDividingNumber(num)).ToList();
    }

    private IEnumerable<int> Range(int left, int right)
    {
        for(var i = left; i <= right; i++)
        {
            yield return i;
        }
    }

    private bool IsSelfDividingNumber (int num)
    {
        if (num >= 1 && num <= 9)
        {
            return true;
        }

        var originalNum = num;
        while(num % 10 != 0)
        {
            var temp = num % 10;
            if (originalNum % temp != 0)
            {
                return false;
            }
            num = num / 10;
        }

        if (num == 0)
        {
            return true;
        }

        return false;
    }
}