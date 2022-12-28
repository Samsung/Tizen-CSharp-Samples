using Tizen.NUI.BaseComponents;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.Applications;
using SpeechToText.Controllers;
using System;
using SpeechToText.views.PreferencePageViews;
using SpeechToText.Converters;
using System.Collections.Generic;
using Tizen.Uix.Stt;
using System.Linq;

namespace SpeechToText.SettingsPageViews.SettingsPage
{
    class SettingsPage : ContentPage
    {
        /// <summary>
        /// Contains the absolute path to the application resource directory
        /// </summary>
        private readonly string directory = Application.Current.DirectoryInfo.Resource;

        /// <summary>
        /// Custom defined Color
        /// </summary>
        private readonly Color color = new("#40ACC6");


        public SettingsPage(float height, float width, SttController sttController, Action onPop)
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
                //To enable the settings button in the main page
                onPop();
                Navigator.Pop();
            };

            //App bar title Text Style
            var mainAppBarTextStyle = new PropertyMap();
            mainAppBarTextStyle.Add("slant", new PropertyValue(FontSlantType.Normal.ToString()));
            mainAppBarTextStyle.Add("width", new PropertyValue(FontWidthType.Expanded.ToString()));
            mainAppBarTextStyle.Add("weight", new PropertyValue(FontWeightType.DemiBold.ToString()));

            //Creating AppBar
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
                    Text = "Settings",
                    PointSize = 10,
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontStyle = mainAppBarTextStyle,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                },
            };

            //View to hold the general buttons
            View generalButtonList = new()
            {
                SizeWidth = width,
                SizeHeight = height * 0.4f,
                BackgroundColor = Color.Transparent,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                },
            };

            //Adding title to the general list
            generalButtonList.Add(new TextLabel()
            {
                Text = "General",
                TextColor = color,
                PointSize = 8,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                SizeHeight = height * 0.05f,
                BackgroundColor = Color.Transparent,
                Margin = new Extents(20, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Center,

            });

            //Adding buttons to the view
            //Language Button
            Button languageButton = new(new ButtonStyle()
            {
                BackgroundColor = new Selector<Color>
                {
                    Pressed = new Color(0, 0, 0, 0.2f),
                    Normal = Color.Transparent,
                },
                Icon = new ImageViewStyle()
                {
                    ResourceUrl = new Selector<string>
                    {
                        Pressed = directory + "/images/language_pressed.png",
                        Other = directory + "/images/language.png",
                    },
                },
                IsEnabled = true,
            })
            {
                PointSize = 6,
                ItemSpacing = new Size2D((int)(width * 0.08f), 0),
                TextColor = Color.Black,
                TextAlignment = HorizontalAlignment.Center,
                SizeHeight = height * 0.1f,
                Padding = new Extents(40, 0, 0, 0),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };

            //Displays the currently selected language
            TextLabel languageValueLabel = new()
            {
                Text = sttController.Language.ToString(),
                PointSize = 6,
                TextColor = new Color(0, 0, 0, 0.6f),
            };

            //Title for the language button
            TextLabel languageTitleLabel = new()
            {
                Text = "Language",
                PointSize = 7,
                TextColor = Color.Black,
            };

            //View that will hold the title and the currently selected language
            View languageButtonView = new()
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                Padding = new Extents(40, 0, 0, 0),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            //Adding the text labels to the view
            languageButtonView.Add(languageTitleLabel);
            languageButtonView.Add(languageValueLabel);

            //Adding the view to the button
            languageButton.Add(languageButtonView);

            //Assigning event to the language button
            languageButton.Clicked += (object sender, ClickedEventArgs e) =>
            {
                //To prevent double clicks
                languageButton.IsEnabled = false;

                //Converts the language code to language name
                var languageConverter = new SupportedLanguageToDisplayNameConverter();

                //Getting the currently selected language code, and converting it to the language name
                string currentLanguageName = languageConverter.Convert(sttController.Language);

                //Initializing empty list that will hold the languages name
                List<string> languageNames = new();

                //Getting languages code
                List<string> languageCodes = sttController.GetSupportedLanguages();

                foreach (string languageCode in languageCodes)
                {
                    //Adding language name to the language list
                    string languageName = languageConverter.Convert(languageCode);
                    languageNames.Add(languageName);
                }

                //Action that will be used to update the value label from the preference page when a new language is selected
                void languageAction(string languageName)
                {
                    string languageCode = languageConverter.ConvertBack(languageName);
                    sttController.Language = languageCode;
                    languageValueLabel.Text = languageCode;
                }

                Navigator.Push(new PreferencesPage(
                    "Language",
                    height: height,
                    width: width,
                    currentPreferenceName: currentLanguageName,
                    preferences: languageNames,
                    preferenceAction: languageAction,
                    delegate () { languageButton.IsEnabled = true; })
                    );
            };

            //Adding the language button to the general list
            generalButtonList.Add(languageButton);

            //Recognition type button
            var recognitionButton = new Button(new ButtonStyle()
            {
                BackgroundColor = new Selector<Color>
                {
                    Pressed = new Color(0, 0, 0, 0.2f),
                    Other = Color.Transparent,
                },
                Icon = new ImageViewStyle()
                {
                    ResourceUrl = new Selector<string>
                    {
                        Pressed = directory + "/images/recognition_type_pressed.png",
                        Other = directory + "/images/recognition_type.png",
                    },
                },
                IsEnabled = true,
            })
            {
                ItemSpacing = new Size2D((int)(width * 0.08f), 0),
                PointSize = 6,
                TextColor = Color.Black,
                TextAlignment = HorizontalAlignment.Center,
                SizeHeight = height * 0.1f,
                TooltipText = "English US",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(40, 0, 0, 0),
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };

            //Displays the currently selected recognition type
            TextLabel recognitionButtonValue = new()
            {
                Text = sttController.RecognitionType.ToString(),
                PointSize = 6,
                TextColor = new Color(0, 0, 0, 0.6f),
            };

            //Title for the recognition button
            TextLabel recognitionButtonTitle = new()
            {
                Text = "Recognition Type",
                PointSize = 7,
                TextColor = Color.Black,
            };

            //View that will hold the title and the currently selected recognition type
            View recognitionButtonView = new()
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                Padding = new Extents(40, 0, 0, 0),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            //Adding the text labels to the view
            recognitionButtonView.Add(recognitionButtonTitle);
            recognitionButtonView.Add(recognitionButtonValue);

            //Adding the view to the button
            recognitionButton.Add(recognitionButtonView);

            //Assign events to the recognition button
            recognitionButton.Clicked += (object sender, ClickedEventArgs e) =>
            {
                //To prevent double clicks
                recognitionButton.IsEnabled = false;

                //Converts the recognition type code to recognition type name
                var recognitionTypeToDisplayNameConverter = new RecognitionTypeToDisplayNameConverter();

                //Initializing empty list that will hold the recognition types name
                List<string> recognitionTypeNames = new();

                //Getting the currently selected recognition type code, and converting it to the recognition type name
                string currentRecognitionTypeName = recognitionTypeToDisplayNameConverter.Convert(sttController.RecognitionType);

                //Getting recognition types code
                List<RecognitionType> recognitionTypeCodes = Enum.GetValues<RecognitionType>().ToList();

                foreach (RecognitionType recognitionTypeCode in recognitionTypeCodes)
                {
                    //Adding recognition type name to the recognition types list
                    string recognitionTypeName = recognitionTypeToDisplayNameConverter.Convert(recognitionTypeCode);
                    recognitionTypeNames.Add(recognitionTypeName);
                }

                //Action that will be used to update the value label from the preference page when a new recognition type is selected
                void recognitionTypeAction(string languageName)
                {
                    RecognitionType recognitionType = recognitionTypeToDisplayNameConverter.ConvertBack(languageName);
                    sttController.RecognitionType = recognitionType;
                    recognitionButtonValue.Text = recognitionType.ToString();
                }

                Navigator.Push(new PreferencesPage(
                    "Recognition Type",
                    height: height,
                    width: width,
                    currentPreferenceName: currentRecognitionTypeName,
                    preferences: recognitionTypeNames,
                    preferenceAction: recognitionTypeAction,
                    delegate () { recognitionButton.IsEnabled = true; }
                    ));
            };

            //Adding the recognition button to the general list
            generalButtonList.Add(recognitionButton);

            //Silence detection button
            var silenceDetectionButton = new Button(new ButtonStyle()
            {
                BackgroundColor = new Selector<Color>
                {
                    Pressed = new Color(0, 0, 0, 0.2f),
                    Other = Color.Transparent,
                },
                Icon = new ImageViewStyle()
                {
                    ResourceUrl = new Selector<string>
                    {
                        Pressed = directory + "/images/silence_detection_pressed.png",
                        Other = directory + "/images/silence_detection.png"
                    },
                },
                IsEnabled = true,
            })
            {
                ItemSpacing = new Size2D((int)(width * 0.08f), 0),
                TextColor = Color.Black,
                TextAlignment = HorizontalAlignment.Center,
                SizeHeight = height * 0.1f,
                TooltipText = "English US",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(40, 0, 0, 0),
                PointSize = 6,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };

            //Title for the silence detection button
            TextLabel silenceDetectionButtonTitle = new()
            {
                Text = "Silence Detection",
                PointSize = 7,
                TextColor = Color.Black,
            };

            //Displays the currently selected silence detection type
            TextLabel silenceDetectionButtonValue = new()
            {
                Text = sttController.SilenceDetection.ToString(),
                PointSize = 6,
                TextColor = new Color(0, 0, 0, 0.6f),
            };

            //View that will hold the title and the currently selected language
            View silenceDetectionButtonView = new()
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                Padding = new Extents(40, 0, 0, 0),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            //Adding the text labels to the view
            silenceDetectionButtonView.Add(silenceDetectionButtonTitle);
            silenceDetectionButtonView.Add(silenceDetectionButtonValue);

            //Adding the view to the button
            silenceDetectionButton.Add(silenceDetectionButtonView);

            //Assign events to the silence detection button
            silenceDetectionButton.Clicked += (object sender, ClickedEventArgs e) =>
            {
                //To prevent double clicks
                silenceDetectionButton.IsEnabled = false;

                //Converts the silence detection type code to silence detection type name
                var silenceDetectionToDisplayNameConverter = new SilenceDetectionToDisplayNameConverter();

                //Initializing empty list that will hold the recognition types name
                List<string> silenceDetectionNames = new();

                //Getting the currently selected silence detection type code, and converting it to the silence detection type name
                string currentSilenceDetectionName = silenceDetectionToDisplayNameConverter.Convert(sttController.SilenceDetection);

                //Getting silence detection types code
                List<SilenceDetection> silenceDetectionCodes = Enum.GetValues<SilenceDetection>().ToList();

                foreach (SilenceDetection silenceDetectionCode in silenceDetectionCodes)
                {
                    //Adding silence detection type name to the silence detection types list
                    silenceDetectionNames.Add(silenceDetectionToDisplayNameConverter.Convert(silenceDetectionCode));
                }

                //Action that will be used to update the value label from the preference page when a new Silence Detection type is selected
                void silenceDetectionAction(string silenceDetectionName)
                {
                    SilenceDetection silenceDetectionType = silenceDetectionToDisplayNameConverter.ConvertBack(silenceDetectionName);
                    sttController.SilenceDetection = silenceDetectionType;
                    silenceDetectionButtonValue.Text = silenceDetectionName;
                }

                Navigator.Push(new PreferencesPage(
                    "Silence Detection",
                    height: height, width: width,
                    currentPreferenceName: currentSilenceDetectionName,
                    preferences: silenceDetectionNames,
                    preferenceAction: silenceDetectionAction, delegate () { silenceDetectionButton.IsEnabled = true; })
                );
            };

            //Adding the silence detection button to the general list
            generalButtonList.Add(silenceDetectionButton);

            //View to hold the personal buttons
            View personalButtonList = new()
            {
                SizeWidth = width,
                SizeHeight = height * 0.3f,
                BackgroundColor = Color.Transparent,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                },
            };

            //Adding title to the persnoal list
            personalButtonList.Add(new TextLabel()
            {
                Text = "Personal",
                TextColor = color,
                PointSize = 8,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                SizeHeight = height * 0.05f,
                BackgroundColor = Color.Transparent,
                Margin = new Extents(20, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Center,
            });

            //Sound button
            var soundsButton = new Button(new ButtonStyle()
            {
                BackgroundColor = new Selector<Color>
                {
                    Pressed = new Color(0, 0, 0, 0.2f),
                    Other = Color.Transparent,
                },
                Icon = new ImageViewStyle()
                {
                    ResourceUrl = new Selector<string>
                    {
                        Pressed = directory + "/images/sounds_pressed.png",
                        Other = directory + "/images/sounds.png"
                    },
                },
                IsEnabled = true,
            })
            {
                ItemSpacing = new Size2D((int)(width * 0.08f), 0),
                TextColor = Color.Black,
                TextAlignment = HorizontalAlignment.Center,
                SizeHeight = height * 0.1f,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(40, 0, 0, 0),
                PointSize = 6,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };

            //Displays the currently selected language
            TextLabel soundsValueLabel = new()
            {
                Text = sttController.Sounds.ToString(),
                PointSize = 6,
                TextColor = new Color(0, 0, 0, 0.6f),
            };

            //Title for the language button
            TextLabel soundsTitleLabel = new()
            {
                Text = "Sounds",
                PointSize = 7,
                TextColor = Color.Black,
            };

            //View that will hold the title and the state of the sound's settings
            View soundsButtonView = new()
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                Padding = new Extents(40, 0, 0, 0),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            //Adding the text labels to the view
            soundsButtonView.Add(soundsTitleLabel);
            soundsButtonView.Add(soundsValueLabel);

            //Adding the view to the button
            soundsButton.Add(soundsButtonView);

            //Assign events to the sound button
            soundsButton.Clicked += (object sender, ClickedEventArgs e) =>
            {
                //To prevent double clicks
                soundsButton.IsEnabled = false;

                Navigator.Push(
                    new SoundsPage(height: height,
                    width: width,
                    sttController,
                    delegate (bool value) { soundsValueLabel.Text = value.ToString(); },
                    delegate () { soundsButton.IsEnabled = true; }));
            };

            //Adding the sound button to the personal list
            personalButtonList.Add(soundsButton);

            //The view that will hold the general and personal lists
            View mainView = new()
            {
                LayoutDirection = ViewLayoutDirectionType.LTR,
                SizeHeight = height - (height / 12),
                SizeWidth = width,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
            };

            //Adding the general and personal lists to the main view
            mainView.Add(generalButtonList);
            mainView.Add(personalButtonList);

            AppBar = appBar;
            Content = mainView;
            BackgroundColor = Color.White;
        }
    }
}
