using System.Linq;

public class Fracts
{
    public static string convertFrac (long[, ] lst)
    {
        var state = 1L;
        for(var index = 0; index < lst.GetLength(0); index++)
        {
            state = mcm(state, lst[index, 1]);
        }

        var sb = new StringBuilder();
        for(var index = 0; index < lst.GetLength(0); index++)
        {
            sb.Append($"({state / lst[index, 1]* lst[index, 0]},{state})");
        }

        return sb.ToString();
    }
    public static long gcd (long x, long y)
    {
        if(x > y)
        {
            if(y == 0)
            {
                return x;
            }
            return gcd(y, x % y);
        }

        if(x == 0)
        {
            return y;
        }
        return gcd(x, y % x);
    }

    public static long mcm (long x, long y)
    {
        return (x * y) / gcd(x, y);
    }
}