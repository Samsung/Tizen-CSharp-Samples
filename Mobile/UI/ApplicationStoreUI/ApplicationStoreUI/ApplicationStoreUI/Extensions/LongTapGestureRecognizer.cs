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

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApplicationStoreUI.Extensions
{
    /// <summary>
    /// A recognizer for the long-tap gesture.
    /// </summary>
    /// <example>
    /// <code>
    /// var image = new Image { Source = ImageSource.FromFile("picture.png") };
    /// var longTapGesture = new LongTapGestureRecognizer
    /// {
    ///     Timeout = 0.05
    /// };
    /// longTapGesture.LongTapUpdated += (sender, args) =>
    /// {
    ///     Debug.WriteLine("LongTap updated : " + args.Status);
    /// };
    /// image.GestureRecognizers.Add(longTapGesture);
    /// </code>
    /// </example>
    public class LongTapGestureRecognizer : Element, IGestureRecognizer, ILongTapGestureController
    {
        /// <summary>
        /// BindableProperty. Identifies the timeout bindable property.
        /// </summary>
        public static readonly BindableProperty TimeoutProperty = BindableProperty.Create("Timeout", typeof(double), typeof(LongTapGestureRecognizer), default(double));

        /// <summary>
        /// BindableProperty. Identifies the TapStartedCommand bindable property.
        /// </summary>
        public static readonly BindableProperty TapStartedCommandProperty = BindableProperty.Create("TapStartedCommand", typeof(ICommand), typeof(LongTapGestureRecognizer), null);

        /// <summary>
        /// BindableProperty. Identifies the TapCompletedCommand bindable property.
        /// </summary>
        public static readonly BindableProperty TapCompletedCommandProperty = BindableProperty.Create("TapCompletedCommand", typeof(ICommand), typeof(LongTapGestureRecognizer), null);

        /// <summary>
        /// BindableProperty. Identifies the TapStartedCommandParameter bindable property.
        /// </summary>
        public static readonly BindableProperty TapStartedCommandParameterProperty = BindableProperty.Create("TapStartedCommandParameter", typeof(object), typeof(LongTapGestureRecognizer), null);

        /// <summary>
        /// BindableProperty. Identifies the TapCompletedCommandParameter bindable property.
        /// </summary>
        public static readonly BindableProperty TapCompletedCommandParameterProperty = BindableProperty.Create("TapCompletedCommandParameter", typeof(object), typeof(LongTapGestureRecognizer), null);

        /// <summary>
        /// Event that is raised when the long-tap gesture updates.
        /// </summary>
        public event EventHandler<LongTapUpdatedEventArgs> LongTapUpdated;

        /// <summary>
        /// Event that is raised when the user presses the view.
        /// </summary>
        public event EventHandler TapStarted;

        /// <summary>
        /// Event that is raised when the user releases the view after pressing the view.
        /// </summary>
        public event EventHandler TapCompleted;

        /// <summary>
        /// Event that is raised when the wrong gesture is detected after pressing the view.
        /// </summary>
        public event EventHandler TapCanceled;

        /// <summary>
        /// Gets or sets the minimum time(seconds) the user should press the view for triggering a long-tap event.
        /// </summary>
        /// <remarks>
        /// The default value is defined in the system policy. In Tizen, the default value is 0.33 seconds.
        /// </remarks>
        public double Timeout
        {
            get { return (double)GetValue(TimeoutProperty); }
            set { SetValue(TimeoutProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command to invoke when the pressed event occurs.
        /// </summary>
        public ICommand TapStartedCommand
        {
            get { return (ICommand)GetValue(TapStartedCommandProperty); }
            set { SetValue(TapStartedCommandProperty, value); }
        }

        /// <summary>
        /// Gests or sets an object to be passed when the PressedCommand is executed.
        /// </summary>
        public object TapStartedCommandParameter
        {
            get { return GetValue(TapStartedCommandParameterProperty); }
            set { SetValue(TapStartedCommandParameterProperty, value); }
        }

        /// <summary>
        /// Gests or sets the command to invoke when the released event occurs.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public ICommand TapCompletedCommand
        {
            get { return (ICommand)GetValue(TapCompletedCommandProperty); }
            set { SetValue(TapCompletedCommandProperty, value); }
        }

        /// <summary>
        /// Gests or sets an object to be passed when the ReleasedCommand is executed.
        /// </summary>
        public object TapCompletedCommandParameter
        {
            get { return GetValue(TapCompletedCommandParameterProperty); }
            set { SetValue(TapCompletedCommandParameterProperty, value); }
        }

        void ILongTapGestureController.SendLongTapStarted(Element sender, double timeStamp)
        {
            ICommand cmd = TapStartedCommand;
            if (cmd != null && cmd.CanExecute(TapStartedCommandParameter))
                cmd.Execute(TapStartedCommandParameter);

            TapStarted?.Invoke(sender, EventArgs.Empty);
            LongTapUpdated?.Invoke(sender, new LongTapUpdatedEventArgs(GestureStatus.Started, timeStamp));
        }

        void ILongTapGestureController.SendLongTapCompleted(Element sender, double timeStamp)
        {
            ICommand cmd = TapCompletedCommand;
            if (cmd != null && cmd.CanExecute(TapCompletedCommandParameter))
                cmd.Execute(TapCompletedCommandParameter);

            TapCompleted?.Invoke(sender, EventArgs.Empty);
            LongTapUpdated?.Invoke(sender, new LongTapUpdatedEventArgs(GestureStatus.Completed, timeStamp));
        }

        void ILongTapGestureController.SendLongTapCanceled(Element sender, double timeStamp)
        {
            TapCanceled?.Invoke(sender, EventArgs.Empty);
            LongTapUpdated?.Invoke(sender, new LongTapUpdatedEventArgs(GestureStatus.Canceled, timeStamp));
        }
    }
}