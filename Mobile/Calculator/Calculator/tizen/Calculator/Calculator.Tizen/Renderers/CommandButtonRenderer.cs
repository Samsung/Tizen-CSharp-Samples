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
using System;

using TizenColor = ElmSharp.Color;
using Tizen;
using System.Threading;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(CommandButton), typeof(CommandButtonRenderer))]
namespace Calculator.Tizen.Renderers
{
    /// <summary>
    /// Calculator command button custom renderer
    /// Actually to implement command button, A image is used instead a button to display as a calculator button.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin.Forms Custom Renderer
    /// https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/
    /// </remarks>
    class CommandButtonRenderer : ImageRenderer//ViewRenderer<Xamarin.Forms.Image, Image>
    {
        /// <summary>
        /// indicates if the Tap, Long Tap is canceled due to Line gesture executing.
        /// </summary>
        private volatile bool isCanceled;

        /// <summary>
        /// Tizen's gesture recognizer for Tap gesture, Long Tap gesture, Line gesture and so on.
        /// </summary>
        private ElmSharp.GestureLayer GestureRecognizer;

        /// <summary>
        /// A resource directory path
        /// </summary>
        private readonly String ResourceDirectory = Program.AppResourcePath;

        /// <summary>
        /// A Command button's color
        /// </summary>
        private static readonly TizenColor RegularColor = new TizenColor(61, 184, 204);

        /// <summary>
        /// A Command button's color if it is touched.
        /// </summary>
        private static readonly TizenColor PressedColor = new TizenColor(34, 104, 115);

        /// <summary>
        /// Register touch event callback for the Tap, the Long Tap and the Line behavior. </summary>
        /// <remarks>
        /// When the button is touched, This class should change the image for each touch down/up situation.
        /// Even a button touching  starts at the Tap touch down, but touch up will be happen in several situations such as the Tap, the Long Tap, the Line.
        /// </remarks>
        /// <param name="args"> A Image element changed event's argument </param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> args)
        {
            base.OnElementChanged(args);

            if (Control == null ||
                Element == null)
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

            Control.Color = RegularColor;

            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.Start, x =>
            {
                KeyDown();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.End, x =>
            {
                ExecuteTapCommand();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.LongTap, ElmSharp.GestureLayer.GestureState.End, x =>
            {
                KeyUp();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.LongTap, ElmSharp.GestureLayer.GestureState.Move, x =>
            {
                ExecuteLongTapCommand();
            });
            GestureRecognizer.SetLineCallback(GestureLayer.GestureState.Move, x =>
            {
                LineDown();
            });
        }

        /// <summary>
        /// Set button image's blending color.
        /// It's right time after updating the button image source.
        /// </summary>
        //protected override void UpdateAfterLoading()
        //{
        //    base.UpdateAfterLoading();
        //    Control.Color = RegularColor;
        //}

        /// <summary>
        /// Cancel command executing for Tap, Long Tap
        /// Also revert the button's color
        /// </summary>
        private void LineDown()
        {
            isCanceled = true;
            KeyUp();
        }

        /// <summary>
        /// Execute LongTap command
        /// But the command will not executed if the Line gesture is executed.
        /// </summary>
        private void ExecuteLongTapCommand()
        {
            if (isCanceled)
            {
                return;
            }

            CommandButton BtnCommand = Element as CommandButton;
            BtnCommand?.LongTabCommand?.Execute(BtnCommand.CommandParameter);
        }

        /// <summary>
        /// Revert the button's color
        /// </summary>
        private void KeyUp()
        {
            Control.Color = RegularColor;
        }

        /// <summary>
        /// A Action delegate which is restore button image as default
        /// and execute button's Command with CommandParameter. </summary>
        private void ExecuteTapCommand()
        {
            CommandButton BtnCommand = Element as CommandButton;
            BtnCommand?.Command?.Execute(BtnCommand.CommandParameter);
            KeyUp();
        }

        /// <summary>
        /// A Action delegate which is restore button image as pressed situation. </summary>
        private void KeyDown()
        {
            Control.Color = PressedColor;
            isCanceled = false;
        }
    }
}