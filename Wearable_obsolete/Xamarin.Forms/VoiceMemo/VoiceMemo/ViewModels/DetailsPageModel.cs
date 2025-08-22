/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using VoiceMemo.Models;

namespace VoiceMemo.ViewModels
{
    // The model class for DetailPage
    public class DetailsPageModel : BasePageModel
    {
        // Record that is shown in DetailsPage
        Record _record;
        public bool IsNew
        {
            get;
            set;
        }
        /// <summary>
        /// Record to display in Details Page
        /// </summary>
        public Record Record
        {
            get
            {
                return _record;
            }

            set
            {
                SetProperty(ref _record, value, "Record");
            }
        }

        public DetailsPageModel(Record record = null)
        {
            Init(record);
        }
        /// <summary>
        /// Initialize Detail Page with Record
        /// </summary>
        /// <param name="record">Record to be shown</param>
        public void Init(Record record)
        {
            if (record is LatestRecord)
            {
                IsNew = true;
                Record = ((LatestRecord)record).Record;
            }
            else
            {
                IsNew = false;
                Record = record;
            }
        }
    }
}
