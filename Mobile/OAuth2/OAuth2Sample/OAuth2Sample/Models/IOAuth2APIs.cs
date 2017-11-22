/*
 * Copyright 2016 by Samsung Electronics, Inc.,
 *
 * This software is the confidential and proprietary information
 * of Samsung Electronics, Inc. ("Confidential Information"). You
 * shall not disclose such Confidential Information and shall use
 * it only in accordance with the terms of the license agreement
 * you entered into with Samsung.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tizen.Account.OAuth2;

namespace OAuth2Sample.Models
{
    public interface IOAuth2APIs
    {
        Task<string> SalesforceGetAccessTokenTest();
        Task<string> GoogleRefreshAccessTokenTest();
        Task<string> GoogleGetAccessTokenTest();
        Task<string> TwitterGetAccessTokenTest();
        Task<string> Code_PROPERTY_READ_ONLY();
        string AuthorizationRequestTest();
    }
}
