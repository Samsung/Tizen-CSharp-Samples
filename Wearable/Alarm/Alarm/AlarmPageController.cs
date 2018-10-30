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

using Xamarin.Forms;
using Alarm.Models;
using Alarm.Views;
using System;
using Alarm.ViewModels;

namespace Alarm
{
    public enum AlarmPages
    {
        /// <summary>
        /// alarm main page
        /// </summary>
        MainPage,
        /// <summary>
        /// alarm edit page
        /// </summary>
        EditPage,
        /// <summary>
        ///
        /// </summary>
        SavePoupPage
    }

    public static class AlarmPageController
    {
        /// <summary>
        /// alarm list page
        /// </summary>
        static MainPage alarmMainPage;

        /// <summary>
        /// alarm edit page
        /// </summary>
        static AlarmEditPage alarmEditPage;

        /// <summary>
        /// no alarm page
        /// </summary>
        static SavePopupPage savePopupPage;

        public static Page GetInstance(AlarmPages page, Object o = null)
        {
            switch (page)
            {
                case AlarmPages.MainPage:
                    return AlarmPageController.alarmMainPage ?? (alarmMainPage = new MainPage(o as MainPageModel));
                case AlarmPages.EditPage:
                    // parameter 'o' is AlarmRecord object to show in AlarmEditPage.
                    // 1. Copy values to AlarmModel.BindableAlarmRecord
                    AlarmModel.BindableAlarmRecord.DeepCopy((AlarmRecord)o);
                    // 2. Apply values to EditPage (at first, create UI. next time, just show it)
                    alarmEditPage?.Update((AlarmRecord)o);
                    return AlarmPageController.alarmEditPage ?? (alarmEditPage = new AlarmEditPage(o as AlarmRecord));
                case AlarmPages.SavePoupPage:
                    return AlarmPageController.savePopupPage ?? (savePopupPage = new SavePopupPage(o as AlarmRecord));
                default:
                    return null;
            }
        }
    }
}