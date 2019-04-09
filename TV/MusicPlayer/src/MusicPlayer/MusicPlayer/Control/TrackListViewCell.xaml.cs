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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.Control
{
    /// <summary>
    /// Represents single item of tracklist.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackListViewCell : ContentView
    {
        #region properties

        /// <summary>
        /// Allows to set title of the track item.
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                                nameof(Title),
                                                typeof(string),
                                                typeof(TrackListViewCell),
                                                "",
                                                BindingMode.OneWay);

        /// <summary>
        /// Gets or sets title of the track item.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Allows to set artist of the track item.
        /// </summary>
        public static readonly BindableProperty ArtistProperty = BindableProperty.Create(
                                                nameof(Artist),
                                                typeof(string),
                                                typeof(TrackListViewCell),
                                                "",
                                                BindingMode.OneWay);

        /// <summary>
        /// Gets or sets artist of the track item.
        /// </summary>
        public string Artist
        {
            get => (string)GetValue(ArtistProperty);
            set => SetValue(ArtistProperty, value);
        }

        /// <summary>
        /// Allows to set album of the track item.
        /// </summary>
        public static readonly BindableProperty AlbumProperty = BindableProperty.Create(
                                                nameof(Album),
                                                typeof(string),
                                                typeof(TrackListViewCell),
                                                "",
                                                BindingMode.OneWay);

        /// <summary>
        /// Gets or sets album of the track item.
        /// </summary>
        public string Album
        {
            get => (string)GetValue(AlbumProperty);
            set => SetValue(AlbumProperty, value);
        }

        /// <summary>
        /// Allows to set artwork of the track item.
        /// </summary>
        public static readonly BindableProperty ArtworkProperty = BindableProperty.Create(
                                                nameof(Artwork),
                                                typeof(byte[]),
                                                typeof(TrackListViewCell),
                                                null,
                                                BindingMode.OneWay);

        /// <summary>
        /// Gets or sets artwork of the track item.
        /// </summary>
        public byte[] Artwork
        {
            get => (byte[])GetValue(ArtworkProperty);
            set => SetValue(ArtworkProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates new instance of class.
        /// </summary>
        public TrackListViewCell()
        {
            InitializeComponent();
        }

        #endregion
    }
}