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

using System.Collections.Generic;
using TPC = Tizen.Pims.Contacts;
using Contacts.Models;

namespace Contacts.Tizen.Port
{
    using TPC.ContactsViews;

    /// <summary>
    /// Represents the Contacts APIs for connecting Sample Contact app.
    /// </summary>
    public class ContactsPort : IContactsAPIs
    {
        /// <summary>
        /// Contact manager.
        /// </summary>
        private readonly TPC.ContactsManager manager;

        /// <summary>
        /// Clean child record.
        /// </summary>
        private void CleanChildRecord(TPC.ContactsRecord record)
        {
            if (record.GetChildRecordCount(Contact.Name) > 0)
            {
                var name = record.GetChildRecord(Contact.Name, 0);
                record.RemoveChildRecord(Contact.Name, name);
            }

            if (record.GetChildRecordCount(Contact.Number) > 0)
            {
                var number = record.GetChildRecord(Contact.Number, 0);
                record.RemoveChildRecord(Contact.Number, number);
            }

            if (record.GetChildRecordCount(Contact.Email) > 0)
            {
                var email = record.GetChildRecord(Contact.Email, 0);
                record.RemoveChildRecord(Contact.Email, email);
            }

            if (record.GetChildRecordCount(Contact.URL) > 0)
            {
                var url = record.GetChildRecord(Contact.URL, 0);
                record.RemoveChildRecord(Contact.URL, url);
            }

            if (record.GetChildRecordCount(Contact.Company) > 0)
            {
                var company = record.GetChildRecord(Contact.Company, 0);
                record.RemoveChildRecord(Contact.Company, company);
            }

            if (record.GetChildRecordCount(Contact.Event) > 0)
            {
                var ievent = record.GetChildRecord(Contact.Event, 0);
                record.RemoveChildRecord(Contact.Event, ievent);
            }

            if (record.GetChildRecordCount(Contact.Note) > 0)
            {
                var note = record.GetChildRecord(Contact.Note, 0);
                record.RemoveChildRecord(Contact.Note, note);
            }
        }

        /// <summary>
        /// Get record from item.
        /// </summary>
        private void ItemToRecord(RecordItem item, TPC.ContactsRecord record)
        {
            var name = new TPC.ContactsRecord(Name.Uri);
            name.Set<string>(Name.First, item.First);
            name.Set<string>(Name.Last, item.Last);
            record.AddChildRecord(Contact.Name, name);

            var number = new TPC.ContactsRecord(Number.Uri);
            number.Set<string>(Number.NumberData, item.Number);
            record.AddChildRecord(Contact.Number, number);

            var email = new TPC.ContactsRecord(Email.Uri);
            email.Set<string>(Email.Address, item.Email);
            record.AddChildRecord(Contact.Email, email);

            var url = new TPC.ContactsRecord(URL.Uri);
            url.Set<string>(URL.URLData, item.Url);
            record.AddChildRecord(Contact.URL, url);

            var company = new TPC.ContactsRecord(Company.Uri);
            company.Set<string>(Company.Label, item.Company);
            record.AddChildRecord(Contact.Company, company);

            var ievent = new TPC.ContactsRecord(Event.Uri);
            ievent.Set<int>(Event.Date, item.Event);
            record.AddChildRecord(Contact.Event, ievent);

            var note = new TPC.ContactsRecord(Note.Uri);
            note.Set<string>(Note.Contents, item.Note);
            record.AddChildRecord(Contact.Note, note);
        }

        /// <summary>
        /// Get item from record.
        /// </summary>
        private void RecordToItem(TPC.ContactsRecord record, RecordItem item)
        {
            item.DisplayName = record.Get<string>(Contact.DisplayName);

            item.Index = record.Get<int>(Contact.Id);

            if (record.GetChildRecordCount(Contact.Name) > 0)
            {
                var name = record.GetChildRecord(Contact.Name, 0);
                item.First = name.Get<string>(Name.First);
                item.Last = name.Get<string>(Name.Last);
            }

            if (record.GetChildRecordCount(Contact.Number) > 0)
            {
                var number = record.GetChildRecord(Contact.Number, 0);
                item.Number = number.Get<string>(Number.NumberData);
            }

            if (record.GetChildRecordCount(Contact.Email) > 0)
            {
                var email = record.GetChildRecord(Contact.Email, 0);
                item.Email = email.Get<string>(Email.Address);
            }

            if (record.GetChildRecordCount(Contact.URL) > 0)
            {
                var url = record.GetChildRecord(Contact.URL, 0);
                item.Url = url.Get<string>(URL.URLData);
            }

            if (record.GetChildRecordCount(Contact.Company) > 0)
            {
                var company = record.GetChildRecord(Contact.Company, 0);
                item.Company = company.Get<string>(Company.Label);
            }

            if (record.GetChildRecordCount(Contact.Event) > 0)
            {
                var ievent = record.GetChildRecord(Contact.Event, 0);
                item.Event = ievent.Get<int>(Event.Date);
            }

            if (record.GetChildRecordCount(Contact.Note) > 0)
            {
                var note = record.GetChildRecord(Contact.Note, 0);
                item.Note = note.Get<string>(Note.Contents);
            }
        }

        /// <summary>
        /// Insert item.
        /// </summary>
        public int Insert(RecordItem item)
        {
            var record = new TPC.ContactsRecord(Contact.Uri);
            ItemToRecord(item, record);
            int recordId = manager.Database.Insert(record);
            return recordId;
        }

        /// <summary>
        /// Update item.
        /// </summary>
        public void Update(RecordItem item)
        {
            var record = manager.Database.Get(Contact.Uri, item.Index);
            CleanChildRecord(record);
            ItemToRecord(item, record);
            manager.Database.Update(record);
        }

        /// <summary>
        /// Delete item.
        /// </summary>
        public void Delete(RecordItem item)
        {
            manager.Database.Delete(Contact.Uri, item.Index);
        }

        /// <summary>
        /// Get all list.
        /// </summary>
        public List<RecordItem> GetAll()
        {
            var itemList = new List<RecordItem>();

            TPC.ContactsList list = manager.Database.GetAll(Contact.Uri, 0, 0);

            int i;
            for (i = 0; i < list.Count; i++)
            {
                var record = list.GetCurrentRecord();
                var item = new RecordItem();
                RecordToItem(record, item);
                itemList.Add(item);
                list.MoveNext();
            }
            return itemList;
        }

        /// <summary>
        /// A constructor.
        /// </summary>
        public ContactsPort()
        {
            manager = new TPC.ContactsManager();
        }
    }
}
