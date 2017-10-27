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

using Xamarin.Forms;
using Tizen.Xamarin.Forms.Extension;
using ApplicationControl.Extensions;

namespace ApplicationControl.Cells
{
    /// <summary>
    /// A class for a custrom view cell will be used as a DataTemplate by the ApplicationContentLayout
    /// </summary>
    public class CustomViewCell : GridViewCell
    {
        /// <summary>
        /// A constructor for the CustomViewCell class
        /// </summary>
        public CustomViewCell() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// To initialize the custom view cell
        /// </summary>
        void InitializeComponent()
        {
            var layout = new RelativeLayout
            {
                WidthRequest = 360,
                HeightRequest = 219,

                HorizontalOptions = LayoutOptions.Start,
            };

            layout.Children.Add(
                new Image
                {
                    Source = "apps_list_item_bg.png",
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
                    return parent.Height;
                }));

            var icon = new BlendImage { };
            icon.SetBinding(BlendImage.SourceProperty, "IconPath");
            icon.SetBinding(BlendImage.BlendColorProperty, "BlendColor");

            layout.Children.Add(
                icon,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0778;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.2740;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.2772;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.4475;
                }));

            var label = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            label.SetBinding(Label.TextProperty, "Id");

            layout.Children.Add(
                label,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.425;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height;
                }));

            View = layout;
        }
    }
}