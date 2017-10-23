/*
 * Copyright 2016 by Samsung Electronics, Inc.,
 *
 * This software is the confidential and proprietary information
 * of Samsung Electronics, Inc. ("Confidential Information"). You
 * shall not disclose such Confidential Information and shall use
 * it only in accordance with the terms of the license agreement
 * you entered into with Samsung.
 */

using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushReceiver.Models
{
    public interface IPushAPIs
    {
        int PushConnect();

        int PushDisconnect();

        string PushGetRegistrationId();
    }
}

