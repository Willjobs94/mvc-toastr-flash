using RedWillow.MvcToastrFlash.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace RedWillow.MvcToastrFlash
{
    /// <summary>
    /// Extensions for HtmlHelper to render Toastr notifications.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders the JavaScript script block to display Toastr notifications.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper to extend.</param>
        /// <returns>JavaScript script block to display Toastr notifications.</returns>
        public static MvcHtmlString ToastrNotifications(this HtmlHelper htmlHelper)
        {
            var messages = new MessageManager(htmlHelper.ViewContext.TempData);
            if (messages.Count <= 0)
                return new MvcHtmlString(string.Empty);

            var toastrCalls = new StringBuilder();
            foreach (var message in messages)
            {
                if (string.IsNullOrWhiteSpace(message.Message))
                    continue;

                toastrCalls.AppendLine(string.Format("toastr.{0}({1});",
                    GetToastrFunctionCall(message.Severity), GetToastrFunctionParameters(message)));
            }

            return new MvcHtmlString(GetScriptBlock(toastrCalls.ToString()));
        }

        /// <summary>
        /// Returns the function parameters for the toastr function call based on the
        /// contents of the message.
        /// </summary>
        /// <param name="message">Flash message to display.</param>
        /// <returns>Parameters for the JavaScript function call.</returns>
        private static object GetToastrFunctionParameters(FlashMessage message)
        {
            var parameters = new List<string>();

            parameters.Add(string.Format("'{0}'", message.Message.Replace("'", "\\'")));

            if (!string.IsNullOrWhiteSpace(message.Title))
                parameters.Add(string.Format("'{0}'", message.Title.Replace("'", "\\'")));

            if (!string.IsNullOrWhiteSpace(message.Options))
            {
                if (parameters.Count <= 1)
                {
                    // Title hasn't been provided, but options have been. Add null title.
                    parameters.Add("null");
                }

                parameters.Add(message.Options);
            }

            return string.Join(", ", parameters);
        }

        /// <summary>
        /// Returns the correct toastr function name based on the message severity.
        /// </summary>
        /// <param name="severity">Message severity.</param>
        /// <returns>Toastr function name.</returns>
        private static object GetToastrFunctionCall(Toastr severity)
        {
            switch (severity)
            {
                case Toastr.SUCCESS:
                    return "success";
                case Toastr.INFO:
                    return "info";
                case Toastr.WARNING:
                    return "warning";
                case Toastr.ERROR:
                    return "error";
                default:
                    throw new ArgumentException("Unknown severity value", "severity");
            }
        }

        /// <summary>
        /// Gets the script block that triggers the toastr notifications.
        /// </summary>
        /// <param name="toastrCalls">JavaScript command to trigger the notifications.</param>
        /// <returns>JavaScript block.</returns>
        private static string GetScriptBlock(string toastrCalls)
        {
            return string.Format(@"
<script type=""text/javascript"">
window.onload = function () {{
    {0}
}};
</script>", toastrCalls);
        }
    }
}
