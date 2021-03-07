using System;
using System.Collections.Generic;
using System.Text;

namespace ListRandom
{
    public class ListNode
    {
        public ListNode Previous;

        public ListNode Next;

        public ListNode Random;

        public string Data;

        public ListNode(string data, ListNode previous)
        {
            Data = data;
            Previous = previous;
        }

        public ListNode(string data) : this(data, null)
        {
        }
    }
}
