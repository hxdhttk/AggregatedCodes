public class ListNode {
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
  }

public class Solution {
    public ListNode RemoveElements(ListNode head, int val) {
        var newHead = new ListNode(0);
        newHead.next = head;
        var curr = newHead;
        while (curr.next != null)
        {
            if(curr.next.val == val)
            {
                curr.next = curr.next.next;
            }
            else
            {
                curr = curr.next;
            }
        }
        return newHead.next;
    }
}