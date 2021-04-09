/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using QRCodeGenerator.Views;
using System.Collections.Generic;
using QRCodeGenerator.Models;
using Xamarin.Forms;
using QRCodeGenerator.Utils;

namespace QRCodeGenerator.ViewModels
{
    /// <summary>
    /// Provides encryption page view abstraction.
    /// </summary>
    public class EncryptionViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        /// <summary>
        /// Backing field of SelectedEncryptionType property.
        /// </summary>
        private EncryptionTypeViewModel _selectedEncryptionType;

        #endregion

        #region properties

        /// <summary>
        /// List of available encryption types.
        /// </summary>
        public List<EncryptionTypeViewModel> EncryptionTypeList { get; }

        /// <summary>
        /// Selected encryption type.
        /// Setting property also navigates to previous page.
        /// </summary>
        public EncryptionTypeViewModel SelectedEncryptionType
        {
            get => _selectedEncryptionType;
            set
            {
                SetProperty(ref _selectedEncryptionType, value);

                ClearAllSelection();
                value.IsSelected = true;

                MainModel.Instance.Encryption = value.Name;

                _navigation.GoToPreviousPage();
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes EncryptionViewModel class instance.
        /// </summary>
        public EncryptionViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();

            EncryptionTypeList = new List<EncryptionTypeViewModel>();

            InitializeCollection();
        }

        /// <summary>
        /// Initializes EncryptionTypeList from model.
        /// </summary>
        private void InitializeCollection()
        {
            foreach (EncryptionType item in MainModel.Instance.EncryptionTypeList)
            {
                if (item == MainModel.Instance.Encryption)
                {
                    EncryptionTypeList.Add(new EncryptionTypeViewModel(item, true));
                }
                else
                {
                    EncryptionTypeList.Add(new EncryptionTypeViewModel(item, false));
                }
            }
        }

        /// <summary>
        /// Clears all selected options.
        /// </summary>
        private void ClearAllSelection()
        {
            foreach (EncryptionTypeViewModel item in EncryptionTypeList)
            {
                item.IsSelected = false;
            }
        }

        #endregion
    }
}