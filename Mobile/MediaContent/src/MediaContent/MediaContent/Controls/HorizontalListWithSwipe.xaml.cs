/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

﻿using System.Collections;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using MediaContent.Models;
using MediaContent.Views.Renderers;

namespace MediaContent.Controls
{
    /// <summary>
    /// HorizontalListWithSwipe class.
    /// Provides logic for HorizontalListWithSwipe.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HorizontalListWithSwipe : AbsoluteLayout
    {
        #region fields

        /// <summary>
        /// Size of labels' font for TVs.
        /// </summary>
        private readonly int _fontSizeTV;

        /// <summary>
        /// Size of labels' font for Phones.
        /// </summary>
        private readonly int _fontSizePhone;

        /// <summary>
        /// Size of labels' font for the rest of devices.
        /// </summary>
        private readonly int _fontSizeDefault;

        #endregion

        #region properties

        /// <summary>
        /// Items source bindable property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(HorizontalListWithSwipe), default(IEnumerable));

        /// <summary>
        /// Selected items bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedItemsProperty =
            BindableProperty.Create(nameof(SelectedItems), typeof(ObservableCollection<string>), typeof(HorizontalListWithSwipe), new ObservableCollection<string>(),
                BindingMode.TwoWay);

        /// <summary>
        /// Items source property.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Selected items property.
        /// </summary>
        public ObservableCollection<string> SelectedItems
        {
            get { return (ObservableCollection<string>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        #endregion

        #region methods

        /// <summary>
        /// Returns size of the font appropriate for the currently used device.
        /// </summary>
        /// <returns>The size of the font appropriate for the currently used device.</returns>
        private int GetProperFontSize()
        {
            switch (Device.Idiom)
            {
                case TargetIdiom.TV: return _fontSizeTV;
                case TargetIdiom.Phone: return _fontSizePhone;
                default: return _fontSizeDefault;
            }
        }

        /// <summary>
        /// Updates horizontal list with elements from 'ItemsSource' property.
        /// </summary>
        private void ItemsSourcePropertyChanged()
        {
            StoragesList.Children.Clear();

            foreach (var item in ItemsSource)
            {
                TizenCheckBox check = new TizenCheckBox()
                {
                    VerticalOptions = LayoutOptions.Center
                };
                check.BindingContext = (item as StorageInfo).RootDirectory;
                check.Toggled += (object sender, ToggledEventArgs e) =>
                {
                    var storageRootDirectory = ((sender as Switch).BindingContext as string);

                    if (e.Value)
                    {
                        SelectedItems.Add(storageRootDirectory);
                    }
                    else
                    {
                        SelectedItems.Remove(storageRootDirectory);
                    }
                };

                StoragesList.Children.Add(check);

                StoragesList.Children.Add(new Xamarin.Forms.Label()
                {
                    Text = (item as StorageInfo).DisplayName,
                    FontSize = GetProperFontSize(),
                    Margin = new Thickness(0, 0, GetProperFontSize(), 0),
                    VerticalOptions = LayoutOptions.Center
                });
            }
        }

        /// <summary>
        /// Notifies the application about update of the property with name given as a parameter.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                ItemsSourcePropertyChanged();
            }
        }

        /// <summary>
        /// HorizontalListWithSwipe class constructor.
        /// </summary>
        public HorizontalListWithSwipe()
        {
            InitializeComponent();

            _fontSizeTV = 100;
            _fontSizePhone = 25;
            _fontSizeDefault = _fontSizeTV;
        }

        #endregion
    }
}