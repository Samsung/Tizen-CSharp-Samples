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
using Xamarin.Forms;

namespace AppCommon.Extensions
{
    /// <summary>
    /// The dialog widget displays its content with buttons and title.
    /// </summary>
    /// <example>
    /// <code>
    /// var dialog = new Dialog();
    /// dialog.Title = "Dialog"
    ///
    /// var positive = new Button()
    /// {
    ///     Text = "OK"
    /// }
    /// var negative = new Button()
    /// {
    ///     Text = "Cancel"
    /// }
    /// negative.Clicked += (s,e)=>
    /// {
    ///     dialog.Hide();
    /// }
    ///
    /// dialog.Positive = positive;
    /// dialog.Negative = negative;
    ///
    /// var label = new Label()
    /// {
    ///     Text = "New Dialog"
    /// }
    ///
    /// dialog.Content = label;
    ///
    /// dialog.Show();
    ///
    /// </code>
    /// </example>
    public class Dialog : BindableObject
    {
        /// <summary>
        /// BindableProperty. Identifies the content bindable property.
        /// </summary>
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(Dialog), null);

        /// <summary>
        /// BindableProperty. Identifies the positive bindable property.
        /// </summary>
        public static readonly BindableProperty PositiveProperty = BindableProperty.Create(nameof(Positive), typeof(Button), typeof(Dialog), null);

        /// <summary>
        /// BindableProperty. Identifies the neutral bindable property.
        /// </summary>
        public static readonly BindableProperty NeutralProperty = BindableProperty.Create(nameof(Neutral), typeof(Button), typeof(Dialog), null);

        /// <summary>
        /// BindableProperty. Identifies the negative bindable property.
        /// </summary>
        public static readonly BindableProperty NegativeProperty = BindableProperty.Create(nameof(Negative), typeof(Button), typeof(Dialog), null);

        /// <summary>
        /// BindableProperty. Identifies the title bindable property.
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(Dialog), null);

        IDialog _dialog = null;

        /// <summary>
        /// Occurs when the dialog is hidden.
        /// </summary>
        public event EventHandler Hidden;

        /// <summary>
        /// Occurs when outside of the dialog is clicked.
        /// </summary>
        public event EventHandler OutsideClicked;

        /// <summary>
        /// Occurs when the dialog is shown on the display.
        /// </summary>
        public event EventHandler Shown;

        /// <summary>
        /// Occurs when the device's back button is pressed.
        /// </summary>
        public event EventHandler BackButtonPressed;

        public Dialog()
        {
            _dialog = DependencyService.Get<IDialog>(DependencyFetchTarget.NewInstance);
            if (_dialog == null)
            {
                throw new Exception("Object reference not set to an instance of a Dialog.");
            }

            _dialog.Hidden += (s, e) =>
            {
                Hidden?.Invoke(this, EventArgs.Empty);
            };

            _dialog.OutsideClicked += (s, e) =>
            {
                OutsideClicked?.Invoke(this, EventArgs.Empty);
            };

            _dialog.Shown += (s, e) =>
            {
                Shown?.Invoke(this, EventArgs.Empty);
            };

            _dialog.BackButtonPressed += (s, e) =>
            {
                BackButtonPressed?.Invoke(this, EventArgs.Empty);
            };

            SetBinding(ContentProperty, new Binding(nameof(Content), mode: BindingMode.OneWayToSource, source: _dialog));
            SetBinding(PositiveProperty, new Binding(nameof(Positive), mode: BindingMode.OneWayToSource, source: _dialog));
            SetBinding(NeutralProperty, new Binding(nameof(Neutral), mode: BindingMode.OneWayToSource, source: _dialog));
            SetBinding(NegativeProperty, new Binding(nameof(Negative), mode: BindingMode.OneWayToSource, source: _dialog));
            SetBinding(TitleProperty, new Binding(nameof(Title), mode: BindingMode.OneWayToSource, source: _dialog));
        }

        /// <summary>
        /// Gets or sets content view of the dialog.
        /// </summary>
        public View Content
        {
            get { return (View)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets positive button of the dialog.
        /// This button is on the left.
        /// When used alone, it is variable in size (can increase to the size of dialog).
        /// Dialog's all buttons style is bottom
        /// </summary>
        public Button Positive
        {
            get { return (Button)GetValue(PositiveProperty); }
            set { SetValue(PositiveProperty, value); }
        }

        /// <summary>
        /// Gets or sets neutral button of the dialog.
        /// This button is at the center.
        /// When used alone or used with positive, its size is half the size of the dialog and is on the right.
        /// </summary>
        public Button Neutral
        {
            get { return (Button)GetValue(NeutralProperty); }
            set { SetValue(NeutralProperty, value); }
        }

        /// <summary>
        /// Gets or sets negative button of the dialog.
        /// This button is always on the right and is displayed at a fixed size.
        /// </summary>
        public Button Negative
        {
            get { return (Button)GetValue(NegativeProperty); }
            set { SetValue(NegativeProperty, value); }
        }

        /// <summary>
        /// Gets or sets title of the dialog.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        public void Show()
        {
            _dialog.Show();
        }

        /// <summary>
        /// Hides the dialog.
        /// </summary>
        public void Hide()
        {
            _dialog.Hide();
        }
    }
}