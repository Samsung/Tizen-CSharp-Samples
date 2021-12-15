using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.Applications;
using System.Collections.Generic;
using System.Linq;
using Badges.Models;
using Badges.ViewModels;

namespace Badges
{
    public partial class XamlPage : ContentPage
    {
        List<AppInfo> changeableApplications;
        List<AppInfo> others;
        View selectedView;
        AppInfo selectedIteam;
        public XamlPage()
        {
            InitializeComponent();
            changeableApplications = new List<AppInfo>();
            others = new List<AppInfo>();
            fillApplicationsList();
            fillScroller();
            IncreaseButton.Clicked += OnKeyEventIncreaseButton;
            ReduceButton.Clicked += OnKeyEventReduceButton;
            ApplyButton.Clicked += OnKeyEventApplyButton;
            ResetButton.Clicked += OnKeyEventResetButton;
        }
        public bool OnTouchEvent(object sender, View.TouchEventArgs args)
        {
            if(selectedView != null)
            {
                selectedView.BackgroundColor = Color.White;
            }
            selectedView = sender as View;
            //#cfd8dc
            selectedView.BackgroundColor = Color.Gray;
            selectedIteam = changeableApplications.Find(x => x.AppName.Equals(selectedView.Name));
            CounterNumberFiled.Text = selectedIteam.BadgeCounter.ToString();
            ReduceButton.IsEnabled = true;
            IncreaseButton.IsEnabled = true;
            ApplyButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
            return true;
        }
        public void OnKeyEventIncreaseButton(object sender, ClickedEventArgs args)
        {
            selectedIteam.BadgeCounter++;
            if(selectedIteam.BadgeCounter > 99)
            {
                CounterNumberFiled.Text = "99+";
            }
            else
            {
                CounterNumberFiled.Text = selectedIteam.BadgeCounter.ToString();
            }
        }
        public void OnKeyEventReduceButton(object sender, ClickedEventArgs args)
        {
            if(selectedIteam.BadgeCounter > 0)
            {
                selectedIteam.BadgeCounter--;
                CounterNumberFiled.Text = selectedIteam.BadgeCounter.ToString();
            }
        }
        public void OnKeyEventApplyButton(object sender, ClickedEventArgs args)
        {
            if(selectedView!=null)
            {
                selectedView.Remove(selectedView.Children.Last());
                selectedView.Add(new TextLabel
                {
                    Text = selectedIteam.BadgeCounter.ToString()
                });
            }
        }
        public void OnKeyEventResetButton(object sender, ClickedEventArgs args)
        {
            if (selectedView != null)
            {
                selectedIteam.BadgeCounter = 0;
                CounterNumberFiled.Text = selectedIteam.BadgeCounter.ToString();
                selectedView.Remove(selectedView.Children.Last());
                selectedView.Add(new TextLabel
                {
                    Text = "0"
                });
            }
        }
        public void fillApplicationsList()
        {
            IEnumerable<Package> packageList = PackageManager.GetPackages();
            foreach (Package pkg in packageList)
            {
                var list = pkg.GetApplications();
                foreach (var app in list)
                {
                    if(!app.IsNoDisplay)
                    {
                        if(app.IsPreload)
                        {
                            others.Add(new AppInfo(app.Label, app.PackageId, false, 0));
                        }
                        else
                        {
                            changeableApplications.Add(new AppInfo(app.Label, app.PackageId, true, 0));
                        }
                    }
                }
            }
        }
        public void fillScroller()
        {
            addListLabelToScroller("Changeable Applications");
            addListToScroller(changeableApplications);
            addListLabelToScroller("Others");
            addListToScroller(others);
        }
        public void addListToScroller(List<AppInfo> list)
        {
            foreach (var app in list)
            {
                Scroller.Add(new View
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    Name = app.AppName,
                    Layout = new FlexLayout
                    {
                        Direction = FlexLayout.FlexDirection.Row,
                        Justification = FlexLayout.FlexJustification.SpaceBetween,
                    }
                });
                if (app.IsAvailable)
                {
                    Scroller.Children.Last().TouchEvent += OnTouchEvent;
                    Scroller.Children.Last().Add(new TextLabel
                    {
                        Text = app.AppName
                    });
                }
                else
                {
                    Scroller.Children.Last().Add(new TextLabel
                    {
                        Text = app.AppName,
                        TextColor = Color.Gray
                    });
                }
                Scroller.Children.Last().Add(new TextLabel
                {
                    Text = app.BadgeCounter.ToString()
                });
            }
        }
        public void addListLabelToScroller(string label)
        {
            Scroller.Add(new View());
            Scroller.Children.Last().Add(new TextLabel
            {
                Text = label,
                PointSize = 12,
                TextColor = Color.Cyan
            });
        }
    }
}
