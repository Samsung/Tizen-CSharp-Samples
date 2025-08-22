/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Linq;
using System.Threading.Tasks;
using SpeechToText.ViewModels;
using SpeechToText.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeechToText
{
    /// <summary>
    /// The application class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        #region properties

        /// <summary>
        /// Main view model of the application.
        /// </summary>
        public MainViewModel AppViewModel => (MainViewModel)BindingContext;

        #endregion

        #region methods

        /// <summary>
        /// The app constructor.
        /// </summary>
        public App()
        {
            InitializeComponent();

            if (Resources == null)
            {
                return;
            }

            Resources.Add("TextColor", Color.FromRgba(247, 247, 247, 0.8));
            Resources.Add("TipColor", Color.FromRgba(250, 250, 250, 0.8));
            Resources.Add("NavigationBarBackgroundColor", Color.FromRgb(87, 187, 201));
        }

        /// <summary>
        /// Handles the application start.
        /// Initializes the view.
        /// </summary>
        protected override async void OnStart()
        {
            base.OnStart();
            var model = new MainViewModel();

            BindingContext = model;

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (Color)Resources?["NavigationBarBackgroundColor"]
            };

            AppViewModel.Navigation = MainPage.Navigation;

            await model.Init();
        }

        /// <summary>
        /// Handles binding context change.
        /// Updates context in all found resources (bindable type).
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.Resources != null)
            {
                foreach (var resource in this.Resources.Values.OfType<BindableObject>())
                {
                    resource.BindingContext = this.BindingContext;
                }
            }
        }

        #endregion
    }
}
