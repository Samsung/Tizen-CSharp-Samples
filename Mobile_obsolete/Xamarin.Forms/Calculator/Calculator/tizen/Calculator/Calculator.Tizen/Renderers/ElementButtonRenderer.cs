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

using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

using Calculator.Controls;
using Calculator.Tizen.Renderers;

using ElmSharp;
using Tizen;
using System;


using TizenColor = ElmSharp.Color;
using XamarinColor = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(ElementButton), typeof(ElementButtonRenderer))]
namespace Calculator.Tizen.Renderers
{
    /// <summary>
    /// Calculator element (Operator, numbers) button custom renderer
    /// Actually to implement command button, A image is used instead a button to display as a calculator button.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin Custom Renderer
    /// https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/
    /// </remarks>
    class ElementButtonRenderer : ImageRenderer//ViewRenderer<Xamarin.Forms.Image, Image>
    {
        /// <summary>
        /// A flags that indicates whether the touch is handled or not.
        /// </summary>
        private volatile bool isTouched;

        /// <summary>
        /// Tizen's gesture recognizer for Tap gesture, Long Tap gesture, Line gesture and so on.
        /// </summary>
        private ElmSharp.GestureLayer GestureRecognizer;

        /// <summary>
        /// A resource directory path
        /// </summary>
        private String ResourceDirectory = Program.AppResourcePath;

        /// <summary>
        /// A button's blending color
        /// </summary>
        private static TizenColor BlendingColor = new TizenColor(0xFA, 0xFA, 0xFA);

        /// <summary>
        /// A button's background color if it is touched.
        /// </summary>
        private static TizenColor BackgroundPressedColor = new TizenColor(0xFA, 0xFA, 0xFA, 0xAA);

        /// <summary>
        /// Making a button with a image and set the image's color and blending color by inputted background color.
        /// Register touch event callback for the Tap, the Long Tap and the Line behavior. </summary>
        /// <remarks>
        /// When the button is touched, This class should change the image for each touch down/up situation.
        /// Even a button touching  starts at the Tap touch down, but touch up will be happen in several situations such as the Tap, the Long Tap, the Line.
        /// </remarks>
        /// <param name="args"> A Image element changed event's argument </param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> args)
        {
            base.OnElementChanged(args);

            if (Control == null)
            {
                return;
            }

            if (GestureRecognizer == null)
            {
                GestureRecognizer = new ElmSharp.GestureLayer(Control);
                GestureRecognizer.Attach(Control);
            }

            if (args.NewElement == null)
            {
                GestureRecognizer.ClearCallbacks();
                return;
            }

            ElementButton BtnElement = args.NewElement as ElementButton;
            if (BtnElement == null)
            {
                return;
            }

            Control.Color = BlendingColor;
            Control.BackgroundColor = GetColor(BtnElement.BackgroundColor, 1f);

            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.Start, x =>
            {
                KeyDown();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.End, x =>
            {
                KeyUp();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.LongTap, ElmSharp.GestureLayer.GestureState.End, x =>
            {
                KeyUp();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.LongTap, ElmSharp.GestureLayer.GestureState.Abort, x =>
            {
                KeyUp();
            });
            GestureRecognizer.SetLineCallback(GestureLayer.GestureState.Move, x =>
            {
                KeyUp();
            });

        }

        /// <summary>
        /// Set button image's blending color.
        /// It's right time after updating the button image source.
        /// </summary>
        //protected override void UpdateAfterLoading()
        //{
        //    base.UpdateAfterLoading();
        //    Control.Color = BlendingColor;
        //    Control.BackgroundColor = GetColor(Element.BackgroundColor, 1f);
        //}

        /// <summary>
        /// A Action delegate which is restore button image as default
        /// and execute button's Command with CommandParameter. </summary>
        private void KeyUp()
        {
            ElementButton BtnElement = Element as ElementButton;

            if (BtnElement == null)
            {
                isTouched = false;
                return;
            }

            Control.Color = BlendingColor;
            Control.BackgroundColor = GetColor(BtnElement.BackgroundColor, 1f);

            if (isTouched)
            {
                BtnElement.Command?.Execute(BtnElement.CommandParameter);
            }

            isTouched = false;
        }

        /// <summary>
        /// A Action delegate which is restore button image as pressed situation. </summary>
        private void KeyDown()
        {
            float[] hsb = new float[3];
            ElementButton BtnElement = Element as ElementButton;

            if (BtnElement == null ||
                Control == null)
            {
                return;
            }

            isTouched = true;
            Control.Color = GetColor(BtnElement.BlendingPressedColor, 1f);
            Control.BackgroundColor = BackgroundPressedColor;
        }

        private TizenColor GetColor(XamarinColor color, float alpha)
        {
            int R = Convert.ToInt32(color.R * 255.0);
            int G = Convert.ToInt32(color.G * 255.0);
            int B = Convert.ToInt32(color.B * 255.0);
            int A = Convert.ToInt32(color.A * 255.0 * alpha);

            return TizenColor.FromRgba(R, G, B, A);
        }
    }
}