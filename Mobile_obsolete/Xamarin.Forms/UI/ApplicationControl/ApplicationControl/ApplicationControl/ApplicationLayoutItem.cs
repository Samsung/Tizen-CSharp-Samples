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
using Image = Xamarin.Forms.Image;
using ApplicationControl.Extensions;
using System;

namespace ApplicationControl
{
    /// <summary>
    /// A class for an application item
    /// </summary>
    public class ApplicationLayoutItem : RelativeLayout
    {
        Label _caption;
        Image _bgButton;
        Image _icon;
        ApplicationListItem _data;

        /// <summary>
        /// The application ID
        /// </summary>
        public string AppId { get ; set;}

        /// <summary>
        /// A constructor for the ApplicationItem
        /// </summary>
        /// <param name="item">An ApplicationListItem item</param>
        public ApplicationLayoutItem(ApplicationListItem item) : base()
        {
            _data = item;
            AppId = _data.Id;
            InitializeComponent();
        }

        /// <summary>
        /// To initialize components of the operation item
        /// </summary>
        void InitializeComponent()
        {

            _bgButton = new Image
            {
                Source = "apps_list_item_bg.png",
            };

            var gestureRecognizer = new LongTapGestureRecognizer();
            //When tap event is invoked. add pressed color to square image.
            gestureRecognizer.TapStarted += (s, e) =>
            {
                //change foreground blend color of image
                ImageAttributes.SetBlendColor(_bgButton, Color.FromRgb(213, 228, 240));
            };

            //If tap is released. set default color to square image.
            gestureRecognizer.TapCanceled += (s, e) =>
            {
                //revert foreground blend color of image
                ImageAttributes.SetBlendColor(_bgButton, Color.Default);
                //Invoke selected event to consumer.
                SendSelected();
            };

            //Set default color to square image.
            gestureRecognizer.TapCompleted += (s, e) =>
            {
                //revert foreground blend color of image
                ImageAttributes.SetBlendColor(_bgButton, Color.Default);
                //Invoke selected event to consumer.
                SendSelected();
            };
            GestureRecognizers.Add(gestureRecognizer);

            Children.Add(
                _bgButton,
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

            _icon = new Image() { };
            _icon.Source = ImageSource.FromFile(_data.IconPath);
            ImageAttributes.SetBlendColor(_bgButton, Color.FromRgb(213, 228, 240));

            Children.Add(
                _icon,
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

            _caption = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 35,
                LineBreakMode = LineBreakMode.CharacterWrap,
            };
            _caption.Text = _data.Id;

            Children.Add(
                _caption,
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
        }

        public event EventHandler Selected;

        /// <summary>
        /// To broadcast a selected event to subscribers
        /// </summary>
        public void SendSelected()
        {
            Selected?.Invoke(this, EventArgs.Empty);
        }
    }
}