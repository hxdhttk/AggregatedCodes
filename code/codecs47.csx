 public class Solution
 {
     public IList<IList<int>> ThreeSum (int[] nums)
     {

         var res = new List<IList<int>> ();
         if (nums.Length < 3)
         {
             return res;
         }

         if (nums.Length == 3)
         {
             if (nums.Sum () == 0)
             {
                 res.Add (nums.ToList ());
                 return res;
             }
         }

         Array.Sort (nums);
         for (var i = 0; i < nums.Length - 2; i++)
         {
             if (i > 0 && nums[i] == nums[i - 1])
             {
                 continue;
             }

             if (nums[i] > 0)
             {
                 break;
             }

             var j = i + 1;
             var k = nums.Length - 1;
             while (j < k)
             {
                 var sum = nums[i] + nums[j] + nums[k];
                 if (sum == 0)
                 {
                     var toAdd = new List<int> ();
                     toAdd.Add (nums[i]);
                     toAdd.Add (nums[j]);
                     toAdd.Add (nums[k]);
                     res.Add (toAdd);
                     while (j < k && nums[j] == nums[j + 1])
                     {
                         j++;
                     }
                     while (j < k && nums[k] == nums[k - 1])
                     {
                         k--;
                     }
                 }

                 if (sum > 0)
                 {
                     k--;
                 }
                 else
                 {
                     j++;
                 }
             }
         }

         return res;
     }
 }