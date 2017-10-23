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
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.Account.AccountManager;
using Tizen.System;

namespace AccountManager.Tizen.Port
{
    /// <summary>
    /// Represents the account manager APIs for connecting Sample Account.
    /// </summary>
    public class AccountManagerPort : IAccountManagerAPIs
    {
        /// <summary>
        /// AccountManagerPort constructor.
        /// </summary>
        public AccountManagerPort()
        {

        }

        /// <summary>
        /// Add account with inputed id and password. You should get these from server communication in actual account provider.
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
                Account account = AccountService.GetAccountById(accountItem.AccountId);

                // Deletes the account information from the account DB.
                AccountService.DeleteAccount(account);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message : " + e.ToString());
            }
        }
    }
}
