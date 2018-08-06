/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Alarm.Implements;
using Alarm.Models;
using Alarm.ViewModels;
using Alarm.Views;
using System;
using Tizen.Applications;
using Xamarin.Forms;

namespace Alarm
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        public static string ResourceDir { get; private set; }

        App app;

        /// <summary>
        /// Called when this application is launched
        /// </summary>
        protected override void OnCreate()
        {
            ResourceDir = DirectoryInfo.Resource;
            base.OnCreate();
            app = new App();
            LoadApplication(app);
        }

        /// <summary>
        /// Called when this app control event receviced.
        /// </summary>
        /// <param name="e">AppControlReceivedEventArgs</param>
        async protected override void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            Console.WriteLine("OnAppControlReceived");
            base.OnAppControlReceived(e);
            AppControl appControl = e.ReceivedAppControl;
            var navi = app.MainPage as NavigationPage;
            var currentPage = navi.CurrentPage;
            try
            {
                if (appControl.ExtraData.Count() != 0)
                {
                    string AlarmCreatedDate = appControl.ExtraData.Get<string>("AlarmRecord.UniqueIdentifier");
                    Console.WriteLine($"ExtraData.Count() != 0 AlarmCreatedDate:{AlarmCreatedDate}");
                    if (AlarmModel.AlarmRecordDictionary == null)
                    {
                        // Need to retrieve at the page creation time
                        AlarmModel.AlarmRecordDictionary = AlarmPersistentHandler.DeserializeAlarmRecord();
                    }

                    AlarmRecord retrievedRecord;
                    if (AlarmModel.AlarmRecordDictionary != null)
                    {
                        if (AlarmModel.AlarmRecordDictionary.TryGetValue(AlarmCreatedDate, out retrievedRecord))
                        {
                            if (retrievedRecord == null)
                            {
                                Console.WriteLine("[OnAppControlReceived] retrievedRecord is null!!");
                            }

                            Console.WriteLine("retrievedRecord:" + retrievedRecord);
                            if (retrievedRecord != null && retrievedRecord.AlarmState < AlarmStates.Inactive)
                            {
                                await currentPage.Navigation.PushAsync(new AlarmAlertPage(new AlertPageModel(retrievedRecord)));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OnAppControlReceived] Exception - Message: " + ex.Message + ", source: " + ex.Source + ", " + ex.StackTrace);
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            // It's mandatory to initialize Circular UI for using Tizen Wearable Circular UI API
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
