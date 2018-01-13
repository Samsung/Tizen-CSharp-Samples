using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace NoughtsAndCrosses
{
    public class App : Application
    {
        public MainLayout mainLayout;
        public App()
        {
            // The root page of your application
            mainLayout = new MainLayout { };

            MainPage = mainLayout;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            mainLayout.SetLabelHelloWorld("Noughts and crosses");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
