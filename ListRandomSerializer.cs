using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ListRandom
{
    public static class ListRandomSerializer
    {
        public static void Serialize(Stream stream, ListRandom serializingList)
        {
            var nodeNumbers = GetNodeNumbers(serializingList);
            using BinaryWriter writer = new BinaryWriter(stream, Encoding.Default, true);
            WriteNodeCount(writer, serializingList);
            WriteRandomNodeNumbers(writer, serializingList, nodeNumbers);
            WriteNodeData(writer, serializingList);
        }

        private static Dictionary<ListNode, int> GetNodeNumbers(ListRandom serializingList)
        {
            var current = serializingList.Head;
            var nodeNumbers = new Dictionary<ListNode, int>();
            for(var i = 0; i < serializingList.Count; ++i)
            {
                nodeNumbers[current] = i;
                current = current.Next;
            }
            return nodeNumbers;
        }

        private static void WriteNodeCount(BinaryWriter writer, ListRandom serializingList)
        {
            writer.Write(serializingList.Count);
        }

        private static void WriteRandomNodeNumbers(BinaryWriter writer, ListRandom serializingList, Dictionary<ListNode, int> nodeNumbers)
        {
            var current = serializingList.Head;
            while(current != null)
            {
                writer.Write(nodeNumbers[current.Random]);
                current = current.Next;
            }
        }

        private static void WriteNodeData(BinaryWriter writer, ListRandom serializingList)
        {
            var current = serializingList.Head;
            while(current != null)
            {
                writer.Write(current.Data);
                current = current.Next;
            }
        }
    }
}
