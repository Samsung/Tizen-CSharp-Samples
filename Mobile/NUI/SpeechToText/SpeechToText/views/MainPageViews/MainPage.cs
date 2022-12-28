using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI;
using Tizen.Applications;
using System;
using SpeechToText.Controllers;
using SpeechToText.SettingsPageViews.SettingsPage;
using SpeechToText.views.MainPageViews;

namespace SpeechToText.MainPageViews.MainPage
{

    class MainPage : ContentPage
    {
        /// <summary>
        /// Contains the absolute path to the application resource directory
        /// </summary>
        private readonly string directory = Application.Current.DirectoryInfo.Resource;

        /// <summary>
        /// Speech to text controller.
        /// </summary>
        private readonly SttController sttController;

        /// <summary>
        /// Text label to hold the result of Speech to text component.
        /// </summary>
        private readonly TextLabel resultText;

        /// <summary>
        /// Microphone button to start recording.
        /// </summary>
        private readonly Button micButton;

        /// <summary>
        /// Action that will be called inside the controller to change the UI according to the state.
        /// This action will update the result text on the Main page when the result is done.
        /// </summary>
        private readonly Action<string> onTextChange;

        /// <summary>
        /// Action that will enable the mic button.
        /// </summary>
        private readonly Action onCreateState;

        /// <summary>
        /// Action that will enable the mic button.
        /// </summary>
        private readonly Action onReadyState;

        /// <summary>
        /// Action that changes the mic button while recording.
        /// </summary>
        private readonly Action onRecordingState;

        /// <summary>
        /// Action that shows loading indicator while processing the data.
        /// </summary>
        private readonly Action onProcessingState;

        /// <summary>
        /// Action that will show a dialog.
        /// </summary>
        private readonly Action<string, string, bool> showDialog;

        /// <summary>
        /// Custom defined Color
        /// </summary>
        private readonly Color color = new("#40ACC6");

        public MainPage(float height, float width, SttController sttController)
        {
            //Loading Indicator
            LoadingIndicator loadingIndicator = new LoadingIndicator();

            onTextChange = delegate (string text)
            {
                if (text != null)
                    resultText.Text = text;
                loadingIndicator.Hide();
                loadingIndicator.Stop();
            };

            onCreateState = delegate ()
            {
                loadingIndicator.Show();
                loadingIndicator.Play();
                micButton.IsEnabled = false;
                micButton.IconURL = directory + "/images/mic2.png";
            };

            onReadyState = delegate ()
            {
                micButton.IsEnabled = true;
                loadingIndicator.Hide();
                loadingIndicator.Stop();
                micButton.IconURL = directory + "/images/mic2.png";
            };

            onRecordingState = delegate ()
            {
                micButton.IconURL = directory + "/images/mic1.png";
            };

            onProcessingState = delegate ()
            {
                micButton.IconURL = directory + "/images/mic2.png";
                loadingIndicator.Show();
                loadingIndicator.Play();
            };

            showDialog = delegate (string titleMessage, string message, bool exit)
            {
                Button okButton = new() { BackgroundColor = color, Text = "Ok", SizeHeight = 100, SizeWidth = 150 };
                okButton.Clicked += (sender, e) =>
                {
                    if (!exit)
                    {
                        Navigator.Pop();
                        return;
                    }
                    Application.Current.Exit();
                };
                DialogPage.ShowAlertDialog(titleMessage, message, okButton);
            };

            //Initializing the speech to text controller
            this.sttController = sttController;
            this.sttController.OnCreateState = onCreateState;
            this.sttController.OnReadyState = onReadyState;
            this.sttController.OnResultChange = onTextChange;
            this.sttController.OnRecordingState = onRecordingState;
            this.sttController.OnProcessingState = onProcessingState;
            this.sttController.OnErrorAlert = showDialog;

            //UI For the main page
            //Navigation button at the top left(settings)
            Button settingsButton = new(new ButtonStyle()
            {
                BackgroundColor = new Selector<Color>()
                {
                    Pressed = new(0, 0, 0, 0.2f),
                    Other = Color.Transparent
                },
            })
            {
                IconURL = directory + "/images/settings.png",
                SizeWidth = 150,
                SizeHeight = 100,
                CornerRadius = 25,
            };

            //Assigning touch event for the settings button to naivgate to the Setting Page
            settingsButton.Clicked += (object sender, ClickedEventArgs e) =>
            {
                //Navigating to settings page if the state is not recording or processing
                //Otherwise it will show a dialog 
                if (this.sttController.State == Tizen.Uix.Stt.State.Recording || this.sttController.State == Tizen.Uix.Stt.State.Processing)
                {
                    showDialog("Notice", "You Can not go the settings while recording or processing", false);
                }
                else
                {
                    settingsButton.IsEnabled = false;
                    Navigator.Push(page: new SettingsPage(SizeHeight, SizeWidth, this.sttController, delegate () { settingsButton.IsEnabled = true; }));
                }

            };

            //App Bar Title Text Style
            var mainAppBarTextStyle = new PropertyMap();
            mainAppBarTextStyle.Add("slant", new PropertyValue(FontSlantType.Normal.ToString()));
            mainAppBarTextStyle.Add("width", new PropertyValue(FontWidthType.Expanded.ToString()));
            mainAppBarTextStyle.Add("weight", new PropertyValue(FontWeightType.DemiBold.ToString()));

            //Creating the AppBar with one navigation button and without any additional buttons
            AppBar appBar = new()
            {
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Center,

                },
                SizeHeight = height / 12,
                SizeWidth = width,
                BackgroundColor = Color.Transparent,
                AutoNavigationContent = false,
                NavigationContent = settingsButton,
                //Disable the action button
                ActionContent = new View() { SizeWidth = width * 0.1f },
                TitleContent = new TextLabel()
                {
                    Text = "Speech To Text",
                    PointSize = 10,
                    TextColor = Color.White,
                    Margin = new Extents((ushort)(width * 0.1f), (ushort)(width * 0.1f), 0, 0),
                    FontStyle = mainAppBarTextStyle,
                },
            };

            //The main view of the page, it will contain the text and the three buttons(cancel, mic, stop)
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

            //Button view to hold the buttons horizontally
            //It will be added to the main view
            View buttonView = new()
            {
                SizeWidth = width,
                SizeHeight = height * 0.15f,
                BackgroundColor = Color.Transparent,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    CellPadding = new Size2D(30, 10),
                },
            };

            //Creating buttons
            //Microphone button
            micButton = new Button()
            {
                Size = new Size(130, 130),
                BackgroundImage = directory + "/images/mic1_bg.png",
                IconURL = directory + "/images/mic2.png",
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            //This button will work only if the _state is ready
            //This button will change the _state to recording while you are speaking
            micButton.Clicked += (object sender, ClickedEventArgs e) =>
            {
                this.sttController.Start();
            };

            //Cancel button to cancel the recording
            //The _state will change from recording to ready
            Button cancelButton = new(new ButtonStyle()
            {
                BackgroundImage = new Selector<string>()
                {
                    Pressed = directory + "images/clear_pressed.png",
                    Normal = directory + "images/clear_default.png",
                }
            })
            {
                SizeWidth = 100,
                SizeHeight = 100,
            };
            cancelButton.Clicked += (object sender, ClickedEventArgs e) =>
            {
                //Canceling the recording process 
                this.sttController.Cancel();
            };

            //Stop button
            Button stopButton = new(new ButtonStyle()
            {
                BackgroundImage = new Selector<string>()
                {
                    Pressed = directory + "images/stop_pressed.png",
                    Normal = directory + "/images/stop_default.png"
                }
            })
            {
                SizeWidth = 100,
                SizeHeight = 100,
            };

            //The _state will change from recording to processing
            //When the processing is finished the _state will be ready again
            stopButton.Clicked += (object sender, ClickedEventArgs e) =>
            {
                this.sttController.Stop();
            };

            //Text view to hold the text label when the result is ready
            View textView = new()
            {
                SizeWidth = width,
                SizeHeight = height * 0.75f,
                BackgroundColor = Color.Transparent,
                Layout = new AbsoluteLayout()
                {
                },
                Padding = new Extents(25, 25, 75, 25),
            };

            //Text style for the result text
            var paragraphTextStyle = new PropertyMap();
            paragraphTextStyle.Add("slant", new PropertyValue(FontSlantType.Normal.ToString()));
            paragraphTextStyle.Add("width", new PropertyValue(FontWidthType.Expanded.ToString()));
            paragraphTextStyle.Add("weight", new PropertyValue(FontWeightType.DemiBold.ToString()));

            resultText = new()
            {
                SizeWidth = width * 0.9f,
                SizeHeight = height * 0.7f,
                PointSize = 8,
                TextColor = new Color(1, 1, 1, 0.8f),
                BackgroundColor = Color.Transparent,
                FontStyle = paragraphTextStyle,
                MultiLine = true,
                Margin = new Extents(50, 50, 50, 50),
                PositionUsesPivotPoint = true,
                PivotPoint = Position.PivotPointCenter,
                ParentOrigin = Position.ParentOriginCenter,
                //Initial state
                Text = "",
            };
            textView.Add(resultText);
            textView.Add(loadingIndicator);

            //Add buttons to button view
            buttonView.Add(cancelButton);
            buttonView.Add(micButton);
            buttonView.Add(stopButton);

            //Add views to the main view
            mainView.Add(textView);
            mainView.Add(buttonView);

            //Adding Components to the content page
            AppBar = appBar;
            Content = mainView;
            BackgroundImage = directory + "/images/bg.png";
        }
        ~MainPage()
        {
            if (sttController != null)
            {
                sttController.ReleaseResources();
            }
        }
    }
}
