/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd
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

using System.Windows.Input;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding;

namespace Calculator.controls
{
    static class CommandButtonBindings
    {
        public static BindingProperty<CommandButton, ICommand> CommandProperty { get; } = new BindingProperty<CommandButton, ICommand>
        {
            Setter = (v, value) => v.Command = value,
        };

        public static BindingProperty<CommandButton, ICommand> LongTapCommandProperty { get; } = new BindingProperty<CommandButton, ICommand>
        {
            Setter = (v, value) => v.LongTapCommand = value,
        };
    }

    internal class CommandButton : ImageView
    {
        public CommandButton(ImageViewStyle style) : base(style)
        {
            BackgroundColor = Color.White;
            CommandParameter = "";

            TouchEvent += OnTouchEvent;

            longPressGestureDetector = new LongPressGestureDetector();
            longPressGestureDetector.Detected += OnLongPressGestureDetected;
            longPressGestureDetector.Attach(this);
        }

        /// <summary>
        /// A command will be executed if the button is touched. </summary>
        public ICommand Command
        {
            get;
            set;
        }

        /// <summary>
        /// A command will be executed if the button is long touched. </summary>
        public ICommand LongTapCommand
        {
            get;
            set;
        }

        /// <summary>
        /// A command parameter will be passed when the Command is executed. </summary>
        /// <see cref="Command"/>
        public string CommandParameter
        {
            get;
            set;
        }

        private bool OnTouchEvent(object s, TouchEventArgs e)
        {
            switch (e.Touch.GetState(0))
            {
                case PointStateType.Down:
                    KeyDown();
                    break;
                case PointStateType.Motion:
                    LineDown();
                    break;
                case PointStateType.Up:
                    ExecuteTapCommand();
                    break;
            }

            return false;
        }

        private void OnLongPressGestureDetected(object source, LongPressGestureDetector.DetectedEventArgs e)
        {
            switch (e.LongPressGesture.State)
            {
                case Gesture.StateType.Finished:
                    ExecuteLongTapCommand();
                    break;
            }
        }

        /// <summary>
        /// A Action delegate which is restore button image as default
        /// and execute button's Command with CommandParameter. </summary>
        private void ExecuteTapCommand()
        {
            Command?.Execute(CommandParameter);
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
            LongTapCommand?.Execute(CommandParameter);
        }

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
        /// Revert the button's color
        /// </summary>
        private void KeyUp()
        {
            ImageColor = RegularColor;
        }

        /// <summary>
        /// A Action delegate which is restore button image as pressed situation. </summary>
        private void KeyDown()
        {
            ImageColor = PressedColor;
            isCanceled = false;
        }

        /// <summary>
        /// indicates if the Tap, Long Tap is canceled due to Line gesture executing.
        /// </summary>
        private volatile bool isCanceled;

        /// <summary>
        /// A Command button's color
        /// </summary>
        private static readonly Color RegularColor = new Color((float)61 / 255, (float)184 / 255, (float)204 / 255, 1.0f);

        /// <summary>
        /// A Command button's color if it is touched.
        /// </summary>
        private static readonly Color PressedColor = new Color((float)34 / 255, (float)104 / 255, (float)115 / 255, 1.0f);

        private LongPressGestureDetector longPressGestureDetector;
    }
}
