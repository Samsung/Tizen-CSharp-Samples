using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FeedbackApp
{
    /// <summary>
    /// Application class
    /// Play Feedback pattern for specific type and show the result
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the app class
        /// </summary>
        public App()
        {
            this.MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Handle when your app starts
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// Handle when your app sleeps
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// Handle when your app resumes
        /// </summary>
        protected override void OnResume()
        {
        }
    }
}
