/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Interfaces;
using Clock.Tizen.Mobile.Impls;
using ElmSharp;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(KeyEventSender))]

namespace Clock.Tizen.Mobile.Impls
{
    class KeyEventSender : IKeyEventSender
    {
        private EcoreEvent<EcoreKeyEventArgs> _key;

        /// <summary>
        /// Register KeyEvent
        /// </summary>
        /// <param name="page">Page to get notified about HW key Event</param>
        /// <param name="key">HW key event string</param>
        public void RegisterKeyEvent(Page page, string key)
        {
            Page _page = page;
            // Register key event callbacks for the window using the EcoreEvent.
            _key = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyUp, EcoreKeyEventArgs.Create);
            _key.On += (s, e) =>
            {
                // Send key event to the portable project using MessagingCenter
                if (e.KeyName.Equals(key))
                {
                    // Send key event to the portable project using MessagingCenter
                    MessagingCenter.Send<IKeyEventSender, string>(this, page.Title + key, e.KeyName);
                }
            };

            // Grab key events using the ElmSharp.Window.KeyGrabEx().
            global::Xamarin.Forms.Platform.Tizen.Forms.Context.MainWindow.KeyGrabEx(key);
        }

        /// <summary>
        /// Unregister KeyEvent
        /// </summary>
        /// <param name="page">Page to get notified about HW key Event</param>
        /// <seealso cref="Page"></seealso>
        /// <param name="key">HW key event string</param>
        /// <seealso cref="string"></seealso>
        public void UnregisterKeyEvent(Page page, string key)
        {
            if (_key != null)
            {
                _key.Dispose();
                _key = null;
                // Grab key events using the ElmSharp.Window.KeyGrabEx().
                global::Xamarin.Forms.Platform.Tizen.Forms.Context.MainWindow.KeyUngrabEx(key);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" +++++ [UnregisterKeyEvent] It's already disposed!!  Page(" + page.Title + ") Key(" + key + ")");
            }
        }
    }
}
