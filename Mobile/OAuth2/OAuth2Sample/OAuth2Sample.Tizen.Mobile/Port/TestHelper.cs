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
using Tizen.Account.OAuth2;

namespace OAuth2Sample.Tizen.Mobile.Port
{
    internal static class TestHelper
    {
        // Predefined information for testing
        internal const string CODE_GRANT_RESPONSE_TYPE_SPEC = "code";
        internal const string IMPLICIT_GRANT_RESPONSE_TYPE_SPEC = "token";

        internal const string CODE_GRANT_GRANT_TYPE_SPEC = "authorization_code";
        internal const string CLIENT_CREDS_GRANT_TYPE_SPEC = "client_credentials";
        internal const string RESOURCE_OWNER_GRANT_TYPE_SPEC = "password";
        internal const string REFRESH_GRANT_TYPE_SPEC = "refresh_token";

        internal const string CLIENT_ID = "53492317821.apps.googleusercontent.com";
        internal const string CLIENT_SECRET = "2SEBA-F4EV9jkqzT1UGJe7Aq";

        internal const string TWITTER_CLIENT_ID = "PiQeUGnE96DQxEw36rAPw";
        internal const string TWITTER_CLIENT_SECRET = "qxLHpdcAg5fCmE6b46GXO8UjDefr6H5C9jXF2SOFAE";
        internal const string TWITTER_TOKEN_URL = "https://api.twitter.com/oauth2/token";
        internal const string TWITTER_AUTH_URL = "https://api.twitter.com/oauth2/token";

        internal const string GOOGLE_CLIENT_ID = "53492317821.apps.googleusercontent.com";
        internal const string GOOGLE_CLIENT_SECRET = "2SEBA-F4EV9jkqzT1UGJe7Aq";
        internal const string GOOGLE_AUTH_URL = "https://accounts.google.com/o/oauth2/auth";
        internal const string GOOGLE_TOK_URL = "https://accounts.google.com/o/oauth2/token";
        internal const string GOOGLE_REDIRECT_URL = "https://localhost:8080";
        internal const string GOOGLE_SCOPE = "email";
        internal const string GOOGLE_REFRESH_TOKEN_URL = "https://www.googleapis.com/oauth2/v3/token";
        internal const string GOOGLE_AUTH_GRANT_CODE = "E4Er9JK5JL9as-45re7Aq";
        internal const string GOOGLE_CLIENT_USER_NAME = "tiztes9242@gmail.com";
        internal const string GOOGLE_CLIENT_PASSWORD = "wlsrms12";

        internal const string FACEBOOK_CLIENT_ID = "dummy_client_id";
        internal const string FACEBOOK_AUTH_URL = "https://www.facebook.com/dialog/oauth";
        internal const string FACEBOOK_SCOPE = "read_stream";

        internal const string SALESFORCE_CLIENT_ID = "3MVG9ZL0ppGP5UrCxqVnY_izjlRzW6tCeDYs64KXns0wGEgbtfqK8cWx16Y4gM3wx2htt0GuoDiQ.CkX2ZuI1";
        internal const string SALESFORCE_CLIENT_SECRET = "3431205550072092349";
        internal const string SALESFORCE_CLIENT_USER_NAME = "sam_test1@outlook.com";
        internal const string SALESFORCE_CLIENT_PASSWORD = "samsung@1gOeXzn5nKDGVNNQP4kJYEqNPp";
        internal const string SALESFORCE_AUTH_URL = "https://login.salesforce.com/services/oauth2/authorize";
        internal const string SALESFORCE_REQ_TOK_URL = "https://login.salesforce.com/services/oauth2/token";

        internal const string LINKEDIN_AUTH_ENDPOINT_URL = "https://www.linkedin.com/uas/oauth2/authorization";
        internal const string LINKEDIN_TOKEN_ENDPOINT_URL = "https://www.linkedin.com/uas/oauth2/accessToken";
        internal const string LINKEDIN_CLIENT_ID = "782p0522d2ri2i";
        internal const string LINKEDIN_CLIENT_SECRET = "Ibj6HdUpZj2M4XIs";
        internal const string LINKEDIN_REDIRECT_URL = "https://developer.tizen.org";
        internal const string LINKEDIN_SCOPE = "r_basicprofile";
        internal const string LINKEDIN_STATE = "DCEEFWF45453sdffef424";


        /// <summary>
        /// Method to initialize Twitter Get Access Token Request Parameters
        /// </summary>
        internal static ClientCredentialsTokenRequest CreateClientCredsTokenRequest(bool dummy = false)
        {
            return new ClientCredentialsTokenRequest()
            {
                TokenEndpoint = new Uri(TWITTER_TOKEN_URL),
                RedirectionEndPoint = new Uri("http://localhost:80"),
                AuthenticationScheme = AuthenticationScheme.Basic,
                ClientSecrets = new ClientCredentials()
                {
                    Id = dummy ? "dummy" : TWITTER_CLIENT_ID,
                    Secret = TWITTER_CLIENT_SECRET
                }
            };
        }

        /// <summary>
        /// Method to initialize Google Get Access Token Request Parameters
        /// </summary>
        internal static CodeGrantTokenRequest CreateCodeGrantTokenRequest(bool dummy = false)
        {
            return new CodeGrantTokenRequest()
            {
                TokenEndpoint = new Uri(GOOGLE_TOK_URL),
                RedirectionEndPoint = new Uri(GOOGLE_REDIRECT_URL),
                ClientSecrets = new ClientCredentials()
                {
                    Id = dummy ? "dummy" : GOOGLE_CLIENT_ID,
                    Secret = GOOGLE_CLIENT_SECRET
                },
                Scopes = new string[] { GOOGLE_SCOPE },
                Code = GOOGLE_AUTH_GRANT_CODE,
                AuthenticationScheme = AuthenticationScheme.RequestBody,
            };
        }

        /// <summary>
        /// Method to initialize Salesforce Get Access Token Request Parameters
        /// </summary>
        internal static ResourceOwnerPwdCredentialsTokenRequest CreateRoPwdTokenRequest(bool dummy = false)
        {
            return new ResourceOwnerPwdCredentialsTokenRequest()
            {
                TokenEndpoint = new Uri(SALESFORCE_REQ_TOK_URL),
                ClientSecrets = new ClientCredentials()
                {
                    Id = dummy ? "dummy" : SALESFORCE_CLIENT_ID,
                    Secret = SALESFORCE_CLIENT_SECRET
                },
                AuthenticationScheme = AuthenticationScheme.RequestBody,
                Username = SALESFORCE_CLIENT_USER_NAME,
                Password = SALESFORCE_CLIENT_PASSWORD,
                RedirectionEndPoint = new Uri("http://localhost:80"),
                State = "state"
            };
        }

        /// <summary>
        /// Method to initialize Google Refresh Access Token Request Parameters
        /// </summary>
        internal static RefreshTokenRequest CreateRefreshTokenRequest(bool dummy = false)
        {
            return new RefreshTokenRequest()
            {
                TokenEndpoint = new Uri(GOOGLE_REFRESH_TOKEN_URL),
                ClientSecrets = new ClientCredentials()
                {
                    Id = dummy ? "dummy" : GOOGLE_CLIENT_ID,
                    Secret = GOOGLE_CLIENT_SECRET
                },
                AuthenticationScheme = AuthenticationScheme.Basic,
                RedirectionEndPoint = new Uri("http://localhost:80"),
                RefreshToken = "AQkAQHKoQRyhc7nlxuk4ecr8MC_7kUoBQxCXQ8PpH3u2VNq2KP96UMrCrrav.aO6gHMVhTeeJt_6PVAUqmP5bQSxH8GWp2sO"

            };
        }
    }
}
