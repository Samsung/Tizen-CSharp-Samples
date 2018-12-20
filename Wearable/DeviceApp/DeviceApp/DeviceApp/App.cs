using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace DeviceApp
{
    /// <summary>
    /// Application class
    /// Perform basic device feature and show the result
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the app class
        /// </summary>
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Handle when app starts
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// Handle when app sleeps
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// Handle when app resumes
        /// </summary>
        protected override void OnResume()
        {
        }
    }
}
