using System;
using System.Collections.Generic;
using Tizen.Location.Geofence;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace Geofence
{
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

        VirtualPerimeter perimeter;

        public MainPage(VirtualPerimeter perimeter)
        {
            InitializeComponent();

            this.perimeter = perimeter;

            AddPlaceButton.Clicked += OnClickedInsertInfoPage;
            AddFenceButton.Clicked += OnClickedSelectIDPage;

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
        /// Event when "Add Place" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedInsertInfoPage(object sender, ClickedEventArgs e)
        {
            Navigator?.Push(new InsertInfoPage(perimeter, (sender as Button).Text));
        }

        /// <summary>
        /// Event when "Add Place" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedSelectIDPage(object sender, ClickedEventArgs e)
        {
            Navigator?.Push(new SelectIDPage(perimeter, (sender as Button).Text));
        }
    }
}
