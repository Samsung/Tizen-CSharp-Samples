using System;
using Tizen.Applications;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;
using Tizen.NUI.Components;
using TextReader.TizenMobile.Services;

namespace TextReader_UI.views
{
    class MainPage : ContentPage
    {
        //Flags for play , repeat , repeat all status. 
        bool playButtonFlag = false;
        bool repeatAllFlag = false;
        bool repeatAllUnitFlag = false;

        //Getting the path for images.
        public string directory = Application.Current.DirectoryInfo.Resource;

        //Storing the selected paragraph id.
        public int selectedParagraphId = -1;

        //Creating object of the TextToSpeechService Service.
        public TextToSpeechService textToSpeechService;

        //Setting consts.
        public const string backgroundColor = "#00a9bf";
        public const string resetColor = "#99CAD2";

        public int playCounter = 0;

        private readonly string[] _paragraphs =
        {
            "Welcome to Tizen .NET!",

            "Tizen .NET is an exciting new way to develop applications for the Tizen operating" +
                " system, running on 50 million Samsung devices, including TVs, wearables," +
                " mobile, and many other IoT devices around the world.",

            "The existing Tizen frameworks are either C-based with no advantages of a managed" +
                " runtime, or HTML5-based with fewer features and lower performance than" +
                " the C-based solution.",

            "With Tizen .NET, you can use the C# programming language and the Common Language" +
                " Infrastructure standards and benefit from a managed runtime for faster" +
                " application development, and efficient, secure code execution."
        };

        //Creating TextLabel array for paragraphs.
        private TextLabel [] paragraphLabel =new TextLabel [4];


        //Play button for controlling the Service .
        private Button playButton;

        //Creating Images to show when paragraph is selected or being played.
        ImageView[] activeParagrphUnitLineImg = new ImageView[4];
  
        //Styling the paragraph when the its finished.
        private Action<int> FinishEffect;

        //Styling the paragraph when the its started.

        private Action<int> StartEffect;
        public MainPage(float height, float width){

            StartEffect = delegate(int currentParagraph)
            { 
                for(int i = 0; i < 4; i++){
                    if(i == currentParagraph){
                        paragraphLabel[i].TextColor = new Color(backgroundColor);
                        activeParagrphUnitLineImg[i].Show();
                    }
                    else
                    {
                        paragraphLabel[i].TextColor = new Color(0, 0, 0, 0.6f);
                        activeParagrphUnitLineImg[i].Hide();
                    }
                }
            };

            FinishEffect = delegate(int currentParagraph)
            {
                for(int i = 0; i <= 4; i++)
                {
                    if(currentParagraph == 4 && i==4)
                    {
                        paragraphLabel[i-1].TextColor = new Color(0, 0, 0, 0.6f);
                        playButton.BackgroundImage = Application.Current.DirectoryInfo.Resource + "images/play.png";
                        activeParagrphUnitLineImg[i-1].Hide();
                        textToSpeechService.Stop();
                        selectedParagraphId = -1;
                        playButtonFlag = false;
                        return;
                    }
                    else if(i == currentParagraph)
                    {
                        paragraphLabel[i].TextColor = new Color(0, 0, 0, 0.6f);
                        activeParagrphUnitLineImg[i].Hide();
                    }
                }
                selectedParagraphId = -1;
            };
        
            textToSpeechService = new TextToSpeechService(OnStart: StartEffect, OnFinish : FinishEffect);

            //Initializing the TTS object.
            textToSpeechService.Init();

            //Styling the text for the appbar.
            var mainAppBarTextStyle = new PropertyMap();
            mainAppBarTextStyle.Add("slant", new PropertyValue(FontSlantType.Normal.ToString()));
            mainAppBarTextStyle.Add("width", new PropertyValue(FontWidthType.Expanded.ToString()));
            mainAppBarTextStyle.Add("weight", new PropertyValue(FontWeightType.DemiBold.ToString()));

            //Button view to hold the buttons horizontally
            View buttonView = new View()
            {
                SizeWidth = width,
                SizeHeight = height * 0.15f,
                BackgroundColor = Color.Transparent,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    CellPadding = new Size2D(20, 10),
                },
               
            };

            //Create button
             playButton = new Button()
            {
                SizeWidth = 125,
                SizeHeight = 125,
                BackgroundImage = directory + "/images/play.png",
            };

            //Cancel button 
            Button backwardButton = new Button()
            {
                SizeWidth = 125,
                SizeHeight = 125,
                BackgroundImage = directory + "/images/back.png",
            };

            //Stop button
            Button forwardButton = new Button()
            {
                SizeWidth = 125,
                SizeHeight = 125,
                BackgroundImage = directory + "/images/forward.png",
            };

            //RpeatAll button
            Button repeatAll = new Button()
            {
                SizeWidth = 125,
                SizeHeight = 125,
                BackgroundImage = directory + "/images/repeat_all.png",
            };

            //RpeatCurrentUnit button
            Button repeatAllUnit = new Button()
            {
                SizeWidth = 125,
                SizeHeight = 125,
                BackgroundImage = directory + "/images/repeat_unit.png",
            };

            //Accept reset button
            Button okButton = new Button() 
            { 
              Text= "OK",      
            };

            //Deny reset button
            Button cancelButton = new Button()
            { 
                Text= "Cancel",
            };

            //Reset button
            Button reseButton = new Button()
            {
                SizeWidth = width * 0.3f,
                BackgroundColor = Color.Transparent,
                PointSize = 8,
                TextColor = new Color(resetColor),
                Text = "RESET",
                IsEnabled=false,
            };

            //Creating the AppBar
            AppBar appBar = new AppBar()
            {
                SizeHeight = height*0.1f,
                SizeWidth = width,
                BackgroundColor = Color.Transparent,
                ActionContent = reseButton,
                NavigationContent =new View() { SizeWidth = width * 0.3f},
                Margin = new Extents(200,0,0,0),
                TitleContent = new TextLabel()
                {   
                    BackgroundColor = Color.Transparent,
                    SizeWidth = width * 0.4f,
                    Text = "Text Reader",
                    PointSize = 10,
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontStyle = mainAppBarTextStyle,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                },              
            };

            //Creating mainView
            View mainView = new View()
            {
                LayoutDirection = ViewLayoutDirectionType.LTR,
                SizeHeight = height - (height / 12),
                SizeWidth = width,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
            };

            //Text view to hold the paragraph text
            View textView = new View()
            {
                SizeWidth = width,
                SizeHeight = height * 0.75f,
                BackgroundColor = Color.White,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
                Padding = new Extents(25, 25, 75, 0),
            };
 
            //Creating TextLabel for  paragraph.
            for (int i = 0; i < 4; i++)
            {
                activeParagrphUnitLineImg[i] = new ImageView();
                activeParagrphUnitLineImg[i].Hide();
                activeParagrphUnitLineImg[i].SetImage(directory + "/images/active_unit_line.png");
                paragraphLabel[i]= new TextLabel()
                {
                    SizeWidth = width,
                    SizeHeight= (float)(height *0.1),
                    PointSize = 6,
                    TextColor = new Color(0, 0, 0, 0.6f),
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    BackgroundColor = Color.Transparent,
                    MultiLine = true,
                };
            }

            //Setting height for the first paragraph.
            paragraphLabel[0].SizeHeight = 50f;

            //Paragraph and Button Effects.
            paragraphLabel[0].TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if ((e.Touch.GetState(0) == PointStateType.Started) && selectedParagraphId != 0 && (textToSpeechService.GetCurrentParagraph() != 0))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                        {
                            paragraphLabel[i].TextColor = new Color(backgroundColor);
                            textToSpeechService.SetCurrentUnit(i);
                            activeParagrphUnitLineImg[i].Show();
                        }
                        else
                        {
                            paragraphLabel[i].TextColor = new Color(0, 0, 0, 0.6f);
                            activeParagrphUnitLineImg[i].Hide();
                        }
                    }
                    selectedParagraphId = 0;
                }
                return true;
            };

            paragraphLabel[1].TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if ((e.Touch.GetState(0) == PointStateType.Started) && selectedParagraphId != 1 && (textToSpeechService.GetCurrentParagraph() != 1))
                {              
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 1)
                        {
                            paragraphLabel[i].TextColor = new Color(backgroundColor);
                            textToSpeechService.SetCurrentUnit(i);
                            activeParagrphUnitLineImg[i].Show();
                        }
                        else
                        {
                            paragraphLabel[i].TextColor = new Color(0, 0, 0, 0.6f);
                            activeParagrphUnitLineImg[i].Hide();
                        }
                    }
                    selectedParagraphId = 1;
                }
                return true;
            };

            paragraphLabel[2].TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if ((e.Touch.GetState(0) == PointStateType.Started) && (selectedParagraphId != 2) && (textToSpeechService.GetCurrentParagraph()!=2))
                {                
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 2)
                        {
                            paragraphLabel[i].TextColor = new Color(backgroundColor);
                            textToSpeechService.SetCurrentUnit(i);
                            activeParagrphUnitLineImg[i].Show();
                        }
                        else
                        {
                            paragraphLabel[i].TextColor = new Color(0, 0, 0, 0.6f);
                            activeParagrphUnitLineImg[i].Hide();
                        }
                    }
                    selectedParagraphId = 2;
                }
                return true;
            };

            paragraphLabel[3].TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if ((e.Touch.GetState(0) == PointStateType.Started)&&selectedParagraphId!=3 )
                {               
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 3)
                        {
                            paragraphLabel[i].TextColor = new Color(backgroundColor);
                            textToSpeechService.SetCurrentUnit(i);
                            activeParagrphUnitLineImg[i].Show();
                        }
                        else
                        {
                            paragraphLabel[i].TextColor = new Color(0, 0, 0, 0.6f);
                            activeParagrphUnitLineImg[i].Hide();
                        }
                    }
                    selectedParagraphId = 3;
                }
                return true;
            };

            playButtonFlag = false;

            playButton.TouchEvent += (object sender, TouchEventArgs e) =>
            {
                reseButton.IsEnabled = true;
                reseButton.TextColor = Color.White;
                if (e.Touch.GetState(0) == PointStateType.Started)
                {
                    //Change icon to pressed
                    if (!playButtonFlag)
                    {
                        playButton.BackgroundImage = directory + "images/play_hover.png";
                    }
                    else
                    {
                        playButton.BackgroundImage = directory + "images/pause_hover.png";
                    }
                }
                if (e.Touch.GetState(0) == PointStateType.Leave)
                {
                    //Return the default icon
                    if (!playButtonFlag)
                    {
                        playButtonFlag = false;
                        playButton.BackgroundImage = directory + "images/play.png";
                        playCounter++;
                        textToSpeechService.Pause();
                    }
                    else
                    {                     
                        playButtonFlag = true;
                        playButton.BackgroundImage = directory + "images/pause.png";

                        if (textToSpeechService.GetCurrentParagraph() == -1)
                        {
                            textToSpeechService.SetCurrentUnit(0);
                            textToSpeechService.Play();
                        }
                        else
                        {
                            textToSpeechService.Play();
                        }
                    }
                }
                if (e.Touch.GetState(0) == PointStateType.Finished)
                {
                    //Return the default icon
                    if (!playButtonFlag) {
                        playButtonFlag = true;
                        playButton.BackgroundImage = directory + "images/pause.png";

                        if (textToSpeechService.GetCurrentParagraph() == -1)
                        {
                            textToSpeechService.SetCurrentUnit(0);
                            textToSpeechService.Play();
                        }
                        else
                        {
                            textToSpeechService.Play();
                        }
                    }
                    else
                    {
                        playButtonFlag = false;
                        playButton.BackgroundImage = directory + "images/play.png";
                        playCounter++;
                        textToSpeechService.Pause();
                    }
                }
                return true;
            };
     
            backwardButton.TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Started)
                {
                    //Change icon to pressed
                    backwardButton.BackgroundImage = directory + "images/back_hover.png";
                }
                if (e.Touch.GetState(0) == PointStateType.Leave)
                {   //Return the default icon
                    backwardButton.BackgroundImage = directory + "images/back.png";
                }
                if (e.Touch.GetState(0) == PointStateType.Finished)
                {   //Return the default icon
                    backwardButton.BackgroundImage = directory + "images/back.png";
                    textToSpeechService.SetRepeat(4);
                }
                return true;
            };
         
            forwardButton.TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Started)
                {
                    //Change icon to pressed
                    forwardButton.BackgroundImage = directory + "images/forward_hover.png";
                }
            
                if (e.Touch.GetState(0) == PointStateType.Finished)
                {
                    //Return the default icon
                    forwardButton.BackgroundImage = directory + "images/forward.png";
                    textToSpeechService.SetRepeat(3);
                }
                return true;
            };

            repeatAll.TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Started)
                {
                    //Change icon to pressed
                    repeatAll.BackgroundImage = directory + "images/repeat_all_hover.png";
                }
                
                if (e.Touch.GetState(0) == PointStateType.Finished)
                {
                    if (repeatAllFlag)
                    {
                        repeatAllFlag = false;
                        repeatAll.BackgroundImage = directory + "images/repeat_all.png";
                        textToSpeechService.SetRepeat(-1);
                    }
                    else
                    {
                        repeatAllFlag = true;
                        repeatAll.BackgroundImage = directory + "images/repeat_all_hover.png";
                        textToSpeechService.SetRepeat(1);
                    }
                }
                return true;
            };

            reseButton.IsEnabled = false;
            reseButton.TextColor = new Color(resetColor);

            reseButton.Clicked += (sender, e) =>
            {            
                int counter = 0;
                bool playingFlag = false;
                if (textToSpeechService.Playing)
                {
                    textToSpeechService.Pause();         
                    playButton.BackgroundImage = directory + "images/play.png";
                    playingFlag = true;
                }
                
                DialogPage.ShowAlertDialog("Reset Confirmation", " Are you sure you want to reset this app to default screen?", cancelButton, okButton);
              
                cancelButton.Clicked += (o, e) =>
                {
                    if (counter == 0)
                    {
                        counter++;
                        try
                        {
                            if (!textToSpeechService.Playing && playingFlag)
                            {      playButton.BackgroundImage = directory + "images/pause.png";
                                playingFlag = false;
                                textToSpeechService.Play();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            playButton.BackgroundImage = directory + "images/play.png";
                            textToSpeechService.Pause();
                        }
                        Navigator.Pop();
                    }
                };

                okButton.Clicked += (o, e) => 
                {
                    for (int i = 0; i < 4; i++)
                    {
                        paragraphLabel[i].TextColor = new Color(0, 0, 0, 0.6f);
                        activeParagrphUnitLineImg[i].Hide();
                    }

                    textToSpeechService.Reset();
                    selectedParagraphId = -1;
                    playButtonFlag = false;
                    repeatAllFlag = false;
                    repeatAllUnitFlag = false;
                    reseButton.IsEnabled = false;
                    reseButton.TextColor = new Color(resetColor);
                    repeatAll.BackgroundImage = directory + "images/repeat_all.png";
                    backwardButton.BackgroundImage = directory + "images/back.png";
                    forwardButton.BackgroundImage = directory + "images/forward.png";
                    repeatAllUnitFlag = false; repeatAllUnit.BackgroundImage = directory + "images/repeat_unit.png";
                    if (counter == 0)
                    {
                        counter++;   Navigator.Pop();
                    }
                };


            };

            repeatAllUnit.TouchEvent += (object sender, TouchEventArgs e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Started)
                {
                    //Change icon to pressed
                    repeatAllUnit.BackgroundImage = directory + "images/repeat_unit_hover.png";
                }
     
                if (e.Touch.GetState(0) == PointStateType.Finished)
                {
                    //Return the default icon
                    if (repeatAllUnitFlag)
                    {
                        repeatAllUnitFlag = false; repeatAllUnit.BackgroundImage = directory + "images/repeat_unit.png";
                        textToSpeechService.SetRepeat(-2);
                    }
                    else
                    {
                        repeatAllUnit.BackgroundImage = directory + "images/repeat_unit_hover.png";
                        repeatAllUnitFlag = true;
                        textToSpeechService.SetRepeat(2);
                    }
                }
                return true;
            };

            //Setting paragraph in textlabel and adding them to the textview.
            for (int i = 0; i < 4; i++)
            {              
                paragraphLabel[i].Text += _paragraphs[i];
                textView.Add(paragraphLabel[i]);
                textView.Add(activeParagrphUnitLineImg[i]);
            }

            //Add buttons to button view
            buttonView.Add(repeatAllUnit);
            buttonView.Add(backwardButton);
            buttonView.Add(playButton);
            buttonView.Add(forwardButton);
            buttonView.Add(repeatAll);

            //Add views to the main view
            mainView.Add(textView);
            mainView.Add(buttonView);

            //Adding Components to the page
            AppBar = appBar;
            Content = mainView;
            BackgroundColor = new Color(backgroundColor);

        }
        ~MainPage() {
            textToSpeechService.Dispose();
        }
    }
}
