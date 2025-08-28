using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI;
using Tizen.Applications;
using System;
using SpeechToText.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SpeechToText.views.PreferencePageViews
{
    public class SoundsPage : ContentPage
    {
        /// <summary>
        /// Custom defined color
        /// </summary>
        private readonly Color color = new("#40ACC6");

        /// <summary>
        /// Contains the absolute path to the application resource directory
        /// </summary>
        private readonly string directory = Application.Current.DirectoryInfo.Resource;

        public SoundsPage(float height, float width, SttController sttController, Action<bool> toggleSound, Action onPop)
        {
            //Back button used in the AppBar to pop the current page
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
                    Text = "Sounds",
                    PointSize = 10,
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontStyle = mainAppBarTextStyle,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                },
            };

            //Scrollable view that holds the buttons
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

            //Sound button
            //View that holdes switch and label for sound toggle
            View soundView = new()
            {
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                Padding = new Extents(25, 0, 0, 0),
                SizeWidth = width,

            };

            //Text label that holds the sound button title
            TextLabel soundTitle = new()
            {
                Text = "Sound",
                PointSize = 7,
                TextColor = Color.Black,
            };

            //Switch that enables and disables start and end sounds
            Switch soundSwitch = new()
            {
                SizeWidth = width,
                SizeHeight = height / 10,
                SwitchBackgroundImageURLSelector = new StringSelector
                {
                    Normal = directory + "images/switch/controller_switch_bg_off.png",
                    Selected = directory + "images/switch/controller_switch_bg_on.png",
                    Disabled = directory + "images/switch/controller_switch_bg_off_dim.png",
                    DisabledSelected = directory + "images/switch/controller_switch_bg_on_dim.png"
                },
                SwitchHandlerImageURLSelector = new StringSelector
                {
                    Normal = directory + "images/switch/controller_switch_handler.png",
                    Selected = directory + "images/switch/controller_switch_handler.png",
                    Disabled = directory + "images/switch/controller_switch_handler_dim.png",
                    DisabledSelected = directory + "images/switch/controller_switch_handler_dim.png",
                },
                Margin = new Extents((ushort)(width * 0.7f), 0, 10, 10),
                IsEnabled = true,
                IsSelectable = true,
                IsSelected = sttController.Sounds,
            };

            //Assigning event to switch
            soundSwitch.SelectedChanged += (object sender, SelectedChangedEventArgs e) =>
            {
                if (e.IsSelected)
                {
                    sttController.Sounds = true;
                    toggleSound(true);
                }
                else
                {
                    sttController.Sounds = false;
                    toggleSound(false);
                };
            };

            //Adding the title and value to the sound toggle
            soundView.Add(soundTitle);
            soundView.Add(soundSwitch);

            //Start sound button
            //Button for selecting start sound
            var startSoundButton = new Button()
            {
                ItemSpacing = new Size2D((int)(width * 0.08f), 0),
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
                TextAlignment = HorizontalAlignment.Center,
                SizeHeight = height * 0.1f,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(25, 0, 0, 0),
                PointSize = 6,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };

            //Displays the state of the start sound file
            TextLabel startSoundValue = new()
            {
                Text = sttController.StartSound == null ? "None" : sttController.StartSound.Split('/').Last().Split(".wav").First(),
                PointSize = 6,
                TextColor = new Color(0, 0, 0, 0.6f),
            };

            //Title for the start sound button
            TextLabel startSoundTitle = new()
            {
                Text = "Start Sound",
                PointSize = 7,
                TextColor = Color.Black,
            };

            //Assigning event to the start sound button
            startSoundButton.TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Started)
                {
                    //Change the background color to give an efffect when pressed
                    startSoundButton.BackgroundColor = new Color(0, 0, 0, 0.2f);
                }
                if (e.Touch.GetState(0) == PointStateType.Leave)
                {
                    //Return the background color to default
                    startSoundButton.BackgroundColor = Color.Transparent;
                }
                if (e.Touch.GetState(0) == PointStateType.Finished)
                {
                    //To prevent double clicks
                    startSoundButton.IsEnabled = false;

                    //Return the background color to default
                    startSoundButton.BackgroundColor = Color.Transparent;
                    MediaController mediaController = new();
                    List<string> sounds = mediaController.GetAvailableStartEndSounds();
                    Navigator.Push(new PreferencesPage(
                        "Sounds",
                        height: height,
                        width: width,
                        currentPreferenceName: sttController.StartSound ?? "None",
                        preferences: sounds,
                        preferenceAction: delegate (string value)
                        {
                            sttController.StartSound = value == "None" ? null : value;
                            startSoundValue.Text = value.Split('/').Last().Split(".wav").First();
                        },
                        onPop: delegate () { startSoundButton.IsEnabled = true; }
                        )
                        );
                }
                return true;
            };

            //Adding the title and value to the start sound button
            startSoundButton.Add(startSoundTitle);
            startSoundButton.Add(startSoundValue);

            //End sound
            //Button for selecting end sound
            var endSoundButton = new Button()
            {
                ItemSpacing = new Size2D((int)(width * 0.08f), 0),
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
                TextAlignment = HorizontalAlignment.Center,
                SizeHeight = height * 0.1f,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(25, 0, 0, 0),
                PointSize = 6,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };

            //Displays the state of the end sound file
            TextLabel endSoundValue = new()
            {
                Text = sttController.EndSound == null ? "None" : sttController.EndSound.Split('/').Last().Split(".wav").First(),
                PointSize = 6,
                TextColor = new Color(0, 0, 0, 0.6f),
            };

            //Title for the end sound button
            TextLabel endSoundTitle = new()
            {
                Text = "End Sound",
                PointSize = 7,
                TextColor = Color.Black,
            };

            //Assigning event to the start sound button
            endSoundButton.TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Started)
                {
                    //Change the background color to give an effect when pressed
                    endSoundButton.BackgroundColor = new Color(0, 0, 0, 0.2f);
                }
                if (e.Touch.GetState(0) == PointStateType.Leave)
                {
                    //Return the background color to default
                    endSoundButton.BackgroundColor = Color.Transparent;
                }
                if (e.Touch.GetState(0) == PointStateType.Finished)
                {
                    //To prevent double clicks
                    endSoundButton.IsEnabled = false;

                    //Return the background color to default
                    endSoundButton.BackgroundColor = Color.Transparent;
                    MediaController mediaController = new();
                    List<string> sounds = mediaController.GetAvailableStartEndSounds();
                    Navigator.Push(new PreferencesPage(
                        "Sounds",
                        height: height,
                        width: width,
                        currentPreferenceName: sttController.EndSound ?? "None",
                        preferences: sounds,
                        preferenceAction: delegate (string value)
                        {
                            sttController.EndSound = value == "None" ? null : value;
                            endSoundValue.Text = value.Split('/').Last().Split(".wav").First();
                        },
                        onPop : delegate () { endSoundButton.IsEnabled = true; }
                        ));
                }
                return true;
            };

            //Adding the title and value to the end sound button
            endSoundButton.Add(endSoundTitle);
            endSoundButton.Add(endSoundValue);

            //Adding button to the list
            scrollableBase.Add(soundView);
            scrollableBase.Add(startSoundButton);
            scrollableBase.Add(endSoundButton);

            AppBar = appBar;
            Content = scrollableBase;
        }
    }
}
