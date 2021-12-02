﻿//Copyright 2018 Samsung Electronics Co., Ltd
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

using System.Linq;
using Weather.Utils;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Weather.Views
{
    /// <summary>
    /// Interaction logic for ForecastPage.xaml.
    /// </summary>
    public partial class ForecastPage : ContentPage, IRotaryEventReceiver
    {
        #region fields
        private bool _rotating = false;

        #endregion fields

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public ForecastPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle bezel rotation
        /// </summary>
        /// <param name="args">Rotary event arguments</param>
        public void Rotate(RotaryEventArgs args)
        {
            var a = this.BindingContext;

            if (_rotating)
            {
                return;
            }

            _rotating = true;           
            if (args.IsClockwise)
            {
                ((ViewModels.ForecastViewModel)BindingContext).NextForecastCommand.Execute(null);
                _rotating = false;
            }
            else
            {
                ((ViewModels.ForecastViewModel)BindingContext).PreviousForecastCommand.Execute(null);
                _rotating = false;
            }
        }

        /// <summary>
        /// Sets the binding context to page resources.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Resources != null)
            {
                foreach (var bindableString in Resources.Values.OfType<BindableString>())
                {
                    bindableString.BindingContext = BindingContext;
                }
            }
        }

        #endregion
    }
}