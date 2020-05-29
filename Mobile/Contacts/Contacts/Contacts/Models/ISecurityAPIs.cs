/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
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

using System.Threading.Tasks;

namespace Contacts.Models
{
    /// <summary>
    /// Definition of an interface class for using each platform which can implement this interface using their own API set.
    /// In this project, Tizen implements the interface in the {ProjectBasePath}/Contacts/Contacts/Contacts.Tizen.Mobile/Port/SecurityPort.cs file.
    /// </summary>
    public interface ISecurityAPIs
    {
        /// <summary>
        /// Check privilege.
        /// </summary>
        Task<bool> CheckPrivilege();
    }
}
