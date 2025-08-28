using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI;
using Tizen.Applications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeechToText.views.PreferencePageViews
{
    public class PreferencesPage : ContentPage
    {
        /// <summary>
        /// Custom defined Color
        /// </summary>
        private readonly Color color = new("#40ACC6");

        /// <summary>
        /// Contains the absolute path to the application resource directory
        /// </summary>
        private readonly string directory = Application.Current.DirectoryInfo.Resource;

        /// <summary>
        /// Holds the selected preference button
        /// </summary>
        private Button selectedButton;

        public PreferencesPage(string title, float height, float width, string currentPreferenceName, List<string> preferences, Action<string> preferenceAction, Action onPop)
        {
            //Back button used in the app bar to pop the current page
            Button backButton = new(new ButtonStyle()
            {
                Opacity = new Selector<float?>
                {
                    Pressed = 0.5f,
                    Other = 1f
                }
            })
            {
                BackgroundImage = directory + "images/back.png",
                SizeWidth = 50,
                SizeHeight = 50,
                Margin = new Extents(25, 0, 0, 0),
            };

            backButton.Clicked += (object sender, ClickedEventArgs e) =>
            {
                onPop();
                Navigator.Pop();
            };

            //Scrollable view that holds the preference buttons
            ScrollableBase scrollableBase = new()
            {
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical
                },
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                SizeHeight = height - height / 12,
                SizeWidth = width,
                ScrollEnabled = true,
            };

            foreach (string preferenceName in preferences)
            {
                //Creating button for each preference
                Button preferenceButton = new(new ButtonStyle()
                {
                    BackgroundColor = new Selector<Color>
                    {
                        Pressed = new Color(color.R, color.G, color.B, 0.3f),
                        Selected = new Color(color.R, color.G, color.B, 0.5f),
                        Other = Color.Transparent,
                    },
                })
                {
                    Layout = new LinearLayout()
                    {
                        LinearOrientation = LinearLayout.Orientation.Horizontal,
                    },
                    //BackgroundColor = currentPreferenceName == preferenceName ? new Color(color.R, color.G, color.B, 0.5f) : Color.Transparent,
                    PointSize = 8,
                    TextAlignment = HorizontalAlignment.Begin,
                    Text = preferenceName.Split('/').Last().Split(".wav").First(),
                    Padding = new Extents(25, 0, 25, 0),
                    TextColor = Color.Black,
                    SizeWidth = width,
                    SizeHeight = height / 10,
                    Margin = new Extents(0, 0, 10, 10),
                    IsSelectable = true,
                };

                //Selecting the Default button
                if (currentPreferenceName == preferenceName)
                {
                    selectedButton = preferenceButton;
                    selectedButton.IsSelected = true;
                }
                preferenceButton.TouchEvent += (object sender, TouchEventArgs e) =>
                {
                    if (e.Touch.GetState(0) == PointStateType.Started)
                    {
                        //Change the default background color to give an effect when pressed
                        //preferenceButton.BackgroundColor = new Color(color.R, color.G, color.B, 0.3f);
                    }
                    if (e.Touch.GetState(0) == PointStateType.Leave)
                    {
                        //Return the default background color
                        //preferenceButton.BackgroundColor = Color.Transparent;
                    }
                    if (e.Touch.GetState(0) == PointStateType.Finished)
                    {
                        //Return the default background color
                        //if (selectedButton != null)
                        //selectedButton.BackgroundColor = Color.Transparent;
                        //Changing the selected button to the pressed one
                        //preferenceButton.BackgroundColor = new Color(color.R, color.G, color.B, 0.5f);
                        selectedButton.IsSelected = false;
                        preferenceButton.IsSelected = true;
                        selectedButton = preferenceButton;
                        //To change the text value on the button of the settings page
                        preferenceAction(preferenceName);
                    }
                    return true;
                };
                //Adding button the scrollable view
                scrollableBase.Add(preferenceButton);
            }

            //Creating AppBar and AppBar style
            var mainAppBarTextStyle = new PropertyMap();
            mainAppBarTextStyle.Add("slant", new PropertyValue(FontSlantType.Normal.ToString()));
            mainAppBarTextStyle.Add("width", new PropertyValue(FontWidthType.Expanded.ToString()));
            mainAppBarTextStyle.Add("weight", new PropertyValue(FontWeightType.DemiBold.ToString()));

            //Creating the AppBar with one navigation button and without any additional buttons
            AppBar appBar = new()
            {
                SizeHeight = height / 12,
                SizeWidth = width,
                BackgroundColor = color,
                AutoNavigationContent = false,
                ActionContent = new View() { SizeWidth = width * 0.1f },
                NavigationContent = backButton,
                TitleContent = new TextLabel()
                {
                    Text = title,
                    PointSize = 10,
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontStyle = mainAppBarTextStyle,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                },
            };
            AppBar = appBar;
            Content = scrollableBase;
        }
    }
}
