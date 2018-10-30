using System.Collections.Generic;

public class Solution {
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t) {
        if(t == 0)
        {
            var hashSet = new HashSet<int>(nums);
            if(nums.Length == hashSet.Count)
            {
                return false;
            }
        }

        for(var i = 0; i < nums.Length; i++)
        {
            for(var j = i + 1; j < i + k + 1; j++)
            {
                if (j >= nums.Length)
                {
                    break;
                }

                var num1 = (long) nums[i];
                var num2 = (long) nums[j];
                if(num1 - num2 >= -t && num1 - num2 <= t)
                {
                    return true;
                }
            }
        }
        return false;
    }
}