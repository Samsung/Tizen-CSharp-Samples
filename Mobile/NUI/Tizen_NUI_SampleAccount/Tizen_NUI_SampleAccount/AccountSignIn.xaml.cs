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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using static Tizen.NUI.BaseComponents.TextField;
using Tizen.Account.AccountManager;

namespace Tizen_NUI_SampleAccount
{
    /// <summary>
    /// Content page of account sign in
    /// </summary>
    public partial class AccountSignIn : ContentPage
    {
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
            ID_TextField.TextChanged += ID_TextChanged;
            PASSWORD_TextField.TextChanged += Pass_TextChanged;
            Submit.Clicked += OnClicked;

            //PropertyMap for hidding characters in password input filed
            PropertyMap hiddenInputSettings = new PropertyMap();
            hiddenInputSettings.Add(HiddenInputProperty.Mode, new PropertyValue((int)HiddenInputModeType.ShowLastCharacter));
            hiddenInputSettings.Add(HiddenInputProperty.ShowLastCharacterDuration, new PropertyValue(500));
            hiddenInputSettings.Add(HiddenInputProperty.SubstituteCharacter, new PropertyValue(0x2A));
            PASSWORD_TextField.HiddenInputSettings = hiddenInputSettings;
        }
        /// <summary>
        /// Event when "SIGN IN" button is clicked.
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="args"> Event arguments</param>
        private void OnClicked(object sender, ClickedEventArgs e)
        {
            int id = 0;
            // Set account item
            

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userPass))
            {
                // Check wrong Input parameter
                var button = new Button()
                {
                    Text = "OK",
                };
                button.Clicked += (object s, ClickedEventArgs a) =>
                {
                    Navigator?.Pop();
                };
                DialogPage.ShowAlertDialog("Error!", "Please Input ID / Password Correctly", button);
            }
            else
            {
                Account account = Account.CreateAccount();
                account.UserName = userId;
                account.AccessToken = userPass;
                AccountSignOut accountSignOut = new AccountSignOut(id, userId);
                Navigator?.Push(accountSignOut);
            }

        }
        /// <summary>
        /// Set user id received user input
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event data of ID text changed </param>
        private void ID_TextChanged(object sender, TextChangedEventArgs e) => userId = e.TextField.Text;
        /// <summary>
        /// Set password received user input
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event data of Password text changed </param>
        private void Pass_TextChanged(object sender, TextChangedEventArgs e) => userPass = e.TextField.Text;
    }
}
