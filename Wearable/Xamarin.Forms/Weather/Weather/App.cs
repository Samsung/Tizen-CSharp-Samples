//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Weather.Config;
using Weather.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Weather
{
    /// <summary>
    /// Main App class - derives Xamarin.Forms application functionalities
    /// </summary>
    public class App : Application
    {
        #region properties

        /// <summary>
        /// Gets or sets value that indicates if weather and forecast for selected city is initialized.
        /// </summary>
        public bool IsInitialized { get; set; }

        #endregion


        #region methods

        /// <summary>
        /// App constructor. 
        /// Makes sure that API Key is defined.
        /// </summary>
        public App()
        {
            Page root;
            if (ApiConfig.IsApiKeyDefined())
            {
                root = new MainPage();
            }
            else
            {
                root = new MissingKeyErrorPage();
            }
            MainPage = new NavigationPage(root);
        }

        #endregion
    }
}