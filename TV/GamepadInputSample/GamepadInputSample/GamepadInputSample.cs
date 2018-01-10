using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.TV.Accessory;


namespace GamepadInputSample
{
    class Program : NUIApplication
    {
        TextLabel gamepadInfo1, gamepadInfo2;
        Timer timer1;

        GamePadState[] gamePadState, gamePadOldState;
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.BackgroundColor = Color.Black;
            Window.Instance.Title = "Gamepad Test App";

            gamePadState = new GamePadState[2];
            gamePadOldState = new GamePadState[2];
            timer1 = new Timer(20);
            timer1.Interval = 20;


            gamepadInfo1 = new TextLabel("Gamepad 1");
            gamepadInfo1.HorizontalAlignment = HorizontalAlignment.Begin;
            gamepadInfo1.VerticalAlignment = VerticalAlignment.Top;
            gamepadInfo1.MultiLine = true;
            gamepadInfo1.PointSize = 40.0f;
            gamepadInfo1.HeightResizePolicy = ResizePolicyType.FillToParent;
            gamepadInfo1.WidthResizePolicy = ResizePolicyType.FillToParent;
            Window.Instance.GetDefaultLayer().Add(gamepadInfo1);


            gamepadInfo2 = new TextLabel("Gamepad 2");
            gamepadInfo2.HorizontalAlignment = HorizontalAlignment.Begin;
            gamepadInfo2.VerticalAlignment = VerticalAlignment.Center;
            gamepadInfo2.MultiLine = true;
            gamepadInfo2.PointSize = 40.0f;
            gamepadInfo2.HeightResizePolicy = ResizePolicyType.FillToParent;
            gamepadInfo2.WidthResizePolicy = ResizePolicyType.FillToParent;
            Window.Instance.GetDefaultLayer().Add(gamepadInfo2);

            for(int i = 0; i < 2; i++)
            {
                try
                {
                    gamePadOldState[i] = GamePad.GetState((PlayerIndex)i);
                }
                catch( Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }

                switch (i)
                {
                    case 0:
                        if (GamePad.GetState((PlayerIndex)i).IsConnected)
                        {
                            gamepadInfo1.TextColor = Color.Green;
                            gamepadInfo1.Text += " is connected";
                        }
                        else
                        {
                            gamepadInfo1.TextColor = Color.Red;
                            gamepadInfo1.Text += " is disconnected";
                        }
                        break;
                    case 1:
                        if (GamePad.GetState((PlayerIndex)i).IsConnected)
                        {
                            gamepadInfo2.TextColor = Color.Green;
                            gamepadInfo2.Text += " is connected";
                        }
                        else
                        {
                            gamepadInfo2.TextColor = Color.Red;
                            gamepadInfo2.Text += " is disconnected";
                        }
                        break;

                }

            }

            //Start the loop
            timer1.Tick += Timer_Tick;
            int MaxGamepads = GamePad.MaximumGamePadCount;

        }

        private bool Timer_Tick(object source, Timer.TickEventArgs e)
        {
            TextLabel gamepadInfo = new TextLabel();

            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {

                switch(i)
                {
                    case 0:
                        gamepadInfo = gamepadInfo1;
                        break;
                    case 1:
                        gamepadInfo = gamepadInfo2;
                        break;
                }

                gamePadState[i] = GamePad.GetState((PlayerIndex)i);
                if( gamePadOldState[i].IsConnected != gamePadState[i].IsConnected)
                {
                    gamePadOldState[i] = gamePadState[i];
                    if( gamePadState[i].IsConnected)
                    {
                        gamepadInfo.TextColor = Color.Green;
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected";
                    }
                    else
                    {
                        gamepadInfo.TextColor = Color.Red;
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is disconnected";
                    }
                }
                if (gamePadState[i].IsConnected)
                {
                    
                    if (gamePadState[i].Buttons.A == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "A is pressed";
                    }
                    if (gamePadState[i].Buttons.B == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "B is pressed";
                    }
                    if (gamePadState[i].Buttons.X == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "X is pressed";
                    }
                    if (gamePadState[i].Buttons.Y == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "Y is pressed";
                    }
                    if (gamePadState[i].Buttons.Back == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "Back is pressed";
                        Exit();
                    }
                    if (gamePadState[i].Buttons.BigButton == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "BigButton is pressed";
                    }
                    if (gamePadState[i].Buttons.LeftShoulder == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "LeftShoulder is pressed";
                    }
                    if (gamePadState[i].Buttons.RightShoulder == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "RightShoulder is pressed";
                    }
                    if (gamePadState[i].Buttons.LeftStick == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "LeftStick is pressed";
                    }
                    if (gamePadState[i].Buttons.RightStick == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "RightStick is pressed";
                    }
                    if (gamePadState[i].Buttons.Start == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "Start is pressed";
                    }
                    if (gamePadState[i].Triggers.Left > 0.0)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "Left trigger = " + gamePadState[i].Triggers.Left.ToString();
                        GamePad.SetVibration((PlayerIndex)i, gamePadState[i].Triggers.Left, gamePadState[i].Triggers.Left * 100);
                    }
                    if (gamePadState[i].Triggers.Right > 0.0)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "Right trigger = " + gamePadState[i].Triggers.Right.ToString();
                        GamePad.SetVibration((PlayerIndex)i, 0.0f, 0.0f);
                    }
                    if (gamePadState[i].ThumbSticks.Left.X != 0 || gamePadState[i].ThumbSticks.Left.Y != 0)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "Left thumb X = " + gamePadState[i].ThumbSticks.Left.X.ToString() + " Left thumb Y = " + gamePadState[i].ThumbSticks.Left.Y.ToString();
                    }
                    if (gamePadState[i].ThumbSticks.Right.X != 0 || gamePadState[i].ThumbSticks.Right.Y != 0)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "Right thumb X = " + gamePadState[i].ThumbSticks.Right.X.ToString() + " Right thumb Y = " + gamePadState[i].ThumbSticks.Right.Y.ToString();
                    }
                    if (gamePadState[i].DPad.Down == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "DPad Down is pressed";
                    }
                    if (gamePadState[i].DPad.Up == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "DPad Up is pressed";
                    }
                    if (gamePadState[i].DPad.Right == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "DPad Right is pressed";
                    }
                    if (gamePadState[i].DPad.Left == ButtonState.Pressed)
                    {
                        gamepadInfo.Text = "Gamepad " + (i + 1).ToString() + " is connected\n";
                        gamepadInfo.Text += "DPad Left is pressed";
                    }
                }

            }
            return true;
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
