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

using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VoiceMemo.Models
{
    /// <summary>
    /// Record class
    /// It presents information about voice recording file
    /// </summary>
    public class Record : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int _id { get; set; }
        // the title of audio file
        public string Title { get; set; }
        // the date of audio file creation
        public string Date { get; set; }
        // the duration of recorded file
        public int Duration { get; set; }
        // the file path
        public string Path { get; set; }
        // indicate that speech-to-text service is enabled or not when recording the audio file
        public bool SttEnabled { get; set; }

        string _Text;
        // Text converted by Speech-to-text service
        public string Text
        {
            get
            {
                return _Text;
            }

            set
            {
                SetProperty(ref _Text, value, "Text");
            }
        }

        bool _Checked;
        /// <summary>
        /// Indicate that it's selected or not to delete
        /// If it's true, it will be deleted.
        /// It's not stored in database
        /// </summary>
        [Ignore]
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                bool changed = SetProperty(ref _Checked, value, "Checked");
                if (changed)
                {
                    //Console.WriteLine("Record.Checked : " + Checked + " --> CheckedNamesCount changed..");
                    ((App)App.Current).mainPageModel.CheckedNamesCount += Checked ? 1 : -1;
                }
            }
        }

        public override string ToString()
        {
            return "Record[" + ID + "," + _id + "] " + Title + ", " + Path + ", " + Date + ", Stt(" + SttEnabled + ") " + "(" + Text + ")";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value,
                                      [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Called to notify that a change of property happened
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
