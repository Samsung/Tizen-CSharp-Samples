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

using Accounts.Models;
using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace Accounts.Views
{
    /// <summary>
    /// Content page of account sign in.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountSignIn : CirclePage
    {
        /// <summary>
        /// Variable of AccountAPIManager.
        /// </summary>
        private static AccountApiManager accountApi;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountSignIn"/> class.
        /// </summary>
        public AccountSignIn()
        {
            InitializeComponent();

            accountApi = new AccountApiManager();
            accountApi.CheckAccountPrivilege();
        }

        /// <summary>
        /// Event when "SIGN IN" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments.</param>
        public async void SignInClicked(object sender, EventArgs args)
        {
            int id = -1;

            // Set account item
            AccountItem account = new AccountItem
            {
                UserName = loginEntry.Text,
                UserPassword = passwordEntry.Text,
            };

            if (string.IsNullOrEmpty(account.UserName) || string.IsNullOrEmpty(account.UserPassword))
            {
                // Check wrong Input parameter
                Toast.DisplayText("Please fill in all fields");
                return;
            }

            if (accountApi.CheckIfAccountAlreadyExists(account))
            {
                // Validate login input
                id = accountApi.GetAccountId(account);

                if (id == -1)
                {
                    Toast.DisplayText("Incorrect password!");
                    return;
                }
            }
            else
            {
                // Add account and receive account id.
                id = accountApi.AccountAdd(account);
            }

            // Load account sign out content page
            AccountSignOut accountSignOut = new AccountSignOut(id, account.UserName);
            await Navigation.PushAsync(accountSignOut);
        }

        /// <summary>
        /// Clears inputs on the AccountSignIn page when AccountSignOut page is loaded.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            loginEntry.Text = null;
            passwordEntry.Text = null;
        }
    }
}