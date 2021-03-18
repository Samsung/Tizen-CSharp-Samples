using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;

namespace MovieLibrary
{
    class Program : NUIApplication
    {
        private readonly uint rows = 4;
        private readonly uint cols = 5;
        private static string resUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource;
        private TextLabel title;
        private TableView table;
        private Dictionary<ImageView, string> movies;
        private string[] movieTitles =
        {
            "Funny Dog",
            "Far far away",
            "Hometown",
            "Lights",
            "Water city",
            "Oblivion",
            "Rocks",
            "Up the sky",
            "Giraffe gang",
            "Two zebras more than one",
            "Eclipse",
            "The Dancer",
            "Depp in eye",
            "Smart guy",
            "Last walk",
            "Tunnel",
            "Rider",
            "The Animal",
            "The End",
            "Illusion",
        };

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            InitTitleLabel();
            InitTableView();
            InitTableViewCells();
        }

        private void InitTitleLabel()
        {
            title = new TextLabel(movieTitles[0])
            {
                TextColor = Color.White,
                PointSize = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                Padding = new Extents(0, 0, 10, 0)
            };
            Window.Instance.Add(title);
        }

        private void InitTableView()
        {
            table = new TableView(rows, cols)
            {
                BackgroundColor = new Color(.15f, .15f, .15f, .5f),
                SizeModeFactor = new Vector3(.9f, .85f, 1.0f),
                WidthResizePolicy = ResizePolicyType.SizeRelativeToParent,
                HeightResizePolicy = ResizePolicyType.SizeRelativeToParent,
                ParentOrigin = new Position(0.05f, 0.1f),
                CellPadding = new Vector2(30f, 10f),
                Focusable = true,
            };
            Window.Instance.Add(table);
        }

        private void InitTableViewCells()
        {
            movies = new Dictionary<ImageView, string>();

            uint movieNumber = 1;
            for (uint row = 0; row < rows; ++row)
            {
                for (uint col = 0; col < cols; ++col, ++movieNumber)
                {
                    ImageView movieImage = new ImageView(resUrl + "movie" + movieNumber + ".jpg")
                    {
                        WidthResizePolicy = ResizePolicyType.SizeRelativeToParent,
                        HeightResizePolicy = ResizePolicyType.SizeRelativeToParent,
                        Focusable = true,
                        Opacity = 0.8f
                    };
                    table.AddChild(movieImage, new TableView.CellPosition(row, col));
                    movies[movieImage] = movieTitles[movieNumber - 1];

                    movieImage.FocusGained += (obj, e) => 
                    { 
                        movieImage.Opacity = 1.0f;
                        title.Text = movies[movieImage];
                    };
                    movieImage.FocusLost += (obj, e) => { movieImage.Opacity = .8f; };

                    if (row == 0 && col == 0)
                        FocusManager.Instance.SetCurrentFocusView(movieImage);
                }
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
            app.Dispose();
        }
    }
}
