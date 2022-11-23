/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Geofence.ViewModels;

namespace Geofence
{
    /// <summary>
    /// Class representing Main Page
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// A List for information labels.
        /// </summary>
        static List<TextLabel> InfoLabelList = null;

        /// <summary>
        /// A List for buttons.
        /// </summary>
        static List<Button> ButtonList = null;

        public static int FenceID { get; set; } = 0;

        public MainPage()
        {
            InitializeComponent();

            AddPlaceButton.Clicked += OnClickedInsertInfoPage;
            RemovePlaceButton.Clicked += OnClickedSelectIDPagePlace;
            AddFenceButton.Clicked += OnClickedSelectIDPagePlace;
            RemoveFenceButton.Clicked += OnClickedSelectIDPageFence;
            StartButton.Clicked += OnClickedSelectIDPageFence;
            StopButton.Clicked += OnClickedStopButton;
            UpdatePlaceButton.Clicked += OnClickedSelectIDPagePlace;
            FenceStatusButton.Clicked += OnClickedSelectIDPageFence;

            InfoLabelList = new List<TextLabel>();
            InfoLabelList.Add(FirstTextLabel);
            InfoLabelList.Add(SecondTextLabel);
            InfoLabelList.Add(ThirdTextLabel);
            InfoLabelList.Add(FirstTextLabel);

            ButtonList = new List<Button>();
            ButtonList.Add(AddPlaceButton);
            ButtonList.Add(RemovePlaceButton);
            ButtonList.Add(AddFenceButton);
            ButtonList.Add(RemoveFenceButton);
            ButtonList.Add(StartButton);
            ButtonList.Add(StopButton);
            ButtonList.Add(UpdatePlaceButton);
            ButtonList.Add(FenceStatusButton);
        }

        public List<TextLabel> GetInfoLabelList
        {
            get => InfoLabelList;
        }

        public List<Button> GetButtonList
        {
            get => ButtonList;
        }

        /// <summary>
        /// Event when Add Place button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedInsertInfoPage(object sender, ClickedEventArgs e)
        {
            Navigator?.PushWithTransition(new InsertInfoPage((sender as Button).Text, -1));
        }

        /// <summary>
        /// Event when button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedSelectIDPagePlace(object sender, ClickedEventArgs e)
        {
            var list = new SelectIDPageViewModel(Program.Perimeter, DataType.PLACE);
            if (list.Source.Count.Equals(0))
            {
                // Check wrong Input parameter
                var button = new Button()
                {
                    Text = "OK",
                };
                button.Clicked += (object s, ClickedEventArgs a) =>
                {
                    Navigator?.Pop();
                };
                DialogPage.ShowAlertDialog("Alert", "Empty", button);
            }
            else
            {
                Navigator?.PushWithTransition(new SelectIDPage((sender as Button).Text, list, this, sender as Button));
            }
        }

        /// <summary>
        /// Event when button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedSelectIDPageFence(object sender, ClickedEventArgs e)
        {
            var list = new SelectIDPageViewModel(Program.Perimeter, DataType.FENCE);
            if (list.Source.Count.Equals(0))
            {
                // Check wrong Input parameter
                var button = new Button()
                {
                    Text = "OK",
                };
                button.Clicked += (object s, ClickedEventArgs a) =>
                {
                    Navigator?.Pop();
                };
                DialogPage.ShowAlertDialog("Alert", "Empty", button);
            }
            else
            {
                Navigator?.PushWithTransition(new SelectIDPage((sender as Button).Text, list, this, sender as Button));
            }
        }

        /// <summary>
        /// Event when Stop button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedStopButton(object sender, ClickedEventArgs e)
        {
            var list = new SelectIDPageViewModel(Program.Perimeter, DataType.FENCE);
            if (list.Source.Count.Equals(0))
            {
                // Check wrong Input parameter
                var button = new Button()
                {
                    Text = "OK",
                };
                button.Clicked += (object s, ClickedEventArgs a) =>
                {
                    Navigator?.Pop();
                };
                DialogPage.ShowAlertDialog("Alert", "Empty", button);
            }
            else
            {
                Program.Geofence.Stop(FenceID);
                StopButton.IsEnabled = false;
            }  
        }
    }
}
