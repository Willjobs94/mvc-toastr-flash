using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedWillow.MvcToastrFlash;
using RedWillow.MvcToastrFlash.Models;
using System;
using System.Linq;

namespace MvcToastrFlash.Tests.Models
{
    [TestClass]
    public class FlashMessageTests
    {
        [TestMethod]
        public void CreateMessageTextTest()
        {
            var message = new FlashMessage(Toastr.INFO, "A message");

            message.Severity.Should().Be(Toastr.INFO);
            message.Title.Should().BeNull();
            message.Message.Should().Be("A message");
            message.Options.Should().BeEmpty();
        }

        [TestMethod]
        public void CreateMessageTitleTextTest()
        {
            var message = new FlashMessage(Toastr.INFO, "A title", "A message");

            message.Severity.Should().Be(Toastr.INFO);
            message.Title.Should().Be("A title");
            message.Message.Should().Be("A message");
            message.Options.Should().BeEmpty();
        }

        [TestMethod]
        public void CreateMessageTest()
        {
            var message = new FlashMessage(Toastr.INFO, "A title", "A message", new { one = 1 });

            message.Severity.Should().Be(Toastr.INFO);
            message.Title.Should().Be("A title");
            message.Message.Should().Be("A message");
            message.Options.Should().NotBeEmpty();
        }

        [TestMethod]
        public void FlashMessageIsMarkedSerializableTest()
        {
            var attributes = typeof(FlashMessage).GetCustomAttributes(true);
            var isSerializable = attributes.Any(a => a is SerializableAttribute);

            isSerializable.Should().BeTrue();
        }
    }
}
