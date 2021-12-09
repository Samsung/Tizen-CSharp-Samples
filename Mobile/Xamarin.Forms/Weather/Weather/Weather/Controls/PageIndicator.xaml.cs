/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using System.Linq;
using Xamarin.Forms;

namespace Weather.Controls
{
    /// <summary>
    /// Interaction logic for PageIndicator.xaml.
    /// </summary>
    public partial class PageIndicator
    {
        #region fields

        /// <summary>
        /// Set of indicator images.
        /// </summary>
        private readonly Image[] _dots;

        /// <summary>
        /// Image name that represents unselected page.
        /// </summary>
        private const string DOT_EMPTY = "dot_empty.png";

        /// <summary>
        /// Image name that represents selected page.
        /// </summary>
        private const string DOT_FULL = "dot_full.png";

        #endregion


        #region properties

        /// <summary>
        /// Bindable property that allows to set position of carousel view.
        /// </summary>
        public static readonly BindableProperty PositionProperty =
            BindableProperty.Create(nameof(Position), typeof(int), typeof(PageIndicator), 0,
                propertyChanged: PositionPropertyChanged);

        /// <summary>
        /// Gets or sets position of carousel view.
        /// </summary>
        public int Position
        {
            get => (int)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public PageIndicator()
        {
            InitializeComponent();
            _dots = IndicatorsStack.Children.OfType<Image>().ToArray();
            _dots[0].Source = DOT_FULL;
        }

        /// <summary>
        /// Updates images source on position changes.
        /// </summary>
        /// <param name="oldPosition">The old position.</param>
        /// <param name="newPosition">The new position.</param>
        public void UpdateIndicators(int oldPosition, int newPosition)
        {
            _dots[oldPosition].Source = DOT_EMPTY;
            _dots[newPosition].Source = DOT_FULL;
        }

        /// <summary>
        /// Invoked on position property changes.
        /// </summary>
        /// <param name="bindable">Object that declared property.</param>
        /// <param name="oldValue">Old value of the property.</param>
        /// <param name="newValue">New value of the property.</param>
        private static void PositionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PageIndicator)bindable).UpdateIndicators((int)oldValue, (int)newValue);
        }

        #endregion
    }
}