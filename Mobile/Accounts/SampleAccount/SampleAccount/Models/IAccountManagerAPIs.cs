/* 
  * Copyright (c) 2016 Samsung Electronics Co., Ltd 
  * 
  * Licensed under the Flora License, Version 1.1 (the "License"); 
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
using System.Threading.Tasks;

namespace AccountManager.Models
{
    /// <summary>
    /// Class of account interfaces for each platform.
    /// </summary>
    public interface IAccountManagerAPIs
    {
        /// <summary>
        /// Inserts into the Database with the new account Information.
        /// </summary>
        /// <param name="accountItem"> The account item.</param>
        /// <returns> The account ID of the account item </returns>
        int AccountAdd(AccountItem accountItem);

        /// <summary>
        /// Deletes the account information from the database.
        /// </summary>
        /// <param name="accountItem"> The account handle.</param>
        void AccountDelete(AccountItem accountItem);

        void CheckAccountPrivilege();
    }
}