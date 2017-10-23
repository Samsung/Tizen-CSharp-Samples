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
    /// Content page of account sign in
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountSignIn : ContentPage
	{
        private static IAccountManagerAPIs accountAPIs;
        string userId;
        string userPass;

        /// <summary>
        /// Account sign in constructor.
        /// </summary>
        public AccountSignIn ()
		{
			InitializeComponent ();

            var entryId = new Entry();
            var entryPass = new Entry();

            entryId.TextChanged += ID_TextChanged;
            entryPass.TextChanged += Pass_TextChanged;

            accountAPIs = DependencyService.Get<IAccountManagerAPIs>();
        }

        /// <summary>
        /// Set user id received user input
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event data of ID text changed </param>
        void ID_TextChanged(object sender, TextChangedEventArgs e) => userId = e.NewTextValue;

        /// <summary>
        /// Set password received user input
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event data of Password text changed </param>
        void Pass_TextChanged(object sender, TextChangedEventArgs e) => userPass = e.NewTextValue;

        /// <summary>
        /// Event when "SIGN IN" button is cliked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        async void CreateClicked(object sender, EventArgs args)
        {
            int id;
            Button button = (Button)sender;

            // Set account item
            AccountItem account = new AccountItem();
            account.UserId = userId;
            account.UserPassword = userPass;

            if (String.IsNullOrEmpty(account.UserId) || String.IsNullOrEmpty(account.UserPassword)) { 
                await DisplayAlert("Error!",
                "Please Input ID / Password Correctly",
                "OK");
            } else {
                // Add account and receive account id.
                id = accountAPIs.AccountAdd(account);

                // Load account sign out content page
                AccountSignOut accountSignOut = new AccountSignOut(id, userId);
                await Navigation.PushAsync(accountSignOut);
            }
        }
    }
}