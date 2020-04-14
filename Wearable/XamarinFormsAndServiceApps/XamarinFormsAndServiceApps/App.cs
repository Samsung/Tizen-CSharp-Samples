using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Applications;

namespace XamarinFormsAndServiceApps
{
    public class App : Xamarin.Forms.Application
    {
        Label label;
        Button button;
        public App()
        {
            label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Text = "Welcome to Xamarin Forms!"
            };

            button = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Start service",
            };
            button.Clicked += OnButtonClicked;

            // The root page of your application
            MainPage = new CirclePage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        label,
                        button
                    }
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).Text = "Started";
            LaunchServiceApp();
        }

        void LaunchServiceApp()
        {
            AppControl appcontrol = new AppControl
            {
                ApplicationId = "org.tizen.example.ServiceApp",
                Operation = AppControlOperations.Default,
            };

            AppControl.SendLaunchRequest(appcontrol, (launchRequest, replyRequest, result) =>
            {
                switch (result)
                {
                    case AppControlReplyResult.Succeeded:
                        label.Text = "Service application is successfully launched.";
                        Console.WriteLine("Service application is successfully launched.");
                        break;
                    case AppControlReplyResult.Failed:
                        label.Text = "Service application is not launched.";
                        Console.WriteLine("Service application is not launched.");
                        break;
                    case AppControlReplyResult.AppStarted:
                        label.Text = "Service application is just started.";
                        Console.WriteLine("Service application is just started.");
                        break;
                    case AppControlReplyResult.Canceled:
                        label.Text = "Service application is canceled.";
                        Console.WriteLine("Service application is canceled.");
                        break;
                }
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
