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
using System.Windows.Input;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponents.Samples.CircleList
{
    /// <summary>
    /// StyleSelectMode class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StyleSelectMode : CirclePage
    {
        public static BindableProperty IsCheckableProperty = BindableProperty.Create(nameof(IsCheckable), typeof(bool), typeof(StyleSelectMode), false);
        public static BindableProperty PopupVisibilityProperty = BindableProperty.Create(nameof(PopupVisibility), typeof(bool), typeof(StyleSelectMode), false);

        /// <summary>
        /// Constructor of StyleSelectMode class
        /// </summary>
        public StyleSelectMode()
        {
            IsCheckable = false;
            LongClickCommand = new Command(OnLongClick);
            InitializeComponent();
        }

        // For List item LongClick
        public ICommand LongClickCommand { get; set; }

        /// <summary>
        /// State of Check of list item
        /// </summary>
        public bool IsCheckable
        {
            get => (bool)GetValue(IsCheckableProperty);
            set => SetValue(IsCheckableProperty, value);
        }

        /// <summary>
        /// State of visibility of select mode context popup
        /// </summary>
        public bool PopupVisibility
        {
            get => (bool)GetValue(PopupVisibilityProperty);
            set => SetValue(PopupVisibilityProperty, value);
        }

        /// <summary>
        /// LongClick command excute method
        /// </summary>
        void OnLongClick()
        {
            if (!IsCheckable)
            {
                IsCheckable = true;
            }
        }


        /// <summary>
        /// Button event handler of select mode button
        /// Request to display context popup 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        void OnCheckedCounterClicked(object sender, EventArgs e)
        {
            PopupVisibility = true;

            Console.WriteLine("Checked is clicked!!!");
        }
    }
}