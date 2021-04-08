//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Tizen.Content.MediaContent;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaContent.Controls
{
    /// <summary>
    /// FileInfoTable class.
    /// Provides logic for FileInfoTable.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileInfoTable : StackLayout
    {
        #region properties

        /// <summary>
        /// File bindable property.
        /// </summary>
        public static readonly BindableProperty FileProperty =
            BindableProperty.Create(nameof(File), typeof(MediaInfo), typeof(FileInfoTable), default(MediaInfo));

        #endregion

        #region methods

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoTable"/> class
        /// </summary>
        public FileInfoTable()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a MediaInfo for file
        /// </summary>
        public MediaInfo File
        {
            get { return (MediaInfo)GetValue(FileProperty); }
            set { SetValue(FileProperty, value); }
        }

        /// <summary>
        /// Notifies the application about update of the property with name given as a parameter.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == FileProperty.PropertyName)
            {
                FilePropertyChanged();
            }
        }

        /// <summary>
        /// Updates information table with file from 'File' property.
        /// </summary>
        private void FilePropertyChanged()
        {
            IEnumerable<PropertyInfo> properties = File.GetType().GetRuntimeProperties();

            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(File, null) as string;

                if (string.IsNullOrEmpty(propValue))
                {
                    continue;
                }

                // Property name label
                InformationTable.Children.Add(new Label()
                {
                    BackgroundColor = Color.FromHex("#2b2b2b"),
                    Text = prop.Name,
                    FontSize = 12,
                    HeightRequest = 65,
                    TextColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                });

                // Property details label
                InformationTable.Children.Add(new Label()
                {
                    Text = propValue,
                    FontSize = 8,
                    Margin = 20,
                    LineBreakMode = LineBreakMode.CharacterWrap,
                    TextColor = Color.FromHex("#4DCFFF"),
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                });
            }
        }

        #endregion
    }
}