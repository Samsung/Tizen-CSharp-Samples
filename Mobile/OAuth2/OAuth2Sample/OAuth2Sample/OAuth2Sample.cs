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

using Xamarin.Forms;
using OAuth2Sample.Models;
using System.Threading.Tasks;
using Tizen.Account.OAuth2;
using Tizen.Security;

namespace OAuth2Sample
{
    /// <summary>
    /// OAuth2 Sample Application Class
    /// </summary>
    public class App : Application
    {
        private static IOAuth2APIs oauth2APIs;

        Label lableTitle;
        Label labelGoogleButton1Clicked;
        Label labelGoogleButton2Clicked;
        Label labelTwitterButtonClicked;
        Label labelSalesforceButtonClicked;
        Label labelAuthorizationRequestlicked;
        Label labelEmpty30;
        Label labelEmpty10_1;
        Label labelEmpty10_1_1;
        Label labelEmpty10_2;
        Label labelEmpty10_3;
        Label labelEmpty10_5;
        int clicksAuthorizationRequest = 0;
        int clickedFontSize = 20;

        public App()
        {
            oauth2APIs = DependencyService.Get<IOAuth2APIs>();

            // Didplay App Title
            lableTitle = new Label
            {
                Text = "OAuth2 Sample",
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };
            lableTitle.FontSize = 40;

            labelEmpty30 = new Label
            {
                Text = " ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelEmpty30.FontSize = 30;

            // Draw Button for Google Get Access Token Test
            Button buttonGoogle1 = new Button
            {
                Text = "  Google Get Access Token Test  ",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            buttonGoogle1.Clicked += OnButtonGoogle1Clicked;
            labelGoogleButton1Clicked = new Label
            {
                Text = "Google Button unclicked ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelGoogleButton1Clicked.FontSize = clickedFontSize;

            labelEmpty10_1_1 = new Label
            {
                Text = " ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelEmpty10_1_1.FontSize = 10;

            // Draw Button for Google Refresh Access Token Test
            Button buttonGoogle2 = new Button
            {
                Text = "  Google Refresh Access Token Test  ",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            buttonGoogle2.Clicked += OnButtonGoogle2Clicked;
            labelGoogleButton2Clicked = new Label
            {
                Text = "Google Button unclicked ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelGoogleButton2Clicked.FontSize = clickedFontSize;

            labelEmpty10_1 = new Label
            {
                Text = " ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelEmpty10_1.FontSize = 10;

            // Draw Button for Twitter Get Access Token Test
            Button buttonTwitter = new Button
            {
                Text = "    Twitter Get Access Token Test    ",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            buttonTwitter.Clicked += OnButtonTwitterClicked;
            labelTwitterButtonClicked = new Label
            {
                Text = "Twitter Button unclicked ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelTwitterButtonClicked.FontSize = clickedFontSize;

            labelEmpty10_2 = new Label
            {
                Text = " ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelEmpty10_2.FontSize = 10;

            // Draw Button for Salesforce Get Access Token Test
            Button buttonSalesforce = new Button
            {
                Text = " Salesforce Get Access Token Test ",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            buttonSalesforce.Clicked += OnButtonSalesforceClicked;
            labelSalesforceButtonClicked = new Label
            {
                Text = "Salesforce Button unclicked ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelSalesforceButtonClicked.FontSize = clickedFontSize;

            labelEmpty10_3 = new Label
            {
                Text = " ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelEmpty10_3.FontSize = 10;

            // Draw Button forAuthorization Request Test
            Button buttonAuthorizationReqeust = new Button
            {
                Text = " Authorization Request Test ",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            buttonAuthorizationReqeust.Clicked += OnButtonAuthorizationReqeustClicked;
            labelAuthorizationRequestlicked = new Label
            {
                Text = "Authorization Request Button unclicked ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelAuthorizationRequestlicked.FontSize = clickedFontSize;

            labelEmpty10_5 = new Label
            {
                Text = " ",
                HorizontalOptions = LayoutOptions.Center,
            };
            labelEmpty10_5.FontSize = 10;

            Button buttonReset = new Button
            {
                Text = "    Reset    ",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.DeepSkyBlue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            buttonReset.Clicked += OnButtonResetClicked;

            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Children = {
                        lableTitle,
                        labelEmpty30,
                        buttonAuthorizationReqeust,
                        labelAuthorizationRequestlicked,
                        labelEmpty10_5,
                        buttonGoogle1,
                        labelGoogleButton1Clicked,
                        labelEmpty10_1_1,
                        buttonGoogle2,
                        labelGoogleButton2Clicked,
                        labelEmpty10_1,
                        buttonTwitter,
                        labelTwitterButtonClicked,
                        labelEmpty10_2,
                        buttonSalesforce,
                        labelSalesforceButtonClicked,
                        labelEmpty10_3,
                        buttonReset
                    }
                }
            };
        }

        /// <summary>
        /// A method will be called when user clicks Google Get Access Token Test.
        /// </summary>
        async void OnButtonGoogle1Clicked(object s, EventArgs e)
        {
            string response;
            labelGoogleButton1Clicked.Text = String.Format("Test is running");
            response = await oauth2APIs.GoogleGetAccessTokenTest();
            labelGoogleButton1Clicked.Text = String.Format("result : {0}", response);
        }

        /// <summary>
        /// A method will be called when user clicks Google Refresh Access Token Test.
        /// </summary>
        async void OnButtonGoogle2Clicked(object s, EventArgs e)
        {
            string response;
            labelGoogleButton2Clicked.Text = String.Format("Test is running");
            response = await oauth2APIs.GoogleRefreshAccessTokenTest();
            labelGoogleButton2Clicked.Text = String.Format("result : {0}", response);
        }

        /// <summary>
        /// A method will be called when user clicks Twitter Get Access Token Test.
        /// </summary>
        async void OnButtonTwitterClicked(object s, EventArgs e)
        {
            string response;
            labelTwitterButtonClicked.Text = String.Format("Test is running");
            response = await oauth2APIs.TwitterGetAccessTokenTest();
            labelTwitterButtonClicked.Text = String.Format("result : {0}", response);
        }

        /// <summary>
        /// A method will be called when user clicks Salesforce Get Access Token Test.
        /// </summary>
        async void OnButtonSalesforceClicked(object s, EventArgs e)
        {
            string response;
            labelSalesforceButtonClicked.Text = String.Format("Test is running");
            response = await oauth2APIs.SalesforceGetAccessTokenTest();
            labelSalesforceButtonClicked.Text = String.Format("result : {0}", response);
        }

        /// <summary>
        /// A method will be called when user clicks Authorization Request Test.
        /// </summary>
        void OnButtonAuthorizationReqeustClicked(object s, EventArgs e)
        {
            string response;
            clicksAuthorizationRequest += 1;
            response = oauth2APIs.AuthorizationRequestTest();
            labelAuthorizationRequestlicked.Text = String.Format("result : {0}", response);
        }

        /// <summary>
        /// A method will be called when user clicks Reset Button.
        /// </summary>
        void OnButtonResetClicked(object s, EventArgs e)
        {
            labelGoogleButton1Clicked.Text = String.Format("Google Button unclicked");
            labelGoogleButton2Clicked.Text = String.Format("Google Button unclicked");
            labelTwitterButtonClicked.Text = String.Format("Twitter Button unclicked");
            labelSalesforceButtonClicked.Text = String.Format("Salesforce Button unclicked");
            labelAuthorizationRequestlicked.Text = String.Format("Authorization Request Button unclicked");
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
