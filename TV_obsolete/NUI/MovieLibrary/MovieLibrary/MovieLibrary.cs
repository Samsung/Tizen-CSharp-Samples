/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;

/// <summary>
/// Namespace for movie library sample
/// </summary>
namespace MovieLibrary
{
    /// <summary>
    /// Main class for movie library sample
    /// </summary>
    class Program : NUIApplication
    {
        /// <summary>
        /// Main view rows count.
        /// </summary>
        private readonly uint rows = 4;

        /// <summary>
        /// Main view columns count.
        /// </summary>
        private readonly uint cols = 5;

        /// <summary>
        /// Helper for store current application resource directory path.
        /// </summary>
        private static string resUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource;

        /// <summary>
        /// TextLabel component which shows selected movie title on the top of the screen.
        /// </summary>
        private TextLabel title;

        /// <summary>
        /// Image views container.
        /// </summary>
        private TableView table;

        /// <summary>
        /// Dictionary which binds ImageView and movie title.
        /// </summary>
        private Dictionary<ImageView, string> movies;

        /// <summary>
        /// Titles storage.
        /// </summary>
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
            "Deep in eye",
            "Smart guy",
            "Last walk",
            "Tunnel",
            "Rider",
            "The Animal",
            "The End",
            "Illusion",
        };

        /// <summary>
        /// OnCreate callback implementation.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// View Components initialization code.
        /// </summary>
        void Initialize()
        {
            // When End key, Back key input, this application is terminated.
            Window.Instance.KeyEvent += OnKeyEvent;
            InitTitleLabel();
            InitTableView();
            InitTableViewCells();
        }

        /// <summary>
        /// Create and setup TextLabel component
        /// </summary>
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

        /// <summary>
        /// Create and setup TableView component
        /// </summary>
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

        /// <summary>
        /// Fill TableView component
        /// </summary>
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
                    {
                        FocusManager.Instance.SetCurrentFocusView(movieImage);
                    }
                }
            }
        }

        /// <summary>
        /// The method called when key pressed down
        /// </summary>
        /// <param name="sender">Key instance</param>
        /// <param name="e">Key's args</param>
        private void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "Escape" ||
                e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "BackSpace"))
            {
                // Terminate application.
                Exit();
            }
        }

        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">arguments of Main (entry point)</param>
        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
            app.Dispose();
        }
    }
}
