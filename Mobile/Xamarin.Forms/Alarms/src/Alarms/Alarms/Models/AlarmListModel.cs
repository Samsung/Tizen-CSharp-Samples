/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using System.Collections.Generic;

namespace Alarms.Models
{
    /// <summary>
    /// Provides information about alarm list element.
    /// </summary>
    public class AlarmListElementInfo
    {
        #region properties

        /// <summary>
        /// Alarm ID value.
        /// </summary>
        public int AlarmID { get; set; }

        /// <summary>
        /// Alarm selected state.
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes instance of AlarmListElementInfo.
        /// </summary>
        /// <param name="alarmID">Alarm ID value.</param>
        /// <param name="isSelected">Alarm selected state.</param>
        public AlarmListElementInfo(int alarmID, bool isSelected)
        {
            AlarmID = alarmID;
            IsSelected = isSelected;
        }

        #endregion
    }

    /// <summary>
    /// Alarm list values storage.
    /// This is singleton. Instance is accessible via <see cref="AlarmListModel.Instance">Instance</see></cref> property.
    /// </summary>
    public sealed class AlarmListModel
    {
        #region fields

        /// <summary>
        /// Backing field of Instance property.
        /// </summary>
        private static AlarmListModel _instance;

        #endregion

        #region properties

        /// <summary>
        /// AlarmListModel instance accessor.
        /// </summary>
        public static AlarmListModel Instance
        {
            get => _instance ?? (_instance = new AlarmListModel());
        }

        /// <summary>
        /// List containing all created alarms.
        /// </summary>
        public List<AlarmListElementInfo> AlarmsList { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes instance of the model.
        /// </summary>
        private AlarmListModel()
        {
            AlarmsList = new List<AlarmListElementInfo>();
        }

        /// <summary>
        /// Clears all elements from list.
        /// </summary>
        public void ClearList()
        {
            AlarmsList.Clear();
        }

        /// <summary>
        /// Adds an new element to alarm list.
        /// </summary>
        /// <param name="alarmID">Alarm ID value.</param>
        /// <param name="isSelected">Alarm selected state.</param>
        public void AddElement(int alarmID, bool isSelected)
        {
            AlarmsList.Add(new AlarmListElementInfo(alarmID, isSelected));
        }

        /// <summary>
        /// Finds edited element and change it's ID to a new one.
        /// </summary>
        /// <param name="editedAlarmID">New ID for edited element.</param>
        public void ChangeEditedElementID(int editedAlarmID)
        {
            foreach (AlarmListElementInfo element in AlarmsList)
            {
                if (element.AlarmID == -1)
                {
                    element.AlarmID = editedAlarmID;
                    break;
                }
            }
        }

        #endregion
    }
}