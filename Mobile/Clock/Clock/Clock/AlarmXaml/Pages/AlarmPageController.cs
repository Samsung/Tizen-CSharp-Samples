/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

using Clock.ViewModels;
using System;
using Xamarin.Forms;

namespace TizenClock.Tizen.AlarmXaml.Pages
{
    /// <summary>
    /// Alarm page enum
    /// </summary>
    public enum AlarmPages
    {
        /// <summary>
        /// alarm list xaml page
        /// </summary>
        ListPageXaml,

        /// <summary>
        /// alarm list xaml page
        /// </summary>
        EditPageXaml,

        /// <summary>
        /// alarm type page
        /// </summary>
        TypePage,
        /// <summary>
        /// alarm repeat page
        /// </summary>
        RepeatPage,
        /// <summary>
        /// alarm tone page
        /// </summary>
        TonePage
    }

    /// <summary>
    /// This class defines page controller which checks availability of the page
    /// and if exists, then push the page.
    /// Otherwise, creates the requested page
    /// </summary>
    public class AlarmXamlPageController
    {
        /// <summary>
        /// alarm list page
        /// </summary>
        public static Clock.Pages.AlarmListPage alarmListPageXaml;

        /// <summary>
        /// alarm edit page
        /// </summary>
        public static Clock.Pages.AlarmEditPage alarmEditPageXaml;
 

        /// <summary>
        /// Static get instace method to get proper page
        /// </summary>
        /// <param name="page">The page to push</param>
        /// <seealso cref="AlarmPages">
        /// <param name="page">Parameter to be passed to the page. This can be null</param>
        /// <seealso cref="Object">
        /// <returns>Returns requested Page object</returns>
        public static Page GetInstance(TizenClock.Tizen.AlarmXaml.Pages.AlarmPages page, Object o = null)
        {
            switch (page)
            {

                case AlarmPages.ListPageXaml:
                    return AlarmXamlPageController.alarmListPageXaml ?? (alarmListPageXaml = new Clock.Pages.AlarmListPage());
                case AlarmPages.EditPageXaml:
                    return AlarmXamlPageController.alarmEditPageXaml ?? (alarmEditPageXaml = new Clock.Pages.AlarmEditPage((AlarmListViewModel)o));
                default:
                    return null;
            }
        }
    }
}