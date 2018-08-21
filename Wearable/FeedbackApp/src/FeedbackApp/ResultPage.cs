
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

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace FeedbackApp
{
    /// <summary>
    /// Result page of Feedback application
    /// </summary>
    class ResultPage : CirclePage
    {
        /// <summary>
        /// Create a Label instance
        /// </summary>
        /// <param name="text">Text string for label</param>
        /// <returns>Created Label</returns>
        private Label CreateLabel(string text)
        {
            // Create a new Label for given text
            return new Label
            {
                Text = text,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }

        /// <summary>
        /// The constructor of ResultPage
        /// </summary>
        /// <param name="pattern">User selected pattern</param>
        /// <param name="result">Feedback play result</param>
        public ResultPage(string pattern, bool result)
        {
            NavigationPage.SetHasNavigationBar(this, false);

            Label patternLabel, resultLabel;

            // Label for pattern string
            patternLabel = CreateLabel(pattern);

            // Create different label as feedback play result
            // Success case
            if (result)
            {
                // Text displayed in case of successful playback
                resultLabel = CreateLabel("Play Succeeded");
            }
            // Fail case
            else
            {
                // Text displayed in case of unsuccessful playback
                resultLabel = CreateLabel("Not Supported");
            }
            
            // Create a StackLayout containting Labels
            // Displays pattern name and playback result
            this.Content = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    patternLabel,
                    resultLabel,
                }
            };
        }
    }
}