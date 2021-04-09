/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Globalization;
using System.Threading;
using VoiceMemo.Data;
using VoiceMemo.Services;
using VoiceMemo.ViewModels;
using VoiceMemo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace VoiceMemo
{
    /// <summary>
    /// VoiceMemo Application class
    /// </summary>
    public partial class App : Application
    {
        MainPage firstPage;
        public MainPageModel mainPageModel;
        public App()
        {
            // Get locale information
            UpdateLocale();
            InitializeComponent();
            firstPage = (MainPage)PageFactory.GetInstance(Pages.StandBy);
            MainPage = new NavigationPage(firstPage);
            mainPageModel = (MainPageModel)firstPage.BindingContext;
        }

        // database for voice records
        static RecordDatabase database;
        public static RecordDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RecordDatabase(DependencyService.Get<IDeviceInformation>().GetLocalDBFilePath("DotnetVoiceMemo.db3"));
                }

                return database;
            }
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            // ask an user to grant the permission for recorder and media storage.
            // Request user consent for this application to use recorder
            bool userPerm = await DependencyService.Get<IUserPermission>().GetPermission("http://tizen.org/privilege/recorder");
            // Request user consent for this application to access and use media storage
            bool userPerm2 = await DependencyService.Get<IUserPermission>().GetPermission("http://tizen.org/privilege/mediastorage");
            if (userPerm && userPerm2)
            {
                // Notify user consent is granted.
                MessagingCenter.Send<App, bool>(this, MessageKeys.UserPermission, true);
            }
            else
            {
                // Close the app if the user refuses the permissions to use recorder and device storage
                await MainPage.DisplayAlert("Notice", "Due to no user permission, this app cannot be executed.", "OK");
                // Terminate this application
                DependencyService.Get<IAppTerminator>().TerminateApp();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// Get the current locale and apply it.
        /// </summary>
        public void UpdateLocale()
        {
            // determine the correct, supported .NET culture
            var ci = DependencyService.Get<ILocaleService>().CurrentCultureInfo;
            SetCultureInfo(ci);

            // Whenever language has been changed, CurrentCulture will be updated.
            MessagingCenter.Subscribe<ILocaleService, CultureInfo>(this, MessageKeys.LanguageChanged, (obj, culture) =>
            {
                SetCultureInfo(culture);
                MessagingCenter.Send<App>(this, MessageKeys.UpdateByLanguageChange);
            });
        }

        // Set the current culture
        // It will be used by the Resource Manager
        void SetCultureInfo(CultureInfo info)
        {
            Resx.AppResources.Culture = info; // set the RESX for resource localization
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
        }

        public void Terminate()
        {
            MessagingCenter.Unsubscribe<ILocaleService, CultureInfo>(this, MessageKeys.LanguageChanged);
            //((MainPageModel)firstPage?.BindingContext).Dispose();
            PageFactory.DestoryPage();
        }
    }
}
