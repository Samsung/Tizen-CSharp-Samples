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
    public class CalculatorMainPage : ContentPage
    {
        private PropertyNotification _positionNotification;
        private TextLabel expressionLabel;
        private CustomScrollView expressionScrollView;
        private float originalExpressionLabelHeight;
        private Timer dismissTimer;

        public CalculatorMainPage() : base()
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
                BackgroundImage = Application.Current.DirectoryInfo.Resource + "bg_p.png",
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
            var expressionViewHeight = winSize.Height * 42 / 100;
            var expressionViewPadding = new Extents(16, 16, 16, 16);

            View expressionView = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = expressionViewHeight,
                Padding = expressionViewPadding,
                BackgroundColor = new Color("#FAFAFA"),
                Layout = new GridLayout()
                {
                    Rows = 5,
                    Columns = 1,
                },
            };
            Content.Add(expressionView);

            // for binding
            var session = new BindingSession<MainPageViewModel>();

            // grid width.
            var gridWidth = winSize.Width - (expressionViewPadding.Start + expressionViewPadding.End);
            var gridHeight = expressionViewHeight - (expressionViewPadding.Top + expressionViewPadding.Bottom);

            // grid row 1 related to that in xaml file.
            var rowHeight0 = gridHeight * 644 / 1000;
            originalExpressionLabelHeight = rowHeight0;

            expressionScrollView = new CustomScrollView()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
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
            var rowHeight1 = gridHeight * 18 / 1000;
            var placeholderView1 = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = rowHeight1,
            };
            expressionView.Add(placeholderView1);

            // grid row 3 related to that in xaml file.
            var rowHeight2 = gridHeight * 170 / 1000;
            var resultLabel2 = new TextLabel(StyleResources.SumTextLabelStyle)
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = rowHeight2,
                EnableMarkup = true,
            };
            resultLabel2.BindingContextChanged += (sender, e) =>
            {
                if (resultLabel2.BindingContext is MainPageViewModel model)
                {
                    session.ViewModel = model;
                }
            };
            resultLabel2.SetBinding(session, TextLabelBindings.TextColorProperty, "ResultColor");
            resultLabel2.SetBinding(session, TextLabelBindings.TextProperty, "ResultText");
            resultLabel2.BindingContext = MainPageViewModel.Instance;
            expressionView.Add(resultLabel2);

            // grid row 4 related to that in xaml file.
            var rowHeight3 = gridHeight * 82 / 1000;
            var placeholderView3 = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = rowHeight3,
            };
            expressionView.Add(placeholderView3);

            // grid row 5 related to that in xaml file.
            var rowHeight4 = gridHeight * 86 / 1000;
            var backButtonView4 = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = rowHeight4,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.End,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            };
            expressionView.Add(backButtonView4);

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
            backButtonView4.Add(backButton);
        }

        private void OnPositionNotified(object source, PropertyNotification.NotifyEventArgs args)
        {
            expressionScrollView.ScrollTo(expressionLabel.SizeHeight - originalExpressionLabelHeight, false);
        }

        private void CreatePanelView()
        {
            var winSize = Window.Default.Size;
            var panelViewHeight = winSize.Height * 58 / 100;

            View panelView = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = panelViewHeight,
                Layout = new GridLayout()
                {
                    Rows = 5,
                    Columns = 4,
                },
            };
            Content.Add(panelView);

            var elementWidth = winSize.Width / 4;
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
                NormalBackgroundColor = new Color("#2E000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_01.png",
                CommandParameter = "x",
            };
            BindModelCommandProperty(elementButton00, session, "Clear");
            panelView.Add(elementButton00);

            var elementButton01 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_02.png",
                CommandParameter = "(",
            };
            BindModelCommandProperty(elementButton01, session, "PressButton");
            panelView.Add(elementButton01);

            var elementButton02 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#00000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_03.png",
                CommandParameter = "%",
            };
            BindModelCommandProperty(elementButton02, session, "PressButton");
            panelView.Add(elementButton02);

            var elementButton03 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#5B000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_04.png",
                CommandParameter = "/",
            };
            BindModelCommandProperty(elementButton03, session, "PressButton");
            panelView.Add(elementButton03);
        }

        private void CreatePanelRow2(View panelView, Size size)
        {
            var session = new BindingSession<MainPageViewModel>();

            var elementButton10 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2EFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_7.png",
                CommandParameter = "7",
            };
            BindModelCommandProperty(elementButton10, session, "PressButton");
            panelView.Add(elementButton10);

            var elementButton11 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#3DFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_8.png",
                CommandParameter = "8",
            };
            BindModelCommandProperty(elementButton11, session, "PressButton");
            panelView.Add(elementButton11);

            var elementButton12 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#4CFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_9.png",
                CommandParameter = "9",
            };
            BindModelCommandProperty(elementButton12, session, "PressButton");
            panelView.Add(elementButton12);

            var elementButton13 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#0F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_05.png",
                CommandParameter = "*",
            };
            BindModelCommandProperty(elementButton13, session, "PressButton");
            panelView.Add(elementButton13);
        }

        private void CreatePanelRow3(View panelView, Size size)
        {
            var session = new BindingSession<MainPageViewModel>();

            var elementButton20 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_4.png",
                CommandParameter = "4",
            };
            BindModelCommandProperty(elementButton20, session, "PressButton");
            panelView.Add(elementButton20);

            var elementButton21 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2EFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_5.png",
                CommandParameter = "5",
            };
            BindModelCommandProperty(elementButton21, session, "PressButton");
            panelView.Add(elementButton21);

            var elementButton22 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#3DFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_6.png",
                CommandParameter = "6",
            };
            BindModelCommandProperty(elementButton22, session, "PressButton");
            panelView.Add(elementButton22);

            var elementButton23 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1F000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_06.png",
                CommandParameter = "-",
            };
            BindModelCommandProperty(elementButton23, session, "PressButton");
            panelView.Add(elementButton23);
        }

        private void CreatePanelRow4(View panelView, Size size)
        {
            var session = new BindingSession<MainPageViewModel>();

            var elementButton30 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#0FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_1.png",
                CommandParameter = "1",
            };
            BindModelCommandProperty(elementButton30, session, "PressButton");
            panelView.Add(elementButton30);

            var elementButton31 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_2.png",
                CommandParameter = "2",
            };
            BindModelCommandProperty(elementButton31, session, "PressButton");
            panelView.Add(elementButton31);

            var elementButton32 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2EFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_3.png",
                CommandParameter = "3",
            };
            BindModelCommandProperty(elementButton32, session, "PressButton");
            panelView.Add(elementButton32);

            var elementButton33 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#2E000000"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_07.png",
                CommandParameter = "+",
            };
            BindModelCommandProperty(elementButton33, session, "PressButton");
            panelView.Add(elementButton33);
        }

        private void CreatePanelRow5(View panelView, Size size)
        {
            var session = new BindingSession<MainPageViewModel>();

            var elementButton40 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#00FAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_0.png",
                CommandParameter = "0",
            };
            BindModelCommandProperty(elementButton40, session, "PressButton");
            panelView.Add(elementButton40);

            var elementButton41 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#0FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_10.png",
                CommandParameter = ".",
            };
            BindModelCommandProperty(elementButton41, session, "PressButton");
            panelView.Add(elementButton41);

            var elementButton42 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#1FFAFAFA"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_number_11.png",
            };
            BindModelCommandProperty(elementButton42, session, "Reverse");
            panelView.Add(elementButton42);

            var elementButton43 = new ElementButton()
            {
                Size = size,
                NormalBackgroundColor = new Color("#59B03A"),
                BlendingPressedColor = new Color("#3DB9CC"),
                ResourceUrl = Application.Current.DirectoryInfo.Resource + "calculator_button_p_08.png",
            };
            BindModelCommandProperty(elementButton43, session, "Equal");
            panelView.Add(elementButton43);
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
