/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

using System;
using System.ComponentModel;
using UIComponents.Extensions;
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EBackground = ElmSharp.Background;
using EBackgroundOptions = ElmSharp.BackgroundOptions;

[assembly: ExportRenderer(typeof(Background), typeof(BackgroundRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class BackgroundRenderer : ViewRenderer<Background, EBackground>
    {
        /// <summary>
        /// Constructor of BackgroundRenderer class
        /// </summary>
        public BackgroundRenderer()
        {
        }

        /// <summary>
        /// Called when element is changed.
        /// </summary>
        /// <param name="e">Argument for ElementChangedEventArgs<Background></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Background> e)
        {
            if (Control == null)
            {
                var background = new EBackground(Forms.NativeParent);
                SetNativeControl(background);
            }

            if (e.NewElement != null)
            {
                UpdateImage();
                UpdateOption();
            }

            base.OnElementChanged(e);
        }

        /// <summary>
        /// Called when element property is changed.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">PropertyChangedEvent</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Background.ImageProperty.PropertyName)
            {
                UpdateImage();
            }
            else if (e.PropertyName == Background.OptionProperty.PropertyName)
            {
                UpdateOption();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        /// <summary>
        /// Update image of Background
        /// </summary>
        void UpdateImage()
        {
            if (Element.Image == null)
            {
                Control.File = "";
            }
            else
            {
                Control.File = ResourcePath.GetPath(Element.Image.File);
            }

            Console.WriteLine("Control.File :" + Control.File);
        }

        /// <summary>
        ///  Update BackgroundOption of Background
        /// </summary>
        void UpdateOption()
        {
            Control.BackgroundOption = ConvertToNativeBackgroundOptions(((Background)Element).Option);
        }

        /// <summary>
        /// Convert BackgroundOptions of UIComponents.Extensions to NativeBackgroundOptions
        /// </summary>
        /// <param name="option">BackgroundOptions</param>
        /// <returns>Returns elmsharp background option</returns>
        EBackgroundOptions ConvertToNativeBackgroundOptions(BackgroundOptions option)
        {
            Console.WriteLine("ConvertToNativeBackgroundOptions : " + option);
            if (option == BackgroundOptions.Stretch)
            {
                return EBackgroundOptions.Stretch;
            }
            else if (option == BackgroundOptions.Tile)
            {
                return EBackgroundOptions.Tile;
            }
            else
            {
                return EBackgroundOptions.Scale;
            }
        }
    }
}
