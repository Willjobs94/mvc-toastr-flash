using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedWillow.MvcToastrFlash;
using RedWillow.MvcToastrFlash.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcToastrFlash.Tests.Models
{
    [TestClass]
    public class MessageManagerTests
    {
        private TempDataDictionary _tempData;
        private MessageManager _manager;

        [TestInitialize]
        public void Init()
        {
            _tempData = new TempDataDictionary();
            _manager = new MessageManager(_tempData);
        }

        [TestMethod]
        public void AddMessageTest()
        {
            // No messages yet
            _manager.Count.Should().Be(0);
            _tempData.Should().NotContainKey(typeof(MessageManager).FullName);

            _manager.Add(new FlashMessage(Toastr.INFO, "A message"));

            _manager.Count.Should().Be(1);
            _tempData.Should().ContainKey(typeof(MessageManager).FullName);
        }

        [TestMethod]
        public void EnumerationClearsMessagesTest()
        {
            _manager.Add(new FlashMessage(Toastr.INFO, "Message 1"));
            _manager.Add(new FlashMessage(Toastr.INFO, "Message 2"));

            _manager.Count.Should().Be(2);
            _tempData.Should().ContainKey(typeof(MessageManager).FullName);

            var enumerator = _manager.GetEnumerator();

            // Managers keeps the list of messages, but it's removed from
            // TempData
            _manager.Count.Should().Be(2);
            _tempData.Should().NotContainKey(typeof(MessageManager).FullName);
        }

        [TestMethod]
        public void LoadsExistingMessagesTest()
        {
            var messageList = new List<FlashMessage>();
            messageList.Add(new FlashMessage(Toastr.INFO, "Message 1"));
            messageList.Add(new FlashMessage(Toastr.INFO, "Message 1"));
            _tempData[typeof(MessageManager).FullName] = messageList;

            _manager = new MessageManager(_tempData);

            _manager.Count.Should().Be(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTempDataTest()
        {
            var manager = new MessageManager(null);
        }
    }
}
