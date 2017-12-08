using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Tizen;
using Tizen.System;

namespace FeedbackApp
{
    /// <summary>
    /// Second page of Feedback application
    /// User can select feedback type
    /// </summary>
    class SecondPage : ContentPage
    {
        private string pattern;

        /// <summary>
        /// Asynchronous method for tapped event of type item
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="a">Event argument</param>
        private async void Type_ItemTapped(object sender, ItemTappedEventArgs a)
        {
            bool ret;
            Feedback feedback = new Feedback();

            // Check whether user select Sound item
            if (string.Compare(a.Item.ToString(), FeedbackType.Sound.ToString()) == 0)
            {
                try
                {
                    // Check user selected pattern is supported for sound type
                    ret = feedback.IsSupportedPattern(FeedbackType.Sound, pattern);
                    // If pattern is supported, play feedback
                    if (ret)
                        feedback.Play(FeedbackType.Sound, pattern);

                    // Create ResultPage with pattern and supported information
                    // If pattern is not supported, there is no sound feedback
                    await this.Navigation.PushModalAsync(new ResultPage(pattern, ret));
                }
                catch (Exception e)
                {
                    Log.Debug("FeedbackApp", e.Message);

                    // Create ResultPage with pattern
                    // When there is exception, feedback play is failed
                    await this.Navigation.PushModalAsync(new ResultPage(pattern, false));
                }
            }
            // Check whether user select Vibration item
            else if (string.Compare(a.Item.ToString(), FeedbackType.Vibration.ToString()) == 0)
            {
                try
                {
                    // Check user selected pattern is supported for vibration type
                    ret = feedback.IsSupportedPattern(FeedbackType.Vibration, pattern);
                    // If pattern is supported, play feedback
                    if (ret)
                        feedback.Play(FeedbackType.Vibration, pattern);

                    // Create ResultPage with pattern and supported information
                    // If pattern is not supported, there is no vibration feedback
                    await this.Navigation.PushModalAsync(new ResultPage(pattern, ret));
                }
                catch (Exception e)
                {
                    Log.Debug("FeedbackApp", e.Message);

                    // Create ResultPage with pattern
                    // When there is exception, feedback play is failed
                    await this.Navigation.PushModalAsync(new ResultPage(pattern, false));
                }

            }
        }

        /// <summary>
        /// The constructor of SecondPage
        /// Feedback types are listed
        ///     Sound
        ///     Vibration
        /// </summary>
        /// <param name="input">User tapped pattern</param>
        public SecondPage(string input)
        {
            this.Title = "Types";
            var TypeListView = new ListView();

            // Create a new ListView of type strings
            TypeListView.ItemsSource = new string[]
            {
                // Sound type
                "Sound",
                // Vibration type
                "Vibration",
            };
            pattern = input;

            // Add ItemTapped event
            TypeListView.ItemTapped += Type_ItemTapped;
            this.Content = TypeListView;
        }
    }
}
