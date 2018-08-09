//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Weather.Utils
{
    /// <summary>
    /// Class responsible for handling HTTP errors and displaying information about them.
    /// </summary>
    public static class ErrorHandler
    {
        #region fields

        /// <summary>
        /// Navigation context.
        /// </summary>
        private static readonly INavigation _navigation;

        /// <summary>
        /// Indicates if any error was handled.
        /// Avoids handling multiple error at once.
        /// </summary>
        private static bool _isHandled;

        #endregion

        #region properties

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public static int Code { get; private set; }

        /// <summary>
        /// Message provided with exception.
        /// </summary>
        public static string Message { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Default constructor.
        /// </summary>
        static ErrorHandler()
        {
            _navigation = Application.Current.MainPage.Navigation;
        }

        /// <summary>
        /// Handles exception.
        /// </summary>
        /// <param name="code">HTTP status code.</param>
        /// <param name="message">Message provided with exception.</param>
        /// <returns>Async task.</returns>
        public static async Task HandleException(int code, string message)
        {
            if (_isHandled)
            {
                return;
            }

            Code = code;
            Message = message;

            await _navigation.PushAsync(new Views.ApiErrorPage());

            RemoveExistingPages();

            _isHandled = true;
        }

        /// <summary>
        /// Removes all pages from navigation stack except error page.
        /// </summary>
        private static void RemoveExistingPages()
        {
            var existingPages = _navigation.NavigationStack.ToList();

            foreach (var page in existingPages)
            {
                if (page.GetType() != typeof(Views.ApiErrorPage))
                {
                    _navigation.RemovePage(page);
                }
            }
        }

        #endregion
    }
}