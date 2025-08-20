/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace VoiceRecorder.Tizen.Mobile.Control
{
    /// <summary>
    /// The class for the items in a ContextPopup.
    /// Each item can have a label and an icon.
    /// </summary>
    /// <example>
    /// <code>
    /// new ContextPopupItem("Text only item");
    /// new ContextPopupItem("Home icon", "home");
    /// new ContextPopupItem("Car", "car.png");
    /// new ContextPopupItem("Chat", StandardIconResource.MenuChat.Name);
    /// </code>
    /// </example>
    public class ContextPopupItem : INotifyPropertyChanged
    {
        #region fields

        /// <summary>
        /// Backing fields of Label property.
        /// </summary>
        string _label;

        /// <summary>
        /// Backing fields of Icon property.
        /// </summary>
        FileImageSource _icon;

        #endregion

        #region properties

        /// <summary>
        /// Occurs when the label or an icon of a ContextPopupItem is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the label of a ContextPopupItem.
        /// </summary>
        public string Label
        {
            get => _label;
            set
            {
                if (value != _label)
                {
                    _label = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the icon of a ContextPopupItem. The icon may be an image or a standard icon.
        /// The available standard icons that can be used are specified in the StandardIconResource class.
        /// The name property of the StandardIconResource class can be used to specify a standard icon.
        /// </summary>
        /// <remarks>
        /// Icon is only supported on the mobile profile.
        /// Icon does not always work as expected on the TV profile.
        /// </remarks>
        public FileImageSource Icon
        {
            get => _icon;
            set
            {
                if (value != _icon)
                {
                    _icon = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates a ContextPopupItem with only a label.
        /// </summary>
        /// <param name="label">The label of the ContextPopupItem.</param>
        public ContextPopupItem(string label)
        {
            _label = label;
        }

        /// <summary>
        /// Creates a ContextPopupItem with a label and an icon. The icon may be an image or a standard icon.
        /// To create a ContextPopupItem with only an icon, set the label to an empty string.
        /// The available standard icons that can be used are specified in the StandardIconResource class.
        /// The name property of the StandardIconResource class can be used to specify a standard icon.
        /// </summary>
        /// <param name="label">The label of the ContextPopupItem.</param>
        /// <param name="icon">The icon of the ContextPopupItem.</param>
        /// <code>
        /// new ContextPopupItem("Text only item");
        /// new ContextPopupItem("Home icon", "home");
        /// new ContextPopupItem("Car", "car.png");
        /// new ContextPopupItem("Chat", StandardIconResource.MenuChat.Name);
        /// </code>
        public ContextPopupItem(string label, FileImageSource icon)
        {
            if (label == null)
            {
                label = "";
            }

            _label = label;
            _icon = icon;
        }

        /// <summary>
        /// Invokes PropertyChanged with automatically obtained property name.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}