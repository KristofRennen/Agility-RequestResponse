using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Agility.RequestResponse.Common;
using NUnit.Framework;

namespace Agility.RequestResponse.Tests
{
    [TestFixture]
    public class SerializationTests
    {
        [Test]
        public void Serialize_SimpleSerializationRequest_ShouldWork()
        {
            var serializationResult = Serialize<SimpleSerializationRequest>(CreateSimpleSerializationRequest());

            Assert.IsNotNull(serializationResult);
        }

        [Test]
        public void Serialize_ArraySerializationRequest_ShouldWork()
        {
            var serializationResult = Serialize<ArraySerializationRequest>(CreateArraySerializationRequest());

            Assert.IsNotNull(serializationResult);
        }

        [Test]
        public void Serialize_ListSerializationRequest_ShouldWork()
        {
            var serializationResult = Serialize<ListSerializationRequest>(CreateListSerializationRequest());

            Assert.IsNotNull(serializationResult);
        }

        [Test]
        public void Deserialize_SimpleSerializationRequest_ShouldWork()
        {
            var serializationResult = Serialize<SimpleSerializationRequest>(CreateSimpleSerializationRequest());
            var deserializationResult = Deserialize<SimpleSerializationRequest>(serializationResult.ToString());

            Assert.IsNotNull(deserializationResult);
        }

        [Test]
        public void Deserialize_ArraySerializationRequest_ShouldWork()
        {
            var serializationResult = Serialize<ArraySerializationRequest>(CreateArraySerializationRequest());
            var deserializationResult = Deserialize<ArraySerializationRequest>(serializationResult.ToString());

            Assert.IsNotNull(deserializationResult);
        }

        [Test]
        public void Deserialize_ListSerializationRequest_ShouldWork()
        {
            var serializationResult = Serialize<ListSerializationRequest>(CreateListSerializationRequest());
            var deserializationResult = Deserialize<ListSerializationRequest>(serializationResult.ToString());

            Assert.IsNotNull(deserializationResult);
        }

        private static ListSerializationRequest CreateListSerializationRequest()
        {
            return new ListSerializationRequest
            {
                ArraySerializationRequests = new List<ArraySerializationRequest> { CreateArraySerializationRequest(), CreateArraySerializationRequest() }
            };
        }

        private static ArraySerializationRequest CreateArraySerializationRequest()
        {
            return new ArraySerializationRequest
            {
                SimpleSerializationRequests = new[] { CreateSimpleSerializationRequest(), CreateSimpleSerializationRequest() }
            };
        }

        private static SimpleSerializationRequest CreateSimpleSerializationRequest()
        {
            return new SimpleSerializationRequest
            {
                NumericValue = int.MaxValue,
                StringValue = "Kristof Rennen",
            };
        }

        private static StringBuilder Serialize<T>(Request request)
        {
            var serializationResult = new StringBuilder();
            var serializationWriter = new StringWriter(serializationResult);
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(serializationWriter, request);
            serializationWriter.Close();
            return serializationResult;
        }

        private static T Deserialize<T>(string serialized)
        {
            var serializationReader = new StringReader(serialized);
            var deserializer = new XmlSerializer(typeof (T));
            return (T) deserializer.Deserialize(serializationReader);
        }
    }

    public class SimpleSerializationRequest : Request
    {
        public int NumericValue { get; set; }
        public string StringValue { get; set; }
    }

    public class ArraySerializationRequest : Request
    {
        public SimpleSerializationRequest[] SimpleSerializationRequests { get; set; }
    }

    public class ListSerializationRequest : Request
    {
        public List<ArraySerializationRequest> ArraySerializationRequests { get; set; }
    }
}