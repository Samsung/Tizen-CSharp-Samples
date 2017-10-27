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
using Label = Xamarin.Forms.Label;
using Image = Xamarin.Forms.Image;

namespace AppCommon.Cells
{
    /// <summary>
    /// A class for a cell of the list on the paths page.
    /// The PathItemCell is composed of a background image, a label for a title, and a label for a detailed path.
    /// </summary>
    public class PathItemCell : ViewCell
    {
        public PathItemCell()
        {
            View = CreateView();
        }

        View CreateView()
        {
            var layout = new RelativeLayout { };

            var backgroundImage = new Image
            {
                Source = new FileImageSource { File = "list_item_bg.png" },
                Aspect = Aspect.Fill,
            };

            layout.Children.Add(
                backgroundImage,
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

            var descriptionLabel = new Label
            {
                Text = "Description",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                FontSize = 30 * 72 * 2.6 / 316,
                FontAttributes = FontAttributes.Bold,
            };
            descriptionLabel.SetBinding(Label.TextProperty, "Title");

            var pathLabel = new Label
            {
                Text = "Path",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                FontSize = 25 * 72 * 2.6 / 316,
                TextColor = Color.FromRgb(146, 146, 146),
            };
            pathLabel.SetBinding(Label.TextProperty, "Path");

            layout.Children.Add(descriptionLabel,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0431;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.3084;
                }));

            layout.Children.Add(pathLabel,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0431;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.5198;
                }));

            return layout;
        }
    }
}