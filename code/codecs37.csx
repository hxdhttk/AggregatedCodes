public class Solution {
    public int SingleNumber(int[] nums) {
        return nums.Aggregate((fst, snd) => fst ^ snd);
    }
}