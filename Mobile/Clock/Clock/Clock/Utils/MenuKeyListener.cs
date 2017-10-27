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
using System;
using Xamarin.Forms;

namespace Clock.Utils
{
    /// <summary>
    /// Defines Key listener for menu key press.
    /// </summary>
    public class MenuKeyListener
    {
        /// <summary>
        /// Key menu used in Tizen
        /// </summary>
        public const string KEY_MENU = "XF86Menu";

        /// <summary>
        /// Start listening HW Menu key and subscribing from messages about pressing HW meny key
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="onKeyPressed">Action<IKeyEventSender, string></param>
        public static void Start(Page page, Action<IKeyEventSender, string> onKeyPressed)
        {
            // Register Menu KeyEvent
            DependencyService.Get<IKeyEventSender>().RegisterKeyEvent(page, KEY_MENU);
            // Subscribe from messages so that no future messages about pressing HW Menu Key are delivered.
            MessagingCenter.Subscribe<IKeyEventSender, string>(page, page.Title + KEY_MENU, onKeyPressed);
        }

        /// <summary>
        /// Stop listening HW Menu key and subscribing from messages about pressing HW meny key
        /// </summary>
        /// <param name="page">Page</param>
        public static void Stop(Page page)
        {
            // Unsubscribe from messages so that no future messages about pressing HW Menu Key are delivered.
            MessagingCenter.Unsubscribe<IKeyEventSender, string>(page, page.Title + KEY_MENU);
            // Unregister Menu KeyEvent
            DependencyService.Get<IKeyEventSender>().UnregisterKeyEvent(page, KEY_MENU);
        }
    }
}
