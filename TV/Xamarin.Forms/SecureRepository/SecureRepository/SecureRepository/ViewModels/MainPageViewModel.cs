/*
 *  Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 *  Contact: Ernest Borowski <e.borowski@partner.samsung.com>
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License
 *
 *
 * @file        Certificates.cs
 * @author      Ernest Borowski (e.borowski@partner.samsung.com)
 * @version     1.0
 * @brief       This file contains implementation of ViewModel
 */

namespace SecureRepository
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    /// <summary>
    /// ViewModel responsible for connecting with Platform dependent Model.
    /// </summary>
    public class MainPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Contains Application Information (UI).
        /// </summary>
        private string appInfo;

        /// <summary>
        /// Contains selected Item prefix.
        /// </summary>
        private string contentPrefix;

        /// <summary>
        /// Contains instance of IMainInterface.
        /// </summary>
        private IMainInterface dep;

        /// <summary>
        /// Contains Selected Item.
        /// </summary>
        private Item selectedItem;

        /// <summary>
        /// Contains Items List (UI).
        /// </summary>
        private ObservableCollection<ListViewGroup> uIList;

        /// <summary>
        /// Initializes a new instance of the MainPageViewModel class.
        /// </summary>
        public MainPageViewModel()
        {
            this.dep = DependencyService.Get<IMainInterface>();
            this.CommandAdd = new Command(this.CallAdd);
            this.CommandEncryptDecrypt = new Command(this.CallEncryptDecrypt);
            this.CommandRemove = new Command(this.CallRemove);
            this.UpdateListView();
        }

        /// <summary>
        /// Handles property value change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets selected Item.
        /// </summary>
        public Item SelectedItem
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                this.selectedItem = value;
                this.GetItemContent();
            }
        }

        /// <summary>
        /// Gets Item List (UI).
        /// </summary>
        public ObservableCollection<ListViewGroup> UIList
        {
            get
            {
                return this.uIList;
            }

            private set
            {
                this.uIList = value;
                this.NotifyPropertyChanged("UIList");
            }
        }

        /// <summary>
        /// Gets Application information (UI).
        /// </summary>
        public string AppInfo
        {
            get
            {
                return this.appInfo;
            }

            private set
            {
                this.appInfo = value;
                this.NotifyPropertyChanged("AppInfo");
            }
        }

        /// <summary>
        /// Gets content prefix (UI).
        /// </summary>
        public string ContentPrefix
        {
            get
            {
                return this.contentPrefix;
            }

            private set
            {
                this.contentPrefix = value;
                this.NotifyPropertyChanged("ContentPrefix");
            }
        }

        /// <summary>
        /// Gets Add command.
        /// </summary>
        public ICommand CommandAdd
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets EncryptDecrypt command.
        /// </summary>
        public ICommand CommandEncryptDecrypt
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets Remove command.
        /// </summary>
        public ICommand CommandRemove
        {
            get;
            private set;
        }

        /// <summary>
        /// Adds Items.
        /// </summary>
        public void CallAdd()
        {
            this.dep.Add();
            this.AppInfo = string.Empty;
            this.ContentPrefix = string.Empty;
            this.UpdateListView();
        }

        /// <summary>
        /// Encrypts / Decrypts data.
        /// </summary>
        private void CallEncryptDecrypt()
        {
            this.dep.EncryptDecrypt();
            this.AppInfo = this.dep.EncryptedText;
            this.ContentPrefix = this.dep.DecryptedText;
            this.UpdateListView();
        }

        /// <summary>
        /// Removes all Items.
        /// </summary>
        private void CallRemove()
        {
            this.dep.Remove();
            this.AppInfo = string.Empty;
            this.ContentPrefix = string.Empty;
            this.UpdateListView();
        }

        /// <summary>
        /// Gets content of currently selected Item.
        /// </summary>
        private void GetItemContent()
        {
            Item selectedItem = this.SelectedItem;

            if (selectedItem == null)
            {
                return;
            }

            string content;
            content = this.dep.GetSelectedItemPrefix(selectedItem);
            ////Check if result is not empty.
            if (content != null && content != string.Empty)
            {
                this.ContentPrefix = "Item prefix: " + content;
            }
            else
            {
                this.ContentPrefix = string.Empty;
            }

            content = this.dep.GetSelectedItemType(selectedItem);
            ////Check if result is not empty.
            if (content != null && content != string.Empty)
            {
                this.AppInfo = "Item type: " + content;
            }
            else
            {
                this.AppInfo = string.Empty;
            }
        }

        /// <summary>
        /// Notifies UI when property value changes.
        /// </summary>
        /// <param name="propertyName">Name of changed property.</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Updates List View (UI).
        /// </summary>
        private void UpdateListView()
        {
            this.UIList = new ObservableCollection<ListViewGroup>(this.dep.ItemList);
            this.NotifyPropertyChanged("AppInfo");
        }
    }
}
