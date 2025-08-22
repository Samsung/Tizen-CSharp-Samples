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
using Xamarin.Forms.Platform.Tizen;
using EImage = ElmSharp.Image;
using AppCommon.Extensions;
using AppCommon.Tizen.Mobile.Effects;
using System;

[assembly: ExportEffect(typeof(ImageClickEffect), "ImageClickEffect")]

namespace AppCommon.Tizen.Mobile.Effects
{
    /// <summary>
    /// A class to get a click event of an image
    /// </summary>
    public class ImageClickEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var image = (EImage)Control;
            image.Clicked += SendClicked;
        }

        protected override void OnDetached()
        {
            var image = (EImage)Control;
            image.Clicked -= SendClicked;
        }

        /// <summary>
        /// To send a click event to the element
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        void SendClicked(object sender, EventArgs e)
        {
            ((AppCommon.Extensions.ImageButton)Element).SendClicked();
        }
    }
}