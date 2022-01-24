using System;
using System.Collections.Generic;
using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Alarms.Services;
using System.Linq;

namespace Alarms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            AlarmListService.GetAlarmList().ForEach(x => addElementToScroller(x));
            SetAlaramButton.Clicked += OnClicked;
        }

        /// <summary>
        /// Event when "Set Alaram" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClicked(object sender, ClickedEventArgs e)
        {
            ChooseAppPage chooseAppPage = new ChooseAppPage();
            Navigator?.Push(chooseAppPage);
        }

        private void addElementToScroller(string element)
        {
            Scroller.Add(new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Name = Scroller.ChildCount.ToString(),
                Layout = new FlexLayout
                {
                    Direction = FlexLayout.FlexDirection.Row,
                    Justification = FlexLayout.FlexJustification.SpaceBetween,
                }
            });
            Scroller.Children.Last().Add(new TextLabel
            {
                Text = element,
                TextColor = Color.Black
            });
        }
    }
}
