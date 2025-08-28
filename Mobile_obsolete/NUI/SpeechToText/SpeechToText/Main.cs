using SpeechToText.Controllers;
using SpeechToText.MainPageViews.MainPage;
using System;
using System.Collections.Generic;
using System.Linq;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.Security;

namespace SpeechToText
{
    internal class Program : NUIApplication
    {
        private Window window;
        private Navigator navigator;
        private ContentPage mainPage;
        private SttController sttController;
        private readonly Color color = new("#40ACC6");

        private void showDialog(string titleMessage, string message, Action action)
        {
            Button okButton = new() { BackgroundColor = color, Text = "Ok", SizeHeight = 100, SizeWidth = 150 };
            okButton.Clicked += (sender, e) =>
            {
                action();
            };
            DialogPage.ShowAlertDialog(titleMessage, message, okButton);
        }

        /// <summary>
        /// Handles record permission request and response.
        ///
        /// If allowed the application will run normally,
        /// if denied the application will close.
        /// </summary>
        /// 
        private async void checkPermissions()
        {
            string[] privileges = new[] {"http://tizen.org/privilege/recorder",
                                    "http://tizen.org/privilege/content.write",
                                    "http://tizen.org/privilege/mediastorage"
            };
            CheckResult[] results = PrivacyPrivilegeManager.CheckPermissions(privileges).ToArray();
            List<string> privilegesWithAskStatus = new List<string>();
            try
            {
                for (int iterator = 0; iterator < results.Length; ++iterator)
                {
                    switch (results[iterator])
                    {
                        case CheckResult.Allow:
                            // Privilege can be used
                            break;
                        case CheckResult.Deny:
                        case CheckResult.Ask:
                            // User permission request required
                            privilegesWithAskStatus.Add(privileges[iterator]);
                            break;
                    }
                }

                RequestMultipleResponseEventArgs request;
                if (privilegesWithAskStatus.Count < 1)
                {
                    sttController.CreateSttClient();
                    return;
                }
                request = await PrivacyPrivilegeManager.RequestPermissions(privilegesWithAskStatus);

                if (request.Cause == CallCause.Error)
                {
                    showDialog("Permission Not Granted", "We have to close the Application", delegate () { Current.Exit(); });
                }
                foreach (PermissionRequestResponse response in request.Responses)
                {
                    // PermissionRequestResponse contains privilege name and request result
                    switch (response.Result)
                    {
                        case RequestResult.AllowForever:
                            sttController.CreateSttClient();
                            Console.WriteLine("User allowed usage of privileges definitely");
                            break;
                        case RequestResult.DenyForever:
                        case RequestResult.DenyOnce:
                            showDialog("Permission Not Granted", "We have to close the Application", delegate () { Current.Exit(); });
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                showDialog("Permission Not Granted", "We have to close the Application", delegate () { Current.Exit(); });
            }
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
            sttController = new SttController();
            mainPage = new MainPage(window.Size.Height, window.Size.Width, sttController);
            navigator.Push(mainPage);
            checkPermissions();
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (sttController != null)
            {
                sttController.UnprepareSttClient();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (sttController != null)
            {
                sttController.PrepareSttClient();
            }
        }

        void Initialize()
        {
            window = GetDefaultWindow();
            window.Title = "Speech To Text";
            window.KeyEvent += OnKeyEvent;
            navigator = window.GetDefaultNavigator();
        }
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }

        protected override void OnTerminate()
        {
            if (sttController != null)
            {
                sttController.ReleaseResources();
            }
            base.OnTerminate();
        }
    }
}
