/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
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

namespace HeartRateMonitor.Constants
{
    /// <summary>
    /// MessageConstants class.
    /// Provides definition of text messages used in application.
    /// </summary>
    public class MessageConstants
    {
        #region fields

        /// <summary>
        /// Text of the title when the application is ready to start measurement.
        /// </summary>
        public static readonly string READY_TITLE = "Ready";

        /// <summary>
        /// Text of the message when the application is ready to start measurement.
        /// </summary>
        public static readonly string READY_CONTENT = "Tap start to proceed.";

        /// <summary>
        /// Text of the title when the measurement process is in progress.
        /// </summary>
        public static readonly string MEASURING_TITLE = "Measuring ... ";

        /// <summary>
        /// Text of the message when the measurement process is in progress.
        /// </summary>
        public static readonly string MEASURING_CONTENT = "Try to keep still and quiet.";

        /// <summary>
        /// Text of the title when the measurement process is finished.
        /// </summary>
        public static readonly string FINISHED_TITLE = "Finished.";

        /// <summary>
        /// Text of the message when the measurement process is finished
        /// and the heart rate value is within the average resting rate.
        /// </summary>
        public static readonly string FINISHED_CONTENT_WITHIN_AVERAGE =
            "Your result is within the average\nresting rate (61-76 bpm).";

        /// <summary>
        /// Text of the message when the measurement process is finished
        /// and the heart rate value is within the defined heart rate limit.
        /// </summary>
        public static readonly string FINISHED_CONTENT_WITHIN_LIMIT =
            "Your result is within the defined\nheart rate limit.";

        /// <summary>
        /// Text of the message when the measurement process is finished
        /// and the heart rate value is above the defined heart rate limit.
        /// </summary>
        public static readonly string FINISHED_CONTENT_ABOVE_LIMIT =
            "Caution! Your result is above\nthe defined heart rate limit.";

        #endregion
    }
}