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

using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace SpeechToText.Views
{
    /// <summary>
    /// The settings page class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : CirclePage
    {
        #region methods

        /// <summary>
        /// The page constructor.
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
            DisableSelection();
            RotaryFocusObject = SettingsList;
        }

        /// <summary>
        /// Disables selection on the settings list.
        /// </summary>
        private void DisableSelection()
        {
            SettingsList.ItemSelected += (sender, args) =>
            {
                if (SettingsList.SelectedItem == null)
                {
                    return;
                }

                SettingsList.SelectedItem = null;
            };
        }

        #endregion
    }
}