/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPC = Tizen.Pims.Calendar;
using Calendar.Models;

namespace Calendar.Tizen.Port
{
    using TPC.CalendarViews;

    /// <summary>
    /// Represents the Calendar APIs for connecting Sample Calendar app.
    /// </summary>
    public class CalendarPort : ICalendarAPIs
    {
        /// <summary>
        /// Calendar manager.
        /// </summary>
        private TPC.CalendarManager manager;

        /// <summary>
        /// Get tick from reminder index.
        /// </summary>
        private int getTick(int reminder)
        {
            int tick = 0;
            switch (reminder)
            {
            case 1:
            /// start time
                tick = 0;
                break;
            case 2:
            /// 5mins before
                tick = 5;
                break;
            case 3:
            /// 15mins before
                tick = 15;
                break;
            case 4:
            /// 30 mins before
                tick = 30;
                break;
            case 5:
            /// 1hour before
                tick = 1;
                break;
            case 6:
            /// 1 day before
                tick = 1;
                break;
            case 7:
            /// 1 week before
                tick = 1;
                break;
            }
            return tick;
        }

        /// <summary>
        /// Get unit from reminder index.
        /// </summary>
        private int getUnit(int reminder)
        {
            int unit = 0;
            switch (reminder)
            {
            case 1:
            /// start time
                unit = (int)TPC.CalendarTypes.TickUnit.Minute;
                break;
            case 2:
            /// 5mins before
                unit = (int)TPC.CalendarTypes.TickUnit.Minute;
                break;
            case 3:
            /// 15mins before
                unit = (int)TPC.CalendarTypes.TickUnit.Minute;
                break;
            case 4:
            /// 30mins before
                unit = (int)TPC.CalendarTypes.TickUnit.Minute;
                break;
            case 5:
            /// 1hour before
                unit = (int)TPC.CalendarTypes.TickUnit.Hour;
                break;
            case 6:
            /// 1day before
                unit = (int)TPC.CalendarTypes.TickUnit.Day;
                break;
            case 7:
            /// 1week before
                unit = (int)TPC.CalendarTypes.TickUnit.Week;
                break;
            }
            return unit;
        }

        /// <summary>
        /// Get priority from priority index.
        /// </summary>
        private int getPriority(int inPriority)
        {
            int priority;
            switch (inPriority)
            {
            default:
            case 0:
            /// low priority
                priority = (int)TPC.CalendarTypes.Priority.Low;
                break;
            case 1:
            /// normal priority
                priority = (int)TPC.CalendarTypes.Priority.Normal;
                break;
            case 2:
            /// high priority
                priority = (int)TPC.CalendarTypes.Priority.High;
                break;
            }
            return priority;
        }

        /// <summary>
        /// Get sensitivity from sensitivity index.
        /// </summary>
        private int getSensitivity(int inSensitivity)
        {
            int sensitivity;
            switch (inSensitivity)
            {
            default:
            case 0:
            /// public sensitivity
                sensitivity = (int)TPC.CalendarTypes.Sensitivity.Public;
                break;
            case 1:
            /// private sensitivity
                sensitivity = (int)TPC.CalendarTypes.Sensitivity.Private;
                break;
            case 2:
            /// confidential sensitivity
                sensitivity = (int)TPC.CalendarTypes.Sensitivity.Confidential;
                break;
            }
            return sensitivity;
        }

        /// <summary>
        /// Get status from status index.
        /// </summary>
        private int getStatus(int inStatus)
        {
            int status;
            switch (inStatus)
            {
            default:
            case 0:
            /// none status
                status = (int)TPC.CalendarTypes.EventStatus.None;
                break;
            case 1:
            /// Tentative status
                status = (int)TPC.CalendarTypes.EventStatus.Tentative;
                break;
            case 2:
            /// Confirmed status
                status = (int)TPC.CalendarTypes.EventStatus.Confirmed;
                break;
            case 3:
            /// Cancelled status
                status = (int)TPC.CalendarTypes.EventStatus.Cancelled;
                break;
            }
            return status;
        }

        /// <summary>
        /// Get reminder index from tick and unit.
        /// </summary>
        private int getReminderIndex(int tick, int unit)
        {
            int index;
            switch (unit)
            {
            default:
            case (int)TPC.CalendarTypes.TickUnit.Minute:
                switch (tick)
                {
                default:
                /// start time
                case 0:
                    index = 1;
                    break;
                /// 5mins before
                case 5:
                    index = 2;
                    break;
                /// 15mins before
                case 15:
                    index = 3;
                    break;
                /// 30mins before
                case 30:
                    index = 4;
                    break;
                }
                break;
            /// Hour unit
            case (int)TPC.CalendarTypes.TickUnit.Hour:
                index = 5;
                break;
            /// Day unit
            case (int)TPC.CalendarTypes.TickUnit.Day:
                index = 6;
                break;
            /// Week unit
            case (int)TPC.CalendarTypes.TickUnit.Week:
                index = 7;
                break;
            }
            return index;
        }

        /// <summary>
        /// Get priority index from priority value.
        /// </summary>
        private int getPriorityIndex(int priority)
        {
            int index;
            switch (priority)
            {
            default:
            /// Low priority
            case (int)TPC.CalendarTypes.Priority.Low:
                index = 0;
                break;
            /// Normal priority
            case (int)TPC.CalendarTypes.Priority.Normal:
                index = 1;
                break;
            /// High priority
            case (int)TPC.CalendarTypes.Priority.High:
                index = 2;
                break;
            }
            return index;
        }

        /// <summary>
        /// Get sensitivity index from sensitivity value.
        /// </summary>
        private int getSensitivityIndex(int sensitivity)
        {
            int index;
            switch (sensitivity)
            {
            default:
            /// Public sensitivity
            case (int)TPC.CalendarTypes.Sensitivity.Public:
                index = 0;
                break;
            /// Private sensitivity
            case (int)TPC.CalendarTypes.Sensitivity.Private:
                index = 1;
                break;
            /// Confidential sensitivity
            case (int)TPC.CalendarTypes.Sensitivity.Confidential:
                index = 2;
                break;
            }
            return index;
        }

        /// <summary>
        /// Get status index from status value.
        /// </summary>
        private int getStatusIndex(int status)
        {
            int index;
            switch (status)
            {
            default:
            /// None status
            case (int)TPC.CalendarTypes.EventStatus.None:
                index = 0;
                break;
            /// Tentative status
            case (int)TPC.CalendarTypes.EventStatus.Tentative:
                index = 0;
                break;
            /// Confirmed status
            case (int)TPC.CalendarTypes.EventStatus.Confirmed:
                index = 0;
                break;
            /// Cancelled status
            case (int)TPC.CalendarTypes.EventStatus.Cancelled:
                index = 0;
                break;
            }
            return index;
        }

        /// <summary>
        /// Get recurrence index from recurrence value.
        /// </summary>
        private int getRecurrenceIndex(int recurrence)
        {
            int index;
            switch (recurrence)
            {
            default:
            /// None recurrence
            case (int)TPC.CalendarTypes.Recurrence.None:
                index = 0;
                break;
            /// Daily recurrence
            case (int)TPC.CalendarTypes.Recurrence.Daily:
                index = 1;
                break;
            /// Weekly recurrence
            case (int)TPC.CalendarTypes.Recurrence.Weekly:
                index = 2;
                break;
            /// Monthly recurrence
            case (int)TPC.CalendarTypes.Recurrence.Monthly:
                index = 3;
                break;
            /// Yearly recurrence
            case (int)TPC.CalendarTypes.Recurrence.Yearly:
                index = 4;
                break;
            }
            return index;
        }

        /// <summary>
        /// Clean child record.
        /// <param name="record">The record to be cleared</param>
        /// </summary>
        private void CleanChildRecord(TPC.CalendarRecord record)
        {
            if (record.GetChildRecordCount(Event.Alarm) > 0)
            {
                var alarm = record.GetChildRecord(Event.Alarm, 0);
                record.RemoveChildRecord(Event.Alarm, alarm);
            }
        }

        /// <summary>
        /// Get record from item.
        /// <param name="item">The item</param>
        /// <param name="record">The record to be converted from item.</param>
        /// </summary>
        private void ItemToRecord(RecordItem item, TPC.CalendarRecord record)
        {
            record.Set<string>(Event.Summary, item.Summary);
            record.Set<string>(Event.Location, item.Location);
            record.Set<string>(Event.Description, item.Description);

            TPC.CalendarTime start;
            TPC.CalendarTime end;
            if (item.IsAllday)
            {
                start = new TPC.CalendarTime(item.StartTime.Year, item.StartTime.Month, item.StartTime.Day,
                        item.StartTime.Hour, item.StartTime.Minute, item.StartTime.Second);
                end = new TPC.CalendarTime(item.EndTime.Year, item.EndTime.Month, item.EndTime.Day,
                        item.EndTime.Hour, item.EndTime.Minute, item.EndTime.Second);
            }
            else
            {
                start = new TPC.CalendarTime(item.StartTime.ToUniversalTime().Ticks);
                end = new TPC.CalendarTime(item.EndTime.ToUniversalTime().Ticks);
            }
            record.Set<TPC.CalendarTime>(Event.Start, start);
            record.Set<TPC.CalendarTime>(Event.End, end);

            switch (item.Recurrence)
            {
            default:
            case 0:
            /// none
                record.Set<int>(Event.Freq, (int)TPC.CalendarTypes.Recurrence.None);
                break;
            case 1:
            /// daily
                record.Set<int>(Event.Freq, (int)TPC.CalendarTypes.Recurrence.Daily);
                break;
            case 2:
            /// weekly
                record.Set<int>(Event.Freq, (int)TPC.CalendarTypes.Recurrence.Weekly);
                break;
            case 3:
            /// monthly
                record.Set<int>(Event.Freq, (int)TPC.CalendarTypes.Recurrence.Monthly);
                break;
            case 4:
            /// yearly
                record.Set<int>(Event.Freq, (int)TPC.CalendarTypes.Recurrence.Yearly);
                break;
            }

            switch (item.UntilType)
            {
            default:
            /// count
            case 0:
                record.Set<int>(Event.RangeType, (int)TPC.CalendarTypes.RangeType.Count);
                record.Set<int>(Event.Count, item.UntilCount);
                break;
            /// until
            case 1:
                record.Set<int>(Event.RangeType, (int)TPC.CalendarTypes.RangeType.Until);
                var until = new TPC.CalendarTime(new DateTime(item.UntilTime.Year, item.UntilTime.Month, item.UntilTime.Day,
                        item.UntilTime.Hour, item.UntilTime.Minute, item.UntilTime.Second, DateTimeKind.Local).Ticks);
                record.Set<TPC.CalendarTime>(Event.Until, until);
                break;
            }

            if (item.Reminder > 0)
            {
                TPC.CalendarRecord alarm;
                alarm = new TPC.CalendarRecord(Alarm.Uri);
                alarm.Set<int>(Alarm.Tick, getTick(item.Reminder));
                alarm.Set<int>(Alarm.TickUnit, getUnit(item.Reminder));
                record.AddChildRecord(Event.Alarm, alarm);
            }

            record.Set<int>(Event.Priority, getPriority(item.Priority));
            record.Set<int>(Event.Sensitivity, getSensitivity(item.Sensitivity));
            record.Set<int>(Event.EventStatus, getStatus(item.Status));
        }

        /// <summary>
        /// Get item from record.
        /// <param name="record">The record</param>
        /// <param name="item">The item to be converted from record.</param>
        /// </summary>
        private void RecordToItem(TPC.CalendarRecord record, RecordItem item, TPC.CalendarTime start, TPC.CalendarTime end, bool isAllday)
        {
            item.Index = record.Get<int>(Event.Id);
            item.Summary = record.Get<string>(Event.Summary);
            item.Location = record.Get<string>(Event.Location);
            item.Description = record.Get<string>(Event.Description);
            item.Priority = getPriorityIndex(record.Get<int>(Event.Priority));
            item.Sensitivity = getSensitivityIndex(record.Get<int>(Event.Sensitivity));
            item.Status = getStatusIndex(record.Get<int>(Event.EventStatus));

            item.StartTime = isAllday == true ? start.LocalTime : start.UtcTime;
            item.EndTime = isAllday == true ? end.LocalTime : end.UtcTime;
            item.IsAllday = isAllday;
            item.Recurrence = getRecurrenceIndex(record.Get<int>(Event.Freq));

            item.DisplayTime = String.Format("{0} - {1}",
                    item.IsAllday == true ? item.StartTime.ToLocalTime().ToString("yyyy/MM/dd") : item.StartTime.ToLocalTime().ToString("yyyy/MM/dd HH:mm"),
                    item.IsAllday == true ? item.EndTime.ToLocalTime().ToString("yyyy/MM/dd") : item.EndTime.ToLocalTime().ToString("yyyy/MM/dd HH:mm"));

            if (item.Recurrence > 0)
            {
                switch (record.Get<int>(Event.RangeType))
                {
                default:
                /// count
                case (int)TPC.CalendarTypes.RangeType.Count:
                    item.UntilType = 0;
                    item.UntilCount = record.Get<int>(Event.Count);
                    break;
                /// until
                case (int)TPC.CalendarTypes.RangeType.Until:
                    item.UntilType = 1;
                    var until = record.Get<TPC.CalendarTime>(Event.Until);
                    item.UntilTime = until.UtcTime;
                    break;
                }
            }

            if (record.Get<int>(Event.HasAlarm) > 0)
            {
                var alarm = record.GetChildRecord(Event.Alarm, 0);
                item.Reminder = getReminderIndex(alarm.Get<int>(Alarm.Tick),  alarm.Get<int>(Alarm.TickUnit));
            }
        }

        /// <summary>
        /// Insert item.
        /// <param name="item">The item to be inserted.</param>
        /// </summary>
        public int Insert(RecordItem item)
        {
            var record = new TPC.CalendarRecord(Event.Uri);
            ItemToRecord(item, record);
            int recordId = manager.Database.Insert(record);
            record.Dispose();
            return recordId;
        }

        /// <summary>
        /// Update item.
        /// Before updating item, item should be clean.
        /// <param name="item">The item to be updated.</param>
        /// </summary>
        public void Update(RecordItem item)
        {
            var record = manager.Database.Get(Event.Uri, item.Index);
            CleanChildRecord(record);
            ItemToRecord(item, record);
            manager.Database.Update(record);
            record.Dispose();
        }

        /// <summary>
        /// Delete item.
        /// <param name="item">The item to be deleted.</param>
        /// </summary>
        public void Delete(RecordItem item)
        {
            manager.Database.Delete(Event.Uri, item.Index);
        }

        /// <summary>
        /// Get utc instance list.
        /// To get not-allday instance list, InstanceUtimeBook uri must be used.
        /// This does not include allday instances.
        /// Range is set from the first day of selected date to the last day of selected data.
        /// <param name="dt">The selected datetime</param>
        /// </summary>
        private TPC.CalendarList GetUtcInstances(DateTime dt)
        {
            TPC.CalendarList list;

            DateTime firstDate = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, DateTimeKind.Local);
            TPC.CalendarTime from = new TPC.CalendarTime(firstDate.ToUniversalTime().Ticks);
            TPC.CalendarTime to = new TPC.CalendarTime(from.UtcTime.AddDays(1).Ticks);

            TPC.CalendarQuery query = new TPC.CalendarQuery(InstanceUtimeBook.Uri);
            TPC.CalendarFilter filter = new TPC.CalendarFilter(InstanceUtimeBook.Uri,
                    InstanceUtimeBook.Start, TPC.CalendarFilter.IntegerMatchType.GreaterThanOrEqual, from);
            filter.AddCondition(TPC.CalendarFilter.LogicalOperator.And,
                    InstanceUtimeBook.Start, TPC.CalendarFilter.IntegerMatchType.LessThan, to);
            query.SetFilter(filter);
            list = manager.Database.GetRecordsWithQuery(query, 0, 0);
            filter.Dispose();
            query.Dispose();

            return list;
        }

        /// <summary>
        /// Get allday instance list.
        /// To get allday instance list, InstanceLocaltimeBook uri must be used.
        /// Range is set from the first day of selected date to the last day of selected data.
        /// <param name="dt">The selected datetime</param>
        /// </summary>
        private TPC.CalendarList GetAlldayInstances(DateTime dt)
        {
            TPC.CalendarList list = null;

            TPC.CalendarTime from = new TPC.CalendarTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            TPC.CalendarTime to = new TPC.CalendarTime(dt.AddDays(1).Year, dt.AddDays(1).Month, dt.AddDays(1).Day, 0, 0, 0);

            TPC.CalendarQuery query = new TPC.CalendarQuery(InstanceLocaltimeBook.Uri);
            TPC.CalendarFilter filter = new TPC.CalendarFilter(InstanceLocaltimeBook.Uri,
                    InstanceLocaltimeBook.Start, TPC.CalendarFilter.IntegerMatchType.GreaterThanOrEqual, from);
            filter.AddCondition(TPC.CalendarFilter.LogicalOperator.And,
                    InstanceLocaltimeBook.Start, TPC.CalendarFilter.IntegerMatchType.LessThan, to);
            query.SetFilter(filter);
            try
            {
                list = manager.Database.GetRecordsWithQuery(query, 0, 0);
            }
            catch (Exception)
            {

            }
            filter.Dispose();
            query.Dispose();

            return list;
        }

        /// <summary>
        /// Get a month instance.
        /// To display allday and not-allday list, combine list together into itemList.
        /// This will shows allday list on the top of the list.
        /// Range is set from the first day of selected date to the last day of selected data.
        /// <param name="dt">The selected datetime</param>
        /// </summary>
        public List<RecordItem> GetMonthRecords(DateTime dt)
        {
            TPC.CalendarList list;
            int i;

            var itemList = new List<RecordItem>();

            list = GetAlldayInstances(dt);
            list.MoveFirst();
            for (i = 0; i < list.Count; i++)
            {
                var instance = list.GetCurrentRecord();
                var record = manager.Database.Get(Event.Uri, instance.Get<int>(InstanceLocaltimeBook.EventId));

                var item = new RecordItem();
                RecordToItem(record, item, instance.Get<TPC.CalendarTime>(InstanceLocaltimeBook.Start),
                        instance.Get<TPC.CalendarTime>(InstanceLocaltimeBook.End), true);
                itemList.Add(item);
                list.MoveNext();
            }
            list.Dispose();

            list = GetUtcInstances(dt);
            list.MoveFirst();
            for (i = 0; i < list.Count; i++)
            {
                var instance = list.GetCurrentRecord();
                var record = manager.Database.Get(Event.Uri, instance.Get<int>(InstanceUtimeBook.EventId));

                var item = new RecordItem();
                RecordToItem(record, item, instance.Get<TPC.CalendarTime>(InstanceUtimeBook.Start),
                        instance.Get<TPC.CalendarTime>(InstanceUtimeBook.End), false);
                itemList.Add(item);
                list.MoveNext();
            }
            list.Dispose();

            return itemList;
        }

        /// <summary>
        /// A constructor.
        /// </summary>
        public CalendarPort()
        {
            manager = new TPC.CalendarManager();
        }
    }
}


