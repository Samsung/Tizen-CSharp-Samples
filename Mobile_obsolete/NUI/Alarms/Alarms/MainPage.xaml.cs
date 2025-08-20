/* 
  * Copyright (c) 2022 Samsung Electronics Co., Ltd 
  * 
  * Licensed under the Flora License, Version 1.1 (the "License"); 
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
using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Alarms.Services;
using System.Linq;                                        
using Tizen.Applications;
using Alarms.ViewModels;
using Tizen.NUI.Binding;

namespace Alarms
{
    /// <summary>
    /// Content page of Main Page
    /// </summary>
    public partial class MainPage : ContentPage
    {
        View selectedView;
        Alarm selectedIteam;
        List<Alarm> alarms;
        public MainPage()
        {
            InitializeComponent();
            this.alarms = AlarmListService.GetAlarmList();
            alarms.ForEach(x => AddElementToScroller(x));
            SetAlaramButton.Clicked += OnClickedSetAlarm;
            RemoveAlaramButton.Clicked += OnClickedRemoveAlarm;
        }

        /// <summary>
        /// Event when "Set Alram" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedSetAlarm(object sender, ClickedEventArgs e)
        {
            AddAlaramPage addAlaramPage = new AddAlaramPage(this);
            Navigator?.Push(addAlaramPage);
        }

        /// <summary>
        /// Event when "Remove Alram" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedRemoveAlarm(object sender, ClickedEventArgs e)
        {
            AlarmListService.RemoveAlarm(selectedIteam);
            Scroller.Remove(selectedView);
            selectedView.Dispose();
            alarms = AlarmListService.GetAlarmList();
            selectedView = null;
            selectedIteam = null;
        }

        public bool OnTouchEvent(object sender, View.TouchEventArgs args)
        {
            if (selectedView != null)
            {
                selectedView.BackgroundColor = Color.White;
            }

            selectedView = sender as View;
            //#cfd8dc
            selectedView.BackgroundColor = Color.Gray;
            selectedIteam = alarms.Find(x => x.AlarmId.ToString().Equals(selectedView.Name));
            RemoveAlaramButton.IsEnabled = true;
            return true;
        }

        public void AddElementToScroller(Alarm element)
        {
            Scroller.Add(new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Name = element.AlarmId.ToString(),
                Layout = new FlexLayout
                {
                    Direction = FlexLayout.FlexDirection.Row,
                    Justification = FlexLayout.FlexJustification.SpaceBetween,
                }
            });
            Scroller.Children.Last().Add(new TextLabel
            {
                Text = Scroller.ChildCount + ". " + element.ScheduledDate,
                TextColor = Color.Black
            });
            Scroller.Children.Last().TouchEvent += OnTouchEvent;
        }
    }
}