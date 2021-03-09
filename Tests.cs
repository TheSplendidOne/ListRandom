using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace ListRandom
{
    public class Tests
    {
        public ListRandom SerializingList;

        [SetUp]
        public void Setup()
        {
            SerializingList = new ListRandom();
            var nodes = new List<ListNode>()
            {
                new ListNode("zero node"),
                new ListNode("first node"),
                new ListNode("second node"),
                new ListNode("third node"),
                new ListNode("fourth node"),
                new ListNode("fifth node"),
                new ListNode("sixth node")
            };
            for(var i = 0; i < nodes.Count - 1; ++i)
            {
                nodes[i].Next = nodes[i + 1];
            }
            for(var i = nodes.Count - 1; i > 0; --i)
            {
                nodes[i].Previous = nodes[i - 1];
            }
            for(var i = 0; i < nodes.Count; ++i)
            {
                nodes[i].Random = nodes[(i + 3) % nodes.Count];
            }

            SerializingList.Head = nodes[0];
            SerializingList.Tail = nodes[^1];
            SerializingList.Count = nodes.Count;
        }

        [Test]
        public void ListRandomWithUniqueNodesData_Equals_AfterSerializationAndDeserialization()
        {
            var stream = new MemoryStream();
            SerializingList.Serialize(stream);
            stream.Position = 0;
            var deserializedList = new ListRandom();
            deserializedList.Deserialize(stream);
            
            var serializingListCurrent = SerializingList.Head;
            var deserializedListCurrent = deserializedList.Head;
            for(var i = 0; i < SerializingList.Count; ++i)
            {
                serializingListCurrent.Data.Should().Be(deserializedListCurrent.Data); // all data equals
                serializingListCurrent.Random.Data.Should().Be(deserializedListCurrent.Random.Data); // all data of random nodes equals
                serializingListCurrent = serializingListCurrent.Next;
                deserializedListCurrent = deserializedListCurrent.Next;
            }

            serializingListCurrent = SerializingList.Head;
            deserializedListCurrent = deserializedList.Head;
            for(var i = 0; i < SerializingList.Count - 1; ++i)
            {
                serializingListCurrent.Next.Data.Should().Be(deserializedListCurrent.Next.Data); // all data of next nodes equals
                serializingListCurrent = serializingListCurrent.Next;
                deserializedListCurrent = deserializedListCurrent.Next;
            }

            serializingListCurrent = SerializingList.Head.Next;
            deserializedListCurrent = deserializedList.Head.Next;
            for(var i = 0; i < SerializingList.Count - 1; ++i)
            {
                serializingListCurrent.Previous.Data.Should().Be(deserializedListCurrent.Previous.Data); // all data of previous nodes equals
                serializingListCurrent = serializingListCurrent.Next;
                deserializedListCurrent = deserializedListCurrent.Next;
            }

            deserializedList.Head.Previous.Should().Be(null);
            deserializedList.Tail.Next.Should().Be(null);
        }

        [Test]
        public void EmptyListRandom_SerializeAndDeserializeCorrectly()
        {
            var emptyList = new ListRandom();
            var stream = new MemoryStream();
            emptyList.Serialize(stream);
            stream.Position = 0;
            var deserializedList = new ListRandom();
            deserializedList.Deserialize(stream);
            deserializedList.Count.Should().Be(0);
            deserializedList.Head.Should().Be(null);
            deserializedList.Tail.Should().Be(null);
        }
    }
}