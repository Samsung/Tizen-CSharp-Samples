/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using MediaContent.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaContent.Controls
{
    /// <summary>
    /// FileInfoTable class.
    /// Provides logic for FileInfoTable.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileInfoTable : AbsoluteLayout
    {
        #region properties

        /// <summary>
        /// File bindable property.
        /// </summary>
        public static readonly BindableProperty FileProperty =
            BindableProperty.Create(nameof(File), typeof(FileInfo), typeof(FileInfoTable), default(FileInfo));

        /// <summary>
        /// File property.
        /// </summary>
        public FileInfo File
        {
            get { return (FileInfo)GetValue(FileProperty); }
            set { SetValue(FileProperty, value); }
        }

        #endregion

        #region methods

        /// <summary>
        /// Updates information table with file from 'File' property.
        /// </summary>
        private void FilePropertyChanged()
        {
            IEnumerable<PropertyInfo> properties = File.GetType().GetRuntimeProperties();

            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(File, null) as string;

                if (String.IsNullOrEmpty(propValue))
                {
                    continue;
                }

                var stackLayout = new StackLayout();
                stackLayout.Children.Add(new TableElementCentered()
                {
                    Text = propValue
                });

                var viewCell = new ViewCell()
                {
                    View = stackLayout
                };

                var tableSection = new TableSection()
                {
                    Title = prop.Name
                };

                tableSection.Add(viewCell);
                InformationTable.Root.Add(tableSection);
            }
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
        /// FileInfoTable class constructor.
        /// </summary>
        public FileInfoTable()
        {
            InitializeComponent();
        }

        #endregion
    }
}