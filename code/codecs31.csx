public class Solution {
    public int FindComplement(int num) {
        var highest = 31;
        while((num & (1 << highest)) < 1)
        {
            highest--;
        }

        for(var i = 0; i <= highest; i++)
        {
            num = num ^ (1 << i);
        }
        return num;
    }
}