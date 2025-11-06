/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Calculator.controls;
using Calculator.ViewModels;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding;
using Tizen.NUI.Components;

namespace Calculator.Views
{
    class CalculatorMainPageLandscape : ContentPage
    {
        private PropertyNotification _positionNotification;
        private TextLabel expressionLabel;
        private CustomScrollView expressionScrollView;
        private float originalExpressionLabelHeight;
        private Timer dismissTimer;

        public CalculatorMainPageLandscape() : base()
        {
            Initialize();
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                if (_positionNotification != null)
                {
                    _positionNotification.Notified -= OnPositionNotified;
                    expressionLabel.RemovePropertyNotification(_positionNotification);
                    _positionNotification.Dispose();
                    _positionNotification = null;
                }

                if (dismissTimer != null && dismissTimer.IsRunning())
                {
                    dismissTimer.Stop();
                }
            }

            base.Dispose(type);
        }

        private void Initialize()
        {
            WidthSpecification = LayoutParamPolicies.MatchParent;
            HeightSpecification = LayoutParamPolicies.MatchParent;

            AppBar = null;

            var rootContent = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundImage = Application.Current.DirectoryInfo.Resource + "bg_l.png",
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center,
                },
            };
            Content = rootContent;

            CreateExpressionView();

            CreatePanelView();

            MainPageViewModel.Instance.ErrorOccured += OnErrorOccured;
            dismissTimer = new Timer(1500);
            dismissTimer.Tick += (s, e) =>
            {
                if (Disposed)
                    return false;

                if (Window.Default.GetDefaultNavigator().PageCount > 1)
                {
                    Window.Default.GetDefaultNavigator().Pop();
                }
                return false;
            };
        }

        private void OnErrorOccured(object s, MainPageViewModel.ErrorEventArgs e)
        {
            var messageLabel = new TextLabel(StyleResources.AlertTextLabelStyle)
            {
                Text = e.Message,
            };
            DialogPage.ShowDialog(messageLabel);

            if (dismissTimer.IsRunning())
            {
                dismissTimer.Stop();
            }
            dismissTimer.Start();
        }

        private void CreateExpressionView()
        {
            var winSize = Window.Default.Size;
            var expressionViewHeight = winSize.Height * 35 / 100;
            var expressionViewPadding = new Extents(32, 18, 32, 28);

            View expressionView = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = expressionViewHeight,
                Padding = expressionViewPadding,
                BackgroundColor = new Color("#FAFAFA"),
                Layout = new GridLayout()
                {
                    Rows = 4,
                    Columns = 2,
                },
            };
            Content.Add(expressionView);

            // grid width.
            var gridWidth = winSize.Width - (expressionViewPadding.Start + expressionViewPadding.End);
            var gridHeight = expressionViewHeight - (expressionViewPadding.Top + expressionViewPadding.Bottom);

            // for binding
            var session = new BindingSession<MainPageViewModel>();

            // grid row 1 related to that in xaml file.
            var rowHeight0 = gridHeight * 452 / 1000;
            originalExpressionLabelHeight = rowHeight0;

            var radLabel00 = new RadTextLabel(StyleResources.SumTextLabelStyle)
            {
                Size = new Size(60, rowHeight0), // originally width is 60
            };
            radLabel00.BindingContextChanged += (sender, e) =>
            {
                if (radLabel00.BindingContext is MainPageViewModel model)
                {
                    session.ViewModel = model;
                }
            };
            radLabel00.SetBinding(session, RadTextLabelBindings.IsVisibleProperty, "IsRadUsing");
            radLabel00.BindingContext = MainPageViewModel.Instance;
            expressionView.Add(radLabel00);

            expressionScrollView = new CustomScrollView()
            {
                WidthSpecification = gridWidth - 60,
                HeightSpecification = rowHeight0,
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.End,
                },
            };
            expressionView.Add(expressionScrollView);

            expressionLabel = new TextLabel(StyleResources.InputTextLabelStyle)
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                EnableMarkup = true,
                MultiLine = true,
            };
            expressionLabel.BindingContextChanged += (sender, e) =>
            {
                if (expressionLabel.BindingContext is MainPageViewModel model)
                {
                    session.ViewModel = model;
                }
            };
            expressionLabel.SetBinding(session, TextLabelBindings.TextProperty, "ExpressionText");
            expressionLabel.BindingContext = MainPageViewModel.Instance;

            // monitor sizeheight changed event for scrolling.
            _positionNotification = expressionLabel.AddPropertyNotification("SizeHeight", PropertyCondition.Step(1.0f));
            _positionNotification.Notified += OnPositionNotified;

            expressionScrollView.Add(expressionLabel);

            // grid row 2 related to that in xaml file.
            var rowHeight1 = gridHeight * 270 / 1000;
            var placeholderView10 = new View()
            {
                Size = new Size(60, rowHeight1),
            };
            expressionView.Add(placeholderView10);

            var resultLabel = new TextLabel(StyleResources.SumTextLabelStyle)
            {
                Size = new Size(gridWidth - 60, rowHeight1),
                EnableMarkup = true,
            };
            resultLabel.BindingContextChanged += (sender, e) =>
            {
                if (resultLabel.BindingContext is MainPageViewModel model)
                {
                    session.ViewModel = model;
                }
            };
            resultLabel.SetBinding(session, TextLabelBindings.TextColorProperty, "ResultColor");
            resultLabel.SetBinding(session, TextLabelBindings.TextProperty, "ResultText");
            resultLabel.BindingContext = MainPageViewModel.Instance;
            expressionView.Add(resultLabel);

            // grid row 3 related to that in xaml file.
            var rowHeight2 = gridHeight * 67 / 1000;
            var placeholderView20 = new View()
            {
                Size = new Size(60, rowHeight2),
            };
            expressionView.Add(placeholderView20);

            var placeholderView21 = new View()
            {
                Size = new Size(gridWidth - 60, rowHeight2),
            };
            expressionView.Add(placeholderView21);

            // grid row 4 related to that in xaml file.
            var rowHeight3 = gridHeight * 211 / 1000;
            var placeholderView30 = new View()
            {
                Size = new Size(60, rowHeight3),
            };
            expressionView.Add(placeholderView30);

            var backButtonView31 = new View()
            {
                Size = new Size(gridWidth - 60, rowHeight3),
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.End,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            expressionView.Add(backButtonView31);

            var backButton = new CommandButton(StyleResources.BackButtonStyle);
            backButton.BindingContextChanged += (sender, e) =>
            {
                if (backButton.BindingContext is MainPageViewModel model)
                {
                    session.ViewModel = model;
                }
            };
            backButton.SetBinding(session, CommandButtonBindings.CommandProperty, "RemoveLast");
            backButton.SetBinding(session, CommandButtonBindings.LongTapCommandProperty, "Clear");
            backButton.BindingContext = MainPageViewModel.Instance;
            backButtonView31.Add(backButton);
        }

        private void OnPositionNotified(object source, PropertyNotification.NotifyEventArgs args)
        {
            expressionScrollView.ScrollTo(expressionLabel.SizeHeight - originalExpressionLabelHeight, false);
        }

        private void CreatePanelView()
        {
            var winSize = Window.Default.Size;
            var panelViewHeight = winSize.Height * 65 / 100;

            View panelView = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = panelViewHeight,
                Layout = new GridLayout()
                {
                    Rows = 5,
                    Columns = 7,
                },
            };
            Content.Add(panelView);

            var elementWidth = winSize.Width / 7;
            var elementHeight = panelViewHeight / 5;
            var elementSize = new Size(elementWidth, elementHeight);

            CreatePanelRow1(panelView, elementSize);
            CreatePanelRow2(panelView, elementSize);
            CreatePanelRow3(panelView, elementSize);
            CreatePanelRow4(panelView, elementSize);
            CreatePanelRow5(panelView, elementSize);
        }

        private void CreatePanelRow1(View panelView, Size size)
        {
            var session = new BindingSession<MainPageViewModel>();

            var elementButton00 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1F000000"),
            };
            elementButton00.BindingContextChanged += (sender, e) =>
            {
                if (elementButton00.BindingContext is MainPageViewModel model)
                {
                    session.ViewModel = model;
                }
            };
            elementButton00.SetBinding(session, ImageViewBindings.ResourceUrlProperty, "DegreeMetricImageFileName");
            elementButton00.SetBinding(session, ElementButtonBindings.CommandProperty, "ChangeDegreeMetric");
            elementButton00.BindingContext = MainPageViewModel.Instance;
            panelView.Add(elementButton00);

            var elementButton01 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#0F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_09.png",
                CommandParameter = "F",
            };
            BindModelCommandProperty(elementButton01, session, "PressButton");
            panelView.Add(elementButton01);

            var elementButton02 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#00000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_10.png",
                CommandParameter = "Q",
            };
            BindModelCommandProperty(elementButton02, session, "PressButton");
            panelView.Add(elementButton02);

            var elementButton03 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2E000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_01.png",
            };
            BindModelCommandProperty(elementButton03, session, "Clear");
            panelView.Add(elementButton03);

            var elementButton04 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_02.png",
                CommandParameter = "(",
            };
            BindModelCommandProperty(elementButton04, session, "PressButton");
            panelView.Add(elementButton04);

            var elementButton05 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#0F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_03.png",
                CommandParameter = "%",
            };
            BindModelCommandProperty(elementButton05, session, "PressButton");
            panelView.Add(elementButton05);

            var elementButton06 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#5B000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_04.png",
                CommandParameter = "/",
            };
            BindModelCommandProperty(elementButton06, session, "PressButton");
            panelView.Add(elementButton06);
        }

        private void CreatePanelRow2(View panelView, Size size)
        {
            var session = new BindingSession<MainPageViewModel>();

            var elementButton10 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2E000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_12.png",
                CommandParameter = "S",
            };
            BindModelCommandProperty(elementButton10, session, "PressButton");
            panelView.Add(elementButton10);

            var elementButton11 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_13.png",
                CommandParameter = "C",
            };
            BindModelCommandProperty(elementButton11, session, "PressButton");
            panelView.Add(elementButton11);

            var elementButton12 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#0F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_14.png",
                CommandParameter = "T",
            };
            BindModelCommandProperty(elementButton12, session, "PressButton");
            panelView.Add(elementButton12);

            var elementButton13 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2EFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_07.png",
                CommandParameter = "7",
            };
            BindModelCommandProperty(elementButton13, session, "PressButton");
            panelView.Add(elementButton13);

            var elementButton14 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#3DFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_08.png",
                CommandParameter = "8",
            };
            BindModelCommandProperty(elementButton14, session, "PressButton");
            panelView.Add(elementButton14);

            var elementButton15 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#4CFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_09.png",
                CommandParameter = "9",
            };
            BindModelCommandProperty(elementButton15, session, "PressButton");
            panelView.Add(elementButton15);

            var elementButton16 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#0F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_05.png",
                CommandParameter = "*",
            };
            BindModelCommandProperty(elementButton16, session, "PressButton");
            panelView.Add(elementButton16);
        }

        private void CreatePanelRow3(View panelView, Size size)
        {
            var session = new BindingSession<MainPageViewModel>();

            var elementButton20 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#3D000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_15.png",
                CommandParameter = "N",
            };
            BindModelCommandProperty(elementButton20, session, "PressButton");
            panelView.Add(elementButton20);

            var elementButton21 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2E000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_16.png",
                CommandParameter = "G",
            };
            BindModelCommandProperty(elementButton21, session, "PressButton");
            panelView.Add(elementButton21);

            var elementButton22 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_17.png",
                CommandParameter = "O",
            };
            BindModelCommandProperty(elementButton22, session, "PressButton");
            panelView.Add(elementButton22);

            var elementButton23 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_04.png",
                CommandParameter = "4",
            };
            BindModelCommandProperty(elementButton23, session, "PressButton");
            panelView.Add(elementButton23);

            var elementButton24 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2EFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_05.png",
                CommandParameter = "5",
            };
            BindModelCommandProperty(elementButton24, session, "PressButton");
            panelView.Add(elementButton24);

            var elementButton25 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#3DFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_06.png",
                CommandParameter = "6",
            };
            BindModelCommandProperty(elementButton25, session, "PressButton");
            panelView.Add(elementButton25);

            var elementButton26 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_06.png",
                CommandParameter = "-",
            };
            BindModelCommandProperty(elementButton26, session, "PressButton");
            panelView.Add(elementButton26);
        }

        private void CreatePanelRow4(View panelView, Size size)
        {
            var session = new BindingSession<MainPageViewModel>();

            var elementButton30 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#4C000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_18.png",
                CommandParameter = "U",
            };
            BindModelCommandProperty(elementButton30, session, "PressButton");
            panelView.Add(elementButton30);

            var elementButton31 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#3D000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_19.png",
                CommandParameter = "P",
            };
            BindModelCommandProperty(elementButton31, session, "PressButton");
            panelView.Add(elementButton31);

            var elementButton32 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2E000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_20.png",
                CommandParameter = "W",
            };
            BindModelCommandProperty(elementButton32, session, "PressButton");
            panelView.Add(elementButton32);

            var elementButton33 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#0FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_01.png",
                CommandParameter = "1",
            };
            BindModelCommandProperty(elementButton33, session, "PressButton");
            panelView.Add(elementButton33);

            var elementButton34 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_02.png",
                CommandParameter = "2",
            };
            BindModelCommandProperty(elementButton34, session, "PressButton");
            panelView.Add(elementButton34);

            var elementButton35 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2EFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_03.png",
                CommandParameter = "3",
            };
            BindModelCommandProperty(elementButton35, session, "PressButton");
            panelView.Add(elementButton35);

            var elementButton36 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2E000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_07.png",
                CommandParameter = "+",
            };
            BindModelCommandProperty(elementButton36, session, "PressButton");
            panelView.Add(elementButton36);
        }

        private void CreatePanelRow5(View panelView, Size size)
        {
            var session = new BindingSession<MainPageViewModel>();

            var elementButton40 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#5C000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_21.png",
                CommandParameter = "A",
            };
            BindModelCommandProperty(elementButton40, session, "PressButton");
            panelView.Add(elementButton40);

            var elementButton41 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#4C000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_22.png",
                CommandParameter = "I",
            };
            BindModelCommandProperty(elementButton41, session, "PressButton");
            panelView.Add(elementButton41);

            var elementButton42 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#3D000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_23.png",
                CommandParameter = "E",
            };
            BindModelCommandProperty(elementButton42, session, "PressButton");
            panelView.Add(elementButton42);

            var elementButton43 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#00FAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_00.png",
                CommandParameter = "0",
            };
            BindModelCommandProperty(elementButton43, session, "PressButton");
            panelView.Add(elementButton43);

            var elementButton44 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#0FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_10.png",
                CommandParameter = ".",
            };
            BindModelCommandProperty(elementButton44, session, "PressButton");
            panelView.Add(elementButton44);

            var elementButton45 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_number_11.png",
            };
            BindModelCommandProperty(elementButton45, session, "Reverse");
            panelView.Add(elementButton45);

            var elementButton46 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#59B03A"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_l_08.png",
            };
            BindModelCommandProperty(elementButton46, session, "Equal");
            panelView.Add(elementButton46);
        }

        private void BindModelCommandProperty(ElementButton button, BindingSession<MainPageViewModel> session, string command)
        {
            button.BindingContextChanged += (sender, e) =>
            {
                if (button.BindingContext is MainPageViewModel model)
                {
                    session.ViewModel = model;
                }
            };
            button.SetBinding(session, ElementButtonBindings.CommandProperty, command);
            button.BindingContext = MainPageViewModel.Instance;
        }
    }
}
