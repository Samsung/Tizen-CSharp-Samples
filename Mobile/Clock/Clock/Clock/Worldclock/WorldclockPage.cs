/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Common;
using Clock.Interfaces;
using System;
using Tizen.Xamarin.Forms.Extension;
using Xamarin.Forms;

namespace Clock.Worldclock
{
    /// <summary>
    /// The world clock page, the class is defined in 2 files
    /// One is for UI part, one is for logical process,
    /// This one is for logical process
    /// </summary>
    public partial class WorldclockPage : ContentPage
    {
        public WorldclockInfo info;
        private static WorldclockPage worldclockPage;

        /// <summary>
        /// Gets WorldclockPage
        /// </summary>
        /// <returns>
        /// Return instance of WorldclockPage
        /// </returns>
        public static WorldclockPage GetInstance()
        {
            if (worldclockPage == null)
            {
                worldclockPage = new WorldclockPage();
            }

            return worldclockPage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldclockPage"/> class
        /// </summary>
        public WorldclockPage()
        {
            Title = "World clock";
            Icon = "maintabbed/clock_tabs_ic_worldclock.png";

            Content = new EmptyPage();
        }

        /// <summary>
        /// Loads WorldclockPage when worldclock tab is selected in TabbedPage
        /// </summary>
        public void ShowPage()
        {
            if (info == null)
            {
                info = new WorldclockInfo(this);
                App.ClockInfo = info;
                BindingContext = info;
                Content = CreateWorldClockPage();
            }
        }

        /// <summary>
        /// Called when the floating button has been clicked
        /// Launchs org.tizen.worldclock-efl application when a floating button is pressed
        /// Adds city
        /// </summary>
        /// <param name="sender">floating button object</param>
        /// <seealso cref="System.object">
        /// <param name="e">Event argument for event of floating button.</param>
        /// <seealso cref="System.EventArgs">
        public void OnFloatingButtonClicked(object sender, EventArgs e)
        {
            if (App.ClockInfo.CityRecordList.Count >= WorldclockCityList.MAX_ITEMS_LIMIT)
            {
                Toast.DisplayText("Maximum number of cities " + WorldclockCityList.MAX_ITEMS_LIMIT + " reached.");
                return;
            }

            DependencyService.Get<IAppControl>().ApplicationLaunchRequest("org.tizen.worldclock-efl", AppControlOperation.PICK, AppControlLaunchType.GROUP);
        }
    }
}
