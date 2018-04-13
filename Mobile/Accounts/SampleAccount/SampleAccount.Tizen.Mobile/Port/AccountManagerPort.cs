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

using AccountManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.Account.AccountManager;
using Tizen.Security;
using Tizen.System;

namespace AccountManager.Tizen.Port
{
    /// <summary>
    /// Represents the account manager APIs for connecting Sample Account.
    /// </summary>
    public class AccountManagerPort : IAccountManagerAPIs
    {
        /// <summary>
        /// Context for privacy privilege manager reponse
        /// </summary>
        static PrivacyPrivilegeManager.ResponseContext context = null;

        /// <summary>
        /// Call back count about request privacy privilege
        /// </summary>
        static int CBCount = 0;

        /// <summary>
        /// AccountManagerPort constructor.
        /// </summary>
        public AccountManagerPort()
        {
            // Constructor
        }

        /// <summary>
        /// Add account with received id and password. You should get these from server communication in actual account provider.
        /// </summary>
        /// <param name="accountItem"> Account item be added.</param>
        /// <returns> The account ID of the account instance </returns>
        public int AccountAdd(AccountItem accountItem)
        {
            if (accountItem == null)
            {
                throw new ArgumentNullException(nameof(accountItem));
            }

            try
            {
                int id = -1;

                // Create account instance
                Account account = Account.CreateAccount();
                if (account == null)
                    return id;

                // Add account with inputed id and password.
                // In sample app, just add id as user_name and password as access_token.
                // But, you should get these from server communication in actual account provider.
                account.UserName = accountItem.UserId;
                account.AccessToken = accountItem.UserPassword;
                account.SyncState = AccountSyncState.Idle;

                // Insert account DB with the new account information.
                // AddAccount returns account id.
                id = AccountService.AddAccount(account);

                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message : " + e.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Deletes the account information from the database.
        /// </summary>
        /// <param name="accountItem"> Account item be added.</param>
        public void AccountDelete(AccountItem accountItem)
        {
            try
            {
                if (accountItem == null)
                {
                    throw new ArgumentNullException(nameof(accountItem));
                }

                // Create account instance with the account ID.
                // GetAccountByID retrieves the account with the account ID.
                // GetAccountByID returned account instance with reference to the given ID
                Account account = AccountService.GetAccountById(accountItem.AccountId);

                // Deletes the account information from the account DB.
                AccountService.DeleteAccount(account);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message : " + e.ToString());
            }
        }

        /// <summary>
        /// Check privacy privilege and if need to ask for user, send request for PPM
        /// </summary>
        public void CheckAccountPrivilege()
        {
            // Make array list for requesting privacy privilege
            // Account sample need 2 privilege, account read and account write
            ArrayList PrivilegeList = new ArrayList();
            PrivilegeList.Add("http://tizen.org/privilege/account.read");
            // Account.read and account.write is same ppm. So if we use both, appear same pop up 2 times. So security team guide use just one.
            //PrivilegeList.Add("http://tizen.org/privilege/account.write");

            // Check and request privacy privilege if app is needed
            foreach (string list in PrivilegeList)
            {
                PrivacyPrivilegeManager.GetResponseContext(list).TryGetTarget(out context);
                if (context != null)
                {
                    ++CBCount;
                    context.ResponseFetched += PPM_RequestResponse;
                }

                CheckResult result = PrivacyPrivilegeManager.CheckPermission(list);
                switch (result)
                {
                    case CheckResult.Allow:
                        // Privilege can be used
                        break;
                    case CheckResult.Deny:
                        // Privilege can't be used
                        break;
                    case CheckResult.Ask:
                        // User permission request required
                        PrivacyPrivilegeManager.RequestPermission(list);
                        break;
                }
            }
        }

        /// <summary>
        /// PPM request response call back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PPM_RequestResponse(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Answer)
            {
                switch (e.result)
                {
                    case RequestResult.AllowForever:
                        Console.WriteLine("User allowed usage of privilege {0} definitely", e.privilege);
                    break;
                    case RequestResult.DenyForever:
                        // If privacy privilege is denied, app is terminated.
                        Console.WriteLine(" User denied usage of privilege {0} definitely", e.privilege);
                        System.Environment.Exit(1);
                    break;
                    case RequestResult.DenyOnce:
                        // If privacy privilege is denied, app is terminated.
                        Console.WriteLine("User denied usage of privilege {0} this time", e.privilege);
                        System.Environment.Exit(1);
                    break;
                };

                --CBCount;
                if (0 == CBCount)
                {
                    // Remove Callback
                    context.ResponseFetched -= PPM_RequestResponse;
                }
            }
            else
            {
                Console.WriteLine("Error occured during requesting permission for {0}", e.privilege);
            }
        }
    }
}
