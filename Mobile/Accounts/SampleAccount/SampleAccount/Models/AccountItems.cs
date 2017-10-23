/*
 * Copyright 2016 by Samsung Electronics, Inc.,
 *
 * This software is the confidential and proprietary information
 * of Samsung Electronics, Inc. ("Confidential Information"). You
 * shall not disclose such Confidential Information and shall use
 * it only in accordance with the terms of the license agreement
 * you entered into with Samsung.
*/


namespace AccountManager.Models
{
    public class AccountItem
    {
        public int AccountId { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
    }
}