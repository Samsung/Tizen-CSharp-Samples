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
using TPC = Tizen.Pims.Contacts;
using Contacts.Models;

namespace Contacts.Tizen.Port
{
    using TPC.ContactsViews;

    public class ContactsPort : IContactsAPIs
    {
        private TPC.ContactsManager manager;

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

        private void RecordToItem(TPC.ContactsRecord record, RecordItem item)
        {
            item.DisplayName = record.Get<string>(Contact.DisplayName);

            var name = record.GetChildRecord(Contact.Name, 0);
            item.First = name.Get<string>(Name.First);
            item.Last = name.Get<string>(Name.Last);

            var number = record.GetChildRecord(Contact.Number, 0);
            item.Number = number.Get<string>(Number.NumberData);

            var email = record.GetChildRecord(Contact.Email, 0);
            item.Email = email.Get<string>(Email.Address);

            var url = record.GetChildRecord(Contact.URL, 0);
            item.Url = url.Get<string>(URL.URLData);

            var company = record.GetChildRecord(Contact.Company, 0);
            item.Company = company.Get<string>(Company.Label);

            var ievent = record.GetChildRecord(Contact.Event, 0);
            item.Event = ievent.Get<int>(Event.Date);

            var note = record.GetChildRecord(Contact.Note, 0);
            item.Note = note.Get<string>(Note.Contents);
        }

        public int Insert(RecordItem item)
        {
            var record = new TPC.ContactsRecord(Contact.Uri);
            ItemToRecord(item, record);
            int recordId = manager.Database.Insert(record);
            return recordId;
        }

        public void Update(RecordItem item)
        {
            var record = manager.Database.Get(Contact.Uri, item.Index);
            ItemToRecord(item, record);
            manager.Database.Update(record);
        }

        public void Delete(RecordItem item)
        {

        }

        public List<RecordItem> GetAll()
        {
            var itemList = new List<RecordItem>();
            TPC.ContactsList list = null;
            list = manager.Database.GetAll(Contact.Uri, 0, 0);
            int i;
            for (i = 0; i < list.Count; i++)
            {
                var record = list.GetCurrentRecord();
                var item = new RecordItem();
                RecordToItem(record, item);
                itemList.Add(item);
                list.MoveNext();
            }
            list?.Dispose();
            return itemList;
        }

        public ContactsPort()
        {
            manager = new TPC.ContactsManager();
        }
    }
}
