/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using Xamarin.Forms;

namespace ImageGallery.Tizen.Mobile.Controls.ToastView
{
    /// <summary>
    /// This class is used internally by the Toast class.
    /// </summary>
    internal class ToastProxy : IToast
    {
        #region fields

        /// <summary>
        /// An instance of class implementing IToast interface.
        /// </summary>
        IToast _toastProxy = null;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public int Duration
        {
            get => _toastProxy.Duration;
            set => _toastProxy.Duration = value;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get => _toastProxy.Text;
            set => _toastProxy.Text = value;
        }

        #endregion

        #region methods

        /// <summary>
        /// ToastProxy class constructor.
        /// </summary>
        public ToastProxy()
        {
            _toastProxy = DependencyService.Get<IToast>(DependencyFetchTarget.NewInstance);

            if (_toastProxy == null)
            {
                throw new Exception("RealObject is null, Internal instance via DependencyService was not created.");
            }
        }

        /// <summary>
        /// Dismisses the specified view.
        /// </summary>
        public void Dismiss()
        {
            _toastProxy.Dismiss();
        }

        /// <summary>
        /// Shows the view for the specified duration.
        /// </summary>
        public void Show()
        {
            _toastProxy.Show();
        }

        #endregion
    }
}
