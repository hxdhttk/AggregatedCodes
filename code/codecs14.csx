using System;
using System.Collections.Generic;

public class Solution {
    public int SmallestDistancePair(int[] nums, int k) {
        Array.Sort(nums);

        var low = 0;
        var high = nums[nums.Length - 1] - nums[0];
        while (low < high)
        {
            var middle = (low + high) / 2;
            var count = 0;
            var left = 0;
            for (var right = 0; right < nums.Length; right++)
            {
                while (nums[right] - nums[left] > middle)
                {
                    left++;
                    count += right - left;
                }

                if (count >= k)
                {
                    high = middle;
                }
                else
                {
                    low = middle + 1;
                }
            }
        }
        return low;
    }
}