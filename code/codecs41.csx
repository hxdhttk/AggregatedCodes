public class Solution
{
    public int HammingDistance (int x, int y)
    {
        var distance = 0;
        var res = x ^ y;
        for (var pos = 0; pos < 32; pos++)
        {
            distance += (res & (1 << pos)) > 0 ? 1 : 0;

        }
        return distance;
    }
}