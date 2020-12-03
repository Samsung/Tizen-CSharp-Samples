using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace VideoViewSample
{
    class Program : NUIApplication
    {
        private readonly string[] btnNames = { "play", "pause", "stop", "forward", "backward" };
        private VideoView player;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.BackgroundColor = Color.White;

            player = new VideoView()
            {
                HeightResizePolicy = ResizePolicyType.SizeRelativeToParent,
                WidthResizePolicy = ResizePolicyType.SizeRelativeToParent,
                SizeModeFactor = new Vector3(1.0f, 0.5f, 1.0f),
                ResourceUrl = DirectoryInfo.Resource + "sample.3gp",
            };
            player.Play();
            Window.Instance.Add(player);

            for (int i = 0; i < btnNames.Length; ++i)
            {
                int buttonPosX = (int)player.SizeHeight + 50 + i * 102;
                Button btn = new Button
                {
                    ParentOrigin = ParentOrigin.Center,
                    Size2D = new Size2D(300, 100),
                    Position2D = new Position2D(-150, buttonPosX),
                    BackgroundColor = new Color("#00bfff"),
                    TextColor = Color.White,
                    Text = btnNames[i],
                };
                btn.Clicked += OnClicked;
                Window.Instance.Add(btn);
            }
        }
        private void OnClicked(object sender, ClickedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Text)
            {
                case "play":
                    player.Play();
                    break;
                case "pause":
                    player.Pause();
                    break;
                case "stop":
                    player.Stop();
                    break;
                case "forward":
                    player.Forward(1000); // +1 sec
                    break;
                case "backward":
                    player.Backward(1000); // -1 sec
                    break;
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
