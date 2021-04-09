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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XStopWatch
{
    using Lap = Tuple<int, TimeSpan, TimeSpan>;

    /// <summary>
    /// LapsPage is a Page that present measured Laps time list
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LapsPage : CirclePage
    {
        public static BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(StopWatch), TimeSpan.Zero);

        public static BindableProperty LapsProperty = BindableProperty.Create(nameof(Laps), typeof(ObservableCollection<Lap>), typeof(LapsPage));
        public ObservableCollection<Lap> Laps { get => (ObservableCollection<Lap>)GetValue(LapsProperty); set => SetValue(LapsProperty, value); }

        public TimeSpan Time { get => (TimeSpan)GetValue(TimeProperty); set => SetValue(TimeProperty, value); }

        public LapsPage()
		{
            Laps = new ObservableCollection<Lap>();
            InitializeComponent();
		}

        // this method put the recorded the elapsed and the lap time to internal collection.
        public void AddLap((TimeSpan Main, TimeSpan Sub) lap)
        {
            Laps.Add(new Lap(Laps.Count + 1, lap.Main, lap.Sub));
        }

        // reset the internal collection.
        public void Reset()
        {
            Laps.Clear();
        }
    }
}