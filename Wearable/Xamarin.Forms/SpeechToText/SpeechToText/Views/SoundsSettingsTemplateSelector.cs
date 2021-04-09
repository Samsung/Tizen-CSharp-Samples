//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Xamarin.Forms;

namespace SpeechToText.Views
{
    /// <summary>
    /// Data template selector class for sounds settings list.
    /// </summary>
    class SoundsSettingsTemplateSelector : DataTemplateSelector
    {
        #region properties

        /// <summary>
        /// Data template for sound on/off switch list item.
        /// </summary>
        public DataTemplate SoundSwitchTemplate { get; set; }

        /// <summary>
        /// Data template for start sound list item.
        /// </summary>
        public DataTemplate StartSoundTemplate { get; set; }

        /// <summary>
        /// Data template for end sound list item.
        /// </summary>
        public DataTemplate EndSoundTemplate { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Returns proper data template for specified list item (view model).
        /// </summary>
        /// <param name="item">List item's view model.</param>
        /// <param name="container">An optional container object.</param>
        /// <returns>Proper data template.</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch ((string)item)
            {
                case "switch":
                    return SoundSwitchTemplate;
                case "start_sound":
                    return StartSoundTemplate;
                case "end_sound":
                    return EndSoundTemplate;
                default:
                    return null;
            }
        }

        #endregion
    }
}
