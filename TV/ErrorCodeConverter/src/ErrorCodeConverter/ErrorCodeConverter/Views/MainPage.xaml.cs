/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Xamarin.Forms;
using ErrorCodeConverter.Interfaces;
using System.Windows.Input;

namespace ErrorCodeConverter.Views
{
    /// <summary>
    /// Application main page.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        #region fields

        /// <summary>
        /// Keyboard service instance.
        /// </summary>
        private IKeyboard _keyboard;

        #endregion

        #region properties

        /// <summary>
        /// Command increasing error code.
        /// </summary>
        private ICommand IncreaseErrorCodeCommand
        {
            get => (ICommand)GetValue(IncreaseErrorCodeCommandProperty);
            set => SetValue(IncreaseErrorCodeCommandProperty, value);
        }

        /// <summary>
        /// Bindable property for <see cref="IncreasErrorCodeCommand">IncreasErrorCodeCommand</see>
        /// </summary>
        public static BindableProperty IncreaseErrorCodeCommandProperty =
            BindableProperty.Create(nameof(IncreaseErrorCodeCommand), typeof(ICommand), typeof(MainPage));

        /// <summary>
        /// Command decreasing error code.
        /// </summary>
        private ICommand DecreaseErrorCodeCommand
        {
            get => (ICommand)GetValue(DecreaseErrorCodeCommandProperty);
            set => SetValue(DecreaseErrorCodeCommandProperty, value);
        }

        /// <summary>
        /// Bindable property for <see cref="DecreaseErrorCodeCommand">DecreaseErrorCodeCommand</see>
        /// </summary>
        public static BindableProperty DecreaseErrorCodeCommandProperty =
            BindableProperty.Create(nameof(DecreaseErrorCodeCommand), typeof(ICommand), typeof(MainPage));


        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// Initializes page component.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            _keyboard = DependencyService.Get<IKeyboard>();

            _keyboard.ArrowDownKeyDown += (s, e) =>
            {
                IncreaseErrorCodeCommand?.Execute(null);
            };

            _keyboard.ArrowUpKeyDown += (s, e) =>
            {
                DecreaseErrorCodeCommand?.Execute(null);
            };
        }

        #endregion
    }
}