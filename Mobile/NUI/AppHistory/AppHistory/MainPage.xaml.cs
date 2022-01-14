/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
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
using System;
using System.Collections.Generic;
using System.Linq;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.Security;

namespace AppHistory
{
    /// <summary>
    /// Class representing Main Page
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            CheckPermission();
            foreach (View view in Scroller.Children)
            {
                view.TouchEvent += OnTouchEvent;
            }
        }

        /// <summary>
        /// Check permission about http://tizen.org/privilege/apphistory.read privilege
        /// </summary>
        private void CheckPermission()
        {
            try
            {
                CheckResult result = PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/apphistory.read");

                switch (result)
                {
                    case CheckResult.Allow:
                        break;
                    case CheckResult.Deny:
                        break;
                    case CheckResult.Ask:
                        PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/apphistory.read");
                        break;
                }
            }
            catch (Exception e)
            {
                LogImplementation.DLog(e.Message.ToString());
            }
        }

        /// <summary>
        /// Event when user touch on of the elements from scroller.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        /// <returns> event return ture if object was selected correctly</returns>
        public bool OnTouchEvent(object sender, View.TouchEventArgs args)
        {
            AppHistoryInformationPage appHistoryInformationPage;
            if (sender.Equals(RecentlyUsedApplications))
            {
                appHistoryInformationPage = new AppHistoryInformationPage(AppHistoryImplementation.QueryRecentlyUsedApplications());
            }
            else if (sender.Equals(FrequentlyUsedApplications))
            {
                appHistoryInformationPage = new AppHistoryInformationPage(AppHistoryImplementation.QueryFrequentlyUsedApplications());
            }
            else if (sender.Equals(BatteryConsumingApplications))
            {
                appHistoryInformationPage = new AppHistoryInformationPage(AppHistoryImplementation.QueryBatteryConsumingApplications());
            }
            else
            {
                appHistoryInformationPage = new AppHistoryInformationPage(new List<StatsInfoItem>());
            }

            Navigator?.Push(appHistoryInformationPage);
            return true;
        }
    }
}
