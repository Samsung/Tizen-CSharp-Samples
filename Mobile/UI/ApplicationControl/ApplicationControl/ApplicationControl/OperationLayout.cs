/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using Xamarin.Forms;
using Tizen.Xamarin.Forms.Extension;
using ApplicationControl.Extensions;

namespace ApplicationControl
{
    /// <summary>
    /// A class for an operation layout as a part of the main layout
    /// </summary>
    public class OperationLayout : RelativeLayout
    {
        /// <summary>
        /// A constructor of the OperationLayout class
        /// </summary>
        public OperationLayout() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A handler be invoked when an item is selected on the application list
        /// </summary>
        /// <param name="s">sender</param>
        /// <param name="e">event</param>
        void OnItemSelected(object s, EventArgs e)
        {
            var item = (OperationItem)s;
            ((MainViewModel)BindingContext).SelectedAppControlType = item.Type;
        }

        /// <summary>
        /// To initialize components of the operation layout
        /// </summary>
        void InitializeComponent()
        {
            Children.Add(
                new Image
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,

                    Source = "bar_header.png",
                    Aspect = Aspect.Fill,
                },
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.3178;
                }));

            var operations = new OperationContentLayout { };

            operations.AddItem("View operation", AppControlType.View)
                .Selected += OnItemSelected;

            operations.AddItem("Pick operation", AppControlType.Pick)
                .Selected += OnItemSelected;

            operations.AddItem("Compose operation", AppControlType.Compose)
                .Selected += OnItemSelected;

            Children.Add(
                operations,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.3178;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.6822;
                }));
        }
    }

    /// <summary>
    /// A class for an operation content layout as a part of the operation layout
    /// </summary>
    public class OperationContentLayout : StackLayout
    {
        /// <summary>
        /// A constructor for OperationContentLayout class
        /// </summary>
        public OperationContentLayout() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// To add an item on the operation content layout
        /// </summary>
        /// <param name="name">A text for the caption</param>
        /// <param name="type">An appcontrol type</param>
        /// <returns>An operation item</returns>
        public OperationItem AddItem(string name, AppControlType type)
        {
            var item = new OperationItem(name, type) { };
            item.WidthRequest = this.Width / 3;
            this.Children.Add(item);
            return item;
        }

        /// <summary>
        /// To initialize components of the operation content layout
        /// </summary>
        void InitializeComponent()
        {
            Orientation = StackOrientation.Horizontal;
            Spacing = 0;
        }
    }

    /// <summary>
    /// A class for an operation item
    /// </summary>
    public class OperationItem : RelativeLayout
    {
        string _name;
        Label _caption;
        BlendImage _radio;
        BlendImage _radioCheck;
        RadioButton _radioButton;

        /// <summary>
        /// A constructor for the OperationItem
        /// </summary>
        /// <param name="name">A text for the caption</param>
        /// <param name="type">An appcontrol type</param>
        public OperationItem(string name, AppControlType type) : base()
        {
            _name = name;
            Type = type;
            InitializeComponent();
        }

        /// <summary>
        /// An appcontrol type
        /// </summary>
        public AppControlType Type { get; private set; }

        /// <summary>
        /// To initialize components of the operation item
        /// </summary>
        void InitializeComponent()
        {
            _caption = new Label
            {
                Text = _name,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            _radio = new BlendImage
            {
                Source = "radio.png",
                BlendColor = Color.FromRgb(231, 214, 25),
            };

            _radioCheck = new BlendImage
            {
                Source = "radio_check.png",
                BlendColor = Color.FromRgb(231, 214, 25),
                Scale = 0,
            };

            _radioButton = new RadioButton
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,

                GroupName = "Group-Operation",
                IsSelected = false,
            };

            _radioButton.Selected += (s, e) =>
            {
                if (e.Value == false)
                {
                    UpdateUncheckedRadio();
                    return;
                }

                UpdateCheckedRadio();
                Selected?.Invoke(this, EventArgs.Empty);
            };
            
            Children.Add(
                _caption,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.125;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.4612;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.750;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.4931;
                }));
                
            Children.Add(
                _radio,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.4;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.1507;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.2;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.2192;
                }));

            Children.Add(
                _radioCheck,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.4292;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.1826;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.1416;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.1553;
                }));

            Children.Add(
                _radioButton,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.1507;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height;
                }));
        }

        /// <summary>
        /// To update radio properties when the radio button is checked
        /// </summary>
        async void UpdateCheckedRadio()
        {
            _radioCheck.Opacity = 100;
            await _radioCheck.ScaleTo(1, length: 50);
        }

        /// <summary>
        /// To update radio properties when the radio button is unchecked
        /// </summary>
        void UpdateUncheckedRadio()
        {
            _radioCheck.Opacity = 0;
            _radioCheck.Scale = 0.5;
        }

        public event EventHandler Selected;
    }
}