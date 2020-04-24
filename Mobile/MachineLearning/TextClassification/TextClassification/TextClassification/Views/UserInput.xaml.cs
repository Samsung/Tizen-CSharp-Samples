/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;
using Log = Tizen.Log;

/// <summary>
/// Show User input page
/// </summary>
namespace TextClassification.Views
{
    /// <summary>
    /// Content page of User input
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserInput : ContentPage
    {
        const string TAG = "TextClassification.Views.UserInput";
        private HandleText TextModel = new HandleText();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public UserInput()
        {
            InitializeComponent();

            var entryComment = new Entry();
        }

        /// <summary>
        /// Event when "Get result" button is clicked.
        /// </summary>
        /// <param name="sender">Instance of the object which invokes event.</param>
        /// <param name="args"> Event argument.</param>
        public async void CreateClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            InputLabel.Text = UserCommentEntry.Text;
            float[] input;
            float[] output = new float[2];

            /// Initialize the model
            TextModel.Init_model();
            /// Convert user input to float array
            input = TextModel.SplitStr(UserCommentEntry.Text);

            if (string.IsNullOrEmpty(UserCommentEntry.Text))
            {
                Log.Warn(TAG, "User input is empty");
                /// Check wrong Input parameter
                await DisplayAlert("Error!", "Please enter your comment", "OK");
                return;
            }
            else
            {
                /// Invoke the model
                API.Text = "Single-shot";
                output = TextModel.Invoke_model(input);
            }

            /// Show text classification resut
            if (output[0].Equals(0) && output[1].Equals(0))
            {
                Out_neg.Text = "Invalid result";
                Out_pos.Text = "InValid result";
            }
            else
            {
                Out_neg.Text = output[0].ToString();
                Out_pos.Text = output[1].ToString();
            }
        }
    }
}
