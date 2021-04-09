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
using System.Windows.Input;
using Xamarin.Forms;

namespace VoiceRecorder.Utils
{
    /// <summary>
    /// SettingsItem class.
    /// </summary>
    public class SettingsItem : BindableObject
    {
        #region properties

        /// <summary>
        /// ShowSettingPopupCommand property definition.
        /// </summary>
        public static readonly BindableProperty ShowSettingPopupCommandProperty = BindableProperty.Create(
            nameof(ShowSettingPopupCommand), typeof(ICommand), typeof(SettingsItem));

        /// <summary>
        /// SettingItemType property definition.
        /// </summary>
        public static readonly BindableProperty SettingItemTypeProperty = BindableProperty.Create(
            nameof(SettingItemType), typeof(SettingsItemType), typeof(SettingsItem), SettingsItemType.FileFormat);


        /// <summary>
        /// Command which shows pop-up with options of the setting.
        /// </summary>
        public ICommand ShowSettingPopupCommand
        {
            get => (ICommand)GetValue(ShowSettingPopupCommandProperty);
            set => SetValue(ShowSettingPopupCommandProperty, value);
        }

        /// <summary>
        /// Setting type.
        /// </summary>
        public SettingsItemType SettingItemType
        {
            get => (SettingsItemType)GetValue(SettingItemTypeProperty);
            set => SetValue(SettingItemTypeProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// SettingsItem constructor.
        /// </summary>
        /// <param name="settingItemType">Setting type.</param>
        /// <param name="showSettingPopup">Command which shows pop-up with options of the setting.</param>
        public SettingsItem(SettingsItemType settingItemType, Command showSettingPopup = null)
        {
            ShowSettingPopupCommand = showSettingPopup;
            SettingItemType = settingItemType;
        }

        #endregion
    }
}
