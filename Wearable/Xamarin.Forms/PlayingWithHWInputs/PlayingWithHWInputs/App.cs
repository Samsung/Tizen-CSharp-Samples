using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using SkiaSharp.Views.Forms;
using SkiaSharp;

namespace PlayingWithHWInputs
{
    /// <summary>
    /// PlayingWithHWInputs application
    /// </summary>
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new RotaryEventPage();
        }

        /// <summary>
        /// Called when the application starts.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when the application enters the sleeping state.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Called when the application resumes from a sleeping state.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
