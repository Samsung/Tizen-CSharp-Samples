/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace TextReader
{
    /// <summary>
    /// Main application page class.
    /// </summary>
    public partial class MainPage : CirclePage
    {
        #region methods
        /// <summary>
        /// Main page constructor.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            MainContext.PropertyChanged += MainContext_PropertyChanged;
        }

        //workaround to update CircleToolbarItem play/pause icon and text
        private void MainContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Playing")
            {
                string icon = MainContext.Playing ? "images/pause.png" : "images/play.png";
                string text = MainContext.Playing ? "Pause" : "Play";
                var newItem = new CircleToolbarItem
                {
                    Command = (ToolbarItems[2] as CircleToolbarItem).Command,
                    Icon = icon,
                    Text = text,
                };
                var th4 = ToolbarItems[3];
                var th5 = ToolbarItems[4];
                var th6 = ToolbarItems[5];
                ToolbarItems.RemoveAt(2);
                ToolbarItems.RemoveAt(2);
                ToolbarItems.RemoveAt(2);
                ToolbarItems.RemoveAt(2);
                ToolbarItems.Add(newItem);
                ToolbarItems.Add(th4);
                ToolbarItems.Add(th5);
                ToolbarItems.Add(th6);
            }
        }

        /// <summary>
        /// Handles binding context change.
        /// Updates context in all found resources (bindable type).
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Resources != null)
            {
                foreach (var resource in Resources.Values.OfType<BindableObject>())
                {
                    resource.BindingContext = BindingContext;
                }
            }
        }

        #endregion
    }
}
