/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;

namespace ApplicationStoreUI.Extensions
{
    /// <summary>
    /// This interface is for internal use by the platform renderer.
    /// </summary>
    public interface ILongTapGestureController
    {
        /// <summary>
        /// For internal use by platform renderes.
        /// </summary>
        /// <param name="sender">The Element that raised the event.</param>
        /// <param name="timeStamp">The timestamp when longtap started.</param>
        void SendLongTapStarted(Element sender, double timeStamp);

        /// <summary>
        /// For internal use by platform renderes.
        /// </summary>?
        /// <param name="sender">The Element that raised the event.</param>
        /// <param name="timeStamp">The timestamp when longtap ended.</param>
        void SendLongTapCompleted(Element sender, double timeStamp);

        /// <summary>
        /// For internal use by platform renderes.
        /// </summary>
        /// <param name="sender">The Element that raised the event.</param>
        /// <param name="timeStamp">The timestamp when longtap canceled.</param>
        void SendLongTapCanceled(Element sender, double timeStamp);
    }
}
