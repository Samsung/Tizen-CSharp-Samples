//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections;
using System.Linq;
using Tizen.Account.AccountManager;
using Tizen.Security;

namespace Accounts.Models
{
    /// <summary>
    /// Represents the account manager APIs for connecting Sample Account.
    /// </summary>
    public class AccountApiManager
    {
        /// <summary>
        /// Context for privacy privilege manager response.
        /// </summary>
        private static PrivacyPrivilegeManager.ResponseContext context = null;

        /// <summary>
        /// Callback count about request privacy privilege.
        /// </summary>
        private static int callbackCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountApiManager"/> class.
        /// </summary>
        public AccountApiManager()
        {
            // Constructor
        }

        /// <summary>
        /// Adds an account with UserId and UserPassword received from accountItem. 
        /// You should get these from server communication in actual account provider.
        /// </summary>
        /// <param name="accountItem"> Account item to add.</param>
        /// <returns> The account ID of the account instance. </returns>
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
                {
                    return id;
                }

                // Add account with inputed id and password.
                // In sample app, just add id as user_name and password as access_token.
                // But, you should get these from server communication in actual account provider.
                account.UserName = accountItem.UserName;
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
        /// <param name="accountItem"> Account item to sign out.</param>
        public void AccountSignOut(AccountItem accountItem)
        {
            try
            {
                if (accountItem == null)
                {
                    throw new ArgumentNullException(nameof(accountItem));
                }

                // GetAccountByID return account instance with reference to the given ID
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
        /// Checks if account is already in database.
        /// </summary>
        /// <param name="accountItem">Account item to check.</param>
        /// <returns>True if account is already in database, false otherwise.</returns>
        public bool CheckIfAccountAlreadyExists(AccountItem accountItem)
        {
            try
            {
                var accounts = AccountService.GetAccountsAsync();
                return accounts.Any(a => a.UserName == accountItem.UserName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message : " + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Gets AccountId from database for selected account item.
        /// </summary>
        /// <param name="account">Account item to find in database.</param>
        /// <returns>Returns AccountId for account item. Returns -1 if 
        /// account with specified userId and userPass doesn't exists in database or
        /// when error occurred.</returns>
        public int GetAccountId(AccountItem account)
        {
            try
            {
                var accounts = AccountService.GetAccountsByUserName(account.UserName);

                var foundAccount = accounts.FirstOrDefault(
                    a => a.AccessToken == account.UserPassword);

                if (foundAccount != null)
                {
                    return foundAccount.AccountId;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message : " + e.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Check privacy privilege and if need to ask for user, 
        /// send request for Privacy Privilege Manager.
        /// </summary>
        public void CheckAccountPrivilege()
        {
            // Make array list for requesting privacy privilege
            // Account sample need 2 privilege, account read and account write
            ArrayList privilegeList = new ArrayList
            {
                "http://tizen.org/privilege/account.read",

                // Account.read and account.write is same ppm. So if we use both, appear same pop up 2 times. So security team guide use just one.
                // "http://tizen.org/privilege/account.write"
            };

            // Check and request privacy privilege if app is needed
            foreach (string list in privilegeList)
            {
                PrivacyPrivilegeManager.GetResponseContext(list).TryGetTarget(out context);
                if (context != null)
                {
                    ++callbackCount;
                    context.ResponseFetched += PpmRequestResponse;
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
        /// Privacy Privilege Manager request response callback.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event.</param>
        /// <param name="e"> Event arguments.</param>
        private static void PpmRequestResponse(object sender, RequestResponseEventArgs e)
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
                }

                --callbackCount;
                if (callbackCount == 0)
                {
                    // Remove Callback
                    context.ResponseFetched -= PpmRequestResponse;
                }
            }
            else
            {
                Console.WriteLine("Error occured during requesting permission for {0}", e.privilege);
            }
        }
    }
}
