using System;
using System.Collections.Generic;
using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Alarms.Services;
using System.Linq;
using Alarms.Models;

namespace Alarms
{
    public partial class ChooseAppPage : ContentPage
    {
        /// <summary>
        /// Application Info od application selected by user from list of changeable applications
        /// </summary>
        AppInfo selectedIteam;

        /// <summary>
        /// List of installed applications
        /// </summary>
        List<AppInfo> appList;

        public ChooseAppPage()
        {
            InitializeComponent();
            appList = AppListService.GetAppList();
            appList.ForEach(x => addElementToScroller(x));
        }

        /// <summary>
        /// Event when user touch on of the elements from application list.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        /// <returns> event return ture if object was selected correctly</returns>
        public bool OnTouchEvent(object sender, TouchEventArgs args)
        {
            View selectedView = sender as View;
            selectedIteam = appList.Find(x => x.AppId.Equals(selectedView.Name));
            ChooseAlarmTypePage chooseAlarmTypePage = new ChooseAlarmTypePage(selectedIteam);
            Navigator?.Push(chooseAlarmTypePage);
            return true;
        }

        private void addElementToScroller(AppInfo element)
        {
            Scroller.Add(new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Name = element.AppId,
                Layout = new FlexLayout
                {
                    Direction = FlexLayout.FlexDirection.Row,
                    Justification = FlexLayout.FlexJustification.SpaceBetween,
                }
            });
            Scroller.Children.Last().Add(new TextLabel
            {
                Text = element.AppName,
                TextColor = Color.Black
            });
            Scroller.Children.Last().TouchEvent += OnTouchEvent;
        }
    }
}
