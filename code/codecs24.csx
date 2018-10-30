public class Solution
{
    public int[] Intersect (int[] nums1, int[] nums2)
    {
        var nums1Dict = ArrayToDict(nums1);
        var nums2Dict = ArrayToDict(nums2);
        var keys = nums1Dict.Keys.Intersect(nums2Dict.Keys);
        var intersection = new List<int>();
        foreach(var key in keys)
        {
            var keyCount = nums1Dict[key] >= nums2Dict[key] ? nums2Dict[key] : nums1Dict[key];
            for(var time = 0; time < keyCount; time++)
            {
                intersection.Add(key);
            }
        }
        return intersection.ToArray();
    }

    public Dictionary<int, int> ArrayToDict (int[] nums)
    {
        return nums.GroupBy(num => num)
                   .ToDictionary(grouping => grouping.Key, grouping => grouping.Count());
    }
}