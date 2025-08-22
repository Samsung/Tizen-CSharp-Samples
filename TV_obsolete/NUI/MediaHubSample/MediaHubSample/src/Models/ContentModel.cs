using System;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// The data struct of the item.
    /// </summary>
    public class ContentModel : IComparable<ContentModel>
    {
        // base attribution
        private int index;
        private string displayName;
        private string contentTitle;  
        private int mediaItemType;
        private string contentId;
        private string filePath;
        // byte , this will be very larger, more than int32
        private string data;
        private string thumbPath = "";
        private string getthumbPath;
        private bool thumbDone;
        private bool isSelected;
        private int folderType = -1;
        private string folderThumbPath = "";

        // for media file
        private string format;        
        private bool available;
        // byte , this will be very larger, more than int32
        private string size;
        
        //only for video & photo
        private int height;
        private int width;
        //private bool drm;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="index">the index</param>
        /// <param name="displayName">the display name</param>
        /// <param name="contentTitle">the title of the content</param>
        /// <param name="mediaItemType">the type of the media item</param>
        /// <param name="contentId">the id of the content</param>
        /// <param name="filePath">the file path</param>
        /// <param name="data">the data</param>
        /// <param name="folderType">the type of the folder</param>
        public ContentModel(int index, string displayName, string contentTitle, int mediaItemType, string contentId = "", string filePath = "", string data = "", int folderType = -1)
        {
            this.index = index;
            this.displayName = displayName;
            this.contentTitle = contentTitle;          
            this.mediaItemType = mediaItemType;
            this.contentId = contentId;
            this.filePath = filePath;
            this.data = data;
            this.thumbDone = false;
            this.folderType = folderType;

        }

        /// <summary>
        /// Compare with another ContentModel instance.
        /// </summary>
        /// <param name="other">another ContentModel instance</param>
        /// <returns>If this ContentModel's size is greater than other's, than return 1, else return 0</returns>
        public int CompareTo(ContentModel other)
        {
            if (null == other)
            {
                return 1;
            }

            int size1 = Convert.ToInt32(other.Size);
            int size2 = Convert.ToInt32(this.Size);
            return size1.CompareTo(size2);
        }

        /// <summary>
        /// The Display name of the item.
        /// </summary>
        public string DisplayName
        {
            get { return this.displayName; }
            set { this.displayName = value; }
        }

        /// <summary>
        /// The contentTitle of the item.
        /// </summary>
        public string ContentTitle
        {
            get { return this.contentTitle; }
            set { this.contentTitle = value; }
        }

        /// <summary>
        /// The thumbnail image path of the item.
        /// </summary>
        public string ThumbnailPath
        {
            get { return this.thumbPath; }
            set { this.thumbPath = value; }
        }

        /// <summary>
        /// The type of the item.
        /// </summary>
        public int MediaItemType
        {
            get { return this.mediaItemType; }
            set { this.mediaItemType = value; }
        }

        /// <summary>
        /// The folder type of the item.
        /// </summary>
        public int FolderType
        {
            get { return folderType; }
            set { folderType = value; }
        }

        /// <summary>
        /// The folder imgae path of the item.
        /// </summary>
        public string FolderThumbPath
        {
            get { return folderThumbPath; }
            set { folderThumbPath = value; }
        }

        /// <summary>
        /// Get/Set the height
        /// </summary>
        public int Height
        {
            get  {   return height;    }
            set  {   height = value;   }
        }

        /// <summary>
        /// Get/Set the width
        /// </summary>
        public int Width
        {
            get
            { 
                return width;
            }

            set
            {
                width = value;
            }
        }

        /// <summary>
        /// Get/Set the available
        /// </summary>
        public bool Available
        {
            get
            {
                return available;
            }

            set
            {
                available = value;
            }
        }

        /// <summary>
        /// Get/Set the format
        /// </summary>
        public string Format
        {
            get
            {
                return format;
            }

            set
            {
                format = value;
            }
        }

        /// <summary>
        /// Get/Set the size
        /// </summary>
        public string Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        /// <summary>
        /// Get/Set the file path.
        /// </summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
            }
        }

        /// <summary>
        /// Get/Set the content id.
        /// </summary>
        public string ContentId
        {
            get
            {
                return contentId;
            }

            set
            {
                contentId = value;
            }
        }

        /// <summary>
        /// Get/Set the Data
        /// </summary>
        public string Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        /// <summary>
        /// Get/Set the thumb done
        /// </summary>
        public bool ThumbDone
        {
            get
            {
                return thumbDone;
            }

            set
            {
                thumbDone = value;
            }
        }

        /// <summary>
        /// Get/Set the index
        /// </summary>
        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        /// <summary>
        /// Get/Set the thumb path
        /// </summary>
        public string GetThumbPath
        {
            get
            {
                return getthumbPath;
            }

            set
            {
                getthumbPath = value;
            }
        }

        /// <summary>
        /// IsSelected.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
            }
        }
    }
}
