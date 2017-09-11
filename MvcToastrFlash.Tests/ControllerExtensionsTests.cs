using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using MvcToastrFlash.Tests.Mocks;
using RedWillow.MvcToastrFlash;
using RedWillow.MvcToastrFlash.Models;
using FluentAssertions;

namespace MvcToastrFlash.Tests
{
    [TestClass]
    public class ControllerExtensionsTests
    {
        private Controller _controller;

        [TestInitialize]
        public void Init()
        {
            _controller = new MockedController();
        }

        [TestMethod]
        public void CanFlashOneMessageTest()
        {
            _controller.Flash(Toastr.INFO, "A message");

            var manager = new MessageManager(_controller.TempData);
            manager.Count.Should().Be(1);
        }

        [TestMethod]
        public void CanFlashMultipleMessagesTest()
        {
            _controller.Flash(Toastr.INFO, "A message");
            _controller.Flash(Toastr.INFO, "Another message");

            var manager = new MessageManager(_controller.TempData);
            manager.Count.Should().Be(2);
        }
    }
}
