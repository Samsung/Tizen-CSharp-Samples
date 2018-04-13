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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AccountManager.Models;

namespace SampleAccount.Views
{
    /// <summary>
    /// Content page of account sign out
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountSignOut : ContentPage
	{
        /// <summary>
        ///  Interface variable of account manager
        /// </summary>
        private static IAccountManagerAPIs accountAPIs;

        /// <summary>
        /// Represents account Item
        /// </summary>
        private AccountItem account;

        /// <summary>
        /// Account sign out constructor. 
        /// </summary>
        /// <param name="accountId"> Account id from account manager</param>
        /// <param name="userId"> User id from user input</param>
        public AccountSignOut(int accountId, string userId)
		{
            InitializeComponent();
            AccountLabel.Text = userId;

            account = new AccountItem
            {
                AccountId = accountId,
                UserId = userId
            };

            accountAPIs = DependencyService.Get<IAccountManagerAPIs>();
        }

        /// <summary>
        /// Event when "SIGN OUT" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments. </param>
        public async void SignOutClicked(object sender, EventArgs args)
        {
            accountAPIs.AccountDelete(account);

            AccountSignIn accountSignIn = new AccountSignIn();
            await DisplayAlert("Account Deleted", "ID : " + account.UserId, "OK");
            await Navigation.PushAsync(accountSignIn);
        }

        /// <summary>
        /// Back button event
        /// </summary>
        /// <returns> true if navigation pushasync is succeed</returns>
        protected override bool OnBackButtonPressed()
        {
            AccountSignIn accountSignIn = new AccountSignIn();
            Navigation.PushAsync(accountSignIn);
            return true;
        }
    }
}