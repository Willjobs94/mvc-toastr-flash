using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using RedWillow.MvcToastrFlash.Serializers;

namespace MvcToastrFlash.Tests.Serializers
{
    [TestClass]
    public class JsonSerializerTests
    {
        [TestMethod]
        public void NullObjectSerializeTest()
        {
            object data = null;
            var json = JsonSerializer.Serialize(data);

            json.Should().Be("null");
        }

        [TestMethod]
        public void ValidObjectSerializeTest()
        {
            object data = new
            {
                one = true,
                two = 2,
                three = (object)null,
                four = "toads"
            };

            var expectedJson = $"{{{Environment.NewLine}"
                + $"    \"one\": true,{Environment.NewLine}"
                + $"    \"two\": \"2\",{Environment.NewLine}"
                + $"    \"three\": null,{Environment.NewLine}"
                + $"    \"four\": \"toads\"{Environment.NewLine}"
                + "}";

            var json = JsonSerializer.Serialize(data);

            json.Should().Be(expectedJson);
        }

        [TestMethod]
        public void ValidObjectSerializeEscapingTest()
        {
            object data = new
            {
                one = true,
                two = 2,
                three = (object)null,
                four = "I say: \"toads\""
            };

            var expectedJson = $"{{{Environment.NewLine}"
                + $"    \"one\": true,{Environment.NewLine}"
                + $"    \"two\": \"2\",{Environment.NewLine}"
                + $"    \"three\": null,{Environment.NewLine}"
                + $"    \"four\": \"I say: \\\"toads\\\"\"{Environment.NewLine}"
                + "}";

            var json = JsonSerializer.Serialize(data);

            json.Should().Be(expectedJson);
        }
    }
}
