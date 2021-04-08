using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace FeedbackApp
{
    /// <summary>
    /// Result page of Feedback application
    /// </summary>
    class ResultPage : ContentPage
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
                FontSize = 25
            };
        }

        /// <summary>
        /// The constructor of ResultPage
        /// </summary>
        /// <param name="pattern">User tapped pattern</param>
        /// <param name="result">Feedback play result</param>
        public ResultPage(string pattern, bool result)
        {
            Label label, label2;

            // Label for pattern string
            label = CreateLabel(pattern);

            // Create different label as feedback play result
            // Success case
            if (result)
            {
                // Label for Feedback play success case
                label2 = CreateLabel("Play Succeed");
            }
            // Fail case
            else
            {
                // Label for Feedback play fail case
                label2 = CreateLabel("Not Supported");
            }

            // Create a StackLayout with Labels
            // Show label and label2 on different line
            this.Content = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    label,
                    label2,
                }
            };
        }
    }
}
