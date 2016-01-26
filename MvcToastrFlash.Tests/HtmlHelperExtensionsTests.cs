using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using RedWillow.MvcToastrFlash.Models;
using RedWillow.MvcToastrFlash;
using FluentAssertions;

namespace MvcToastrFlash.Tests
{
    [TestClass]
    public class HtmlHelperExtensionsTests
    {
        private HtmlHelper _htmlHelper;

        [TestInitialize]
        public void Init()
        {
            var viewPage = new ViewPage();
            var viewContext = new ViewContext();
            viewContext.TempData = new TempDataDictionary();

            _htmlHelper = new HtmlHelper(viewContext, viewPage);
        }

        [TestMethod]
        public void NoNotificationTest()
        {
            var html = _htmlHelper.ToastrNotifications();

            html.ToString().Should().BeEmpty();
        }

        [TestMethod]
        public void OneNotificationTest()
        {
            AddFlashMessages(1);

            var html = _htmlHelper.ToastrNotifications();

            html.ToString().Should().NotBeEmpty();
            html.ToString().Should().Contain("toastr.info(");
        }

        [TestMethod]
        public void MultipleNotificationsCLearingTest()
        {
            AddFlashMessages(2);

            var html = _htmlHelper.ToastrNotifications();
            html.ToString().Should().NotBeEmpty();

            html = _htmlHelper.ToastrNotifications();
            html.ToString().Should().BeEmpty();
        }

        private void AddFlashMessages(int number)
        {
            if (number <= 0) return;

            var manager = new MessageManager(_htmlHelper.ViewContext.TempData);

            while (number-- >= 0)
            {
                manager.Add(new FlashMessage(Toastr.INFO, $"Message {number}"));
            }
        }
    }
}
