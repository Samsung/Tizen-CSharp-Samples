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

using Clock.Alarm;
using Clock.Common;
using Clock.Interfaces;
using Clock.MainTabbed;
using Native = Tizen.Applications;
using Size = ElmSharp.Size;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tizen.System;
using Tizen.Xamarin.Forms.Extension.Renderer;
using Xamarin.Forms;

namespace Clock.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        App app_;
        protected override void OnCreate()
        {
            base.OnCreate();
            MainWindow.AvailableRotations = ElmSharp.DisplayRotation.Degree_0;
            app_ = new App();
            LoadApplication(app_);
            Size screenSize = global::Xamarin.Forms.Platform.Tizen.Forms.Context.MainWindow.ScreenSize;
            app_.ScreenWidth = screenSize.Width;
            app_.ScreenHeight = screenSize.Height;
            app_.Is24hourFormat = SystemSettings.LocaleTimeFormat24HourEnabled;
            SystemSettings.LocaleTimeFormat24HourSettingChanged += SystemSettings_LocaleTimeFormat24HourSettingChanged;
        }

        /// <summary>
        /// Invoked when locale Time format has been changed
        /// It can be changed via Settings application.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">LocaleTimeFormat24HourSettingChangedEventArgs</param>
        private void SystemSettings_LocaleTimeFormat24HourSettingChanged(object sender, LocaleTimeFormat24HourSettingChangedEventArgs e)
        {
            app_.Is24hourFormat = e.Value;
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
            app_.Terminate();
        }

        async protected override void OnAppControlReceived(Native.AppControlReceivedEventArgs e)
        {
            base.OnAppControlReceived(e);
            Native.AppControl appControl = e.ReceivedAppControl;
            try
            {
                if (appControl.ExtraData.Count() != 0)
                {
                    IEnumerable<string> stack1 = appControl.ExtraData.GetKeys();
                    string type = appControl.ExtraData.Get<string>("RingType");
                    if (type == RingType.RING_TYPE_ALARM.ToString())
                    {
                        var navi = app_.MainPage as NavigationPage;
                        var currentPage = navi.CurrentPage;
                        string AlarmCreatedDate = appControl.ExtraData.Get<string>("AlarmRecord.UniqueIdentifier");
                        if (AlarmModel.AlarmRecordDictionary == null)
                        {
                            // IAlarmPersistentHandler serizliazer = DependencyService.Get<IAlarmPersistentHandler>();
                            // Need to retrieve at the page creation time
                            AlarmModel.AlarmRecordDictionary = DependencyService.Get<IAlarmPersistentHandler>().DeserializeAlarmRecord();
                        }

                        AlarmRecord retrievedRecord;
                        if (AlarmModel.AlarmRecordDictionary != null)
                        {
                            if (AlarmModel.AlarmRecordDictionary.TryGetValue(AlarmCreatedDate, out retrievedRecord))
                            {
                                if (retrievedRecord == null)
                                {
                                    DependencyService.Get<ILog>().Error("[OnAppControlReceived]", "retrievedRecord is null!!");
                                }

                                if (retrievedRecord != null && retrievedRecord.AlarmState < AlarmStates.Inactive)
                                {
                                    retrievedRecord.PrintProperty();
                                    await currentPage.Navigation.PushAsync(new RingPage(RingType.RING_TYPE_ALARM, retrievedRecord));
                                }
                            }
                        }
                    }
                    else if (type == RingType.RING_TYPE_TIMER.ToString())
                    {
                        var navi = app_.MainPage as NavigationPage;
                        var currentPage = navi.CurrentPage as MainTabbedPage;
                        await currentPage.Navigation.PushAsync(new RingPage(RingType.RING_TYPE_TIMER));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[OnAppControlReceived] Exception - Message: " + ex.Message + ", source: " + ex.Source + ", " + ex.StackTrace);
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            TizenFormsExtension.Init();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }
    }
}
