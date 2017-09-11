using RedWillow.MvcToastrFlash.Serializers;
using System;

namespace RedWillow.MvcToastrFlash.Models
{
    /// <summary>
    /// Object that represents a Toastr notification message.
    /// </summary>
#if (FULLBUILD)
    [Serializable]
#endif
    internal class FlashMessage
    {
        /// <summary>
        /// Gets the severity of this flash message.
        /// </summary>
        public Toastr Severity { get; private set; }

        /// <summary>
        /// Gets the flash message title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the flash message text.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the flash message options.
        /// </summary>
        public string Options { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlashMessage"/> class.
        /// </summary>
        /// <param name="severity">The message severity.</param>
        /// <param name="title">The message title.</param>
        /// <param name="message">The message text.</param>
        /// <param name="options">The message options.</param>
        public FlashMessage(Toastr severity, string title, string message, object options)
        {
            Severity = severity;
            Title = title;
            Message = message;
            Options = options != null ? JsonSerializer.Serialize(options) : string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlashMessage"/> class.
        /// </summary>
        /// <param name="severity">The message severity.</param>
        /// <param name="title">The message title.</param>
        /// <param name="message">The message text.</param>
        public FlashMessage(Toastr severity, string title, string message)
            : this(severity, title, message, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlashMessage"/> class.
        /// </summary>
        /// <param name="severity">The message severity.</param>
        /// <param name="message">The message text.</param>
        public FlashMessage(Toastr severity, string message)
            : this(severity, null, message, null)
        { }

    }
}
