public class Solution
{
    public IList<int> FindDisappearedNumbers (int[] nums)
    {
        var res = new List<int> ();
        foreach (var num in nums)
        {
            nums[Math.Abs (num) - 1] = -Math.Abs (nums[Math.Abs (num) - 1]);
        }
        for (var index = 0; index < nums.Length; index++)
        {
            if (nums[index] > 0)
            {
                res.Add (index + 1);
            }
        }
        return res;
    }
}