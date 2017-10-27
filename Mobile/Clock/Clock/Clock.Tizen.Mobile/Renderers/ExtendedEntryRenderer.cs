/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Controls;
using Clock.Tizen.Mobile.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]

namespace Clock.Tizen.Mobile.Renderers
{
    /// <summary>
    /// The Renderer class of a ExtendedEntry widget
    /// It extends EntryRenderer class.
    /// </summary>
    class ExtendedEntryRenderer : EntryRenderer
    {
        bool first = false;

        /// <summary>
        /// The constructor of ExtendedEntryRenderer class
        /// </summary>
        public ExtendedEntryRenderer()
        {
        }

        string SetFilter(ElmSharp.Entry entry, string text)
        {
            if (!first && entry.Text.Length == 2)
            {
                return "";
            }
            else
            {
                first = false;
                return text;
            }
        }

        /// <summary>
        /// Override the OnElementChanged method for choosing the proper keyboard.
        /// </summary>
        /// <param name="e">ElementChangedEventArgs<Entry></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var nativeEditText = Control;
                if (nativeEditText != null)
                {
                    nativeEditText.Keyboard = Xamarin.Forms.Platform.Tizen.Native.Keyboard.DateTime;
                    nativeEditText.AppendMarkUpFilter(SetFilter);
                }
            }
        }

        /// <summary>
        /// Override the OnElementPropertyChanged to select all texts in the entry when it's focused.
        /// </summary>
        /// <param name="sender">Entry</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Entry exEntry = sender as Entry;

            if (e.PropertyName == VisualElement.IsFocusedProperty.PropertyName && exEntry.IsFocused)
            {
                Control.SelectAll();
                first = true;
            }

            base.OnElementPropertyChanged(sender, e);
        }
    }
}
