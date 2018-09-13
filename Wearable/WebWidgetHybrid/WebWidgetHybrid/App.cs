using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace WebWidgetHybrid
{
    /// <summary>
    /// Web widget - Tizen .NET C# app hybrid sample main class.
    /// This class creates an UI app to show a simple text.
    /// The purpose of this application is to demonstrate using Web widget with this
    /// Tizen .NET C# app. You need to use Tizen SDK CLI tool to generate a wgt package
    /// which include this Tizen .NET C# app.
    /// 
    /// Prerequisite: You need to install Tizen Studio SDK (in addition to Visual Studio Tools  for Tizen).
    /// 
    /// Usage of CLI tool for this purpose: 
    /// $[TIZEN_STUDIO_DIR]/tools/ide/bin$ ./tizen package -t wgt -s [SECURITY_PROFILE] -r [TPK_FILE_PATH] -- [WGT_FILE_PATH]
    /// 
    /// To test this sample, follow below instructions:
    /// 
    /// 1) Build this app on Visual Studio Tools for Tizen.
    /// 2) Build WorldClockWidget2 on the Tizen Studio in this sample's root directory (Tizen-CSharp-Samples\Wearable\WebWidgetHybrid).
    /// 3) Copy build result of 1) and 2) (org.tizen.example.WebWidgetHybrid-1.0.0.tpk and WorldClockWidget2.wgt) in $[TIZEN_STUDIO_DIR]/tools/ide/bin
    /// 4) Check your security profile (You can check it in the Tizen Certificate Manager).
    /// 5) Run below command. Here assume your security profile name is 'test_certificate'. 
    /// 
    /// F:\tizen\tools\ide\bin>tizen.bat package -t wgt -s test_certificate -r org.tizen.example.WebWidgetHybrid-1.0.0.tpk -- WorldClockWidget2.wgt
    /// 
    /// After this instruction, WorldClockWidget2.wgt is the web widget and Tizen .NET app combined hybrid package. 
    /// 
    /// You can install it and see both Tizen .NET App and web widget are available on your device.
    /// </summary>
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new CirclePage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Check WorldClockWidget2 web widget installed with this app"
                        }
                    }
                }
            };
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
