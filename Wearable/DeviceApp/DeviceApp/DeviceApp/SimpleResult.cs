using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace DeviceApp
{
    /// <summary>
    /// Result page of Device application
    /// </summary>
    class SimpleResult : CirclePage
    {
        /// <summary>
        /// The constructor of SimpleResult class
        /// Show the simple text on the screen
        /// </summary>
        /// <param name="text">Result text to show</param>
        public SimpleResult(string text)
        {
            this.Content = new StackLayout()
            {
                Padding = 15,
                Children =
                {
                    new Label()
                    {
                        Text = text,
                        HorizontalTextAlignment = TextAlignment.Center,
                        LineBreakMode = LineBreakMode.WordWrap,
                    },
                },
            };
        }
    }
}
