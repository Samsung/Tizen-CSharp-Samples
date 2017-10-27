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

using Clock.Controls;
using Clock.Styles;
using Clock.Timer;
using System;
using System.Collections.Generic;
using Tizen.Xamarin.Forms.Extension;
using Xamarin.Forms;

namespace Clock.Stopwatch
{
    /// <summary>
    /// The stopwatch page, the class is defined in 2 files
    /// One is for UI part, one is for logical process,
    /// This one is for UI part.
    /// </summary>
    public partial class StopwatchPage : ContentPage
    {
        StackLayout mainView;
        RelativeLayout rLayout;
        ListView listView;
        BoxView pdBox;
        Button startButton, stopButton, lapButton;
        CounterView counterview;
        internal static int index = 1;
        LapListObservableCollection listItems;

        internal static TimeSpan lastLap;
        internal static bool expanded = false;
        internal static bool changed = false;
        public const int MAX_LAP_RECORDS = 1000;

        /// <summary>
        /// Update the panel text info.
        /// </summary>
        /// <param name="showTS">The <see cref="TimeSpan"/> object to be shown as the panel text.</param>
        private void UpdatePanelText(TimeSpan showTS)
        {
            string hmsTime;
            string msTime;

            if (showTS.Hours == 0)
            {
                hmsTime = String.Format("{0:00}:{1:00}", showTS.Minutes, showTS.Seconds);
                msTime = String.Format(".{0:00}", showTS.Milliseconds / 10);
                if (expanded)
                {
                    expanded = false;
                    changed = true;
                }
                else
                {
                    changed = false;
                }
            }
            else
            {
                hmsTime = String.Format("{0:00}:{1:00}:{2:00}", showTS.Hours, showTS.Minutes, showTS.Seconds);
                msTime = String.Format(".{0:00}", showTS.Milliseconds / 10);
                if (!expanded)
                {
                    expanded = true;
                    changed = true;
                }
                else
                {
                    changed = false;
                }
            }

            if (index == 1)
            {
                counterview.SetTime(hmsTime, msTime, expanded, changed);
            }
            else
            {
                counterview.SetTime(hmsTime, msTime, expanded, changed, true);
            }
        }

        /// <summary>
        /// Create the stopwatch main page.
        /// </summary>
        /// <returns> The StackLayout. </returns>
        private StackLayout CreateStopWatchPage()
        {
            if (mainView == null)
            {
                counterview = new CounterView(CounterType.COUNTER_TYPE_STOPWATCH);
                rLayout = counterview.GetCounterLayout();

                listItems = new LapListObservableCollection();

                // Create the start button
                startButton = new Button
                {
                    Style = CommonStyle.oneButtonStyle,
                    Text = "Start",
                };
                VisualAttributes.SetThemeStyle(startButton, "bottom");
                startButton.Clicked += StartButton_Clicked;

                // Create the stop button
                stopButton = new Button
                {
                    Style = CommonStyle.twoButtonStyle,
                    Text = "Stop",
                    IsVisible = false,
                };
                VisualAttributes.SetThemeStyle(stopButton, "bottom");
                stopButton.Clicked += StopButton_Clicked;

                // Create the lap button
                lapButton = new Button
                {
                    Style = CommonStyle.twoButtonStyle,
                    Text = "Lap",
                    IsVisible = false,
                };
                VisualAttributes.SetThemeStyle(lapButton, "bottom");
                lapButton.Clicked += LapButton_Clicked;

                // Create the layout to place start button
                StackLayout bsLayout = new StackLayout
                {
                    HeightRequest = 150,
                    Spacing = 0,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.End,
                    BackgroundColor = Color.White,
                    Children =
                    {
                        startButton,
                        stopButton,
                        lapButton
                    }
                };

                // Create the main view
                mainView = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 0,
                    //BackgroundColor = Color.Red,
                    Children =
                    {
                        rLayout,
                        bsLayout
                    }
                };
            }

            return mainView;
        }

        /// <summary>
        /// Called when Start button is clicked
        /// </summary>
        /// <param name="sender">StartButton object</param>
        /// <param name="e">EventArgs</param>
        private void StartButton_Clicked(object sender, EventArgs e)
        {
            startButton.IsVisible = false;
            stopButton.IsVisible = true;
            lapButton.IsVisible = true;
            CreatePanelTimer();
        }

        /// <summary>
        /// Called when Stop button is clicked
        /// </summary>
        /// <param name="sender">stopbutton object</param>
        /// <param name="e">EventArgs</param>
        private void StopButton_Clicked(object sender, EventArgs e)
        {
            if (tmState == SWTimerState.started)
            {
                stopButton.Text = "Resume";
                lapButton.Text = "Reset";

                StopPanelTimer();
            }
            else if (tmState == SWTimerState.stopped)
            {
                stopButton.Text = "Stop";
                lapButton.Text = "Lap";

                CreatePanelTimer();
            }

        }

        /// <summary>
        /// Called when Lap button is clicked
        /// </summary>
        /// <param name="sender">stopbutton object</param>
        /// <param name="e">EventArgs</param>
        private void LapButton_Clicked(object sender, EventArgs e)
        {
            if (tmState == SWTimerState.started)
            {
                if (index == MAX_LAP_RECORDS)
                {
                    Toast.DisplayText("Maximum number of stopwatch records " + MAX_LAP_RECORDS + " reached.");
                    return;
                }

                rLayout.HeightRequest = 532;

                if (listView == null)
                {
                    // Create list view
                    listView = new ListView
                    {
                        Margin = new Thickness(0, 0, 0, 0),
                        HeightRequest = 360, // 360 +
                        RowHeight = 120,
                        ItemTemplate = new WatchDataTemplateSelector(),
                        ItemsSource = listItems,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center
                    };

                    pdBox = new BoxView
                    {
                        Margin = new Thickness(0, 0, 0, 0),
                        HeightRequest = 40,
                        Color = Color.FromRgba(0.9412, 0.9412, 0.9412, 1),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center
                    };
                    mainView.Children.Insert(1, listView);
                    mainView.Children.Insert(2, pdBox);
                }
                else
                {
                    listView.IsVisible = true;
                    pdBox.IsVisible = true;
                }
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime;
                if (ts.Hours == 0)
                {
                    elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    counterview.SetMargin(false, true);
                }
                else
                {
                    elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    counterview.SetMargin(true, true);
                }

                string intervalTime;

                if (index == 1)
                {
                    intervalTime = elapsedTime;
                }
                else
                {
                    TimeSpan iv = ts.Subtract(lastLap);
                    if (iv.Hours == 0)
                    {
                        intervalTime = String.Format("{0:00}:{1:00}:{2:00}", iv.Minutes, iv.Seconds, iv.Milliseconds / 10);
                    }
                    else
                    {
                        intervalTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", iv.Hours, iv.Minutes, iv.Seconds, iv.Milliseconds / 10);
                    }
                }

                // Add the new info at the top of the list
                var item = new List<WatchListItem>();
                item.Add(new WatchListItem()
                {
                    No = String.Format("{0:00}", (index++)),
                    SplitTime = elapsedTime,
                    LapTime = intervalTime,
                });
                listItems.InsertRange(0, item);

                lastLap = ts;

            }
            else if (tmState == SWTimerState.stopped)
            {
                //If curState is stopped, then reset timer and view.
                ResetPanelTimer();

                listItems.Clear();

                counterview.SetMargin(false, false);
                //labelTime1.Margin = hmsLabelMargin;
                //labelTime2.Margin = msLabelMargin;
                rLayout.HeightRequest = 932;

                if (listView != null)
                {
                    listView.IsVisible = false;
                    pdBox.IsVisible = false;
                }

                stopButton.IsVisible = false;
                lapButton.IsVisible = false;
                startButton.IsVisible = true;

                index = 1;

                stopButton.Text = "Stop";
                lapButton.Text = "Lap";
                //counterview.DisplayTime(0, 0, 0, 0);
                counterview.SetTime("00:00", ".00", false, true);

                //labelTime1.Text = "00:00";
                //labelTime2.Text = ".00";
                //expanded = false;
                //mainView.Children.Add(rLayout);
                //mainView.Children.Add(bsLayout);
            }
        }

        /// <summary>
        /// The customized WatchDataTemplateSelector derived from DataTemplateSelector.
        /// </summary>
        class WatchDataTemplateSelector : DataTemplateSelector
        {
            private readonly DataTemplate _viewListTemplate;

            /// <summary>
            /// Create the list template.
            /// </summary>
            public WatchDataTemplateSelector()
            {
                _viewListTemplate = new DataTemplate(() =>
                {
                    return new ViewCell
                    {
                        View = CreateWatchList(),
                    };
                });

            }

            private StackLayout CreateWatchList()
            {
                var listLayout = new RelativeLayout
                {
                    WidthRequest = 720,
                    HeightRequest = 360
                };

                // Create label of index
                Label lapNoLabel = new Label
                {
                    Margin = new Thickness(32, 0, 0, 0),
                    WidthRequest = 70,
                    HeightRequest = 120,
                    //HorizontalOptions = LayoutOptions.Center,
                    //VerticalOptions = LayoutOptions.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    TextColor = Color.Black,
                    //BackgroundColor = Color.Pink,
                    FontSize = CommonStyle.GetDp(42),
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(lapNoLabel, FontWeight.Light);
                lapNoLabel.SetBinding(Label.TextProperty, "No");
                // Add the labelIndex to listLayout
                listLayout.Children.Add(lapNoLabel,
                    Constraint.RelativeToParent((parent) => { return 0; }),
                    Constraint.RelativeToParent((parent) => { return 0; })); /*parent.Y + (parent.Height - 120) / 2*/

                // Create label of start time
                Label labelStart = new Label
                {
                    Margin = new Thickness(90/*130*/, 0, 0, 0),
                    WidthRequest = 210/*190*/,
                    HeightRequest = 120,
                    //HorizontalOptions = LayoutOptions.Center,
                    //VerticalOptions = LayoutOptions.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    TextColor = Color.Black,
                    //BackgroundColor = Color.Lime,
                    FontSize = CommonStyle.GetDp(42),
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(labelStart, FontWeight.Light);
                labelStart.SetBinding(Label.TextProperty, "SplitTime");
                // Add the labelStart to listLayout
                listLayout.Children.Add(labelStart,
                    Constraint.RelativeToView(lapNoLabel, (parent, sibling) => { return sibling.X + sibling.Width; }),
                    Constraint.RelativeToView(lapNoLabel, (parent, sibling) => { return sibling.Y + (sibling.Height - 120) / 2; }));

                // Create label of time period
                Label labelPeriod = new Label
                {
                    Margin = new Thickness(76, 0, 0, 0),
                    WidthRequest = 210/*190*/,
                    HeightRequest = 120,
                    //HorizontalOptions = LayoutOptions.Center,
                    //VerticalOptions = LayoutOptions.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    TextColor = Color.FromHex("0000AA"),
                    //BackgroundColor = Color.Silver,
                    FontSize = CommonStyle.GetDp(42),
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(labelPeriod, FontWeight.Light);
                labelPeriod.SetBinding(Label.TextProperty, "LapTime");
                // Add the labelPeriod to listLayout
                listLayout.Children.Add(labelPeriod,
                    Constraint.RelativeToView(labelStart, (parent, sibling) => { return sibling.X + sibling.Width; }),
                    Constraint.RelativeToView(lapNoLabel, (parent, sibling) => { return sibling.Y + (sibling.Height - 120) / 2; }));

                return new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        listLayout
                    }
                };
            }

            // OnSelectTemplate is for  selecting the corresponding template, currently one _viewListTemplate.
            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
                var myItem = item as WatchListItem;
                if (myItem == null)
                {
                    return null;
                }

                return _viewListTemplate;
            }
        }
    }
}
