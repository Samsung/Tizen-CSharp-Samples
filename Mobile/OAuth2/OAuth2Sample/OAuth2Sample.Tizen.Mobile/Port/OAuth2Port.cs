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
using System.Threading.Tasks;
using Tizen.System;
using System.Linq;
using System.Text;
using OAuth2Sample.Models;
using Tizen;
using Tizen.Applications;
using Tizen.Account.OAuth2;

namespace OAuth2Sample.Tizen.Mobile.Port
{
    public class OAuth2Port : IOAuth2APIs
    {
        private static AuthorizationRequest[] _requests;
        public OAuth2Port()
        {
            _requests = new AuthorizationRequest[] {
                new CodeGrantAuthorizationRequest(),
                new ImplicitGrantAuthorizationRequest()
                };
        }
        public static void Destroy()
        {
            _requests = null;
        }

        /// <summary>
        /// Used to test Getting Acess Token for Salesforce request token url
        /// </summary>
        /// <returns>accessToken as "PASS" or "FAIL"</returns>
        public async Task<string> SalesforceGetAccessTokenTest()
        {
            // Initialize return value with "PASS"
            string accessToken = "PASS";
            var authorizer = new ResourceOwnerPwdCredentialsAuthorizer();
            try
            {
                // Create ResourceOwnerPwdCredentialsTokenRequest for Salesforce request token url
                var request = TestHelper.CreateRoPwdTokenRequest();
                // Connect to get access token for Salesforce url
                var response = await authorizer.GetAccessTokenAsync(request);
            }
            catch (Exception)
            {
                // This is a positive test. So any exception should not be throwm
                accessToken = "FAIL";
                return accessToken;
            }

            authorizer.Dispose();
            return accessToken;
        }

        /// <summary>
        /// Used to test Refreshing access Token for google refresh token url
        /// </summary>
        /// <returns>accessToken as "PASS" or "FAIL"</returns>
        public async Task<string> GoogleRefreshAccessTokenTest()
        {
            // Initialize return value with "FAIL" for negative test
            string accessToken = "FAIL";
            var authorizer = new ResourceOwnerPwdCredentialsAuthorizer();

            try
            {
                // Create RefreshTokenRequest for google refresh token url
                var request = TestHelper.CreateRefreshTokenRequest(true);
                // Connect to refresh access token for google refresh token url
                var response = await authorizer.RefreshAccessTokenAsync(request);
                accessToken = response.AccessToken.ToString();
            }
            catch
            {
                // This is a negative test. So an exception should be throwm
                accessToken = "PASS";
                return accessToken;
            }

            authorizer.Dispose();
            return accessToken;

        }

        /// <summary>
        /// Used to test Getting access Token for google token url
        /// </summary>
        /// <returns>accessToken as "PASS" or "FAIL"</returns>
        public async Task<string> GoogleGetAccessTokenTest()
        {
            // Initialize return value with "FAIL" for negative test
            string accessToken = "FAIL";
            var authorizer = new CodeGrantAuthorizer();
            try
            {
                // Create CodeGrantTokenRequest for google token url
                var request = TestHelper.CreateCodeGrantTokenRequest(true);
                // Connect to get access token for google token url
                var response = await authorizer.GetAccessTokenAsync(request);
                accessToken = response.AccessToken.ToString();
            }
            catch
            {
                // This is a negative test. So an exception should be throwm
                accessToken = "PASS";
                return accessToken;
            }

            authorizer.Dispose();
            return accessToken;
        }

        /// <summary>
        /// Used to test Getting access Token for twittwer token url
        /// </summary>
        /// <returns>accessToken as "PASS" or "FAIL"</returns>
        public async Task<string> TwitterGetAccessTokenTest()
        {
            // Initialize return value with "FAIL" for negative test
            string accessToken = "FAIL";
            var authorizer = new ClientCredentialsAuthorizer();
            try
            {
                // Create ClientCredsTokenRequest for twittwer token url
                var request = TestHelper.CreateClientCredsTokenRequest(true);
                // Connect to get access token for twittwer token url
                var response = await authorizer.GetAccessTokenAsync(request);
                accessToken = response.AccessToken.ToString();
            }
            catch
            {
                // This is a negative test. So an exception should be throwm
                accessToken = "PASS";
                return accessToken;
            }

            authorizer.Dispose();
            return accessToken;
        }

        /// <summary>
        /// Checks the properties of sub-class objects of AuthorizationRequest
        /// </summary>
        /// <returns>accessToken as "PASS" or "FAIL"</returns>
        public string AuthorizationRequestTest()
        {
            // Initialize return value with "FAIL"
            string resultString = "PASS";
            foreach (var request in _requests)
            {
                if (request == null)
                    resultString = "FAIL : request is null";
            }

            // Checks the construction of sub-class objects of AuthorizationRequest
            var result = _requests[0].ResponseType;
            if (TestHelper.CODE_GRANT_RESPONSE_TYPE_SPEC != result)
                resultString = "FAIL : CodeGrantAuthorizationRequest response ";
            result = _requests[1].ResponseType;
            if (TestHelper.IMPLICIT_GRANT_RESPONSE_TYPE_SPEC != result)
                resultString = "FAIL : ImplicitGrantAuthorizationRequest response";

            // Checks the ClientSecrets property of sub-class objects of AuthorizationRequest for read/write
            foreach (var item in _requests)
            {
                item.ClientSecrets = new ClientCredentials()
                {
                    Id = TestHelper.CLIENT_ID,
                    Secret = TestHelper.CLIENT_SECRET
                };
                if (TestHelper.CLIENT_ID != item.ClientSecrets.Id)
                    resultString = "FAIL : ClientSecrets::Id Read/Write failed";
                if (TestHelper.CLIENT_SECRET != item.ClientSecrets.Secret)
                    resultString = "FAIL : ClientSecrets::Secret Read/Write failed";
            }

            // Checks the AuthorizationEndpoint property of sub-class objects of AuthorizationRequest for read/write
            foreach (var item in _requests)
            {
                var expected = new Uri("https://localhost:8080");

                item.AuthorizationEndpoint = expected;
                if (!expected.Equals(item.AuthorizationEndpoint))
                    resultString = "FAIL : AuthorizationEndpoint Read/Write failed";
            }

            // Validates the property TokenEndPoint in the sub-classes of AuthorizationRequest
            foreach (var item in _requests)
            {
                var expected = new Uri("https://localhost:8080");

                item.TokenEndpoint = expected;
                if (!expected.Equals(item.TokenEndpoint))
                    resultString = "FAIL : TokenEndpoint Read/Write failed";
            }

            // Checks the RedirectionEndPoint property of sub-class objects of AuthorizationRequest for read/write
            foreach (var item in _requests)
            {
                var expected = new Uri("https://localhost:8080");

                item.RedirectionEndPoint = expected;
                if (!expected.Equals(item.RedirectionEndPoint))
                    resultString = "FAIL : TokenEndpoint Read/Write failed";
            }

            // Checks the Scopes property of sub-class objects of AuthorizationRequest for read/write
            foreach (var item in _requests)
            {
                var expected = new List<string> {
                    "email",
                    "r_basicprofile"
                    };
                item.Scopes = expected;
                var resultScopes = Enumerable.SequenceEqual<string>(expected, item.Scopes);
                if (resultScopes != true)
                    resultString = "FAIL : Scopes Read/Write failed";
            }

            // Checks the State property of sub-class objects of AuthorizationRequest for read/write
            foreach (var item in _requests)
            {
                item.State = "ZSRFDNJVJGLDSLKNMVL40345903kdsfgp0344";
                if (item.State != "ZSRFDNJVJGLDSLKNMVL40345903kdsfgp0344")
                    resultString = "FAIL : State Read/Write failed";
            }

            // Checks the CustomData property of sub-class objects of AuthorizationRequest for read/write
            foreach (var item in _requests)
            {
                var expected = new Dictionary<string, string> {
                    { "key1", "param1" },
                    { "key2", "param2" }
                    };
                item.CustomData = expected;

                var resultCustomData = Enumerable.SequenceEqual<KeyValuePair<string, string>>(expected, item.CustomData);
                if (resultCustomData != true)
                    resultString = "FAIL : Scopes Read/Write failed";
            }
            return resultString;
        }
    }
}
