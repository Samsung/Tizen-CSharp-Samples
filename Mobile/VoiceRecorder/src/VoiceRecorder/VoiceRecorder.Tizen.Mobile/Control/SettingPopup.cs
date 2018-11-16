/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Windows.Input;
using VoiceRecorder.Utils;
using Xamarin.Forms;

namespace VoiceRecorder.Tizen.Mobile.Control
{
    /// <summary>
    /// Pop-up control with setting options.
    /// </summary>
    public class SettingPopup : BindableObject
    {
        #region properties

        /// <summary>
        /// Execute command property definition.
        /// </summary>
        public static readonly BindableProperty ExecuteCommandProperty = BindableProperty.Create(
            nameof(ExecuteCommand), typeof(ICommand), typeof(SettingPopup), null, BindingMode.OneWayToSource);

        /// <summary>
        /// Confirm command property definition.
        /// </summary>
        public static readonly BindableProperty SelectCommandProperty = BindableProperty.Create(
            nameof(SelectCommand), typeof(ICommand), typeof(SettingPopup));

        /// <summary>
        /// Dialog title bindable property definition.
        /// </summary>
        public static readonly BindableProperty SettingTypeProperty = BindableProperty.Create(
            nameof(SettingType), typeof(SettingsItemType), typeof(SettingPopup), SettingsItemType.FileFormat);

        /// <summary>
        /// Dialog message bindable property definition.
        /// </summary>
        public static readonly BindableProperty SettingItemsProperty = BindableProperty.Create(
            nameof(SettingItems), typeof(Array), typeof(SettingPopup));

        /// <summary>
        /// Command which shows the dialog.
        /// </summary>
        public ICommand ExecuteCommand
        {
            get => (ICommand)GetValue(ExecuteCommandProperty);
            set => SetValue(ExecuteCommandProperty, value);
        }

        /// <summary>
        /// Command which is executed when user confirms the message (taps OK button).
        /// </summary>
        public ICommand SelectCommand
        {
            get => (ICommand)GetValue(SelectCommandProperty);
            set => SetValue(SelectCommandProperty, value);
        }

        /// <summary>
        /// Setting type.
        /// </summary>
        public SettingsItemType SettingType
        {
            get => (SettingsItemType)GetValue(SettingTypeProperty);
            set => SetValue(SettingTypeProperty, value);
        }

        /// <summary>
        /// Setting options.
        /// </summary>
        public Array SettingItems
        {
            get => (Array)GetValue(SettingItemsProperty);
            set => SetValue(SettingItemsProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// The setting options popup constructor.
        /// </summary>
        public SettingPopup()
        {
            ExecuteCommand = new Command(ShowContextPopup);
        }

        /// <summary>
        /// Initializes ContextPopup class instance.
        /// Calls the method Show() on it.
        /// </summary>
        /// <param name="sender">Instance of the object which invokes event.</param>
        private void ShowContextPopup(object sender)
        {
            ContextPopup popup;

            try
            {
                popup = new ContextPopup
                {
                    DirectionPriorities = new ContextPopupDirectionPriorities(
                        ContextPopupDirection.Down,
                        ContextPopupDirection.Left,
                        ContextPopupDirection.Right,
                        ContextPopupDirection.Up)
                };
            }
            catch
            {
                return;
            }

            foreach (string item in SettingItems)
            {
                popup.Items.Add(new ContextPopupItem(item));
            }

            popup.SelectedIndexChanged += (s, e) =>
            {
                if (s is ContextPopup contextPopup)
                {
                    if (SettingType == SettingsItemType.FileFormat)
                    {
                        switch (contextPopup.SelectedIndex)
                        {
                            case 0:
                                SelectCommand?.Execute(FileFormatType.MP4);
                                break;
                            case 1:
                                SelectCommand?.Execute(FileFormatType.WAV);
                                break;
                            case 2:
                                SelectCommand?.Execute(FileFormatType.OGG);
                                break;
                            default:
                                return;
                        }
                    }
                    else if (SettingType == SettingsItemType.RecordingQuality)
                    {
                        switch (contextPopup.SelectedIndex)
                        {
                            case 0:
                                SelectCommand?.Execute(AudioBitRateType.High);
                                break;
                            case 1:
                                SelectCommand?.Execute(AudioBitRateType.Medium);
                                break;
                            case 2:
                                SelectCommand?.Execute(AudioBitRateType.Low);
                                break;
                            default:
                                return;
                        }
                    }
                }

                popup.Dismiss();
            };

            popup.Show(sender as Xamarin.Forms.View);
        }

        #endregion
    }
}