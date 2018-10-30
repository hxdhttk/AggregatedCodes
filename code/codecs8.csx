using System;

public class Node : Object
{
    public int Data;
    public Node Next;

    public Node (int data, Node next = null)
    {
        this.Data = data;
        this.Next = next;
    }

    public override bool Equals (Object obj)
    {
        // Check for null values and compare run-time types.
        if (obj == null || GetType () != obj.GetType ()) { return false; }

        return this.ToString () == obj.ToString ();
    }

    public override string ToString ()
    {
        List<int> result = new List<int> ();
        Node curr = this;

        while (curr != null)
        {
            result.Add (curr.Data);
            curr = curr.Next;
        }

        return String.Join (" -> ", result) + " -> null";
    }
}

public static class Kata
{
    private static readonly Dictionary<char, int> CharToIntDict =
        new Dictionary<char, int>
        {
            ['0'] = 0,
            ['1'] = 1,
            ['2'] = 2,
            ['3'] = 3,
            ['4'] = 4,
            ['5'] = 5,
            ['6'] = 6,
            ['7'] = 7,
            ['8'] = 8,
            ['9'] = 9,
        };

    public static Node Parse (string nodes)
    {
        if (nodes == "null")
        {
            return null;
        }

        var integers = GetIntegersFromString (nodes);
        var ret = new Node(integers.Pop());
        while(integers.Any())
        {
            var temp = new Node(integers.Pop(), ret);
            ret = temp;
        }

        return ret;
    }

    private static Stack<int> GetIntegersFromString (string nodes)
    {
        var nodeChars = nodes.ToCharArray ();
        var ret = new Stack<int>();

        int? acc = null;
        foreach (var ch in nodeChars)
        {
            if (Char.IsDigit (ch))
            {
                if (acc == null)
                {
                    acc = 0;
                }

                acc = acc * 10 + CharToIntDict[ch];
            }
            else
            {
                if (acc != null)
                {
                    ret.Push(acc.Value);
                    acc = null;
                }
            }
        }

        return ret;
    }
}