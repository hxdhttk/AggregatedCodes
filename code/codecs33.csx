public class Solution {
    public int ArrayPairSum(int[] nums) {
        Array.Sort(nums);
        var res = 0;
        for(var i = 0; i <= nums.Length - 2; i = i + 2)
        {
            res += nums[i];
        }
        return res;
    }
}