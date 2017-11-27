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

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tizen.Account.OAuth2;

namespace OAuth2Sample.Models
{
    /// <summary>
    /// OAuth2 Sample Interfaces
    /// </summary>
    public interface IOAuth2APIs
    {
        /// <summary>
        /// OAuth2 Sample Interface for Salesforce Get Access TokenTest
        /// </summary>
        Task<string> SalesforceGetAccessTokenTest();

        /// <summary>
        /// OAuth2 Sample Interface for Google Refresh Access TokenTest
        /// </summary>
        Task<string> GoogleRefreshAccessTokenTest();

        /// <summary>
        /// OAuth2 Sample Interface for Google Get Access TokenTest
        /// </summary>
        Task<string> GoogleGetAccessTokenTest();

        /// <summary>
        /// OAuth2 Sample Interface for Twitter Get Access TokenTest
        /// </summary>
        Task<string> TwitterGetAccessTokenTest();

        /// <summary>
        /// OAuth2 Sample Interface for Authorization RequestTest
        /// </summary>
        string AuthorizationRequestTest();
    }
}
