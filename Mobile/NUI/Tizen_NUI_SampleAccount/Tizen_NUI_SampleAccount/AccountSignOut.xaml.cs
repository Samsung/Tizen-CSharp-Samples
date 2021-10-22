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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace Tizen_NUI_SampleAccount
{
    /// <summary>
    /// Content page of account sign out
    /// </summary>
    public partial class AccountSignOut : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountSignOut"/> class.
        /// </summary>
        public AccountSignOut(int accountId, string userId)
        {
            InitializeComponent();
            AccountLabel.Text = userId;
            SignOutButton.Clicked += SignOutClicked;
        }
        /// <summary>
        /// Event when "SIGN OUT" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        public void SignOutClicked(object sender, EventArgs args)
        {
            //
            var button = new Button()
            {
                Text = "OK",
            };
            button.Clicked += (object s, ClickedEventArgs a) =>
            {
                Navigator?.Pop();
                AccountSignIn xamlPage = new AccountSignIn();
                Navigator?.Push(xamlPage);
            };
            DialogPage.ShowAlertDialog("Account Deleted", "ID : " + AccountLabel.Text, button);
        }
    }
}
