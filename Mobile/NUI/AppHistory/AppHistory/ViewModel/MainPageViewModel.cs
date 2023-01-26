/*
 * Copyright (c) 2023 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AppHistory.ViewModel
{
    class MainPageViewModel
    {
        public ObservableCollection<MainPageListIteam> Source { get; private set; } = new ObservableCollection<MainPageListIteam>();
        public MainPageViewModel()
        {
            Source.Add(new MainPageListIteam("Top 5 recently used applications", "during the last 5 hours"));
            Source.Add(new MainPageListIteam("Top 10 frequently used applications", "during the last 3 days"));
            Source.Add(new MainPageListIteam("Top 10 battery consuming applications", "since the last time when the device has fully charged"));
        }
    }

    public class MainPageListIteam : INotifyPropertyChanged
    {
        private string firstLine;
        private string secondLine;

        public string FirstLine
        {
            get => firstLine;
        }
        public string SecondLine
        {
            get => secondLine;
        }

        public MainPageListIteam(string _firstLine, string _secondLine)
        {
            firstLine = _firstLine;
            secondLine = _secondLine;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
