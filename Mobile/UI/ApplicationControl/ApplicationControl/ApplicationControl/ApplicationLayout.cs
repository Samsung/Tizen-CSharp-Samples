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
using Tizen.Xamarin.Forms.Extension;
using ApplicationControl.Extensions;
using ApplicationControl.Cells;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ApplicationControl
{
    /// <summary>
    /// A class for an application list layout as a part of the main layout
    /// </summary>
    public class ApplicationLayout : RelativeLayout
    {
        /// <summary>
        /// A constructor of the ApplicationLayout class
        /// </summary>
        public ApplicationLayout(int screenWidth, int screenHeight) : base()
        {
            SetPropertyChangeListener();
            InitializeComponent(screenWidth, screenHeight);
        }

        /// <summary>
        /// A handler be invoked when the properties of the BindingContext is changed
        /// </summary>
        /// <param name="s">sender</param>
        /// <param name="e">event</param>
        async void OnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedAppControlType")
            {
                ((MainViewModel)BindingContext).Applications.Clear();
                await UpdateContentLayout();
            }
            else if (e.PropertyName == "SelectedItem")
            {
                var selectedItem = ((MainViewModel)BindingContext).SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }

                selectedItem.BlendColor = Color.Default;
            }
        }

        /// <summary>
        /// To update application layout
        /// </summary>
        /// <returns>A Task to update application layout asynchrously</returns>
        Task UpdateContentLayout()
        {
            return Task.Run(() =>
            {
                var selected = ((MainViewModel)BindingContext).SelectedAppControlType;
                var apps = ApplicationControlHelper.GetApplicationIdsForSpecificAppControlType(selected);
                foreach (var app in apps)
                {
                    var iconPath = ApplicationControlHelper.GetApplicationIconPath(app);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        /// Add an item to the list
                        ((MainViewModel)BindingContext)
                            .Applications.Add(new ApplicationListItem
                            {
                                Id = app,
                                IconPath = iconPath,
                                BlendColor = Color.Gray,
                            });
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    /// Add an extra empty item.
                    ((MainViewModel)BindingContext)
                        .Applications.Add(new ApplicationListItem
                        {
                            Id = null,
                            IconPath = null,
                            BlendColor = Color.Gray,
                        });
                });
            });
        }

        /// <summary>
        /// To initialize the application layout
        /// </summary>
        void InitializeComponent(int screenWidth, int screenHeight)
        {
            Children.Add(
               new Image
               {
                   HorizontalOptions = LayoutOptions.FillAndExpand,
                   VerticalOptions = LayoutOptions.FillAndExpand,
                   Margin = new Thickness(0),

                   Source = "bar_apps.png",
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
                   return parent.Height * 0.2411;
               }));

            var applications = new ApplicationContentLayout(screenWidth, screenHeight);
            applications.ItemSelected += (s, e) =>
            {
                var item = (ApplicationListItem)e.SelectedItem;
                var selectedItem = ((MainViewModel)BindingContext).SelectedItem;
                if (selectedItem != null)
                {
                    selectedItem.BlendColor = Color.Gray;
                }

                ((MainViewModel)BindingContext).SelectedItem = item;
            };

            BindingContextChanged += (s, e) =>
            {
                if (BindingContext == null)
                {
                    return;
                }

                applications.ItemsSource = ((MainViewModel)BindingContext).Applications;
            };

            /// For application list
            Children.Add(
                applications,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.2411;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.5265;
                }));

            var executeButton = new ImageButton
            {
                Source = "execute_button.jpg",
            };
            executeButton.Clicked += (s, e) =>
            {
                ((MainViewModel)BindingContext).ExecuteSelectedApplication();
            };

            /// For execute button
            Children.Add(
                executeButton,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.7589;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.2411;
                }));

            var killButton = new ImageButton
            {
                Source = "kill_button.jpg"
            };
            killButton.Clicked += (s, e) =>
            {
                ((MainViewModel)BindingContext).KillSelectedApplication();
            };

            /// For kill button
            Children.Add(
                killButton,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.7589;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.2411;
                }));
        }

        void SetPropertyChangeListener()
        {
            BindingContextChanged += (s, e) =>
            {
                if (BindingContext == null)
                {
                    return;
                }

                ((MainViewModel)BindingContext).PropertyChanged += OnPropertyChanged;
            };
        }
    }

    /// <summary>
    /// A class for an application content layout
    /// </summary>
    public class ApplicationContentLayout : GridView
    {
        /// <summary>
        /// A constructor for the ApplicationContentLayout
        /// </summary>
        public ApplicationContentLayout(int screenWidth, int screenHeight) : base()
        {
            var widthScale = (double)screenWidth / 720.0;
            var heightScale = (double)screenHeight / 1280.0;

            var scaledItemWidth = (int)(360.0 * widthScale);
            var scaledItemHeight = (int)(219.0 * heightScale);

            ItemWidth = scaledItemWidth;
            ItemHeight = scaledItemHeight;
            Orientation = GridViewOrientation.Horizontal;
            IsHighlightEffectEnabled = false;
            ItemTemplate = new DataTemplate(typeof(CustomViewCell));
        }
    }
}