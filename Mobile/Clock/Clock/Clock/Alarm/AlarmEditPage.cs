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

using Clock.Controls;
using Clock.Interfaces;
using MultilineCell = Clock.Controls.MultilineCell;
using System;
using Tizen.Xamarin.Forms.Extension;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// This class defines Alarm Edit View page contents.
    /// This page contains table view for alarm info input.
    /// Each table cell is binded with sub-pages if necessary.
    /// </summary>
    public class AlarmEditPage : ContentPage
    {
        /// <summary>
        /// Title bar with two buttons to move on two pages (back, done for adding/updating alarm info)
        /// </summary>
        private TitleBar titleBar;

        /// <summary>
        /// Alarm time edit cell
        /// </summary>
        AlarmEditTimePickerCell editTimePickerCell;

        /// <summary>
        /// Alarm name edit cell
        /// </summary>
        AlarmEditNameCell nameCell;

        /// <summary>
        /// Initial AlarmRecord
        /// It is used to compare to the modified AlarmRecord when an App user presses Backbutton in AlarmEditPage
        /// </summary>
        private AlarmRecord originalRecord;

        /// <summary>
        /// Dialog to give an app user an opportunity to discard changes when Backbutton is pressed
        /// </summary>
        private Dialog dialog;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmEditPage"/> class.
        /// </summary>
        /// <param name="alarmRecordData">AlarmRecord</param>
        public AlarmEditPage(AlarmRecord alarmRecordData)
        {
            // Keep the initial Alarm Record
            originalRecord = alarmRecordData;
            BindingContext = AlarmModel.BindableAlarmRecord;
            Draw();
        }

        /// <summary>
        /// This method is called when this page is showing up.
        /// </summary>
        /// <param name="alarmRecordData">The alarm record to show on this page. If a new one, default alarm record is passed.</param>
        internal void Update(AlarmRecord alarmRecordData)
        {
            originalRecord = alarmRecordData;
            BindingContext = AlarmModel.BindableAlarmRecord;

            // Update the Text of TitleLabel
            titleBar.TitleLabel.Text = AlarmModel.BindableAlarmRecord.IsSerialized ? "Edit" : "Create";
            // Update the Time of TimePicker
            editTimePickerCell.Time = AlarmModel.BindableAlarmRecord.ScheduledDateTime.TimeOfDay;
            // Update the Text of Entry for alarm name
            ((AlarmEditName)(nameCell.View)).mainEntry.Text = AlarmModel.BindableAlarmRecord.AlarmName;
        }

        /// <summary>
        /// Create AlarmEditPage's UI
        /// </summary>
        private void Draw()
        {
            // Hide navigation bar (title and buttons)
            NavigationPage.SetHasNavigationBar(this, false);

            // Create TitleBar
            titleBar = new TitleBar();
            titleBar.LeftButton.Text = "CANCEL";
            titleBar.RightButton.Text = "DONE";
            titleBar.TitleLabel.Text = "Create";
            titleBar.LeftButton.Clicked += CANCEL_Clicked;
            titleBar.RightButton.Clicked += DONE_Clicked;

            // Create time edit cell
            editTimePickerCell = new AlarmEditTimePickerCell(AlarmModel.BindableAlarmRecord);
            editTimePickerCell.SetBinding(AlarmEditTimePickerCell.TimeProperty, new Binding("ScheduledDateTime", BindingMode.Default, source: AlarmModel.BindableAlarmRecord));

            // Create repeat edit cell
            TextCell repeatTextCell = new TextCell
            {
                Text = "Repeat weekly",
                Detail = "Never",
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(AlarmPageController.GetInstance(AlarmPages.RepeatPage));
                })
            };
            repeatTextCell.SetBinding(TextCell.DetailProperty, new Binding("WeekFlag", mode: BindingMode.Default, converter: new AlarmValueConverter(), source: AlarmModel.BindableAlarmRecord));

            // Create alarm type cell
            TextCell typeTextCell = new TextCell
            {
                Text = "Alarm type",
                Detail = "Sound",
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(AlarmPageController.GetInstance(AlarmPages.TypePage));
                })
            };
            typeTextCell.SetBinding(TextCell.DetailProperty, new Binding("AlarmType", mode: BindingMode.Default, converter: new AlarmValueConverter(), source: AlarmModel.BindableAlarmRecord));

            // Create sound type cell
            AlarmEditSliderCell soundVolumeCell = new AlarmEditSliderCell(AlarmModel.BindableAlarmRecord);

            // Create tone selection cell
            TextCell toneTextCell = new TextCell
            {
                Text = "Alarm tone",
                Detail = "Default",
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(AlarmPageController.GetInstance(AlarmPages.TonePage));
                })
            };
            toneTextCell.SetBinding(TextCell.DetailProperty, new Binding("AlarmToneType", mode: BindingMode.Default, converter: new AlarmValueConverter(), source: AlarmModel.BindableAlarmRecord));

            // Create snooze setting cell
            MultilineCell snoozeMultilineCell = new MultilineCell
            {
                Text = "Snooze",
                Multiline = "Sound the alarm 3 times, at 5-minute intervals.",
                IsCheckVisible = true,
                IsChecked = true,
            };
            snoozeMultilineCell.SetBinding(MultilineCell.IsCheckedProperty, new Binding("Snooze", BindingMode.TwoWay, source: AlarmModel.BindableAlarmRecord));

            // Create name setting cell
            nameCell = new AlarmEditNameCell(AlarmModel.BindableAlarmRecord);

            // TableRoot
            TableRoot root = new TableRoot()
            {
                new TableSection()
                {
                    editTimePickerCell,
                    repeatTextCell,
                    typeTextCell,
                    soundVolumeCell,
                    toneTextCell,
                    snoozeMultilineCell,
                    nameCell,
                }
            };

            TableView alarmEditTable = new TableView
            {
                Root = root,
                HasUnevenRows = true,
            };

            // Main layout for AlarmEditPage
            StackLayout mainLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BindingContext = AlarmModel.BindableAlarmRecord,
                Children =
                {
                    titleBar,
                    alarmEditTable
                }
            };

            Content = mainLayout;
        }

        /// <summary>
        /// Invoked when "CANCEL" button is clicked.
        /// </summary>
        /// <param name="sender">Titlebar's left button(CANCEL button)</param>
        /// <param name="e">EventArgs</param>
        private void CANCEL_Clicked(object sender, EventArgs e)
        {
            // To avoid FloatingButton's flickering issue
            ((App)Application.Current).floatingButton.Show();
            // Go back to the previous page
            Navigation.PopAsync();
        }

        // When button clicked, need to do followings:
        // 1. check time set by users
        // 2. convert TimeSpan to DateTime (since epoc time)
        // 3. get alarm name
        // 4. get alarm key (creation date which identify alarm uniquely)
        // 5. save a new record or update existing alarm.
        /// <summary>
        /// Invoked when "DONE" button is clicked
        /// </summary>
        /// <param name="sender">Titlebar's right button(DONE button)</param>
        /// <param name="e">EventArgs</param>
        private void DONE_Clicked(object sender, EventArgs e)
        {
            // Get time from TimePicker
            TimeSpan ts = editTimePickerCell.Time;
            DateTime now = System.DateTime.Now;
            AlarmModel.BindableAlarmRecord.ScheduledDateTime = new System.DateTime(now.Year, now.Month, now.Day, ts.Hours, ts.Minutes, 0);
            // Get name from Entry
            AlarmModel.BindableAlarmRecord.AlarmName = ((AlarmEditName)nameCell.View).mainEntry.Text;
            AlarmModel.BindableAlarmRecord.WeekdayRepeatText = AlarmModel.BindableAlarmRecord.GetFormatted(AlarmModel.BindableAlarmRecord.WeekFlag, AlarmModel.BindableAlarmRecord.AlarmState < AlarmStates.Inactive ? true : false);

            DependencyService.Get<ILog>().Debug("*** [START] [Clicked_SaveAlarm] BindableAlarmRecord : " + AlarmModel.BindableAlarmRecord);
            AlarmModel.BindableAlarmRecord.PrintProperty();

            AlarmRecord duplicate = new AlarmRecord();
            bool existSameAlarm = CheckAlarmExist(ref duplicate);
            if (existSameAlarm)
            {
                DependencyService.Get<ILog>().Debug("[Clicked_SaveAlarm] SAME ALARM EXISTS!!!!  duplicate : " + duplicate);
                duplicate.PrintProperty();

                // Need to show information
                Toast.DisplayText("Alarm already set for " + AlarmModel.BindableAlarmRecord.ScheduledDateTime + " " + AlarmModel.BindableAlarmRecord.AlarmName + ".\r\n Existing alarm updated.");

                // Use alarm created date for unique identifier for an alarm record
                string alarmUID = AlarmModel.BindableAlarmRecord.GetUniqueIdentifier();

                if (!AlarmModel.BindableAlarmRecord.IsSerialized)
                {
                    // in case that AlarmEditPage is shown by clicking a FloatingButton
                    // when trying to create a system alarm and save it, find the same alarm
                    // expected behavior : update the previous one and do not create a new alarm
                    DependencyService.Get<ILog>().Debug("  case 1:   A new alarm will be not created. The existing alarm(duplicate) will be updated with new info.");
                    AlarmModel.BindableAlarmRecord.IsSerialized = true;
                    // Copy modified data to the existing alarm record except alarm UID & native UID
                    duplicate.DeepCopy(AlarmModel.BindableAlarmRecord, false);
                    // Update the existing alarm(duplicate)
                    AlarmModel.UpdateAlarm(duplicate);
                }
                else if (alarmUID.Equals(duplicate.GetUniqueIdentifier()))
                {
                    // in case that AlarmEditPage is shown by selecting an item of ListView in AlarmListPage
                    // At saving time, just update itself. (It doesn't affect other alarms)
                    DependencyService.Get<ILog>().Debug("  case 2:   duplicate == AlarmModel.BindableAlarmRecord. So, just update BindableAlarmRecord.");
                    AlarmModel.UpdateAlarm(AlarmModel.BindableAlarmRecord);
                }
                else
                {
                    // in case that AlarmEditPage is shown by selecting an item of ListView in AlarmListPage
                    // At saving time, the same alarm is found.
                    // In case that this alarm is not new, the existing alarm(duplicate) will be deleted and it will be updated.
                    DependencyService.Get<ILog>().Debug("  case 3:   delete duplicate and then update BindableAlarmRecord.");
                    // 1. delete duplicate alarm
                    AlarmModel.DeleteAlarm(duplicate);
                    // 2. update bindableAlarmRecord
                    AlarmModel.UpdateAlarm(AlarmModel.BindableAlarmRecord);
                }
            }
            else
            {
                DependencyService.Get<ILog>().Debug("NO SAME ALARM EXISTS!!!!");
                if (!AlarmModel.BindableAlarmRecord.IsSerialized)
                {
                    // In case that AlarmEditPage is shown by clicking FloatingButton
                    // There's no same alarm. So, just create a new alarm and add to list and dictionary.
                    DependencyService.Get<ILog>().Debug("  case 4:   just create an alarm");
                    AlarmModel.BindableAlarmRecord.IsSerialized = true;
                    AlarmModel.CreateAlarmAndSave();
                }
                else
                {
                    // in case that AlarmEditPage is shown by selecting an item of ListView in AlarmListPage
                    // There's no same alarm. So, just update itself.
                    DependencyService.Get<ILog>().Debug("  case 5:   just update itself");
                    AlarmModel.UpdateAlarm(AlarmModel.BindableAlarmRecord);
                }
            }

            AlarmModel.PrintAll("Move from AlarmEditPage to AlarmListPage");
            ((App)Application.Current).floatingButton.Show();
            Navigation.PopAsync();
        }

        /// <summary>
        /// Determines whether ObservableAlarmList contains an alarm which is scheduled at the same time and has the same alarm name.
        /// </summary>
        /// <param name="duplicate">AlarmRecord</param>
        /// <returns> true if a same alarm is found in the list; otherwise, false.</returns>
        private bool CheckAlarmExist(ref AlarmRecord duplicate)
        {
            foreach (AlarmRecord item in AlarmModel.ObservableAlarmList)
            {
                // Check scheduled time and alarm name
                if (item.ScheduledDateTime.Equals(AlarmModel.BindableAlarmRecord.ScheduledDateTime)
                    && item.AlarmName.Equals(AlarmModel.BindableAlarmRecord.AlarmName))
                {
                    // a same alarm is found.
                    duplicate.DeepCopy(item);
                    return true;
                }
            }
            // there's no same alarm.
            duplicate = null;
            return false;
        }

        /// <summary>
        /// Invoked when backbutton is pressed in AlarmEditPage
        /// If there's no changes,
        ///   just go back to AlarmListPage
        /// If there's changes,
        ///   a dialog will be shown and an app user gets an opportunity to save or discard them.
        /// </summary>
        /// <returns>bool</returns>
        protected override bool OnBackButtonPressed()
        {
            // Compare : initial AlarmRecord vs. current AlarmRecord
            bool same = AlarmModel.Compare(originalRecord, AlarmModel.BindableAlarmRecord);
            if (same)
            {
                ((App)Application.Current).floatingButton.Show();
                // Just go back to the previous screen
                return base.OnBackButtonPressed();
            }
            else
            {
                if (dialog == null)
                {
                    // Cancel button
                    Button cancelButton = new Button
                    {
                        Text = "Cancel",
                    };
                    cancelButton.Clicked += CancelButton_Clicked;

                    // Discard button
                    Button discardButton = new Button
                    {
                        Text = "Discard"
                    };
                    discardButton.Clicked += DiscardButton_Clicked;

                    Label label = new Label
                    {
                        Text = "All changes will be discarded.",
                        FontSize = 26,
                    };
                    FontFormat.SetFontWeight(label, FontWeight.Light);

                    StackLayout content = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(20, 20, 20, 20),
                        Children =
                        {
                            label
                        },
                        WidthRequest = 720,
                    };

                    // Dialog
                    dialog = new Dialog
                    {
                        HorizontalOption = LayoutOptions.Fill,
                        Title = "Discard change",
                        Positive = cancelButton,
                        Neutral = discardButton,
                        Content = content
                    };
                }

                dialog.Show();
                return true;
            }
        }

        /// <summary>
        /// Invoked when 'Discard' button is clicked in a dialog
        /// Goes back from AlarmEditPage to AlarmListPage.
        /// Changes in AlarmEditPage will be discarded.
        /// </summary>
        /// <param name="sender">Discard button</param>
        /// <param name="e">EventArgs</param>
        private void DiscardButton_Clicked(object sender, EventArgs e)
        {
            dialog.Hide();
            ((App)Application.Current).floatingButton.Show();
            //((App)Application.Current).ShowFloatingButton("Alarm");
            Navigation.PopAsync();
        }

        /// <summary>
        /// Invoked when 'Cancel' button is clicked in a dialog
        /// Hide a dialog
        /// </summary>
        /// <param name="sender">Cancel button</param>
        /// <param name="e">EventArgs</param>
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            dialog.Hide();
        }
    }
}
