/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
 *
 */
using System;
using Tizen.NUI;

using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;
using ChannelList;
using ListSample;

/// <summary>
/// namespace for channel list sample
/// </summary>
namespace ListSample
{
    /// <summary>
    /// Program structure.
    /// </summary>
    struct Program
    {
        //program index
        public string programIndex;
        // program name
        public string program;
        // program channel
        public string channel;
        // favorite flag
        public bool favorite; 

        /// <summary>
        /// The constructor with parameters.
        /// </summary>
        /// <param name="programIndex">the program index</param>
        /// <param name="program">the name of the program</param>
        /// <param name="channel">the program channel</param>
        /// <param name="favorite">the favorite flag</param>
        public Program(string programIndex, string program, string channel, bool favorite)
        {
            this.programIndex = programIndex;
            this.program = program;
            this.channel = channel;
            this.favorite = favorite;
        }
    }

    /// <summary>
    /// Sublist information stucture.
    /// </summary>
    struct SubListInfo
    {
        // sublist normal icon
        public string iconN;
        // sublist select icon
        public string iconS;
        // sublist text
        public string text; 

        /// <summary>
        /// The constructor with parameters.
        /// </summary>
        /// <param name="iconN">the path of the normal icon image</param>
        /// <param name="iconS">the path of the select icon image</param>
        /// <param name="text">the text</param>
        public SubListInfo(string iconN, string iconS, string text)
        {
            this.iconN = iconN;
            this.iconS = iconS;
            this.text = text;
        }
    }

    /// <summary>
    /// Favorite information stucture.
    /// </summary>
    struct FavoriteInfo
    {
        public string name; // favorite name
        public int num; //favorite num

        /// <summary>
        /// The constructor with parameters.
        /// </summary>
        /// <param name="name">the favorite name</param>
        /// <param name="num">the favorite num</param>
        public FavoriteInfo(string name, int num)
        {
            this.name = name;
            this.num = num;
        }
    }

    /// <summary>
    /// Channel list data set.
    /// </summary>
    public class ListItemData
    {
        private static Program[] Programs =
        {
            new Program("1", "The Late Late Show With James Corden", "ATV", true),
            new Program("2", "Overnight News", "ABS", false),
            new Program("3", "Liv and Maddie", "CCTV 4", false),
            new Program("4", "Accidents caught on Camera with HD Mirror Cam!", "CETV", true),
            new Program("5", "Morning News", "CBC", true),
            new Program("6", "The Perfect Cup of Coffee by Keurig", "A&E", true),
            new Program("7", "LifeLock Protection", "TVB", false),
            new Program("8", "Street Signs", "Hunan TV", true),
            new Program("9", "News", "FOX", true),
            new Program("10", "Worldwide Exchange", "Disney", false),
            new Program("11", "Noon News", "Goxi", false),
            new Program("12", "CMT Music", "Discovery", true),
            new Program("13", "Unlimited Wardrobe: A Smarter way to dress!", "ABC", false),
            new Program("14", "Scrubs", "ABC", true),
            new Program("15", "Fresh Quilting", "ABC", true),
            new Program("16", "Jalen & Jacoby", "ABC", false),
            new Program("18", "Afternoon News", "ABC", true),
            new Program("19", "College Football", "ABC", false),
            new Program("20", "LifeLock Protection", "ABC", true),
            new Program("21", "Champagne Charlie", "ABC", false),
            new Program("22", "In Time", "ABC", true)
        };

        private static Program[] Favorite1 =
        {
            new Program("0", "The Late Late Show With James Corden", "ATV", true),
            new Program("4", "Accidents caught on Camera with HD Mirror Cam!", "CETV", true),
            new Program("5", "Morning News", "CBC", true),
            new Program("6", "The Perfect Cup of Coffee by Keurig", "A&E", true),
            new Program("8", "Street Signs", "Hunan TV", true),
            new Program("9", "News", "FOX", true),
            new Program("12", "CMT Music", "Discovery", true),
            new Program("14", "Scrubs", "ABC", true),
            new Program("15", "Fresh Quilting", "ABC", true),
            new Program("18", "Afternoon News", "ABC", true),
            new Program("20", "LifeLock Protection", "ABC", true),
            new Program("22", "In Time", "ABC", true)
        };

        private static Program[] Favorite2 =
        {
            new Program("0", "The Late Late Show With James Corden", "ATV", true),
            new Program("4", "Accidents caught on Camera with HD Mirror Cam!", "CETV", true),
            new Program("8", "Street Signs", "Hunan TV", true),
            new Program("9", "News", "FOX", true),
            new Program("12", "CMT Music", "Discovery", true),
            new Program("18", "Afternoon News", "ABC", true),
            new Program("22", "In Time", "ABC", true)
        };

        private static Program[] Favorite3 =
        {
            new Program("0", "The Late Late Show With James Corden", "ATV", true),
            new Program("5", "Morning News", "CBC", true),
            new Program("8", "Street Signs", "Hunan TV", true),
            new Program("12", "CMT Music", "Discovery", true),
            new Program("15", "Fresh Quilting", "ABC", true),
            new Program("20", "LifeLock Protection", "ABC", true)
        };

        private static Program[] Favorite4 =
        {
            new Program("4", "Accidents caught on Camera with HD Mirror Cam!", "CETV", true),
            new Program("6", "The Perfect Cup of Coffee by Keurig", "A&E", true),
            new Program("9", "News", "FOX", true),
            new Program("14", "Scrubs", "ABC", true),
            new Program("18", "Afternoon News", "ABC", true)
        };

        private static Program[] Favorite5 =
        {
        };

        private static Program[] Action =
        {
            new Program("0", "The Late Late Show With James Corden", "ATV", true),
            new Program("2", "Overnight News", "ABS", false),
            new Program("3", "Liv and Maddie", "CCTV 4", false),
            new Program("4", "Accidents caught on Camera with HD Mirror Cam!", "CETV", true),
            new Program("5", "Morning News", "CBC", true),
            new Program("6", "The Perfect Cup of Coffee by Keurig", "A&E", true),
            new Program("7", "LifeLock Protection", "TVB", false),
            new Program("8", "Street Signs", "Hunan TV", true)
        };

        private static Program[] Adventure =
        {
            new Program("4", "Accidents caught on Camera with HD Mirror Cam!", "CETV", true),
            new Program("5", "Morning News", "CBC", true),
            new Program("6", "The Perfect Cup of Coffee by Keurig", "A&E", true),
            new Program("7", "LifeLock Protection", "TVB", false),
            new Program("8", "Street Signs", "Hunan TV", true),
            new Program("9", "News", "FOX", true),
            new Program("10", "Worldwide Exchange", "Disney", false),
            new Program("11", "Noon News", "Goxi", false)
        };

        private static Program[] Animation =
        {
            new Program("14", "Scrubs", "ABC", true),
            new Program("15", "Fresh Quilting", "ABC", true),
            new Program("16", "Jalen & Jacoby", "ABC", false),
            new Program("18", "Afternoon News", "ABC", true),
            new Program("19", "College Football", "ABC", false),
            new Program("20", "LifeLock Protection", "ABC", true),
            new Program("21", "Champagne Charlie", "ABC", false),
            new Program("22", "In Time", "ABC", true)
        };

        private static Program[] Comedy =
        {
            new Program("12", "CMT Music", "Discovery", true),
            new Program("13", "Unlimited Wardrobe: A Smarter way to dress!", "ABC", false),
            new Program("14", "Scrubs", "ABC", true),
            new Program("15", "Fresh Quilting", "ABC", true),
            new Program("16", "Jalen & Jacoby", "ABC", false),
            new Program("18", "Afternoon News", "ABC", true)
        };

        private static Program[] Crime =
        {
            new Program("8", "Street Signs", "Hunan TV", true),
            new Program("9", "News", "FOX", true),
            new Program("10", "Worldwide Exchange", "Disney", false)
        };

        private static Program[] Drama =
        {
            new Program("7", "LifeLock Protection", "TVB", false),
            new Program("8", "Street Signs", "Hunan TV", true),
            new Program("18", "Afternoon News", "ABC", true),
            new Program("19", "College Football", "ABC", false),
            new Program("20", "LifeLock Protection", "ABC", true),
            new Program("21", "Champagne Charlie", "ABC", false)
        };

        private static Program[] Family =
        {
            new Program("14", "Scrubs", "ABC", true),
            new Program("15", "Fresh Quilting", "ABC", true),
            new Program("16", "Jalen & Jacoby", "ABC", false),
            new Program("18", "Afternoon News", "ABC", true),
            new Program("19", "College Football", "ABC", false)
        };

        private string programIndex; //program index.
        private string program; //program
        private string channel; // channel
        private bool favorite; // favorite flag
        private int num = 0; //number.

        /// <summary>
        /// ListItemData constructor with parameters.
        /// </summary>
        /// <param name="name">the name of the channel</param>
        /// <param name="index">the index of the channel</param>
        public ListItemData(string name, int index)
        {
            if (name == "All_Channel")
            {
                num = Programs.Length;
                if (index < num)
                {
                    programIndex = Programs[index].programIndex;
                    program = Programs[index].program;
                    channel = Programs[index].channel;
                    favorite = Programs[index].favorite;
                }
            }
            else if (name == "Favorite1")
            {
                num = Favorite1.Length;
                if (index < num)
                {
                    programIndex = Favorite1[index].programIndex;
                    program = Favorite1[index].program;
                    channel = Favorite1[index].channel;
                    favorite = Favorite1[index].favorite;
                }
            }
            else if (name == "Favorite2")
            {
                num = Favorite2.Length;
                if (index < num)
                {
                    programIndex = Favorite2[index].programIndex;
                    program = Favorite2[index].program;
                    channel = Favorite2[index].channel;
                    favorite = Favorite2[index].favorite;
                }
            }
            else if (name == "Favorite3")
            {
                num = Favorite3.Length;
                if (index < num)
                {
                    programIndex = Favorite3[index].programIndex;
                    program = Favorite3[index].program;
                    channel = Favorite3[index].channel;
                    favorite = Favorite3[index].favorite;
                }
            }
            else if (name == "Favorite4")
            {
                num = Favorite4.Length;
                if (index < num)
                {
                    programIndex = Favorite4[index].programIndex;
                    program = Favorite4[index].program;
                    channel = Favorite4[index].channel;
                    favorite = Favorite4[index].favorite;
                }
            }
            else if (name == "Favorite5")
            {
                num = Favorite5.Length;
                if (index < num)
                {
                    programIndex = Favorite5[index].programIndex;
                    program = Favorite5[index].program;
                    channel = Favorite5[index].channel;
                    favorite = Favorite5[index].favorite;
                }
            }
            else if (name == "Action")
            {
                num = Action.Length;
                if (index < num)
                {
                    programIndex = Action[index].programIndex;
                    program = Action[index].program;
                    channel = Action[index].channel;
                    favorite = Action[index].favorite;
                }
            }
            else if (name == "Adventure")
            {
                num = Adventure.Length;
                if (index < num)
                {
                    programIndex = Adventure[index].programIndex;
                    program = Adventure[index].program;
                    channel = Adventure[index].channel;
                    favorite = Adventure[index].favorite;
                }
            }
            else if (name == "Animation")
            {
                num = Animation.Length;
                if (index < num)
                {
                    programIndex = Animation[index].programIndex;
                    program = Animation[index].program;
                    channel = Animation[index].channel;
                    favorite = Animation[index].favorite;
                }
            }
            else if (name == "Comedy")
            {
                num = Comedy.Length;
                if (index < num)
                {
                    programIndex = Comedy[index].programIndex;
                    program = Comedy[index].program;
                    channel = Comedy[index].channel;
                    favorite = Comedy[index].favorite;
                }
            }
            else if (name == "Crime")
            {
                num = Crime.Length;
                if (index < num)
                {
                    programIndex = Crime[index].programIndex;
                    program = Crime[index].program;
                    channel = Crime[index].channel;
                    favorite = Crime[index].favorite;
                }
            }
            else if (name == "Drama")
            {
                num = Drama.Length;
                if (index < num)
                {
                    programIndex = Drama[index].programIndex;
                    program = Drama[index].program;
                    channel = Drama[index].channel;
                    favorite = Drama[index].favorite;
                }
            }
            else if (name == "Family")
            {
                num = Family.Length;
                if (index < num)
                {
                    programIndex = Family[index].programIndex;
                    program = Family[index].program;
                    channel = Family[index].channel;
                    favorite = Family[index].favorite;
                }
            }
        }

        /// <summary>
        /// Get all channel list number.
        /// </summary>
        public int Num
        {
            get
            {
                return num;
            }
        }

        /// <summary>
        /// Get wheather the program is favorite.
        /// </summary>
        public bool Favorite
        {
            get
            {
                return favorite;
            }
        }

        /// <summary>
        /// Get the program index.
        /// </summary>
        public string ProgramIndex
        {
            get
            {
                return programIndex;
            }
        }

        /// <summary>
        /// Get the program name.
        /// </summary>
        public string Program
        {
            get
            {
                return program;
            }
        }

        /// <summary>
        /// Get the program channel.
        /// </summary>
        public string Channel
        {
            get
            {
                return channel;
            }
        }
    }

    /// <summary>
    /// Sublist item data.
    /// </summary>
    public class SubListData
    {
        private static string resourcesChannel = "/home/owner/apps_rw/org.tizen.example.ChannelList/res/images/channelList/"; // channel resource path
        private static string iconAllNImage = resourcesChannel + "channel_list_icon_all_n.png"; // normal icon all.
        private static string iconAllSImage = resourcesChannel + "channel_list_icon_all_s.png"; // select icon all.
        private static string iconSamsungtvNImage = resourcesChannel + "channel_list_icon_samsungtv_n.png"; //normal samsung tv icon
        private static string iconSamsungtvSImage = resourcesChannel + "channel_list_icon_samsungtv_s.png"; //select samsung tv icon
        private static string iconFavoriteNImage = resourcesChannel + "channel_list_icon_favorite_n.png"; //normal favorite icon
        private static string iconFavoriteSImage = resourcesChannel + "channel_list_icon_favorite_s.png"; //selected favorite icon
        private static string iconGenreNImage = resourcesChannel + "channel_list_icon_genre_n.png"; //normal genre icon
        private static string iconGenreSImage = resourcesChannel + "channel_list_icon_genre_s.png"; //select genre icon
        private static string iconCategoryNImage = resourcesChannel + "channel_list_icon_category_n.png"; // normal category icon
        private static string iconCategorySImage = resourcesChannel + "channel_list_icon_category_s.png"; // select categoty icon
        private static string iconSortNImage = resourcesChannel + "channel_list_icon_sort_n.png"; //normal sort icon
        private static string iconSortSImage = resourcesChannel + "channel_list_icon_sort_s.png"; //select sort icon
        private static string iconSatelliteNImage = resourcesChannel + "channel_list_icon_satellite_n.png"; //normal satelite icon
        private static string iconSatelliteSImage = resourcesChannel + "channel_list_icon_satellite_s.png"; //select satelite icon
        private int num = subListInfo.Length; //sublist num
        private string iconS; //select icon
        private string iconN; //normal icon
        private string text; //item text

        private static SubListInfo[] subListInfo =
        {
            new SubListInfo(iconAllNImage, iconAllSImage, "All Channels"),
            new SubListInfo(iconFavoriteNImage, iconFavoriteSImage, "Favorite"),
            new SubListInfo(iconGenreNImage, iconGenreSImage, "Genre")
            //new SubListInfo(iconCategoryNImage, iconCategorySImage, "Category"),
            //new SubListInfo(iconSortNImage, iconSortSImage, "Sort"),
            //new SubListInfo(iconSamsungtvNImage, iconSamsungtvSImage, "TV Plus"),
            //new SubListInfo(iconSatelliteNImage, iconSatelliteSImage, "Satellite")
        };

        /// <summary>
        /// SubListData constructor.
        /// </summary>
        /// <param name="index">the index</param>
        public SubListData(int index)
        {
            if (index < num)
            {
                iconN = subListInfo[index].iconN;
                iconS = subListInfo[index].iconS;
                text = subListInfo[index].text;
            }
        }

        /// <summary>
        /// Get sublist number.
        /// </summary>
        public int Num
        {
            get
            {
                return num;
            }
        }

        /// <summary>
        /// Get normal icon url of sublist.
        /// </summary>
        public string IconN
        {
            get
            {
                return iconN;
            }
        }

        /// <summary>
        /// Get selected icon url of sublist.
        /// </summary>
        public string IconS
        {
            get
            {
                return iconS;
            }
        }

        /// <summary>
        /// Get item text of sublist.
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
        }
    }

    /// <summary>
    /// Favorite list item data.
    /// </summary>
    public class FavoriteListData
    {
        private static FavoriteInfo[] Favorite =
        {
            new FavoriteInfo("Favorite1", new ListItemData("Favorite1", 0).Num),
            new FavoriteInfo("Favorite2", new ListItemData("Favorite2", 0).Num),
            new FavoriteInfo("Favorite3", new ListItemData("Favorite3", 0).Num),
            new FavoriteInfo("Favorite4", new ListItemData("Favorite4", 0).Num),
            new FavoriteInfo("Favorite5", new ListItemData("Favorite5", 0).Num)
        };

        private int num; //favorite list total number.
        private int favoriteNum; //favorite no.
        private string name; //item name

        /// <summary>
        /// Get favorite list total number.
        /// </summary>
        public int Num
        {
            get
            {
                return num;
            }
        }

        /// <summary>
        /// Get favorite no.
        /// </summary>
        public int FavoriteNum
        {
            get
            {
                return favoriteNum;
            }
        }

        /// <summary>
        /// Get favorite program name.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Set favorite program information.
        /// </summary>
        /// <param name="index">the index</param>
        public FavoriteListData(int index)
        {
            num = Favorite.Length;
            if (index < num)
            {
                favoriteNum = Favorite[index].num;
                name = Favorite[index].name;
            }
        }
    }

    /// <summary>
    /// Genre List class.
    /// </summary>
    public class GenreListData
    {
        private static string[] Genre =
        {
            "Action",
            "Adventure",
            "Animation",
            "Comedy",
            "Crime",
            "Drama",
            "Family"
        };

        private int num; // genre list total num.
        private string name; //genre list item name.

        /// <summary>
        /// Get/Set genre program number.
        /// </summary>
        public int Num
        {
            get
            {
                return num;
            }
        }

        /// <summary>
        /// Get/Set genre program name.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Set genre program information.
        /// </summary>
        /// <param name="index">the index</param>
        public GenreListData(int index)
        {
            num = Genre.Length;
            if (index < num)
            {
                name = Genre[index];
            }
        }
    }
}