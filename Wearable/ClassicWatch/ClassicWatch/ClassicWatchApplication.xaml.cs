/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassicWatch
{
    /// <summary>
    /// ClassicWatch Application class
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClassicWatchApplication : Application
	{
        WatchViewModel _viewModel;

        /// <summary>
        /// Constructor of ClassicWatch Application
        /// </summary>
        /// <param name="viewModel">WatchViewModel</param>
        public ClassicWatchApplication(WatchViewModel viewModel)
		{
            _viewModel = viewModel;
            InitializeComponent();
            BindingContext = viewModel;
        }

        /// <summary>
        /// Rotate Watch hands
        /// </summary>
        public void MoveHands()
        {
            //Set Rotation degree each hour/min/sec hand
            hand_hr.Rotation = _viewModel.HourRotation;
            hand_min.Rotation = _viewModel.MinuteRotation;
            hand_sec.Rotation = _viewModel.SecondRotation;

            //Set Rotation degree each hour/min/sec hand shadow
            hand_sec_shadow.Rotation = _viewModel.SecondRotation;
            hand_min_shadow.Rotation = _viewModel.MinuteRotation;
            hand_hr_shadow.Rotation = _viewModel.HourRotation;

            //Set month hand rotation degree if value has been changed.
            if (hand_month.Rotation != _viewModel.MonthRotation)
            {
                hand_month.Rotation = _viewModel.MonthRotation;
                hand_month_shadow.Rotation = _viewModel.MonthRotation;
            }

            //Set day hand rotation degree if value has been changed.
            if (hand_day_of_month.Rotation != _viewModel.DayRotation)
            {
                hand_day_of_month.Rotation = _viewModel.DayRotation;
                hand_day_of_month_shadow.Rotation = _viewModel.DayRotation;
            }
        }
    }
}