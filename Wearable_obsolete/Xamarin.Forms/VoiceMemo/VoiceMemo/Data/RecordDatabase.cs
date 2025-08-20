﻿/*
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

using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoiceMemo.Models;

namespace VoiceMemo.Data
{
    /// <summary>
    /// Database class
    /// It stores collection of records
    /// </summary>
    public class RecordDatabase
    {
        // SQLite connection
        private readonly AsyncLazy<SQLiteAsyncConnection> database;

        public RecordDatabase(string dbPath)
        {
            database = new AsyncLazy<SQLiteAsyncConnection>(async () =>
            {
                var db = new SQLiteAsyncConnection(dbPath);
                await db.CreateTableAsync<Record>();
                return db;
            });
        }

        /// <summary>
        /// Get list or records in database
        /// </summary>
        /// <returns>Task<List<Record>></returns>
        public async Task<List<Record>> GetItemsAsync()
        {
            return await (await database).Table<Record>().ToListAsync();
        }

        /// <summary>
        /// Save record in database
        /// </summary>
        /// <param name="item">Record</param>
        /// <returns>Task<int></returns>
        public async Task<int> SaveItemAsync(Record item)
        {
            if (item.ID != 0)
            {
                // in case that item already exists in database
                return await (await database).UpdateAsync(item);
            }
            else
            {
                // for the first time item will be added in database
                return await (await database).InsertAsync(item);
            }
        }

        /// <summary>
        /// Delete record from database
        /// </summary>
        /// <param name="item">Record</param>
        /// <returns>Task<int></returns>
        public async Task<int> DeleteItemAsync(Record item)
        {
            return await (await database).DeleteAsync(item);
        }
    }
}
