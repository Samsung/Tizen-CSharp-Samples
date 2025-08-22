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
    /// Create the data struct.
    /// </summary>
    public class ResourceData
    {
        //the list which includes all items for all resources
        private List<object> dataList;
        //the list which includes all items for all photos
        private List<object> photoDataList;
        //the list which includes all items for all videos
        private List<object> videoDataList;
        //the list which includes all items for all musics
        private List<object> musicDataList;
        //the list which includes all items for all resources and the items are sorted by size
        private List<ContentModel> sortedDataList;
        //the list which includes photos for all resources and the items are sorted by size
        private List<ContentModel> sortedPhotoList;
        //the list which includes videos for all resources and the items are sorted by size
        private List<ContentModel> sortedVideoList;
        //the list which includes musics for all resources and the items are sorted by size
        private List<ContentModel> sortedMusicList;
        //the random instance to create random data.
        private Random myRandom;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResourceData()
        {
            myRandom = new Random();
            dataList = new List<object>();
            photoDataList = new List<object>();
            videoDataList = new List<object>();
            musicDataList = new List<object>();

            sortedDataList = new List<ContentModel>();
            sortedPhotoList = new List<ContentModel>();
            sortedVideoList = new List<ContentModel>();
            sortedMusicList = new List<ContentModel>();

            Initialize();
        }

        /// <summary>
        /// The method to create the data
        /// </summary>
        private void Initialize()
        {
            int size = (int)myRandom.Next(0, 100);
            int year = (int)myRandom.Next(2015, 2017);
            int month = (int)myRandom.Next(0, 12);
            int day = (int)myRandom.Next(0, 28);
            for (int index = 0; index < 60; index++)
            {
                if (index == 0)
                {
                    //Create some items for music
                    ContentModel contentModel = new ContentModel(index, index.ToString(), index.ToString(), ContentItemType.eItemMusic, index.ToString(), "", "", 10);
                    contentModel.Format = "MP3";
                    contentModel.Size = (size * (index + 1)).ToString();
                    contentModel.Data = day + "/" + month + "/" + year;
                    dataList.Add(contentModel);
                    sortedDataList.Add(contentModel);
                    musicDataList.Add(contentModel);
                    sortedMusicList.Add(contentModel);
                }
                else if (index > 10 && index <= 20)
                {
                    //Create some items for movies
                    string path = CommonResource.GetLocalReosurceURL() + "Movies/" + (index - 10).ToString() + ".jpg";
                    ContentModel contentModel = new ContentModel(index, index.ToString(), "NO." + index.ToString(), ContentItemType.eItemVideo, index.ToString(), path, "", 10);
                    contentModel.ThumbnailPath = path;
                    contentModel.ThumbDone = true;
                    contentModel.Format = "MP4";
                    contentModel.Data = day + "/" + month + "/" + year;
                    contentModel.Size = (size * (index + 1)).ToString();
                    dataList.Add(contentModel);
                    sortedDataList.Add(contentModel);
                    videoDataList.Add(contentModel);
                    sortedVideoList.Add(contentModel);
                }
                else if (index > 53)
                {
                    if (index % 2 == 0)
                    {
                        //Create some items for music
                        string path = CommonResource.GetLocalReosurceURL() + "Musics/" + ((index - 50) / 2).ToString() + ".jpg";
                        ContentModel contentModel = new ContentModel(index, index.ToString(), "NO." + index.ToString(), ContentItemType.eItemMusic, index.ToString(), path, "", 10);
                        contentModel.ThumbnailPath = path;
                        contentModel.ThumbDone = true;
                        contentModel.Format = "MP3";
                        contentModel.Data = day + "/" + month + "/" + year;
                        contentModel.Size = (size * (index + 1)).ToString();
                        dataList.Add(contentModel);
                        sortedDataList.Add(contentModel);
                        musicDataList.Add(contentModel);
                        sortedMusicList.Add(contentModel);
                    }
                    else
                    {
                        //Create some items for music
                        ContentModel contentModel = new ContentModel(index, index.ToString(), "NO." + index.ToString(), ContentItemType.eItemMusic, index.ToString(), "", "", 10);
                        contentModel.Format = "MP3";
                        contentModel.Data = day + "/" + month + "/" + year;
                        contentModel.Size = (size * (index + 1)).ToString();
                        dataList.Add(contentModel);
                        sortedDataList.Add(contentModel);
                        musicDataList.Add(contentModel);
                        sortedMusicList.Add(contentModel);
                    }
                    
                }
                else if (index % 4 == 0 && index % 3 == 0)
                {
                    //Create some items for upFolder
                    ContentModel contentModel = new ContentModel(index, index.ToString(), "NO." + index.ToString(), ContentItemType.eItemUpFolder, index.ToString(), "", "", 1);
                    dataList.Add(contentModel);
                    sortedDataList.Add(contentModel);
                }
                else if (index % 7 == 0)
                {
                    //Create some items for upFolder
                    ContentModel contentModel = new ContentModel(index, index.ToString(), "NO." + index.ToString(), ContentItemType.eItemUpFolder, index.ToString(), "", "", 2);
                    dataList.Add(contentModel);
                    sortedDataList.Add(contentModel);
                }
                else
                {
                    //Create some items for photo
                    string path = CommonResource.GetLocalReosurceURL() + "Photos/gallery-medium-" + index.ToString() + ".jpg";
                    ContentModel contentModel = new ContentModel(index, index.ToString(), "NO." + index.ToString(), ContentItemType.eItemPhoto, index.ToString(), "", "", 10);
                    contentModel.Format = "JPEG";
                    contentModel.Data = day + "/" + month + "/" + year;
                    contentModel.ThumbnailPath = path;
                    contentModel.Size = (size * (index + 1)).ToString();
                    contentModel.ThumbDone = true;
                    dataList.Add(contentModel);
                    sortedDataList.Add(contentModel);
                    photoDataList.Add(contentModel);
                    sortedPhotoList.Add(contentModel);
                }
            }
            //sort the data
            sortedDataList.Sort();
            sortedPhotoList.Sort();
            sortedVideoList.Sort();
            sortedMusicList.Sort();
            CommonResource.SortedList.Clear();
            CommonResource.SortedPhotoList.Clear();
            CommonResource.SortedVideoList.Clear();
            CommonResource.SortedMusicList.Clear();

            for (int index = 0; index < sortedDataList.Count; index++)
            {
                CommonResource.SortedList.Add(sortedDataList[index]);
            }

            for (int index = 0; index < sortedPhotoList.Count; index++)
            {
                CommonResource.SortedPhotoList.Add(sortedPhotoList[index]);
            }

            for (int index = 0; index < sortedVideoList.Count; index++)
            {
                CommonResource.SortedVideoList.Add(sortedVideoList[index]);
            }

            for (int index = 0; index < sortedMusicList.Count; index++)
            {
                CommonResource.SortedMusicList.Add(sortedMusicList[index]);
            }

            CommonResource.DataList = dataList;
            CommonResource.MusicDataList = musicDataList;
            CommonResource.PhotoDataList = photoDataList;
            CommonResource.VideoDataList = videoDataList;
        }

        /// <summary>
        /// Get the detail data.
        /// </summary>
        /// <returns>return a list, which contain all the data</returns>
        public List<object> GetData()
        {
            return dataList;
        }
    }
        
}
