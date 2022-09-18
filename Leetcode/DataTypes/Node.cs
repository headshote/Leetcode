namespace Leetcode.DataTypes
{
    public class Node
    {
        public int val;
        public Node next;
        public Node random;

        public Node(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }

        public override string ToString()
        {
            if (next != null)
                return $"{val}->{next}";
            return $"{val}";
        }
    }
}
