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

            Button buttonLinkedin = new Button
            {
                Text = "        LinkedIn OAuth2 Test        ",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
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
        async void OnButtonGoogle1Clicked(object s, EventArgs e)
        {
            string response;
            labelGoogleButton1Clicked.Text = String.Format("Test is running");
            response = await oauth2APIs.GoogleGetAccessTokenTest();
            labelGoogleButton1Clicked.Text = String.Format("result : {0}", response);
        }
        async void OnButtonGoogle2Clicked(object s, EventArgs e)
        {
            string response;
            labelGoogleButton2Clicked.Text = String.Format("Test is running");
            response = await oauth2APIs.GoogleRefreshAccessTokenTest();
            labelGoogleButton2Clicked.Text = String.Format("result : {0}", response);
        }
        async void OnButtonTwitterClicked(object s, EventArgs e)
        {
            string response;
            labelTwitterButtonClicked.Text = String.Format("Test is running");
            response = await oauth2APIs.TwitterGetAccessTokenTest();
            labelTwitterButtonClicked.Text = String.Format("result : {0}", response);
        }
        async void OnButtonSalesforceClicked(object s, EventArgs e)
        {
            string response;
            labelSalesforceButtonClicked.Text = String.Format("Test is running");
            response = await oauth2APIs.SalesforceGetAccessTokenTest();
            labelSalesforceButtonClicked.Text = String.Format("result : {0}", response);
        }
        void OnButtonAuthorizationReqeustClicked(object s, EventArgs e)
        {
            string response;
            clicksAuthorizationRequest += 1;
            response = oauth2APIs.AuthorizationRequestTest();
            labelAuthorizationRequestlicked.Text = String.Format("result : {0}", response);
        }
        void OnButtonResetClicked(object s, EventArgs e)
        {
            labelGoogleButton1Clicked.Text = String.Format("Google Button unclicked");
            labelGoogleButton2Clicked.Text = String.Format("Google Button unclicked");
            labelTwitterButtonClicked.Text = String.Format("Twitter Button unclicked");
            labelSalesforceButtonClicked.Text = String.Format("Salesforce Button unclicked");
            labelAuthorizationRequestlicked.Text = String.Format("AuthorizationRequest Button unclicked");
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
