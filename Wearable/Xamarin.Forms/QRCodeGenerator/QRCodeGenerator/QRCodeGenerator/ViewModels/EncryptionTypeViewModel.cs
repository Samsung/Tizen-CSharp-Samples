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
using QRCodeGenerator.Utils;

namespace QRCodeGenerator.ViewModels
{
    /// <summary>
    /// Provides encryption selection abstraction.
    /// </summary>
    public class EncryptionTypeViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of Name property.
        /// </summary>
        private EncryptionType _name;

        /// <summary>
        /// Backing field of IsSelected property.
        /// </summary>
        private bool _isSelected;

        #endregion

        #region properties

        /// <summary>
        /// Encryption type name.
        /// </summary>
        public EncryptionType Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Indicates if encryption type is selected.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes EncryptionTypeViewModel class instance.
        /// </summary>
        /// <param name="name">Encryption type name.</param>
        /// <param name="isSelected">Indicates if encryption type is selected.</param>
        public EncryptionTypeViewModel(EncryptionType name, bool isSelected)
        {
            Name = name;
            IsSelected = isSelected;
        }

        #endregion
    }
}