/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace VoiceRecorder.Tizen.Mobile.Service.Privilege
{
    /// <summary>
    /// Class describing a privilege status.
    /// </summary>
    public class PrivilegeItem
    {
        #region properties

        /// <summary>
        /// The privilege key.
        /// </summary>
        public string Privilege { get; }

        /// <summary>
        /// Flag indicating whether the permission is granted or not.
        /// </summary>
        public bool Granted { get; private set; }

        /// <summary>
        /// Flag indicating whether the permission has been checked or not.
        /// </summary>
        public bool Checked { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes PrivilegeItem class instance.
        /// </summary>
        /// <param name="privilege">Privilege key.</param>
        public PrivilegeItem(string privilege)
        {
            Privilege = privilege;
        }

        /// <summary>
        /// Sets the flag indicating whether permission has been granted or not.
        /// </summary>
        /// <param name="value">True if permission has been granted, false otherwise.</param>
        public void GrantPermission(bool value)
        {
            Granted = value;
            Checked = true;
        }

        #endregion
    }
}
