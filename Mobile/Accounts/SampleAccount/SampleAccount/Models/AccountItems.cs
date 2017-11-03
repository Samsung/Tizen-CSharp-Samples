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

namespace AccountManager.Models
{
    /// <summary>
    /// Represents a Account Item
    /// </summary>
    public class AccountItem
    {
        /// <summary>
        /// Id of the account.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// User id/name of account 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User password of account
        /// </summary>
        public string UserPassword { get; set; }
    }
}