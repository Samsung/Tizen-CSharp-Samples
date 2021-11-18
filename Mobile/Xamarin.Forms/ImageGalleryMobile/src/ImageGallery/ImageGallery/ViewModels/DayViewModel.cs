/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
 */
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ImageGallery.ViewModels
{
    /// <summary>
    /// DayViewModel class.
    /// </summary>
    public class DayViewModel : ViewModelBase, IEnumerable, INotifyCollectionChanged
    {
        #region fields

        /// <summary>
        /// Backing field of the Images property.
        /// </summary>
        private ObservableCollection<ImageViewModel> _images;

        #endregion

        #region properties

        /// <summary>
        /// Date property.
        /// </summary>
        public DateTimeOffset Date { set; get; }

        /// <summary>
        /// Images list property.
        /// </summary>
        public ObservableCollection<ImageViewModel> Images
        {
            set
            {
                if (!SetProperty(ref _images, value))
                {
                    return;
                }

                _images.CollectionChanged += (s, e) =>
                {
                    CollectionChanged?.Invoke(this, e);
                };
            }

            get { return _images; }
        }

        /// <summary>
        /// Notifies about changes of the images collection.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region methods

        /// <summary>
        /// DayViewModel class constructor.
        /// </summary>
        public DayViewModel()
        {
            Images = new ObservableCollection<ImageViewModel>();
        }

        /// <summary>
        /// Adds an instance of ImageViewModel class to the Images list.
        /// </summary>
        /// <param name="imageViewModel">An instance of the ImageViewModel class.</param>
        public void AddImage(ImageViewModel imageViewModel)
        {
            Images.Add(imageViewModel);
        }

        /// <summary>
        /// Returns an enumerator over day images.
        /// </summary>
        /// <returns>Collection enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            return Images.GetEnumerator();
        }

        #endregion
    }
}
