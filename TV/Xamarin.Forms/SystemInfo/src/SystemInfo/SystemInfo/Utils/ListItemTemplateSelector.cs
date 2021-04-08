/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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
using SystemInfo.ViewModel.List;
using Xamarin.Forms;

namespace SystemInfo.Utils
{
    /// <summary>
    /// Helper class that allows to create list with different items template.
    /// </summary>
    public class ListItemTemplateSelector : DataTemplateSelector
    {
        #region properties

        /// <summary>
        /// Regular template of item.
        /// </summary>
        public DataTemplate StandardTemplate { get; set; }

        /// <summary>
        /// Item template with progress bar.
        /// </summary>
        public DataTemplate ProgressBarTemplate { get; set; }

        #endregion

        #region methods

        /// <summary>Method that allows to select DataTemplate by item type.</summary>
        /// <param name="item">The item for which to return a template.</param>
        /// <param name="container">An optional container object in which DataTemplateSelector objects could be stored.</param>
        /// <returns>Returns defined DataTemplate that is used to display item.</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.GetType() != typeof(ItemViewModel))
            {
                throw new NotSupportedException("Item type not supported");
            }

            var i = (ItemViewModel)item;

            switch (i.ListItemType)
            {
                case ListItemType.Standard:
                    return StandardTemplate;
                case ListItemType.WithProgress:
                    return ProgressBarTemplate;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}