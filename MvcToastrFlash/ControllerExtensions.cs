using RedWillow.MvcToastrFlash.Models;
using System.Web.Mvc;

namespace RedWillow.MvcToastrFlash
{
    /// <summary>
    /// Extension methods for <see cref="Controller"/>s to add messages for display on page load.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Adds a Toastr notification of the specified severity to be displayed on page load.
        /// </summary>
        /// <param name="controller">The controller to extend.</param>
        /// <param name="severity">The message severity.</param>
        /// <param name="message">The message text.</param>
        public static void Flash(this Controller controller, Toastr severity, string message)
        {
            var messages = new MessageManager(controller.TempData);
            messages.Add(new FlashMessage(severity, message));
        }

        /// <summary>
        /// Adds a Toastr notification of the specified severity to be displayed on page load.
        /// </summary>
        /// <param name="controller">The controller to extend.</param>
        /// <param name="severity">The message severity.</param>
        /// <param name="title">The message title.</param>
        /// <param name="message">The message text.</param>
        public static void Flash(this Controller controller, Toastr severity, string title, string message)
        {
            var messages = new MessageManager(controller.TempData);
            messages.Add(new FlashMessage(severity, title, message));
        }

        /// <summary>
        /// Adds a Toastr notification of the specified severity to be displayed on page load.
        /// </summary>
        /// <param name="controller">The controller to extend.</param>
        /// <param name="severity">The message severity.</param>
        /// <param name="title">The message title.</param>
        /// <param name="message">The message text.</param>
        /// <param name="options">The message options.</param>
        public static void Flash(this Controller controller, Toastr severity, string title, string message, object options)
        {
            var messages = new MessageManager(controller.TempData);
            messages.Add(new FlashMessage(severity, title, message, options));
        }
    }
}
