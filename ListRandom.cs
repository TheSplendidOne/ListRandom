using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ListRandom
{
    public class ListRandom
    {
        public ListNode Head;

        public ListNode Tail;

        public int Count;

        public void Serialize(Stream s)
        {
            ListRandomSerializer.Serialize(s, this);
        }

        public void Deserialize(Stream s)
        {
            var result = ListRandomDeserializer.Deserialize(s);
            Head = result.Head;
            Tail = result.Tail;
            Count = result.Count;
        }
    }
}
