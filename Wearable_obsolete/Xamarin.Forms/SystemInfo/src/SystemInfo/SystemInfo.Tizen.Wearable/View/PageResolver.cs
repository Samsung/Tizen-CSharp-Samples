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
using SystemInfo.Tizen.Wearable.View;
using SystemInfo.View;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageResolver))]
namespace SystemInfo.Tizen.Wearable.View
{
    /// <summary>
    /// Class that provides application with platform depended views.
    /// </summary>
    public class PageResolver : IPageResolver
    {
        #region fields

        /// <summary>
        /// Local storage of MainPage.
        /// </summary>
        private MainPage _mainPage;

        /// <summary>
        /// Local storage of BatteryPage.
        /// </summary>
        private BatteryPage _batteryPage;

        /// <summary>
        /// Local storage of CapabilitiesPage.
        /// </summary>
        private CapabilitiesPage _capabilitiesPage;

        /// <summary>
        /// Local storage of DisplayPage.
        /// </summary>
        private DisplayPage _displayPage;

        /// <summary>
        /// Local storage of LedPage.
        /// </summary>
        private LedPage _ledPage;

        /// <summary>
        /// Local storage of SettingsPage.
        /// </summary>
        private SettingsPage _settingsPage;

        /// <summary>
        /// Local storage of UsbPage.
        /// </summary>
        private UsbPage _usbPage;

        /// <summary>
        /// Local storage of VibratorPage.
        /// </summary>
        private VibratorPage _vibratorPage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets MainPage.
        /// </summary>
        public Page MainPage => _mainPage ?? (_mainPage = new MainPage());

        /// <summary>
        /// Gets BatteryPage.
        /// </summary>
        public Page BatteryPage => _batteryPage ?? (_batteryPage = new BatteryPage());

        /// <summary>
        /// Gets CapabilitiesPage.
        /// </summary>
        public Page CapabilitiesPage => _capabilitiesPage ?? (_capabilitiesPage = new CapabilitiesPage());

        /// <summary>
        /// Gets DisplayPage.
        /// </summary>
        public Page DisplayPage => _displayPage ?? (_displayPage = new DisplayPage());

        /// <summary>
        /// Gets LedPage.
        /// </summary>
        public Page LedPage => _ledPage ?? (_ledPage = new LedPage());

        /// <summary>
        /// Gets SettingsPage.
        /// </summary>
        public Page SettingsPage => _settingsPage ?? (_settingsPage = new SettingsPage());

        /// <summary>
        /// Gets UsbPage.
        /// </summary>
        public Page UsbPage => _usbPage ?? (_usbPage = new UsbPage());

        /// <summary>
        /// Gets VibratorPage.
        /// </summary>
        public Page VibratorPage => _vibratorPage ?? (_vibratorPage = new VibratorPage());

        #endregion
    }
}
