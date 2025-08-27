using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI.Binding;
using System;
using Tizen.Uix.InputMethodManager;

namespace IMEManager
{
    public class Operations
    {
        public Operations(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string ViewLabel
        {
            get
            {
                return Name;
            }
        }

        public bool Selected { get; set; }
    }
    internal class Program : NUIApplication
    {
        private Window window;
        private Navigator navigator;
        private AppBar mainAppBar, customAppBar;
        private ContentPage mainPage, customPage;
        private View mainView, customView;
        private CollectionView mainCollectionView;
        private List<Operations> operations = new List<Operations> { new Operations(" ShowIMEList"), new Operations(" ShowIMESelector"), new Operations(" IsIMEEnabled"), new Operations(" GetActiveIME"), new Operations(" GetEnabledIMECount") };
        TextLabel IMEStatusLabel;
        LinearLayout customPageLayout;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
            SetMainPage();
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        void Initialize()
        {
            //Initialize the defined variables 
            window = GetDefaultWindow();
            window.Title = "IMEManager";
            window.KeyEvent += OnKeyEvent;
            navigator = window.GetDefaultNavigator();
            var statusLabelTextStyle = new PropertyMap();
            statusLabelTextStyle.Add("slant", new PropertyValue(FontSlantType.Normal.ToString()));
            statusLabelTextStyle.Add("width", new PropertyValue(FontWidthType.Expanded.ToString()));
            statusLabelTextStyle.Add("weight", new PropertyValue(FontWeightType.Light.ToString()));
            IMEStatusLabel = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextColor = Color.Black,
                PointSize = 10f,
                FontStyle = statusLabelTextStyle,
            };
            customPageLayout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
        }

        //This function will be called when the event clicked or select.
        public void OnSelectionChanged(object sender, SelectionChangedEventArgs ev)
        {
            Console.WriteLine($"@@@ OnSelectionChanged() {ev.CurrentSelection}");

            customView.Layout = customPageLayout;

            foreach (object item in ev.CurrentSelection)
            {
                if (item == null)
                {
                    break;
                }

                var selItem = item as Operations;

                if (selItem.Name == " ShowIMEList")
                {
                    try
                    {
                        customView.ClearBackground();
                        IMEStatusLabel.Text = "ShowIMEList";
                        customView.Add(IMEStatusLabel);
                        Manager.ShowIMEList();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Exit();
                    }

                }
                else if (selItem.Name == " ShowIMESelector")
                {
                    try
                    {
                        customView.ClearBackground();
                        IMEStatusLabel.Text = "ShowIMESelection";
                        customView.Add(IMEStatusLabel);
                        Manager.ShowIMESelector();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Exit();
                    }
                }
                else if (selItem.Name == " IsIMEEnabled")
                {
                    try
                    {
                        customView.ClearBackground();
                        bool IMEEnabled = Manager.IsIMEEnabled(this.ApplicationInfo.ApplicationId);
                        string status = IMEEnabled ? " ON" : "Off";
                        IMEStatusLabel.Text = "IME State : " + status;
                        customView.Add(IMEStatusLabel);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Exit();
                    }
                }
                else if (selItem.Name == " GetActiveIME")
                {
                    try
                    {
                        customView.ClearBackground();
                        IMEStatusLabel.Text = "IME Active : " + Manager.GetActiveIME();
                        customView.Add(IMEStatusLabel);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Exit();
                    }
                }
                else if (selItem.Name == " GetEnabledIMECount")
                {
                    try
                    {
                        customView.ClearBackground();
                        IMEStatusLabel.Text = "IME Count : " + Manager.GetEnabledIMECount();
                        customView.Add(IMEStatusLabel);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Exit();
                    }
                }
            }
            navigator.Push(customPage);
        }

        private void SetMainPage()
        {
            //Setting the AppBar variable.
            var mainAppBarTextStyle = new PropertyMap();
            mainAppBarTextStyle.Add("slant", new PropertyValue(FontSlantType.Normal.ToString()));
            mainAppBarTextStyle.Add("width", new PropertyValue(FontWidthType.Expanded.ToString()));
            mainAppBarTextStyle.Add("weight", new PropertyValue(FontWeightType.Medium.ToString()));
            mainAppBar = new()
            {
                TitleContent = new TextLabel()
                {
                    HeightSpecification = LayoutParamPolicies.MatchParent,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextColor = Color.White,
                    PointSize = 12,
                    Text = "IMEManager",
                    FontStyle = mainAppBarTextStyle,
                },
                AutoNavigationContent = false,
                SizeWidth = window.Size.Width,
                SizeHeight = window.Size.Height / 12,
                BackgroundColor = Color.Transparent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                ActionContent = new View(),
                NavigationContent = new View(),
            };

            //Creating the mainView to display for the user.
            mainView = new()
            {
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },

                //Without adding specification items will now show in the collection
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = Color.White,
            };

            //Putting all galleries in collection as list to display it . 
            mainCollectionView = new()
            {
                ItemsSource = operations,
                ItemsLayouter = new LinearLayouter(),
                ItemTemplate = new DataTemplate(() =>
                {
                    DefaultLinearItem item = new DefaultLinearItem()
                    {
                        WidthSpecification = LayoutParamPolicies.MatchParent,
                    };
                    item.Label.SetBinding(TextLabel.TextProperty, "ViewLabel");
                    item.Label.HorizontalAlignment = HorizontalAlignment.Begin;
                    item.SizeHeight = window.Size.Height / 8;
                    //BaseComponents' Focusable is false as a default value, true should be set to navigate key focus.
                    return item;

                }),
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                HideScrollbar = true,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                SelectionMode = ItemSelectionMode.Single,//For selecting only one item from list.
            };

            //When the event SelectionChanged ocures will cal the function OnSelectionChanged.
            mainCollectionView.SelectionChanged += OnSelectionChanged;

            //The custom appbar will have a back button this is the reason its created to get the user back to the main page. 
            customAppBar = new()//every page needs a new App bar ** do not use the same app bar for more than one page
            {
                TitleContent = new TextLabel()
                {
                    HeightSpecification = LayoutParamPolicies.MatchParent,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextColor = Color.White,
                    PointSize = 12,
                    Text = "IMEManager",
                    FontStyle = mainAppBarTextStyle,
                },
                AutoNavigationContent = false,
                SizeWidth = window.Size.Width,
                SizeHeight = window.Size.Height / 12,
                BackgroundColor = Color.Transparent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                ActionContent = new View(),
            };

            //Creating custom view for the new page which will be opened after the selection .
            customView = new()
            {
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            //Custome page is the page that will be opened after the selection.
            //Seting the appbar and contenet for the pages.
            customPage = new ContentPage()
            {
                AppBar = customAppBar,
                Content = customView,
                BackgroundImage = DirectoryInfo.Resource + "images/bg.png",
            };

            mainView.Add(mainCollectionView);
            mainPage = new ContentPage()
            {
                AppBar = mainAppBar,
                Content = mainView,
                BackgroundImage = DirectoryInfo.Resource + "images/bg.png",
            };

            //Pushing the main page for the user.
            navigator.Push(mainPage);
        }
        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
