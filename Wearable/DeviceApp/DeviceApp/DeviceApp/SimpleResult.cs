using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace DeviceApp
{
    class SimpleResult : CirclePage
    {
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
