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
using Tizen.NUI.BaseComponents;

/// <summary>
/// namespace for Tizen.NUI.MediaHub package
/// </summary>
namespace Tizen.NUI.MediaHub
{
    public class AllContentThumbnailView : ThumbnailView
    {       
        /// <summary>
        /// Construct AllContentThumbnailView
        /// </summary>
        /// <param name="o">ContentModel object of data</param>
        /// <param name="editMode">the edit mode for gridlist item</param>
        public AllContentThumbnailView(object o, bool editMode = false) : base()
        {   
          //  FolderThumbnailImage.SetImage(CommonReosurce.GetLocalReosurceURL() + "Folder/mc_folder_folder.png");

            this.editMode = editMode;
            
            ContentModel item = o as ContentModel;
            if (item == null)
            {
                Tizen.Log.Fatal("NUI" ,"[ListView]ItemView, item is null");
                return;
            }

            LoadItem(item);
            ShowCheckBox(editMode, item.IsSelected);
        }

        /// <summary>
        /// Create folder thumbnail
        /// </summary>
        /// <param name="item">object for class ContentModel</param>
        private void CreateFolder(ContentModel item)
        {
            StateEditabled = false;
            string defaultIcon = CommonResource.GetLocalReosurceURL() + "Folder/mc_folder.png";
            MainImageURL = "";
            MainText = item.DisplayName;
            StateEnabled = !editMode;

            if (item.FolderType == 0)
            { 
                // exit file thumbnail
                defaultIcon = CommonResource.GetLocalReosurceURL() + "Folder/mc_folder_preview.png";
                TopLayerImage.SetImage(defaultIcon);
                FolderThumbnailImage.SetImage(item.ThumbnailPath);
                Add(FolderThumbnailImage);
            }
            else if (item.FolderType == 1)
            {
                // folder in folder
                defaultIcon = CommonResource.GetLocalReosurceURL() + "Folder/mc_folder_folder.png";
                TopLayerImage.SetImage(defaultIcon);

            }
            else if (item.FolderType == 2)
            {
                // no contents folder
                defaultIcon = CommonResource.GetLocalReosurceURL() + "Folder/mc_folder_nocont.png";
                TopLayerImage.SetImage(defaultIcon);
            }
            else
            {   //request failed
                TopLayerImage.SetImage(defaultIcon);
            }

            Add(TopLayerImage);
            Tizen.Log.Fatal("NUI" ,"allcontent thumbnail createFolder Addimage -1 TopLayerImage =  end");
            TopLayerImage.RaiseToTop();

            InformationIconURLArray = null;

        }

        /// <summary>
        /// Create thumbnail for photo file 
        /// </summary>
        /// <param name="item">object for class ContentModel</param>
        private void CreatePhoto(ContentModel item)
        {
            StateEditabled = true;
            string thumb = CommonResource.GetLocalReosurceURL() + "DefaultThumb/mc_f_default_photo.png";
            inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_contents_camera.png";

            if (!item.Available)
            {
                inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_unsupported_photo.png";
            }   

            if (item.ThumbDone)
            {
                thumb = item.ThumbnailPath;
            }

            MainImageURL = thumb;

            MainText = item.DisplayName;
            InformationIconURLArray = inforIconURLArray;
        }

        
        /// <summary>
        /// Create UpFolder thumbnail 
        /// </summary>
        /// <param name="item">object for class ContentModel</param>
        private void CreateUpFolder(ContentModel item)
        {
            StateEditabled = false;
            string folderImage = CommonResource.GetLocalReosurceURL() + "Folder/mc_upper_folder_n.png";
            MainImageURL = folderImage;
            MainText = item.DisplayName;
            StateEnabled = !editMode;
        }

        /// <summary>
        /// Create thumbnail for video file
        /// </summary>
        /// <param name="item">object for class ContentModel</param>
        private void CreateVideo(ContentModel item)
        {
            StateEditabled = true;
            string thumb = CommonResource.GetLocalReosurceURL() + "DefaultThumb/mc_f_default_video.png";
            inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_contents_play.png";

            if (item.ThumbDone)
            {
                thumb = item.ThumbnailPath;
            }

            if (!item.Available)
            {
                inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_unsupported_video.png";
            }
            
            MainImageURL = thumb;
            MainText = item.DisplayName;

            InformationIconURLArray = inforIconURLArray;
        }

        /// <summary>
        /// Create thumbnail for music file
        /// </summary>
        /// <param name="item">object for class ContentModel</param>
        private void CreateMusic(ContentModel item)
        {
            StateEditabled = true;
            string thumb = CommonResource.GetLocalReosurceURL() + "DefaultThumb/mc_f_default_music.png";
            inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_contents_music.png";
            if (!item.Available)
            {
                inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_unsupported_music.png";
            }

            if (item.ThumbDone)
            {
                thumb = item.ThumbnailPath;
            }

            MainImageURL = thumb;
            MainText = item.DisplayName;
            InformationIconURLArray = inforIconURLArray;
        }

        /// <summary>
        /// Load item 
        /// </summary>
        /// <param name="data">object for class ContentModel</param>
        public void LoadItem(object data)
        {
            ContentModel item = data as ContentModel;
            int itemType = item.MediaItemType;
            
            if (itemType == ContentItemType.eItemFolder)
            {
                //Create item for folder
                CreateFolder(item);
            }
            else if (itemType == ContentItemType.eItemPhoto)
            {
                //Create item for photo
                CreatePhoto(item);
            }
            else if (itemType == ContentItemType.eItemVideo)
            {
                //Create item for video
                CreateVideo(item);
            }
            else if (itemType == ContentItemType.eItemMusic)
            {
                //Create item for music
                CreateMusic(item);
            }
            else if (itemType == ContentItemType.eItemUpFolder)
            {
                CreateUpFolder(item);
            }         
        }

        /// <summary>
        /// Update view that display all the content thumbnail in the device
        /// </summary>
        /// <param name="bridge">grid view bridge</param>
        /// <param name="groupIndex">index for group</param>
        /// <param name="itemIndex">index for item</param>
        /// <param name="editMode">the edit mode: true / false </param>
        public override void UpdateItem(GridBridge bridge, int groupIndex, int itemIndex, bool editMode = false)
        {
            // return;
            string Folderpath = null;
            if (bridge == null)
            {
                return;
            }

            ContentModel itemData = bridge.GetData(itemIndex, groupIndex) as ContentModel;
            if (itemData == null)
            {
                return;
            }

            int itemType = itemData.MediaItemType;

            try
            {
                switch (itemType)
                {
                    case ContentItemType.eItemFolder:
                        //MainImageURL = (CommonReosurce.GetLocalReosurceURL() + "Folder/mc_folder.png");
                        MainImageURL = "";
                        MainText = itemData.DisplayName;
                        Folderpath = itemData.FolderThumbPath;
                        string defaultIcon = CommonResource.GetLocalReosurceURL() + "Folder/mc_folder.png";
                        if (itemData.FolderType == 0)
                        { // exit file thumbnail
                            defaultIcon = CommonResource.GetLocalReosurceURL() + "Folder/mc_folder_preview.png";
                            
                            FolderThumbnailImage.SetImage(itemData.FolderThumbPath);
                            FolderThumbnailImage.Size2D = new Size2D(109, 79);
                            FolderThumbnailImage.Position = new Position(55, 95, 0);
                            Add(FolderThumbnailImage);

                            TopLayerImage.SetImage(defaultIcon);
                        }
                        else if (itemData.FolderType == 1)
                        {// folder in folder
                            defaultIcon = CommonResource.GetLocalReosurceURL() + "Folder/mc_folder_folder.png";
                            TopLayerImage.SetImage(defaultIcon);
                            Remove(FolderThumbnailImage);

                        }
                        else if (itemData.FolderType == 2)
                        {// no contents folder
                            defaultIcon = CommonResource.GetLocalReosurceURL() + "Folder/mc_folder_nocont.png";
                            TopLayerImage.SetImage(defaultIcon);
                            Remove(FolderThumbnailImage);
                        }
                        else
                        {   //request failed
                            FolderThumbnailImage.SetImage(itemData.FolderThumbPath);
                            FolderThumbnailImage.Size2D = new Size2D(109, 79);
                            FolderThumbnailImage.Position = new Position(55, 95, 0);
                            Add(FolderThumbnailImage);
                            TopLayerImage.SetImage(defaultIcon);
                            Remove(FolderThumbnailImage);
                        }

                        Add(TopLayerImage);
                        TopLayerImage.RaiseToTop();
                        StateEnabled = !editMode;
                       // inforIconURLArray[0] = "";
                        InformationIconURLArray = null;

                        break;
                    case ContentItemType.eItemUpFolder:
                        this.Remove(TopLayerImage);
                        this.Remove(FolderThumbnailImage);

                        MainImageURL = (CommonResource.GetLocalReosurceURL() + "Folder/mc_upper_folder_n.png");
                        MainText = itemData.DisplayName;

                        StateEnabled = !editMode;
                       // inforIconURLArray[0] = "";
                        InformationIconURLArray = null;
                        break;
                    case ContentItemType.eItemPhoto:
                        this.Remove(TopLayerImage);
                        this.Remove(FolderThumbnailImage);

                        inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_contents_camera.png";
                         StateEnabled = true;
                  
                        if (itemData.ThumbDone)
                        {
                            MainImageURL = (itemData.ThumbnailPath);
                        }
                        else
                        {
                            MainImageURL = (CommonResource.GetLocalReosurceURL() + "DefaultThumb/mc_f_default_photo.png");
                        }

                        this.MainText = itemData.DisplayName;
                        InformationIconURLArray = inforIconURLArray;
                        break;
                    case ContentItemType.eItemVideo:
                        this.Remove(TopLayerImage);
                        this.Remove(FolderThumbnailImage);
                        inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_contents_play.png";
                       
                        StateEnabled = true;
                        if (itemData.ThumbDone)
                        {
                            MainImageURL = (itemData.ThumbnailPath);                                                       
                        }
                        else
                        {
                            MainImageURL = (CommonResource.GetLocalReosurceURL() + "DefaultThumb/mc_f_default_video.png");
                        }

                        MainText = itemData.DisplayName;
                        InformationIconURLArray = inforIconURLArray;
                        break;
                    case ContentItemType.eItemMusic:
                        this.Remove(TopLayerImage);
                        this.Remove(FolderThumbnailImage);
                        inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_contents_music.png";
                        if (!itemData.Available)
                        {
                            inforIconURLArray[0] = CommonResource.GetLocalReosurceURL() + "Contents_icon/mc_icon_unsupported_music.png";
                        }

                        StateEnabled = true;
                        if (itemData.ThumbDone)
                        {
                            MainImageURL = (itemData.ThumbnailPath);
                        }
                        else
                        {
                            MainImageURL = (CommonResource.GetLocalReosurceURL() + "DefaultThumb/mc_f_default_music.png");
                        }

                        MainText = itemData.DisplayName;
                        InformationIconURLArray = inforIconURLArray;
                        break;
                }
                
                ShowCheckBox(editMode, itemData.IsSelected);
            }
            catch (Exception e)
            {
                Tizen.Log.Fatal("NUI" ," Thumbnail UpdateItem  error: " + e.Message);
            }

        }
    }
    
}