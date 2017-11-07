/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;

namespace ApplicationControl.Extensions
{
    /// <summary>
    /// A class to enable to make an editor diseditable
    /// </summary>
    public class DisEditableEditor : Editor
    {
        public static readonly BindableProperty IsEditableProperty = BindableProperty.Create("IsEditable", typeof(bool), typeof(DisEditableEditor), true,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var element = (DisEditableEditor)bindable;
                var isEditable = (bool)newValue;
                UpdateIsEditable(element, isEditable);
            });

        public DisEditableEditor() : base()
        {
            UpdateIsEditable(this, false);
        }

        /// <summary>
        /// To set if an editor is editable or not
        /// </summary>
        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        /// <summary>
        /// To update if IsEditable is changed
        /// </summary>
        /// <param name="element">An element to update if the element is editable</param>
        /// <param name="isEditable">A value to update</param>
        static void UpdateIsEditable(DisEditableEditor element, bool isEditable)
        {
            element.Effects.Add(Effect.Resolve("ApplicationControl.EditorDisEditableEffect"));
        }
    }
}