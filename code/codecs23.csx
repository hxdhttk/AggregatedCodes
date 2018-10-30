public class ListNode {
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
  }

public class Solution {
    public ListNode ReverseList(ListNode head) {
        ListNode curr = head, reversed = null;
        while(curr != null)
        {
            (reversed, curr.next, curr) = (curr, reversed, curr.next);
        }

        return reversed;
    }
}
