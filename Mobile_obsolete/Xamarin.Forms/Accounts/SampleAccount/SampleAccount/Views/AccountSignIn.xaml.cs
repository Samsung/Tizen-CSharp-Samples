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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleAccount.Views
{
    /// <summary>
    /// Content page of account sign in
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountSignIn : ContentPage
	{
        /// <summary>
        ///  Interface variable of account manager
        /// </summary>
        private static IAccountManagerAPIs accountAPIs; 

        /// <summary>
        /// User ID value
        /// </summary>
        private string userId;

        /// <summary>
        /// User password value
        /// </summary>
        private string userPass;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountSignIn"/> class.
        /// </summary>
        public AccountSignIn()
		{
            InitializeComponent();

            var entryId = new Entry();
            var entryPass = new Entry();

            IdTextChange(entryId);
            PassTextChange(entryPass);

            accountAPIs = DependencyService.Get<IAccountManagerAPIs>();

            accountAPIs.CheckAccountPrivilege();
        }

        /// <summary>
        /// Event when "SIGN IN" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        public async void CreateClicked(object sender, EventArgs args)
        {
            int id;
            Button button = (Button)sender;

            // Set account item
            AccountItem account = new AccountItem();
            account.UserId = userId;
            account.UserPassword = userPass;

            if (string.IsNullOrEmpty(account.UserId) || string.IsNullOrEmpty(account.UserPassword))
            {
                // Check wrong Input parameter
                await DisplayAlert("Error!", "Please Input ID / Password Correctly", "OK");
            }
            else
            {
                // Add account and receive account id.
                id = accountAPIs.AccountAdd(account);

                // Load account sign out content page
                AccountSignOut accountSignOut = new AccountSignOut(id, userId);
                await Navigation.PushAsync(accountSignOut);
            }
        }

        /// <summary>
        /// Back button event
        /// </summary>
        /// <returns> true if event is succeed.</returns>
        protected override bool OnBackButtonPressed()
        {
            System.Environment.Exit(1);
            return true;
        }

        /// <summary>
        /// Received changed password in UI
        /// </summary>
        /// <param name="entryPass"> entry of password</param>
        private void PassTextChange(Entry entryPass) => entryPass.TextChanged += Pass_TextChanged;

        /// <summary>
        /// Received changed id in UI
        /// </summary>
        /// <param name="entryId"> entry of id </param>
        private void IdTextChange(Entry entryId) => entryId.TextChanged += ID_TextChanged;

        /// <summary>
        /// Set user id received user input
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event data of ID text changed </param>
        private void ID_TextChanged(object sender, TextChangedEventArgs e) => userId = e.NewTextValue;

        /// <summary>
        /// Set password received user input
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event data of Password text changed </param>
        private void Pass_TextChanged(object sender, TextChangedEventArgs e) => userPass = e.NewTextValue;
    }
}