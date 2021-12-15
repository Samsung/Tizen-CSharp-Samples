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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.Applications;
using System.Collections.Generic;
using System.Linq;
using Badges.Models;

namespace Badges
{
    /// <summary>
    /// Content page of main application page
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// List of installed applications
        /// </summary>
        List<AppInfo> changeableApplications;
        /// <summary>
        /// List of preloaded applications
        /// </summary>
        List<AppInfo> others;
        /// <summary>
        /// View selected by user from list of changeable applications 
        /// </summary>
        View selectedView;
        /// <summary>
        /// Application Info od application selected by user from list of changeable applications
        /// </summary>
        AppInfo selectedIteam;

        /// <summary>
        /// Initializes a new instance of the <see cref="XamlPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            changeableApplications = new List<AppInfo>();
            others = new List<AppInfo>();
            FillApplicationsList();
            FillScroller();
            IncreaseButton.Clicked += OnClickedEventIncreaseButton;
            ReduceButton.Clicked += OnClickedEventReduceButton;
            ApplyButton.Clicked += OnClickedEventApplyButton;
            ResetButton.Clicked += OnClickedEventResetButton;
        }

        /// <summary>
        /// Event when user touch on of the elements from application list.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        /// <returns> event return ture if object was selected correctly</returns>
        public bool OnTouchEvent(object sender, TouchEventArgs args)
        {
            if (selectedView != null)
            {
                selectedView.BackgroundColor = Color.White;
            }

            selectedView = sender as View;
            selectedView.BackgroundColor = Color.Gray;
            selectedIteam = changeableApplications.Find(x => x.AppName.Equals(selectedView.Name));
            CounterNumberFiled.Text = selectedIteam.BadgeCounter.ToString();
            ReduceButton.IsEnabled = true;
            IncreaseButton.IsEnabled = true;
            ApplyButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
            return true;
        }

        /// <summary>
        /// Event when "Increase Button" is Clicked
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        public void OnClickedEventIncreaseButton(object sender, ClickedEventArgs args)
        {
            selectedIteam.BadgeCounter++;
            if (selectedIteam.BadgeCounter > 99)
            {
                CounterNumberFiled.Text = "99+";
            }
            else
            {
                CounterNumberFiled.Text = selectedIteam.BadgeCounter.ToString();
            }
        }

        /// <summary>
        /// Event when "Reduce Button" is Clicked
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        public void OnClickedEventReduceButton(object sender, ClickedEventArgs args)
        {
            if (selectedIteam.BadgeCounter > 0)
            {
                selectedIteam.BadgeCounter--;
                CounterNumberFiled.Text = selectedIteam.BadgeCounter.ToString();
            }
        }

        /// <summary>
        /// Event when "Apply Button" is Clicked
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        public void OnClickedEventApplyButton(object sender, ClickedEventArgs args)
        {
            if (selectedView != null)
            {
                selectedView.Remove(selectedView.Children.Last());
                selectedView.Add(new TextLabel
                {
                    Text = selectedIteam.BadgeCounter.ToString()
                });
            }
        }

        /// <summary>
        /// Event when "Reset Button" is Clicked
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        public void OnClickedEventResetButton(object sender, ClickedEventArgs args)
        {
            if (selectedView != null)
            {
                selectedIteam.BadgeCounter = 0;
                CounterNumberFiled.Text = selectedIteam.BadgeCounter.ToString();
                selectedView.Remove(selectedView.Children.Last());
                selectedView.Add(new TextLabel
                {
                    Text = "0"
                });
            }
        }

        /// <summary>
        /// Method for filling applications lists with aplications installed and preloaded on device.
        /// </summary>
        public void FillApplicationsList()
        {
            IEnumerable<Package> packageList = PackageManager.GetPackages();
            foreach (Package pkg in packageList)
            {
                var list = pkg.GetApplications();
                foreach (var app in list)
                {
                    if (!app.IsNoDisplay)
                    {
                        if (app.IsPreload)
                        {
                            others.Add(new AppInfo(app.Label, app.PackageId, false, 0));
                        }
                        else
                        {
                            changeableApplications.Add(new AppInfo(app.Label, app.PackageId, true, 0));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method for filling scroller with informations about applications on device.
        /// </summary>
        public void FillScroller()
        {
            AddListLabelToScroller("Changeable Applications");
            AddListToScroller(changeableApplications);
            AddListLabelToScroller("Others");
            AddListToScroller(others);
        }

        /// <summary>
        /// Method for filling the scroller with informatins on application from list
        /// </summary>
        /// <param name="list"> List with applications </param>
        public void AddListToScroller(List<AppInfo> list)
        {
            foreach (var app in list)
            {
                Scroller.Add(new View
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    Name = app.AppName,
                    Layout = new FlexLayout
                    {
                        Direction = FlexLayout.FlexDirection.Row,
                        Justification = FlexLayout.FlexJustification.SpaceBetween,
                    }
                });
                if (app.IsAvailable)
                {
                    Scroller.Children.Last().TouchEvent += OnTouchEvent;
                    Scroller.Children.Last().Add(new TextLabel
                    {
                        Text = app.AppName
                    });
                }
                else
                {
                    Scroller.Children.Last().Add(new TextLabel
                    {
                        Text = app.AppName,
                        TextColor = Color.Gray
                    });
                }

                Scroller.Children.Last().Add(new TextLabel
                {
                    Text = app.BadgeCounter.ToString()
                });
            }
        }

        /// <summary>
        /// Method for adding a label to Scroller
        /// </summary>
        /// <param name="label"> String that will be put on label </param>
        public void AddListLabelToScroller(string label)
        {
            Scroller.Add(new View());
            Scroller.Children.Last().Add(new TextLabel
            {
                Text = label,
                PointSize = 12,
                TextColor = Color.Cyan
            });
        }
    }
}
