//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.IO;

namespace SQLite.NET.Sample
{
    /// <summary>
    /// A definition of each row of  table Item in the database
    /// </summary>
    public class Item
    {
        /// <summary>
        /// A primary key 'ID' in the Item table
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// Created time
        /// </summary>
        public string Time { get; set; }
    }

    /// <summary>
    /// A main class of SQLite sample application. 
    /// This class creates a database 'SQLite3.db3' in its data directory.
    /// Then, it creates a table 'Item' and insert two items.
    /// Note that to use SQLite, you need to add three SQLite nuget dependencies
    /// (SQLite, sqlite-net-base, and SQLitePCLRaw.provider.sqlite3.netstandard11).
    /// </summary>
    public class App : Application
    {
        // The file name of database to be saved in the application writable directory.
        const string databaseFileName = "SQLite3.db3";
        // The file name of database to be saved in the application writable directory.
        SQLiteConnection dbConnection;
        static string databasePath;

        public App()
        {
            // Initiate database first.
            InitiateSQLite();
            // Get items in the Item table.
            var itemList = dbConnection.Table<Item>();
            List<string> dbList = new List<string>();
            dbList.Add("Rows in Item table:");
            // Add to itemList to be used as ItemsSource in a CircleListView.
            foreach (var item in itemList)
            {
                dbList.Add("Row " + item.Id + ": " + item.Time);
            }

            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new ListView
                        {
                            ItemsSource = dbList
                        }
                    }
                }
            };
        }

        public void InitiateSQLite()
        {
            bool needCreateTable = false;
            // Get writable directory info for this app.
            string dataPath = global::Tizen.Applications.Application.Current.DirectoryInfo.Data;
            // Combine with database path and name
            databasePath = Path.Combine(dataPath, databaseFileName);

            // Check the database file to decide table creation.
            if (!File.Exists(databasePath))
            {
                needCreateTable = true;
            }

            dbConnection = new SQLiteConnection(databasePath);
            if (needCreateTable)
            {
                dbConnection.CreateTable<Item>();
            }
        
            //insert into table
            Item item1 = new Item
            {
                Time = DateTime.Now.ToString(),
            };
            dbConnection.Insert(item1);

            Item item2 = new Item
            {
                Time = DateTime.Now.ToString(),
            };

            dbConnection.Insert(item2);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
