
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

using Xamarin.Forms.Platform.Tizen;

using Calculator.Controls;
using Calculator.Renderers;
using ElmSharp;
using System;

using TizenColor = ElmSharp.Color;
using ImageButtonRenderer = Calculator.Renderers.ImageButtonRenderer;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ImageButton), typeof(ImageButtonRenderer))]
namespace Calculator.Renderers
{
    /// <summary>
    /// Calculator command button custom renderer
    /// Actually to implement command button, A image is used instead a button to display as a calculator button.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin.Forms Custom Renderer
    /// https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/
    /// </remarks>
    class ImageButtonRenderer : ImageRenderer
    {

        /// <summary>
        /// Tizen's gesture recognizer for Tap gesture, Long Tap gesture, Line gesture and so on.
        /// </summary>
        private ElmSharp.GestureLayer GestureRecognizer;

        /// <summary>
        /// Resource directory path
        /// </summary>
        private readonly String ResourceDirectory = Program.AppResourcePath;

        /// <summary>
        /// Command button's color
        /// </summary>
        private static readonly TizenColor RegularColor = Color.White;

        /// <summary>
        /// Command button's color if it is touched.
        /// </summary>
        private static readonly TizenColor PressedColor = new TizenColor(200, 200, 200);

        /// <summary>
        /// Register touch event callback for the Tap, the Long Tap and the Line behavior.
        /// </summary>
        /// <param name="args"> A Image element changed event's argument </param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> args)
        {
            base.OnElementChanged(args);

            if (Control == null || Element == null)
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

            GestureRecognizer.SetTapCallback(GestureLayer.GestureType.Tap, GestureLayer.GestureState.Start, x => KeyDown() );
            GestureRecognizer.SetTapCallback(GestureLayer.GestureType.Tap, GestureLayer.GestureState.End, x => ExecuteTapCommand() );
            GestureRecognizer.SetTapCallback(GestureLayer.GestureType.LongTap, GestureLayer.GestureState.End, x => KeyUp() );
            GestureRecognizer.SetTapCallback(GestureLayer.GestureType.LongTap, GestureLayer.GestureState.Abort, x => KeyUp() );
            GestureRecognizer.SetLineCallback(GestureLayer.GestureState.Move, x => KeyUp() );
        }

        /// <summary>
        /// Set button image's blending color.
        /// It's right time after updating the button image source.
        /// </summary>
        protected override void UpdateAfterLoading(bool initialize)
        {
            base.UpdateAfterLoading(initialize);
            Control.Color = RegularColor;
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
            ImageButton BtnCommand = Element as ImageButton;
            BtnCommand?.Command?.Execute(BtnCommand.CommandParameter);
            KeyUp();
        }

        /// <summary>
        /// A Action delegate which is restore button image as pressed situation. </summary>
        private void KeyDown()
        {
            Control.Color = PressedColor;
        }
    }
}