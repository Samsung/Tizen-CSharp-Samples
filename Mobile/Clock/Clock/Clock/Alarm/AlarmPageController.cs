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

using System;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// Alarm page enum
    /// </summary>
    public enum AlarmPages
    {
        /// <summary>
        /// alarm list page
        /// </summary>
        ListPage,
        /// <summary>
        /// alarm edit page
        /// </summary>
        EditPage,
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
        TonePage,
        /// <summary>
        /// delete page
        /// </summary>
        DeletePage,
    }

    /// <summary>
    /// This class defines page controller which checks availability of the page
    /// and if exists, then push the page.
    /// Otherwise, creates the requested page
    /// </summary>
    public class AlarmPageController
    {
        /// <summary>
        /// alarm list page
        /// </summary>
        public static AlarmListPage alarmListPage;

        /// <summary>
        /// alarm edit page
        /// </summary>
        public static AlarmEditPage alarmEditPage;

        /// <summary>
        /// alarm tyoe page
        /// </summary>
        public static AlarmTypePage alarmTypePage;

        /// <summary>
        /// alarm repeat page
        /// </summary>
        public static AlarmRepeatPage alarmRepeatPage;

        /// <summary>
        /// alarm tone page
        /// </summary>
        public static AlarmTonePage alarmTonePage;

        /// <summary>
        /// alarm delete page
        /// </summary>
        public static AlarmDeletePage alarmDeletePage;

        /// <summary>
        /// Static get instace method to get proper page
        /// </summary>
        /// <param name="page">The page to push</param>
        /// <seealso cref="AlarmPages">
        /// <param name="page">Parameter to be passed to the page. This can be null</param>
        /// <seealso cref="Object">
        /// <returns>Returns requested Page object</returns>
        public static Page GetInstance(AlarmPages page, Object o = null)
        {
            switch (page)
            {
                case AlarmPages.ListPage:
                    return AlarmPageController.alarmListPage ?? (alarmListPage = new AlarmListPage());
                case AlarmPages.EditPage:
                    // parameter 'o' is AlarmRecord object to show in AlarmEditPage.
                    // 1. Copy values to AlarmModel.BindableAlarmRecord
                    AlarmModel.BindableAlarmRecord.DeepCopy((AlarmRecord)o);
                    // 2. Apply values to EditPage (at first, create UIs. next time, just show it)
                    alarmEditPage?.Update((AlarmRecord)o);
                    return AlarmPageController.alarmEditPage ?? (alarmEditPage = new AlarmEditPage((AlarmRecord)o));
                case AlarmPages.TypePage:
                    alarmTypePage?.Update();
                    return AlarmPageController.alarmTypePage ?? (alarmTypePage = new AlarmTypePage());
                case AlarmPages.RepeatPage:
                    alarmRepeatPage?.Update();
                    return AlarmPageController.alarmRepeatPage ?? (alarmRepeatPage = new AlarmRepeatPage());
                case AlarmPages.TonePage:
                    alarmTonePage?.Update();
                    return AlarmPageController.alarmTonePage ?? (alarmTonePage = new AlarmTonePage());
                case AlarmPages.DeletePage:
                    return AlarmPageController.alarmDeletePage ?? (alarmDeletePage = new AlarmDeletePage());
                default:
                    return null;
            }
        }
    }
}
