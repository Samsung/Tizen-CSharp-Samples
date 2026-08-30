/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Tizen.NUI;
using ApplicationControl.Models;
using ApplicationControl.Helpers;

namespace ApplicationControl.ViewModels
{
    /// <summary>
    /// A class for main view model
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        ObservableCollection<ApplicationListItem> _applications;
        Message _message;
        AppControlType _selectedAppControlType;
        ApplicationListItem _selectedItem;

        /// <summary>
        /// A constructor for the MainViewModel class
        /// </summary>
        public MainViewModel()
        {
            Initialize();
        }

        /// <summary>
        /// To execute the selected application
        /// </summary>
        public void ExecuteSelectedApplication()
        {
            if (SelectedItem == null || SelectedItem?.Id == null || SelectedAppControlType == AppControlType.Unknown)
            {
                return;
            }

            Console.WriteLine($"Execute id:{SelectedItem.Id}, type:{SelectedAppControlType}");
            ApplicationControlHelper.ExecuteApplication(SelectedAppControlType, SelectedItem.Id);
        }

        /// <summary>
        /// To kill the selected application
        /// </summary>
        public void KillSelectedApplication()
        {
            if (SelectedItem == null || SelectedItem?.Id == null)
            {
                return;
            }

            ApplicationControlHelper.KillApplication(SelectedItem.Id);
        }

        /// <summary>
        /// To send a message
        /// </summary>
        public void SendMessage()
        {
            ApplicationControlHelper.ExecuteApplication(AppControlType.Send, null, Message);
        }

        /// <summary>
        /// The selected application control type
        /// </summary>
        public AppControlType SelectedAppControlType
        {
            get
            {
                return _selectedAppControlType;
            }

            set
            {
                if (_selectedAppControlType == value)
                {
                    return;
                }

                _selectedAppControlType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedAppControlType"));
            }
        }

        /// <summary>
        /// Applications information for a specific application control
        /// </summary>
        public ObservableCollection<ApplicationListItem> Applications
        {
            get
            {
                return _applications;
            }
        }

        /// <summary>
        /// The selected application item on the application list
        /// </summary>
        public ApplicationListItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if (_selectedItem == value)
                {
                    return;
                }

                if (_selectedItem != null)
                {
                    _selectedItem.BlendColor = Resources.Gray;
                }

                _selectedItem = value;
                if (_selectedItem != null)
                {
                    _selectedItem.BlendColor = Resources.Transparent;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }

        /// <summary>
        /// A Message to send
        /// </summary>
        public Message Message
        {
            get
            {
                return _message;
            }
        }

        /// <summary>
        /// To initialize some fields when the class is instantiated
        /// </summary>
        void Initialize()
        {
            _applications = new ObservableCollection<ApplicationListItem>();
            _message = new Message { };
            _selectedAppControlType = AppControlType.Unknown;
            _selectedItem = new ApplicationListItem { Id = null };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
