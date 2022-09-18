namespace Leetcode.DataTypes
{
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
             this.val = val;
             this.next = next;                  
        }

        public override string ToString()
        {
            if(next != null)
                return $"{val}->{next}";
            return $"{val}";
        }
    }
}
