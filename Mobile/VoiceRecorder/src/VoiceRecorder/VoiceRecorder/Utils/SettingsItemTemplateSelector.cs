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
using Xamarin.Forms;

namespace VoiceRecorder.Utils
{
    /// <summary>
    /// SettingsItemTemplateSelector class.
    /// Selects Data Template by SettingItemType.
    /// </summary>
    public class SettingsItemTemplateSelector : DataTemplateSelector
    {
        #region properties

        /// <summary>
        /// File format setting template.
        /// </summary>
        public DataTemplate FileFormatTemplate { get; set; }

        /// <summary>
        /// Recording quality setting template.
        /// </summary>
        public DataTemplate RecordingQualityTemplate { get; set; }

        /// <summary>
        /// Stereo setting template.
        /// </summary>
        public DataTemplate StereoTemplate { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Method that allows to select DataTemplate by item type.
        /// </summary>
        /// <param name="item">The item for which to return a template.</param>
        /// <param name="container">An optional container object in which DataTemplateSelector
        /// objects could be stored.</param>
        /// <returns>Returns defined DataTemplate that is used to display item.</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.GetType() != typeof(SettingsItem))
            {
                throw new NotSupportedException("Item type not supported");
            }

            var settingsItem = (SettingsItem)item;

            switch (settingsItem.SettingItemType)
            {
                case SettingsItemType.Stereo:
                    return StereoTemplate;
                case SettingsItemType.RecordingQuality:
                    return RecordingQualityTemplate;
                case SettingsItemType.FileFormat:
                    return FileFormatTemplate;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
