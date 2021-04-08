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

using SpeechToText.ViewModels;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace SpeechToText.Views
{
    /// <summary>
    /// The class of the page which allows user to change sounds settings of the STT client.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoundsSettingsPage : CirclePage
    {
        #region methods

        /// <summary>
        /// The page constructor.
        /// </summary>
        public SoundsSettingsPage()
        {
            InitializeComponent();
            DisableSelection();
        }

        /// <summary>
        /// Disables selection on the list.
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

        protected override bool OnBackButtonPressed()
        {
            var viewModel = this.BindingContext as MainViewModel;
            viewModel.WizardSaveSoundSettingsCommand.Execute(null);
            return base.OnBackButtonPressed();
        }
    }
}