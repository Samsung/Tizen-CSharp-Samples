using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace SkiaSharp2DSample
{
    public class App : Application
    {
        public App()
        {
            MainPage = new SkiaSharp2DSample.MainPage();
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
