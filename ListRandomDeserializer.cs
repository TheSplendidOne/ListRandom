using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ListRandom
{
    public static class ListRandomDeserializer
    {
        public static ListRandom Deserialize(Stream stream)
        {
            using BinaryReader reader = new BinaryReader(stream, Encoding.Default, true);
            var nodeCount = ReadNodeCount(reader);
            var randomNodeNumbers = ReadRandomNodeNumbers(reader, nodeCount);
            var nodes = ReadNodes(reader, nodeCount);
            return CombineRandomNodes(nodes, randomNodeNumbers);
        }

        private static int ReadNodeCount(BinaryReader reader)
        {
            return reader.ReadInt32();
        }

        private static List<int> ReadRandomNodeNumbers(BinaryReader reader, int nodeCount)
        {
            var nodeNumbers = new List<int>();
            for(var i = 0; i < nodeCount; ++i)
            {
                nodeNumbers.Add(reader.ReadInt32());
            }
            return nodeNumbers;
        }

        private static List<ListNode> ReadNodes(BinaryReader reader, int nodeCount)
        {
            var nodes = new List<ListNode>(nodeCount);
            var head = new ListNode(reader.ReadString());
            nodes.Add(head);
            var current = head;
            for(var i = 1; i < nodeCount; ++i)
            {
                var newNode = new ListNode(reader.ReadString(), current);
                current.Next = newNode;
                current = newNode;
                nodes.Add(current);
            }
            return nodes;
        }

        private static ListRandom CombineRandomNodes(List<ListNode> nodes, List<int> randomNodeNumbers)
        {
            var list = new ListRandom { Head = nodes[0], Tail = nodes[^1], Count = nodes.Count };
            for(var i = 0; i < list.Count; ++i)
            {
                nodes[i].Random = nodes[randomNodeNumbers[i]];
            }
            return list;
        }
    }
}
