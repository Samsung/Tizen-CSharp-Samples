using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using Clock.Interfaces;
using Clock.Models;
using System;
using System.IO;

namespace Clock.DataProvider
{
    public class SQLiteDBAccessor
    {
        static readonly object locker = new object();
        private SQLiteConnection database;
        public static SQLiteDBAccessor Database { get; private set; }
        private static string DatabasePath;
        private const string DatabaseFilename = "TodoSQLite.db3";
        private static SQLiteDBAccessor __Instance;
        public static SQLiteDBAccessor Instance
        {
            get {
                if (__Instance == null)
                {
                    __Instance = new SQLiteDBAccessor();
                }
                return __Instance;
            }
            private set {

            }
        }

        public SQLiteDBAccessor()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
            SQLitePCL.raw.FreezeProvider(true);
            string dataPath = global::Tizen.Applications.Application.Current.DirectoryInfo.Data;
            DatabasePath = Path.Combine(dataPath, DatabaseFilename);
            database = GetConnection();
            database.CreateTable<AlarmRecord>();
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(DatabasePath);
        }

        public IEnumerable<AlarmRecord> GetItems()
        {
            lock (locker)
            {
                return database.Table<AlarmRecord>().ToList();
            }
        }

        public AlarmRecord Find(DateTime dt)
        {
            AlarmRecord ret = null;
            lock (locker)
            {
                var alarmRecordList = database.Table<AlarmRecord>().ToList();
                foreach (var ar in alarmRecordList)
                {
                    if (ar.ScheduledDateTime.Hour == dt.Hour &&
                        ar.ScheduledDateTime.Minute == dt.Minute)
                    {
                        ret = ar;
                        break;
                    }
                }
            }
            return ret;
        }

        public int Insert(AlarmRecord item)
        {
            lock (locker)
            {
                return database.Insert(item);
            }
        }

        public int Update(AlarmRecord item)
        {
            lock (locker)
            {
                return database.Update(item);
            }
        }

        public int Delete(AlarmRecord item)
        {
            lock (locker)
            {
                return database.Delete<AlarmRecord>(item.DateCreated);
            }
        }
    }
}
