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

using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace ApplicationControl
{
    /// <summary>
    /// A part of class for main page UI layout
    /// </summary>
    public partial class MainPage
    {
        bool _contentLoaded;

        /// <summary>
        /// A constructor for the MainPage class
        /// </summary>
        /// <param name="screenWidth">screen width</param>
        /// <param name="screenHeight">screen height</param>
        public MainPage(int screenWidth, int screenHeight)
        {
            if (_contentLoaded)
            {
                return;
            }

            _contentLoaded = true;

            BindingContext = new MainViewModel();

            InitializeComponent(screenWidth, screenHeight);
        }
    }

    /// <summary>
    /// A class for an application list item
    /// </summary>
    public class ApplicationListItem : INotifyPropertyChanged
    {
        Color _blendColor;

        /// <summary>
        /// The application ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The icon path
        /// </summary>
        public string IconPath { get; set; }

        /// <summary>
        /// The color be blended with an image
        /// </summary>
        public Color BlendColor
        {
            get
            {
                return _blendColor;
            }

            set
            {
                if (_blendColor == value)
                {
                    return;
                }

                _blendColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BlendColor"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// A class for a composed message will be sent
    /// </summary>
    public class Message
    {
        /// <summary>
        /// A constructor for the Message class
        /// </summary>
        public Message()
        {
            Initialize();
        }

        /// <summary>
        /// An email address to send
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// An Subject for the message
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// An text content to send
        /// </summary>
        public string Text { get; set; }

        void Initialize()
        {
            Subject = "Message from appcontrol";
            Text = "Dear Developer,\n\nThis is the default message sent from\nappcontrol sample application.\nFeel free to modify this text message in email composer.\n\nBest Regards.";
        }
    }

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
                    _selectedItem.BlendColor = Color.Gray;
                }

                _selectedItem = value;
                _selectedItem.BlendColor = Color.Default;
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