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

    public class CalendarPort : ICalendarAPIs
    {
        private TPC.CalendarManager manager;

        private int getTick(int reminder)
        {
            int tick = 0;
            switch (reminder)
            {
            case 1:
                tick = 0;
                break;
            case 2:
                tick = 5;
                break;
            case 3:
                tick = 15;
                break;
            case 4:
                tick = 30;
                break;
            case 5:
                tick = 1;
                break;
            case 6:
                tick = 1;
                break;
            case 7:
                tick = 1;
                break;
            }
            return tick;
        }

        private int getUnit(int reminder)
        {
            int unit = 0;
            switch (reminder)
            {
            case 1:
                unit = (int)TPC.CalendarTypes.TickUnit.Minute;
                break;
            case 2:
                unit = (int)TPC.CalendarTypes.TickUnit.Minute;
                break;
            case 3:
                unit = (int)TPC.CalendarTypes.TickUnit.Minute;
                break;
            case 4:
                unit = (int)TPC.CalendarTypes.TickUnit.Minute;
                break;
            case 5:
                unit = (int)TPC.CalendarTypes.TickUnit.Hour;
                break;
            case 6:
                unit = (int)TPC.CalendarTypes.TickUnit.Day;
                break;
            case 7:
                unit = (int)TPC.CalendarTypes.TickUnit.Week;
                break;
            }
            return unit;
        }

        private int getPriority(int inPriority)
        {
            int priority;
            switch (inPriority)
            {
            default:
            case 0:
                priority = (int)TPC.CalendarTypes.Priority.Low;
                break;
            case 1:
                priority = (int)TPC.CalendarTypes.Priority.Normal;
                break;
            case 2:
                priority = (int)TPC.CalendarTypes.Priority.High;
                break;
            }
            return priority;
        }

        private int getSensitivity(int inSensitivity)
        {
            int sensitivity;
            switch (inSensitivity)
            {
            default:
            case 0:
                sensitivity = (int)TPC.CalendarTypes.Sensitivity.Public;
                break;
            case 1:
                sensitivity = (int)TPC.CalendarTypes.Sensitivity.Private;
                break;
            case 2:
                sensitivity = (int)TPC.CalendarTypes.Sensitivity.Confidential;
                break;
            }
            return sensitivity;
        }

        private int getStatus(int inStatus)
        {
            int status;
            switch (inStatus)
            {
            default:
            case 0:
                status = (int)TPC.CalendarTypes.EventStatus.None;
                break;
            case 1:
                status = (int)TPC.CalendarTypes.EventStatus.Tentative;
                break;
            case 2:
                status = (int)TPC.CalendarTypes.EventStatus.Confirmed;
                break;
            case 3:
                status = (int)TPC.CalendarTypes.EventStatus.Cancelled;
                break;
            }
            return status;
        }

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
                case 0:
                    index = 1;
                    break;
                case 5:
                    index = 2;
                    break;
                case 15:
                    index = 3;
                    break;
                case 30:
                    index = 4;
                    break;
                }
                break;
            case (int)TPC.CalendarTypes.TickUnit.Hour:
                index = 5;
                break;
            case (int)TPC.CalendarTypes.TickUnit.Day:
                index = 6;
                break;
            case (int)TPC.CalendarTypes.TickUnit.Week:
                index = 7;
                break;
            }
            return index;
        }

        private int getPriorityIndex(int priority)
        {
            int index;
            switch (priority)
            {
            default:
            case (int)TPC.CalendarTypes.Priority.Low:
                index = 0;
                break;
            case (int)TPC.CalendarTypes.Priority.Normal:
                index = 1;
                break;
            case (int)TPC.CalendarTypes.Priority.High:
                index = 2;
                break;
            }
            return index;
        }

        private int getSensitivityIndex(int sensitivity)
        {
            int index;
            switch (sensitivity)
            {
            default:
            case (int)TPC.CalendarTypes.Sensitivity.Public:
                index = 0;
                break;
            case (int)TPC.CalendarTypes.Sensitivity.Private:
                index = 1;
                break;
            case (int)TPC.CalendarTypes.Sensitivity.Confidential:
                index = 2;
                break;
            }
            return index;
        }

        private int getStatusIndex(int status)
        {
            int index;
            switch (status)
            {
            default:
            case (int)TPC.CalendarTypes.EventStatus.None:
                index = 0;
                break;
            case (int)TPC.CalendarTypes.EventStatus.Tentative:
                index = 0;
                break;
            case (int)TPC.CalendarTypes.EventStatus.Confirmed:
                index = 0;
                break;
            case (int)TPC.CalendarTypes.EventStatus.Cancelled:
                index = 0;
                break;
            }
            return index;
        }

        private int getRecurrenceIndex(int recurrence)
        {
            int index;
            switch (recurrence)
            {
            default:
            case (int)TPC.CalendarTypes.Recurrence.None:
                index = 0;
                break;
            case (int)TPC.CalendarTypes.Recurrence.Daily:
                index = 1;
                break;
            case (int)TPC.CalendarTypes.Recurrence.Weekly:
                index = 2;
                break;
            case (int)TPC.CalendarTypes.Recurrence.Monthly:
                index = 3;
                break;
            case (int)TPC.CalendarTypes.Recurrence.Yearly:
                index = 4;
                break;
            }
            return index;
        }

        private void CleanChildRecord(TPC.CalendarRecord record)
        {
            if (record.GetChildRecordCount(Event.Alarm) > 0)
            {
                var alarm = record.GetChildRecord(Event.Alarm, 0);
                record.RemoveChildRecord(Event.Alarm, alarm);
            }
        }

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
            case 0: /// none
                break;
            case 1: /// daily
                record.Set<int>(Event.Freq, (int)TPC.CalendarTypes.Recurrence.Daily);
                break;
            case 2: /// weekly
                record.Set<int>(Event.Freq, (int)TPC.CalendarTypes.Recurrence.Weekly);
                break;
            case 3: /// monthly
                record.Set<int>(Event.Freq, (int)TPC.CalendarTypes.Recurrence.Monthly);
                break;
            case 4: /// yearly
                record.Set<int>(Event.Freq, (int)TPC.CalendarTypes.Recurrence.Yearly);
                break;
            }

            switch (item.UntilType)
            {
            default:
            case 0: /// count
                record.Set<int>(Event.RangeType, (int)TPC.CalendarTypes.RangeType.Count);
                record.Set<int>(Event.Count, item.UntilCount);
                break;
            case 1: /// until
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

            if (item.Recurrence > 0)
            {
                switch (record.Get<int>(Event.RangeType))
                {
                default:
                case (int)TPC.CalendarTypes.RangeType.Count:
                    item.UntilType = 0;
                    item.UntilCount = record.Get<int>(Event.Count);
                    break;
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

        public int Insert(RecordItem item)
        {
            var record = new TPC.CalendarRecord(Event.Uri);
            ItemToRecord(item, record);
            int recordId = manager.Database.Insert(record);
            record.Dispose();
            return recordId;
        }

        public void Update(RecordItem item)
        {
            var record = manager.Database.Get(Event.Uri, item.Index);
            CleanChildRecord(record);
            ItemToRecord(item, record);
            manager.Database.Update(record);
            record.Dispose();
        }

        public void Delete(RecordItem item)
        {
            manager.Database.Delete(Event.Uri, item.Index);
        }

        public List<RecordItem> GetAll()
        {
            var itemList = new List<RecordItem>();
            var list = manager.Database.GetAll(Event.Uri, 0, 0);
            int i;
            for (i = 0; i < list.Count; i++)
            {
                var record = list.GetCurrentRecord();
                var item = new RecordItem();
                //RecordToItem(record, item);
                itemList.Add(item);
                list.MoveNext();
            }
            list.Dispose();
            return itemList;
        }

        private TPC.CalendarList GetUtcInstances(DateTime dt)
        {
            TPC.CalendarList list;

            DateTime firstDate = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, DateTimeKind.Local);
            TPC.CalendarTime from = new TPC.CalendarTime(firstDate.ToUniversalTime().Ticks);
            TPC.CalendarTime to = new TPC.CalendarTime(from.UtcTime.AddDays(1).Ticks);

            TPC.CalendarQuery query = new TPC.CalendarQuery(InstanceUtimeBook.Uri);
            TPC.CalendarFilter filter = new TPC.CalendarFilter(InstanceUtimeBook.Uri,
                    InstanceUtimeBook.Start, TPC.CalendarFilter.IntegerMatchType.GreaterThanOrEqual,
                    from);
            filter.AddCondition(TPC.CalendarFilter.LogicalOperator.And,
                    InstanceUtimeBook.Start, TPC.CalendarFilter.IntegerMatchType.LessThan,
                    to);
            query.SetFilter(filter);
            list = manager.Database.GetRecordsWithQuery(query, 0, 0);
            filter.Dispose();
            query.Dispose();

            return list;
        }

        private TPC.CalendarList GetAlldayInstances(DateTime dt)
        {
            TPC.CalendarList list;

            TPC.CalendarTime from = new TPC.CalendarTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            TPC.CalendarTime to = new TPC.CalendarTime(dt.AddDays(1).Year, dt.AddDays(1).Month, dt.AddDays(1).Day, 0, 0, 0);

            TPC.CalendarQuery query = new TPC.CalendarQuery(InstanceLocaltimeBook.Uri);
            TPC.CalendarFilter filter = new TPC.CalendarFilter(InstanceLocaltimeBook.Uri,
                    InstanceLocaltimeBook.Start, TPC.CalendarFilter.IntegerMatchType.GreaterThanOrEqual,
                    from);
            filter.AddCondition(TPC.CalendarFilter.LogicalOperator.And,
                    InstanceLocaltimeBook.Start, TPC.CalendarFilter.IntegerMatchType.LessThan,
                    to);
            query.SetFilter(filter);
            list = manager.Database.GetRecordsWithQuery(query, 0, 0);
            filter.Dispose();
            query.Dispose();

            return list;
        }

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
                RecordToItem(record, item,
                        instance.Get<TPC.CalendarTime>(InstanceLocaltimeBook.Start),
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
                RecordToItem(record, item,
                        instance.Get<TPC.CalendarTime>(InstanceUtimeBook.Start),
                        instance.Get<TPC.CalendarTime>(InstanceUtimeBook.End), false);
                itemList.Add(item);

                list.MoveNext();
            }
            list.Dispose();

            return itemList;
        }

        public CalendarPort()
        {
            manager = new TPC.CalendarManager();
        }
    }
}


