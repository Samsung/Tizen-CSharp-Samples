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

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Stopwatch.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stopwatch.Views
{
    /// <summary>
    /// Main page class.
    /// Handles whole user interface of the application.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        #region methods

        /// <summary>
        /// Main page class constructor.
        /// Initializes page components and registers listener
        /// which scrolls the list of laps.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            ((StopwatchViewModel)BindingContext).Laps.CollectionChanged +=
                LapsOnCollectionChanged;
        }

        /// <summary>
        /// Handles lap collection change event.
        /// Scrolls the list of laps to the beginning (new laps are added
        /// to the top of the list).
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="args">Event arguments.</param>
        private void LapsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            var laps = (ObservableCollection<LapViewModel>)sender;

            if (laps.Count > 0)
            {
                LapsListView.ScrollTo(
                    laps.First(),
                    ScrollToPosition.Start,
                    animated: false);
            }
        }

        #endregion
    }
}