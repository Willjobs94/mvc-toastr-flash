using System;
using System.Collections;
using System.Collections.Generic;
#if (FULLBUILD)
using ITempDataDictionary = System.Web.Mvc.TempDataDictionary;
#else
using Microsoft.AspNetCore.Mvc.ViewFeatures;
#endif

namespace RedWillow.MvcToastrFlash.Models
{
    /// <summary>
    /// Manager used for storing <see cref="FlashMessage"/> messages in the Controller's
    /// <see cref="ITempDataDictionary"/> and for providing access to them at render time.
    /// When the message is rendered (iterated over) it is removed from the
    /// <see cref="ITempDataDictionary"/> store.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{FlashMessage}" />
    internal class MessageManager : IEnumerable<FlashMessage>
    {
        private readonly ITempDataDictionary _tempData;
        private readonly List<FlashMessage> _messageList;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageManager"/> class.
        /// </summary>
        /// <param name="tempData">The Controller's temporary data dictionary.</param>
        /// <exception cref="System.ArgumentNullException">If tempData is null.</exception>
        public MessageManager(ITempDataDictionary tempData)
        {
            if (tempData == null)
                throw new ArgumentNullException("tempData");

            _tempData = tempData;
            _messageList = new List<FlashMessage>();

            var currentMessages = (_tempData[typeof(MessageManager).FullName] as IEnumerable<FlashMessage>) ?? new List<FlashMessage>();
            _messageList.AddRange(currentMessages);
        }

        /// <summary>
        /// Gets the number of <see cref="FlashMessage"/> stored.
        /// </summary>
        public int Count
        {
            get
            {
                return _messageList.Count;
            }
        }

        /// <summary>
        /// Adds the specified <see cref="FlashMessage"/>.
        /// </summary>
        /// <param name="item">The <see cref="FlashMessage"/>.</param>
        public void Add(FlashMessage item)
        {
            _messageList.Add(item);
            SaveMessageList();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<FlashMessage> GetEnumerator()
        {
            try
            {
                return _messageList.GetEnumerator();
            }
            finally
            {
                _tempData.Remove(typeof(MessageManager).FullName);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Saves the message list to the <see cref="ITempDataDictionary"/>.
        /// </summary>
        private void SaveMessageList()
        {
            _tempData[typeof(MessageManager).FullName] = _messageList;
            _tempData.Keep(typeof(MessageManager).FullName);
        }
    }
}
