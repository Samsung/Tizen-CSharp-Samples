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
using Clock.Controls;
using Clock.Interfaces;
using Clock.Styles;
using Clock.Utils;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Clock.Timer
{
    ///<summary>
    ///The TimerPage of the Clock application
    ///</summary>
    public partial class TimerPage : ContentPage
    {
        Time set_time_;
        Timer model_;
        CounterView counterview;

        StackLayout mainStackLayout;
        RelativeLayout topRelativeLayout;
        StackLayout bottomStackLayout;

        RelativeLayout timerLayout, counterLayout;

        Button startButton, pauseButton, cancelButton, resetButton, resumeButton;
        Button hourIncButton, hourDecButton, minIncButton, minDecButton, secIncButton, secDecButton;
        Label hoursLabel, minutesLabel, secondsLabel;
        ExtendedEntry hoursEntry, minutesEntry, secondsEntry;

        static Thickness counterMargin = new Thickness(0, 0, 0, 0);
        static Thickness selectorMargin = new Thickness(50, 190.5, 50, 0);
        static Thickness selectorWithKeyboardMargin = new Thickness(50, 25.5, 50, 0);
        bool isReset = false;
        private bool keyboardShown = false;

        /// <summary>
        /// Create Start/Pause/Cancel/Reset/Resume Buttons
        /// </summary>
        void CreateMenuButtons()
        {
            startButton = new Button
            {
                Style = CommonStyle.oneButtonStyle,
                Text = "Start",
            };
            VisualAttributes.SetThemeStyle(startButton, "bottom");
            SetEnabledStartButton(false);
            startButton.Clicked += OnStartButtonClicked;

            pauseButton = new Button
            {
                Style = CommonStyle.twoButtonStyle,
                Text = "Pause",
                IsVisible = false,
            };
            VisualAttributes.SetThemeStyle(pauseButton, "bottom");
            pauseButton.Clicked += OnPauseButtonClicked;

            cancelButton = new Button
            {
                Style = CommonStyle.twoButtonStyle,
                Text = "Cancel",
                IsVisible = false,
            };
            VisualAttributes.SetThemeStyle(cancelButton, "bottom");
            cancelButton.Clicked += OnCancelButtonClicked;

            resetButton = new Button
            {
                Style = CommonStyle.twoButtonStyle,
                Text = "Reset",
                IsVisible = false,
            };
            VisualAttributes.SetThemeStyle(resetButton, "bottom");
            resetButton.Clicked += OnResetButtonClicked;

            resumeButton = new Button
            {
                Style = CommonStyle.twoButtonStyle,
                Text = "Resume",
                IsVisible = false,
            };
            VisualAttributes.SetThemeStyle(resumeButton, "bottom");
            resumeButton.Clicked += OnResumeButtonClicked;
        }

        /// <summary>
        /// Called when the Start Button is clicked
        /// </summary>
        /// <param name="sender">StartButton</param>
        /// <param name="e">EventArgs</param>
        void OnStartButtonClicked(object sender, EventArgs e)
        {
            UpdateTime();
            UpdateView();

            int hour = 0, minute = 0, second = 0;
            GetTime(ref hour, ref minute, ref second);

            model_.SetTime(hour, minute, second);
            model_.Run();

            ShowRunningMenu();

            CreatePanelTimer();

            long timeLeft = model_.GetRemainingTime();
            DependencyService.Get<IAlarm>().ActivateAlarm((int)timeLeft, RingType.RING_TYPE_TIMER);
        }

        /// <summary>
        /// Called when Pause Button is clicked
        /// - Make Timer paused
        /// - Make alarm deactivated
        /// </summary>
        /// <param name="sender">PauseButton</param>
        /// <param name="e">EventArgs</param>
        void OnPauseButtonClicked(object sender, EventArgs e)
        {
            model_.Stop();
            StopPanelTimer();
            ShowPausedMenu();
            DependencyService.Get<IAlarm>().DeactivateAlarm();
        }

        /// <summary>
        /// Called when Cancel Button is clicked
        /// </summary>
        /// <param name="sender">CancelButton</param>
        /// <param name="e">EventArgs</param>
        void OnCancelButtonClicked(object sender, EventArgs e)
        {
            model_.Stop();
            model_.Reset();
            StopPanelTimer();
            ShowStartupMenu();
            DependencyService.Get<IAlarm>().DeactivateAlarm();
        }

        /// <summary>
        /// Called when Reset Button is clicked
        /// </summary>
        /// <param name="sender">ResetButton</param>
        /// <param name="e">EventArgs</param>
        void OnResetButtonClicked(object sender, EventArgs e)
        {
            topRelativeLayout.Margin = selectorMargin;

            set_time_.Hour = 0;
            set_time_.Min = 0;
            set_time_.Sec = 0;

            isReset = true;
            UpdateView();
            ResetPanelTimer();
            isReset = false;
        }

        /// <summary>
        /// Called when Resume Button is clicked
        /// </summary>
        /// <param name="sender">ResumeButton</param>
        /// <param name="e">EventArgs</param>
        void OnResumeButtonClicked(object sender, EventArgs e)
        {
            model_.Resume();
            CreatePanelTimer();
            ShowRunningMenu();

            long timeLeft = model_.GetRemainingTime();
            DependencyService.Get<IAlarm>().ActivateAlarm((int)timeLeft, RingType.RING_TYPE_TIMER);
        }

        private bool IsZeroExcept(Entry entry)
        {
            if (!entry.Equals(hoursEntry) && set_time_.Hour != 0)
            {
                return false;
            }

            if (!entry.Equals(minutesEntry) && set_time_.Min != 0)
            {
                return false;
            }

            if (!entry.Equals(secondsEntry) && set_time_.Sec != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Called when Entry's text is changed
        /// </summary>
        /// <param name="sender">Entry</param>
        /// <param name="e">TextChangedEventArgs</param>
        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (isReset)
            {
                if (resetButton.IsVisible)
                {
                    hourIncButton.Focus();
                    startButton.IsVisible = true;
                    resetButton.IsVisible = false;
                    pauseButton.IsVisible = false;
                    resumeButton.IsVisible = false;
                    cancelButton.IsVisible = false;
                }

                SetEnabledStartButton(false);
                return;
            }

            Entry entry = sender as Entry;

            try
            {
                int val = Int32.Parse(entry.Text);
                if (val == 0 && IsZeroExcept(entry))
                {
                    SetEnabledStartButton(false);
                    if (!(entry != null && entry.IsFocused))
                    {
                        ShowStartupMenu();
                    }
                }
                else
                {
                    SetEnabledStartButton(true);
                    if (!(entry != null && entry.IsFocused))
                    {
                        ShowStartupMenu(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[OnTextChanged]  Exception Message : {0}", ex.Message);
            }

            bool isFocused = entry.IsFocused;
            int len = entry.Text.Length;

            if (isFocused && (entry.Text.Length == 2))
            {
                if (entry.Equals(hoursEntry))
                {
                    minutesEntry.Focus();
                }
                else if (entry.Equals(minutesEntry))
                {
                    secondsEntry.Focus();
                }
            }
        }

        /// <summary>
        /// Called when the user finalizes the text in an entry with the return key.
        /// </summary>
        /// <param name="sender">Entry</param>
        /// <param name="e">EventArgs</param>
        void OnCompleted(object sender, EventArgs e)
        {
            UpdateTime();
            UpdateView();
            ShowStartupMenu();
        }

        /// <summary>
        /// Called when the entry receives the focus
        /// </summary>
        /// <param name="sender">Entry</param>
        /// <param name="e">EventArgs</param>
        void OnFocused(object sender, EventArgs e)
        {
            keyboardShown = true;
            topRelativeLayout.Margin = selectorWithKeyboardMargin;
        }

        /// <summary>
        /// Called when the entry loses focus
        /// </summary>
        /// <param name="sender">Entry</param>
        /// <param name="e">EventArgs</param>
        void OnUnfocused(object sender, EventArgs e)
        {
            keyboardShown = false;
            topRelativeLayout.Margin = selectorMargin;

            Entry entry = sender as Entry;
            String text = entry.Text;
            int numVal = Int32.Parse(text);
            int maxVal = (entry.Equals(hoursEntry) ? 99 : 59);
            if (numVal > maxVal)
            {
                entry.Text = maxVal.ToString();
            }
            else if (numVal < 10)
            {
                entry.Text = text;
            }
            else
            {
                entry.Text = text;
            }
        }

        void CreateCounterView()
        {
            counterview = new CounterView(CounterType.COUNTER_TYPE_TIMER);
            counterLayout = counterview.GetCounterLayout();
            counterLayout.IsVisible = false;
            topRelativeLayout.Children.Add(counterLayout,
               Constraint.RelativeToParent((parent) => { return 50; }),
               Constraint.RelativeToParent((parent) => { return 190.5 + 42 + 20 + 76 + 4; }));
        }

        void CreateSelector()
        {
            if (timerLayout != null)
            {
                timerLayout.IsVisible = true;
            }
        }

        /// <summary>
        /// Called when the 'increase/decrease' buttons are pressed for time setting
        /// </summary>
        /// <param name="sender">Entry</param>
        /// <param name="e">EventArgs</param>
        void OnButtonClicked(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            UpdateSetTime(bt);
        }

        void CreateSelectors()
        {
            hoursLabel = new Label
            {
                Style = TimerStyle.timelabelStyle,
                Text = "Hours",
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(hoursLabel, FontWeight.Normal);

            minutesLabel = new Label
            {
                Style = TimerStyle.timelabelStyle,
                Text = "Minutes",
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(minutesLabel, FontWeight.Normal);

            secondsLabel = new Label
            {
                Style = TimerStyle.timelabelStyle,
                Text = "Seconds",
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(secondsLabel, FontWeight.Normal);

            CreateEntries();

            hourIncButton = new Button
            {
                Style = TimerStyle.arrowUpButtonStyle,
            };
            hourIncButton.Clicked += OnButtonClicked;

            hourDecButton = new Button
            {
                Style = TimerStyle.arrowDownButtonStyle,
            };
            hourDecButton.Clicked += OnButtonClicked;

            minIncButton = new Button
            {
                Style = TimerStyle.arrowUpButtonStyle,
            };
            minIncButton.Clicked += OnButtonClicked;

            minDecButton = new Button
            {
                Style = TimerStyle.arrowDownButtonStyle,
            };
            minDecButton.Clicked += OnButtonClicked;

            secIncButton = new Button
            {
                Style = TimerStyle.arrowUpButtonStyle,
            };
            secIncButton.Clicked += OnButtonClicked;

            secDecButton = new Button
            {
                Style = TimerStyle.arrowDownButtonStyle,
            };
            secDecButton.Clicked += OnButtonClicked;

            // TODO CHECK : GUI GUIDE  (additionally, X position -= 26)
            topRelativeLayout.Children.Add(hoursLabel,
               Constraint.RelativeToParent((parent) => { return 20/*46*/; }),
               Constraint.RelativeToParent((parent) => { return 0; }));

            topRelativeLayout.Children.Add(minutesLabel,
               Constraint.RelativeToParent((parent) => { return 46 + 146 + 45; }),
               Constraint.RelativeToParent((parent) => { return 0; }));

            // TODO CHECK : GUI GUIDE  (additionally, X position += 26)
            topRelativeLayout.Children.Add(secondsLabel,
               Constraint.RelativeToParent((parent) => { return 26 + 46 + 146 + 45 + 146 + 45; }),
               Constraint.RelativeToParent((parent) => { return 0; }));

            // time-increasing buttons
            topRelativeLayout.Children.Add(hourIncButton,
               Constraint.RelativeToParent((parent) => { return 20; }),
               Constraint.RelativeToParent((parent) => { return 42 + 20; }));

            topRelativeLayout.Children.Add(minIncButton,
               Constraint.RelativeToParent((parent) => { return 46 + 146 + 45; }),
               Constraint.RelativeToParent((parent) => { return 42 + 20; }));

            topRelativeLayout.Children.Add(secIncButton,
               Constraint.RelativeToParent((parent) => { return 26 + 46 + 146 + 45 + 146 + 45; }),
               Constraint.RelativeToParent((parent) => { return 42 + 20; }));

            // time-decreasing buttons
            // TODO CHECK : GUI GUIDE  (additionally, X position -= 26)
            topRelativeLayout.Children.Add(hourDecButton,
               Constraint.RelativeToParent((parent) => { return 20; }),
               Constraint.RelativeToParent((parent) => { return 42 + 20 + 76 + 4 + 204 + 4; }));

            topRelativeLayout.Children.Add(minDecButton,
               Constraint.RelativeToParent((parent) => { return 46 + 146 + 45; }),
               Constraint.RelativeToParent((parent) => { return 42 + 20 + 76 + 4 + 204 + 4; }));

            // TODO CHECK : GUI GUIDE  (additionally, X position += 26)
            topRelativeLayout.Children.Add(secDecButton,
               Constraint.RelativeToParent((parent) => { return 26 + 46 + 146 + 45 + 146 + 45; }),
               Constraint.RelativeToParent((parent) => { return 42 + 20 + 76 + 4 + 204 + 4; }));
        }

        private void CreateEntries()
        {
            hoursEntry = new ExtendedEntry
            {
                Style = TimerStyle.timeSelectorEntryStyle,
                Text = "00",
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(hoursEntry, FontWeight.Thin);
            hoursEntry.TextChanged += OnTextChanged;
            hoursEntry.Completed += OnCompleted;
            hoursEntry.Focused += OnFocused;
            hoursEntry.Unfocused += OnUnfocused;

            minutesEntry = new ExtendedEntry
            {
                Style = TimerStyle.timeSelectorEntryStyle,
                Text = "00",
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(minutesEntry, FontWeight.Thin);
            minutesEntry.TextChanged += OnTextChanged;
            minutesEntry.Completed += OnCompleted;
            minutesEntry.Focused += OnFocused;
            minutesEntry.Unfocused += OnUnfocused;

            secondsEntry = new ExtendedEntry
            {
                Style = TimerStyle.timeSelectorEntryStyle,
                Text = "00",
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(secondsEntry, FontWeight.Thin);
            secondsEntry.TextChanged += OnTextChanged;
            secondsEntry.Completed += OnCompleted;
            secondsEntry.Focused += OnFocused;
            secondsEntry.Unfocused += OnUnfocused;

            Label colonLabel1 = new Label
            {
                Style = TimerStyle.timeSelectorLabelStyle,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(colonLabel1, FontWeight.Thin);

            Label colonLabel2 = new Label
            {
                Style = TimerStyle.timeSelectorLabelStyle,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(colonLabel2, FontWeight.Thin);

            timerLayout = new RelativeLayout
            {
                //Margin = new Thickness(0, /*364*//*236*/, 0, 0),
                WidthRequest = 620,
                HeightRequest = 204,
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                //BackgroundColor = Color.Silver,
            };

            timerLayout.Children.Add(hoursEntry,
                Constraint.RelativeToParent((parent) => { return 0; }),
                Constraint.RelativeToParent((parent) => { return 0; }));

            timerLayout.Children.Add(colonLabel1,
                Constraint.RelativeToParent((parent) => { return 190; }),
                Constraint.RelativeToParent((parent) => { return 0; }));

            timerLayout.Children.Add(minutesEntry,
                Constraint.RelativeToParent((parent) => { return 190 + 25; }),
                Constraint.RelativeToParent((parent) => { return 0; }));

            timerLayout.Children.Add(colonLabel2,
                Constraint.RelativeToParent((parent) => { return 190 + 25 + 190; }),
                Constraint.RelativeToParent((parent) => { return 0; }));

            timerLayout.Children.Add(secondsEntry,
                Constraint.RelativeToParent((parent) => { return 190 + 25 + 190 + 25; }),
                Constraint.RelativeToParent((parent) => { return 0; }));

            // time selection entries
            topRelativeLayout.Children.Add(timerLayout,
               Constraint.RelativeToParent((parent) => { return 0; }),
               Constraint.RelativeToParent((parent) => { return 42 + 20 + 76 + 4; }));

        }

        public StackLayout CreateTimerPage()
        {
            if (mainStackLayout == null)
            {
                CreateMenuButtons();
                bottomStackLayout = new StackLayout
                {
                    HeightRequest = 150,
                    Spacing = 0,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White,
                    Children =
                    {
                        startButton,
                        resetButton,
                        pauseButton,
                        resumeButton,
                        cancelButton,
                    }
                };

                topRelativeLayout = new RelativeLayout
                {
                    //HeightRequest = 861,
                    Margin = selectorMargin, // pd.left = 50, pd.right = 50, pd.bt.bg = 58
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                CreateCounterView();
                CreateSelectors();

                mainStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 0,
                    //BackgroundColor = Color.Red,
                    Children =
                    {
                        topRelativeLayout,
                        bottomStackLayout
                    }
                };
            }

            return mainStackLayout;
        }

        void SetEnabledStartButton(bool enable)
        {
            if (startButton != null)
            {
                bool isEnabled = startButton.IsEnabled;
                if (!isEnabled && enable)
                {
                    startButton.IsEnabled = true;
                }
                else if (isEnabled && !enable)
                {
                    // When one button is shown, the button's width should be 500.
                    startButton.IsEnabled = false;
                    startButton.WidthRequest = 500;
                }
            }
        }

        internal void MoveLabels(bool init)
        {
            if (init && hoursLabel.TranslationX != 0)
            {
                hoursLabel.TranslationX -= 50 + 20;
                hoursLabel.TranslationY -= 190.5 + 80;

                minutesLabel.TranslationX -= 50;
                minutesLabel.TranslationY -= 190.5 + 80;

                secondsLabel.TranslationX -= 40;
                secondsLabel.TranslationY -= 190.5 + 80;

            }
            else if (!init)
            {
                hoursLabel.TranslationX = 50 + 20;
                hoursLabel.TranslationY = 190.5 + 80;

                minutesLabel.TranslationX = 50;
                minutesLabel.TranslationY = 190.5 + 80;

                secondsLabel.TranslationX = 40;
                secondsLabel.TranslationY = 190.5 + 80;
            }
        }

        void ShowStartupMenu(bool edit = false)
        {
            bool editMode = false;
            if (!keyboardShown)
            {
                topRelativeLayout.Margin = selectorMargin;
            }

            if (this.set_time_.Hour != 0 || this.set_time_.Min != 0 || this.set_time_.Sec != 0)
            {
                editMode = true;
            }

            if (edit)
            {
                editMode = true;
            }

            ShowSelector();

            startButton.IsVisible = true;

            if (editMode)
            {
                startButton.WidthRequest = 300;
                resetButton.IsVisible = true;
            }
            else
            {
                resetButton.IsVisible = false;
            }

            pauseButton.IsVisible = false;
            resumeButton.IsVisible = false;
            cancelButton.IsVisible = false;

            MoveLabels(true);

            EnableArrowButton(true);
        }

        void ShowRunningMenu()
        {
            topRelativeLayout.Margin = counterMargin;
            counterview.DisplayTime(model_.GetTime());
            ShowCounter();

            MoveLabels(false);

            pauseButton.IsVisible = true;
            if (!(hoursEntry.IsFocused || secondsEntry.IsFocused || minutesEntry.IsFocused))
            {
                pauseButton.Focus();
            }

            cancelButton.IsVisible = true;
            if (!(hoursEntry.IsFocused || secondsEntry.IsFocused || minutesEntry.IsFocused))
            {
                cancelButton.Focus();
            }

            startButton.IsVisible = false;
            resetButton.IsVisible = false;
            resumeButton.IsVisible = false;

            EnableArrowButton(false);
        }

        private void EnableArrowButton(bool show)
        {
            if (show)
            {
                hourIncButton.IsVisible = hourDecButton.IsVisible = true;
                minIncButton.IsVisible = minDecButton.IsVisible = true;
                secIncButton.IsVisible = secDecButton.IsVisible = true;
            }
            else
            {
                hourIncButton.IsVisible = hourDecButton.IsVisible = false;
                minIncButton.IsVisible = minDecButton.IsVisible = false;
                secIncButton.IsVisible = secDecButton.IsVisible = false;
            }
        }

        void ShowPausedMenu()
        {
            startButton.IsVisible = false;
            resetButton.IsVisible = false;
            pauseButton.IsVisible = false;
            resumeButton.IsVisible = true;
            cancelButton.IsVisible = true;
        }

        void ShowSelector()
        {
            //topRelativeLayout.Margin = new Thickness(50, 190.5, 50, 0);
            if (counterLayout != null && counterLayout.IsVisible)
            {
                counterLayout.IsVisible = false;
            }

            if (timerLayout != null && !timerLayout.IsVisible)
            {
                timerLayout.IsVisible = true;
            }
        }

        void ShowCounter()
        {
            //topRelativeLayout.Margin = new Thickness(0, 0, 0, 0);
            if (timerLayout != null && timerLayout.IsVisible)
            {
                timerLayout.IsVisible = false;
            }

            if (counterLayout != null && !counterLayout.IsVisible)
            {
                counterLayout.IsVisible = true;
            }
        }

        void GetTime(ref int hour, ref int minute, ref int second)
        {
            hour = set_time_.Hour;
            minute = set_time_.Min;
            second = set_time_.Sec;
        }

        void UpdateTime()
        {
            if (hoursEntry != null)
            {
                if (hoursEntry.Text == null)
                {
                    set_time_.Hour = 0;
                }
                else if (hoursEntry.Text[0] == '0')
                {
                    set_time_.Hour = Int32.Parse(hoursEntry.Text[1].ToString());
                }
                else
                {
                    set_time_.Hour = Int32.Parse(hoursEntry.Text);
                }
            }
            else
            {
                return;
            }

            if (minutesEntry != null)
            {
                if (minutesEntry.Text == null)
                {
                    set_time_.Min = 0;
                }
                else if (minutesEntry.Text[0] == '0')
                {
                    set_time_.Min = Int32.Parse(minutesEntry.Text[1].ToString());
                }
                else
                {
                    set_time_.Min = Int32.Parse(minutesEntry.Text);
                }
            }
            else
            {
                return;
            }

            if (secondsEntry != null)
            {
                if (secondsEntry.Text == null)
                {
                    set_time_.Sec = 0;
                }
                else if (secondsEntry.Text.Length == 2 && secondsEntry.Text[0] == '0')
                {
                    set_time_.Sec = Int32.Parse(secondsEntry.Text[1].ToString());
                }
                else if (secondsEntry.Text.Length > 2)
                {
                    var shortedText = secondsEntry.Text.Substring(0, 2);
                    set_time_.Sec = Int32.Parse(shortedText);
                }
                else
                {
                    set_time_.Sec = Int32.Parse(secondsEntry.Text);
                }
            }
            else
            {
                return;
            }

        }

        void UpdateSetTime(Button button)
        {
            if (button.Equals(hourIncButton))
            {
                set_time_.Hour++;
                if (set_time_.Hour > 99)
                {
                    set_time_.Hour = 0;
                }

                hoursEntry.Text = String.Format("{0:00}", set_time_.Hour);
            }
            else if (button.Equals(minIncButton))
            {
                set_time_.Min++;
                if (set_time_.Min > 59)
                {
                    set_time_.Min = 0;
                }

                minutesEntry.Text = String.Format("{0:00}", set_time_.Min);
            }
            else if (button.Equals(secIncButton))
            {
                set_time_.Sec++;
                if (set_time_.Sec > 59)
                {
                    set_time_.Sec = 0;
                }

                secondsEntry.Text = String.Format("{0:00}", set_time_.Sec);
            }
            else if (button.Equals(hourDecButton))
            {
                set_time_.Hour--;
                if (set_time_.Hour < 0)
                {
                    set_time_.Hour = 99;
                }

                hoursEntry.Text = String.Format("{0:00}", set_time_.Hour);
            }
            else if (button.Equals(minDecButton))
            {
                set_time_.Min--;
                if (set_time_.Min < 0)
                {
                    set_time_.Min = 59;
                }

                minutesEntry.Text = String.Format("{0:00}", set_time_.Min);
            }
            else if (button.Equals(secDecButton))
            {
                set_time_.Sec--;
                if (set_time_.Sec < 0)
                {
                    set_time_.Sec = 59;
                }

                secondsEntry.Text = String.Format("{0:00}", set_time_.Sec);
            }

        }

        void UpdateView()
        {
            if (hoursEntry != null)
            {
                hoursEntry.Text = String.Format("{0:00}", set_time_.Hour);
            }

            if (minutesEntry != null)
            {
                minutesEntry.Text = String.Format("{0:00}", set_time_.Min);
            }

            if (secondsEntry != null)
            {
                secondsEntry.Text = String.Format("{0:00}", set_time_.Sec);
            }

        }

    }
}
