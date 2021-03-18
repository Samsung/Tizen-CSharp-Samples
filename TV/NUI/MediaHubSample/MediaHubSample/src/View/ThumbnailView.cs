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
using Tizen.NUI.BaseComponents;
using Tizen.NUI;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// To show the image, music video or folder in the media hub.
    /// </summary>
    public class ThumbnailView : ImageItem
    {
        protected bool editMode = false;
        protected string[] inforIconURLArray = new string[1];


        protected ImageView TopLayerImage;
        protected ImageView FolderThumbnailImage;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ThumbnailView() : base()
        {
            StateEditabled = true;
            TopLayerImage = new ImageView();
            TopLayerImage.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            TopLayerImage.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            TopLayerImage.Size2D = new Size2D(218 - 4, 236);
            TopLayerImage.Position = new Position(2, 2, 0);

            TopLayerImage.WidthResizePolicy = ResizePolicyType.Fixed;
            TopLayerImage.HeightResizePolicy = ResizePolicyType.Fixed;

            FolderThumbnailImage = new ImageView();

        }

        /// <summary>
        /// Update item in Grid
        /// </summary>
        /// <param name="bridge">The instance of the bridge</param>
        /// <param name="groupIndex">The index of the group</param>
        /// <param name="itemIndex">The index of the item</param>
        /// <param name="editMode">The flag show the item is editable or not</param>
        public virtual void UpdateItem(GridBridge bridge, int groupIndex, int itemIndex, bool editMode = false)
        {

        }

        /// <summary>
        /// Unload this thumbnail item.
        /// </summary>
        public void UnloadItem()
        {
            MainText = null;
            MainImageURL = null;

            try
            {
                InformationIconURLArray = null;
            }
            catch (Exception err)
            {
                Tizen.Log.Fatal("NUI" ," thumbnail  UnloadItem InformationIconURLArray = null error" + err.Message);
            }
           
        }

        /// <summary>
        /// Show the check box on the thumbnail item.
        /// </summary>
        /// <param name="editMode">the flag of the editable</param>
        /// <param name="select">the state of the item</param>
        protected void ShowCheckBox(bool editMode, bool select)
        {
            Tizen.Log.Fatal("NUI" ,"Thumbnail View showCheckBox editMode:" + editMode);
            if (!editMode)
            {
                return;
            }

            if (StateEnabled)
            {
                Tizen.Log.Fatal("NUI" ,"Thumbnail View showCheckBox StateEnabled:" + StateEnabled);
                StateEditabled = true;
                //Check whether the item is selected or not.
                if (select)
                {
                    StateSelected = true;
                }
            }
            else
            {
                StateEditabled = false;
                StateSelected = false;
            }
        }
    }   
    
}