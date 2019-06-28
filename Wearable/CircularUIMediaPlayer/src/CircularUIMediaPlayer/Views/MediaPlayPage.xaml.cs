/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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

using CircularUIMediaPlayer.ViewModels;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Xamarin.Forms.Xaml;

namespace CircularUIMediaPlayer.Views
{
    using Tizen = Xamarin.Forms.PlatformConfiguration.Tizen;

    /// <summary>
    /// MainPlayPage class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediaPlayPage : CirclePage
    {
        public MediaPlayPage(MainPageModel model)
        {
            BindingContext = model;
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            // to set Tizen circle button style
            // API Spec: https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.platformconfiguration.tizenspecific.buttonstyle?view=xamarin-forms
            backBtn.On<Tizen>().SetStyle(ButtonStyle.Circle);
            playBtn.On<Tizen>().SetStyle(ButtonStyle.Circle);
            forwardBtn.On<Tizen>().SetStyle(ButtonStyle.Circle);
            // set CircularUI MediaPlayer
            model.SetPlayer(mediaPlayer);
        }
    }
}