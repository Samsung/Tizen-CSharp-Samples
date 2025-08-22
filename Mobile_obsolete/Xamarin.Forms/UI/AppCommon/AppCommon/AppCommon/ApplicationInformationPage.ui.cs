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
using AppCommon.Extensions;
using AppCommon.Styles;

namespace AppCommon
{
    /// <summary>
    /// A class about application information page
    /// </summary>
    public partial class ApplicationInformationPage : ContentPage
    {
        /// <summary>
        /// To initialize UI Components of an application information page
        /// </summary>
        private void InitializeComponent()
        {
            Title = "ApplicationInfo";

            /// The mainLayout consists of several parts to display application information.
            var mainLayout = new RelativeLayout { };

            /// To display an image as the background
            var background = new Background
            {
                Image = new FileImageSource { File = "background_app.png" },
                Option = BackgroundOptions.Stretch,
            };
            mainLayout.Children.Add(
                background,
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

            /// To display an application icon path
            var icon = new Image
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            mainLayout.Children.Add(
                icon,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0556;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0537;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.1319;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0774;
                }));

            /// To display an application name
            var applicationName = new Label { };
            applicationName.Style = ApplicationInformationStyle.ContentStyle;

            mainLayout.Children.Add(
                applicationName,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.2472;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0961;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.7389;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0293;
                }));

            /// To display an application ID
            var applicationID = new Label { };
            applicationID.Style = ApplicationInformationStyle.ContentStyle;

            mainLayout.Children.Add(
                applicationID,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0375;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.3014;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.4486;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0519;
                }));

            /// To display an application version
            var applicationVersion = new Label { };
            applicationVersion.Style = ApplicationInformationStyle.ContentStyle;

            mainLayout.Children.Add(
                applicationVersion,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5416;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.3014;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.4486;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0319;
                }));

            /// To display the device memory status
            var memoryLED = new Image
            {
                Source = "led.png"
            };

            mainLayout.Children.Add(
                memoryLED,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0375;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.4661;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0236;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0152;
                }));

            /// To display the device battery status
            var batteryLED = new Image
            {
                Source = "led.png"
            };

            mainLayout.Children.Add(
                batteryLED,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5416;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.4661;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0236;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0152;
                }));

            /// To display the language setting on the device
            var language = new Label { };
            language.Style = ApplicationInformationStyle.ContentStyle;

            mainLayout.Children.Add(
                language,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0375;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.7032;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.4486;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0319;
                }));

            /// To display the region format setting on the device
            var regionFormat = new Label { };
            regionFormat.Style = ApplicationInformationStyle.ContentStyle;

            mainLayout.Children.Add(
                regionFormat,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5416;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.7032;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.4486;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0319;
                }));

            /// To display the orientation of the device
            var deviceOrienation = new Label { };
            deviceOrienation.Style = ApplicationInformationStyle.ContentStyle;

            mainLayout.Children.Add(
                deviceOrienation,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.0375;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.9109;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.4661;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.0321;
                }));

            /// To display the orientation degree of the device
            var rotationDegree = new Label { };
            rotationDegree.Style = ApplicationInformationStyle.LargerContentStyle;

            mainLayout.Children.Add(
                rotationDegree,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.5416;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.8254;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.4486;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.1276;
                }));

            BindingContextChanged += (s, e) =>
            {
                if (BindingContext == null)
                {
                    return;
                }

                icon.Source = ((ApplicationInformationViewModel)BindingContext).IconPath;
                applicationName.Text = ((ApplicationInformationViewModel)BindingContext).Name;
                applicationID.Text = ((ApplicationInformationViewModel)BindingContext).ID;
                applicationVersion.Text = ((ApplicationInformationViewModel)BindingContext).Version;

                memoryLED.BindingContext = BindingContext;
                batteryLED.BindingContext = BindingContext;
                language.BindingContext = BindingContext;
                regionFormat.BindingContext = BindingContext;
                deviceOrienation.BindingContext = BindingContext;
                rotationDegree.BindingContext = BindingContext;

                language.SetBinding(Label.TextProperty, "Language");
                regionFormat.SetBinding(Label.TextProperty, "RegionFormat");
                deviceOrienation.SetBinding(Label.TextProperty, "DeviceOrientation");
                rotationDegree.SetBinding(Label.TextProperty, "RotationDegree");

                memoryLED.SetBinding(ImageAttributes.BlendColorProperty, "LowMemoryLEDColor");
                batteryLED.SetBinding(ImageAttributes.BlendColorProperty, "LowBatteryLEDColor");
            };

            /// Set mainLayout as Content of the page
            Content = mainLayout;
        }
    }
}