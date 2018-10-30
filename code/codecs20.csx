public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode (int x) { val = x; }
}

public class Solution
{
    public TreeNode MergeTrees (TreeNode t1, TreeNode t2)
    {
        InnerMerge (t1, t2);
        return t1 != null ? t1 : t2;
    }

    public void InnerMerge (TreeNode t1, TreeNode t2)
    {
        if (t1 != null && t2 != null)
        {
            t1.val += t2.val;
            if (t1.left != null && t2.left != null)
            {
                InnerMerge (t1.left, t2.left);
            }
            else if (t1.left == null)
            {
                t1.left = t2.left;
            }

            if (t1.right != null && t2.right != null)
            {
                InnerMerge (t1.right, t2.right);
            }
            else if (t1.right == null)
            {
                t1.right = t2.right;
            }
        }
    }
}