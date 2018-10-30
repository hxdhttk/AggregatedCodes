public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode (int x) { val = x; }
}

public class Solution {
    public TreeNode InvertTree(TreeNode root) {
        InnerInvert(root);
        return root;
    }

    private void InnerInvert(TreeNode root)
    {
        if(root != null)
        {
            var temp = root.left;
            root.left = root.right;
            root.right = temp;

            InnerInvert(root.left);
            InnerInvert(root.right);
        }
    }
}