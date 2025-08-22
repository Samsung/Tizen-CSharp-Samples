/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.MachineLearning.Train;
using Tizen.MachineLearning.Inference;
using Tizen;

namespace MachineLearningTrainSample
{
    class Program : NUIApplication
    {
        /// <summary>
        /// Main Application Window instance.
        /// </summary>
        private Window ApplicationWindow;

        /// <summary>
        /// Button names array.
        /// </summary>
        private readonly string[] buttonNames = { "CIFAR", "MNIST" };

        /// <summary>
        /// OnCreate Event Handler. First function called after Application constructor.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// On Key Pressed Event Handler. Used to exit application when back button was pressed.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                switch (e.Key.KeyPressedName)
                {
                    case "Escape":
                    case "Back":
                    case "XF86Back": //Handle back key for emulator
                        {
                            Exit();
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Function initializes application UI.
        /// </summary>
        void Initialize()
        {
            //Save the application window instance.
            ApplicationWindow = Window.Instance;
            ApplicationWindow.BackgroundColor = Color.White;

            //Setup event handlers.
            ApplicationWindow.KeyEvent += OnKeyEvent;

            //Create main application view.
            View mainView = new View();

            //Setup linear layout for components stored in main view.
            LinearLayout mainLayout = new LinearLayout()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                LinearOrientation = LinearLayout.Orientation.Vertical,
                CellPadding = new Size2D(0, 10),
                Padding = new Extents(10, 10, 10, 10),
            };
            mainView.WidthResizePolicy = ResizePolicyType.FillToParent;
            mainView.HeightResizePolicy = ResizePolicyType.FillToParent;
            mainView.Layout = mainLayout;
            ApplicationWindow.Add(mainView);

            //Create button container layout.
            View buttonContainer = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = Color.Gainsboro,
                Weight = 0.5f,
            };
            LinearLayout buttonLayout = new LinearLayout()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                LinearOrientation = LinearLayout.Orientation.Vertical,
                CellPadding = new Size2D(0, 10),
            };
            buttonContainer.Layout = buttonLayout;
            mainView.Add(buttonContainer);

            //Create buttons and push them into the button container.
            for (int i = 0; i < buttonNames.Length; ++i)
            {
                Button btn = new Button
                {
                    Size2D = new Size2D(300, 100),
                    TextColor = Color.White,
                    Text = buttonNames[i],
                };
                btn.Clicked += OnClicked;
                buttonContainer.Add(btn);
            }
        }

        void MNIST() {
            string iniPath = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "MNIST/mnist.ini";
            string dataPath = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "MNIST/mnist_trainingSet.dat";
 
            Dataset dataset = new Dataset();
            dataset.AddFile(NNTrainerDatasetMode.Train, dataPath);
            dataset.AddFile(NNTrainerDatasetMode.Valid, dataPath);
 
            Model model = new Model();
            model.Load(iniPath, NNTrainerModelFormat.IniWithBin);
 
            Optimizer optimizer = new Optimizer(NNTrainerOptimizerType.Adam);
            model.SetOptimizer(optimizer);
            model.Compile("epochs=1");
 
            model.SetDataset(dataset);
            model.Run();
 
            model.Dispose();
        }

        void CIFAR() {
            string confPath = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "CIFAR/model.ini";
            string filePath = Tizen.Applications.Application.Current.DirectoryInfo.Data + "cifar_model.bin";
            string savePathParam = "save_path="+filePath;
 
            Model model = new Model(confPath);
            model.Compile();
            model.Run(savePathParam);
            model.Dispose();
        }

        /// <summary>
        /// Button Clicked event handler.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameters</param>
        private void OnClicked(object sender, ClickedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Text)
            {
                case "CIFAR":
                    CIFAR();
                    break;
                case "MNIST":
                    MNIST();
                    break;
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
