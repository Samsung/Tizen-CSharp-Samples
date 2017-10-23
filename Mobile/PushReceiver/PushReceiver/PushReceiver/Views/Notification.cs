using System;
using System.Collections.Generic;
using System.Text;

namespace PushReceiver.Views
{
    public class Notification
    {
        public string Text { get; set; }

        public string Detail { get; set; }

        public Notification (string text, string detail)
        {
            Text = text;
            Detail = detail;
        }
    }
}
