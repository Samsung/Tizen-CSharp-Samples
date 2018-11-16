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
using System.Collections.Generic;
using QRCodeGenerator.Utils;
using System;
using QRCodeGenerator.Services;
using Xamarin.Forms;

namespace QRCodeGenerator.Models
{
    /// <summary>
    /// Connection credentials storage.
    /// This is singleton. Instance is accessible via <see cref="MainModel.Instance">Instance</see></cref> property.
    /// </summary>
    public sealed class MainModel
    {
        #region fields

        /// <summary>
        /// Backing field of Instance property.
        /// </summary>
        private static MainModel _instance;

        /// <summary>
        /// Reference to the service providing QR codes operations.
        /// </summary>
        private readonly IQRService _QR;

        /// <summary>
        /// Backing field of SSID property.
        /// </summary>
        private string _ssid;

        /// <summary>
        /// Backing field of Password property.
        /// </summary>
        private string _password;

        /// <summary>
        /// Backing field of Encryption property.
        /// </summary>
        private EncryptionType _encryption;

        #endregion

        #region properties

        /// <summary>
        /// List of available encryption types.
        /// </summary>
        public List<EncryptionType> EncryptionTypeList { get; }

        /// <summary>
        /// Event invoked when setting value is submitted.
        /// </summary>
        public event EventHandler SettingChanged;

        /// <summary>
        /// SSID text value.
        /// </summary>
        public string SSID
        {
            get => _ssid;
            set
            {
                _ssid = value;
                SettingChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Password text value.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                SettingChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Encryption enumeration value.
        /// </summary>
        public EncryptionType Encryption
        {
            get => _encryption;
            set
            {
                _encryption = value;

                if (value == EncryptionType.None)
                {
                    Password = string.Empty;
                }

                SettingChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// QR type connection string value.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return "WIFI: S:" + SSID + "; T:" +
                (Encryption == EncryptionType.None ? string.Empty : Encryption.ToString()) +
                "; P:" + Password + "; ;";
            }
        }

        /// <summary>
        /// MainModel instance accessor.
        /// </summary>
        public static MainModel Instance
        {
            get => _instance ?? (_instance = new MainModel());
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes instance of the model.
        /// Encryption 'None' is selected by default.
        /// </summary>
        private MainModel()
        {
            SSID = string.Empty;
            Password = string.Empty;
            Encryption = EncryptionType.None;

            EncryptionTypeList = new List<EncryptionType>
            {
                EncryptionType.None,
                EncryptionType.WPA,
                EncryptionType.WEP
            };

            _QR = DependencyService.Get<IQRService>();
        }

        /// <summary>
        /// Determines if all required credentials were provided.
        /// </summary>
        /// <returns>Bool value indicating if generating QR code is allowed.</returns>
        public bool CanBeGenerated()
        {
            if (Encryption == EncryptionType.None)
            {
                if (!string.IsNullOrEmpty(SSID))
                {
                    return true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(SSID) && !string.IsNullOrEmpty(Password))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Generates QR code image.
        /// </summary>
        /// <returns>Path to QR code image.</returns>
        public string Generate()
        {
            return _QR.Generate(ConnectionString);
        }

        #endregion
    }
}