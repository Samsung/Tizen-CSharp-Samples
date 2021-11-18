/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
namespace ImageGallery.Models
{
    /// <summary>
    /// PrivilegeState class.
    /// Contains information about state of privileges needed by application.
    /// </summary>
    public static class PrivilegeState
    {
        #region fields

        /// <summary>
        /// Flag indicating whether the media storage permission is granted or not.
        /// </summary>
        public static bool MediastoragePermissionGranted;

        #endregion
    }
}
