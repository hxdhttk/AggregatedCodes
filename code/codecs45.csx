public class Solution
{
    public int Divide (int dividend, int divisor)
    {
        try
        {
            return dividend / divisor;
        }
        catch (OverflowException)
        {
            return int.MaxValue;
        }
    }
}
