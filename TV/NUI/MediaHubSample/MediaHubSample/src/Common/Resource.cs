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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System;
using System.Collections.Generic;

namespace Tizen.NUI.MediaHub
{

    /// <summary>
    /// Data source of the GridView
    /// </summary>
    public class CommonResource
    {
        //the bgView of the main page
        private static View bgView = null;
        //the footView on the main page
        private static FootView footView = null;
        //the list which includes all items for all resources
        private static List<object> dataList = null;
        //the list which includes all items for all photos
        private static List<object> photoDataList = null;
        //the list which includes all items for all videos
        private static List<object> videoDataList = null;
        //the list which includes all items for all musics
        private static List<object> musicDataList = null;
        //the list which includes all items for all resources and the items are sorted by size
        private static List<object> sortedList = new List<object>();
        //the list which includes photos for all resources and the items are sorted by size
        private static List<object> sortedPhotoList = new List<object>();
        //the list which includes videos for all resources and the items are sorted by size
        private static List<object> sortedVideoList = new List<object>();
        //the list which includes musics for all resources and the items are sorted by size
        private static List<object> sortedMusicList = new List<object>();

        /// <summary>
        /// The path of the image resource.
        /// </summary>
        /// <returns>return the path of the resource</returns>
        public static string GetLocalReosurceURL()
        {
            return @"/home/owner/apps_rw/org.tizen.example.MediaHubSample/res/images/mediaHub/";
        }

        /// <summary>
        /// The path of the image resource.
        /// </summary>
        public static string DeviceName
        {
            get
            {
                return "Media Hub Sample";
            }
        }

        /// <summary>
        /// Get/Set the main page instance.
        /// </summary>
        public static MainPage MainViewInstance { get; set; }

        /// <summary>
        /// Get/Set the bg view.
        /// </summary>
        public static View BGView
        {
            get
            {
                return bgView;
            }

            set
            {
                bgView = value;
            }
        }

        /// <summary>
        /// Get/Set the foot view
        /// </summary>
        public static FootView FootView
        {
            get
            {
                return footView;
            }

            set
            {
                footView = value;
            }
        }

        /// <summary> 
        /// All the Data on the devices. 
        /// </summary>
        public static List<object> DataList
        {
            get
            {
                return dataList;
            }

            set
            {
                dataList = value;
            }
        }

        /// <summary> 
        /// The Data which just include the photo on the devices. 
        /// </summary>
        public static List<object> PhotoDataList
        {
            get
            {
                return photoDataList;
            }

            set
            {
                photoDataList = value;
            }
        }

        /// <summary> 
        /// The Data which just include the video on the devices. 
        /// </summary>
        public static List<object> VideoDataList
        {
            get
            {
                return videoDataList;
            }

            set
            {
                videoDataList = value;
            }
        }

        /// <summary> 
        /// The Data which just include the music on the devices. 
        /// </summary>
        public static List<object> MusicDataList
        {
            get
            {
                return musicDataList;
            }

            set
            {
                musicDataList = value;
            }
        }

        /// <summary> 
        /// All the Data on the devices and sorted by the size. 
        /// </summary>
        public static List<object> SortedList
        {
            get
            {
                return sortedList;
            }
        }

        /// <summary> 
        /// The Data which just include the photo on the devices and sorted by the size. 
        /// </summary>
        public static List<object> SortedPhotoList
        {
            get
            {
                return sortedPhotoList;
            }
        }

        /// <summary> 
        /// The Data which just include the video on the devices and sorted by the size. 
        /// </summary>
        public static List<object> SortedVideoList
        {
            get
            {
                return sortedVideoList;
            }
        }

        /// <summary> 
        /// The Data which just include the music on the devices and sorted by the size. 
        /// </summary>
        public static List<object> SortedMusicList
        {
            get
            {
                return sortedMusicList;
            }
        }
    }

    /// <summary>
    /// Content type.
    /// </summary>
    public static class ContentViewType
    {
        /// <summary>
        /// All types.
        /// </summary>
        public const int ALL = 0;
        /// <summary>
        /// Video type.
        /// </summary>
        public const int VIDEO = 1;
        /// <summary>
        /// Photo type.
        /// </summary>
        public const int PHOTO = 2;
        /// <summary>
        /// Music type.
        /// </summary>
        public const int MUSIC = 3;
        /// <summary>
        /// Record type.
        /// </summary>
        public const int RECORD = 4;
    }
}
