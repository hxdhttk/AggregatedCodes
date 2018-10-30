public class Solution
{
    public bool LemonadeChange (int[] bills)
    {
        if (!bills.Any ())
        {
            return true;
        }

        if (bills[0] != 5)
        {
            return false;
        }

        var fives = 1;
        var tens = 0;
        for (var index = 1; index < bills.Length; index++)
        {
            if(bills[index] == 5)
            {
                fives += 1;
                continue;
            }

            if(bills[index] == 10)
            {
                tens += 1;
                fives -= 1;
                if(fives < 0)
                {
                    return false;
                }
                continue;
            }

            if(bills[index] == 20)
            {
                if(tens == 0)
                {
                    fives -= 3;
                    if(fives < 0)
                    {
                        return false;
                    }
                    continue;
                }

                tens -= 1;
                fives -= 1;
                if(tens < 0 || fives < 0)
                {
                    return false;
                }
                continue;
            }
        }
        return true;
    }
}