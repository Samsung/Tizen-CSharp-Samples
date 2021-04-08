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
using System.Threading.Tasks;
using System.ComponentModel;
using System;

namespace ApplicationControl
{
    /// <summary>
    /// A class for an application list layout as a part of the main layout
    /// </summary>
    public class ApplicationLayout : RelativeLayout
    {
        ApplicationContentLayout _applications;
        /// <summary>
        /// A constructor of the ApplicationLayout class
        /// </summary>
        /// <param name="screenWidth">screen width</param>
        /// <param name="screenHeight">screen height</param>
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    _applications.Children.Clear();
                });

                var selected = ((MainViewModel)BindingContext).SelectedAppControlType;
                var apps = ApplicationControlHelper.GetApplicationIdsForSpecificAppControlType(selected);
                foreach (var app in apps)
                {
                    var iconPath = ApplicationControlHelper.GetApplicationIconPath(app);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        /// Add an item to the list
                        var item = new ApplicationListItem
                        {
                            Id = app,
                            IconPath = iconPath,
                            BlendColor = Color.Gray,
                        };
                        ((MainViewModel)BindingContext).Applications.Add(item);

                        _applications.AddItem(item).Selected += OnItemSelected;
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    /// Add an empty item to the list
                    var item = new ApplicationListItem
                    {
                        Id = null,
                        IconPath = null,
                        BlendColor = Color.Gray,
                    };
                    ((MainViewModel)BindingContext).Applications.Add(item);

                    _applications.AddItem(item);
                });

                /// For application list
                Children.Add(
                    _applications,
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
            });
        }

        /// <summary>
        /// A handler be invoked when an item is selected on the application list
        /// </summary>
        /// <param name="s">sender</param>
        /// <param name="e">event</param>
        void OnItemSelected(object s, EventArgs e)
        {
            var item = (ApplicationLayoutItem)s;
            foreach (var app in ((MainViewModel)BindingContext).Applications)
            {
                if (app.Id.Equals(item.AppId))
                {
                    ((MainViewModel)BindingContext).SelectedItem = app;
                    break;
                }
            }
        }

        /// <summary>
        /// To initialize the application layout
        /// </summary>
        /// <param name="screenWidth">screen width</param>
        /// <param name="screenHeight">screen height</param>
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

            _applications = new ApplicationContentLayout(screenWidth, screenHeight);

            /// For application list
            Children.Add(
                _applications,
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

            var executeButton = new CustomImageButton
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

            var killButton = new CustomImageButton
            {
                Source = "kill_button.jpg",
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
    public class ApplicationContentLayout : StackLayout
    {
        /// <summary>
        /// A constructor for the ApplicationContentLayout
        /// </summary>
        /// <param name="screenWidth">screen width</param>
        /// <param name="screenHeight">screen height</param>
        public ApplicationContentLayout(int screenWidth, int screenHeight) : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// To initialize components of the operation content layout
        /// </summary>
        void InitializeComponent()
        {
            Orientation = StackOrientation.Horizontal;
            Spacing = 0;
        }

        /// <summary>
        /// To add an item on the application content layout
        /// </summary>
        /// <param name="item">ApplicationListItem</param>
        /// <returns>ApplicationLayoutItem</returns>
        public ApplicationLayoutItem AddItem(ApplicationListItem item)
        {
            var layoutItem = new ApplicationLayoutItem(item) { };
            layoutItem.WidthRequest = this.Width / 2;
            this.Children.Add(layoutItem);
            return layoutItem;
        }
    }
}