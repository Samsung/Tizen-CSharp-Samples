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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ElmSharp;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using ApplicationControl.Extensions;
using ApplicationControl.Tizen.Mobile.Renderers;
using ESize = ElmSharp.Size;
using Size = Xamarin.Forms.Size;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;

[assembly: ExportRenderer(typeof(RadioButton), typeof(RadioButtonRenderer))]

namespace ApplicationControl.Tizen.Mobile.Renderers
{
    public class RadioButtonRenderer : ViewRenderer<RadioButton, Radio>
    {
        readonly Span _span = new Span();

        static Lazy<RadioGroupManager> s_GroupManager = new Lazy<RadioGroupManager>();

        int _changedCallbackDepth = 0;

        public RadioButtonRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RadioButton> e)
        {
            if (Control == null)
            {
                var radio = new Radio(TForms.NativeParent) { StateValue = 1 };
                SetNativeControl(radio);
            }

            if (e.OldElement != null)
            {
                Control.ValueChanged -= ChangedEventHandler;
            }

            if (e.NewElement != null)
            {
                Control.ValueChanged += ChangedEventHandler;
                UpdateAll();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == RadioButton.TextProperty.PropertyName)
            {
                UpdateText();
            }
            else if (e.PropertyName == RadioButton.TextColorProperty.PropertyName)
            {
                UpdateTextColor();
            }
            else if (e.PropertyName == RadioButton.FontSizeProperty.PropertyName)
            {
                UpdateFontSize();
            }
            else if (e.PropertyName == RadioButton.GroupNameProperty.PropertyName)
            {
                UpdateGroupName();
            }
            else if (e.PropertyName == RadioButton.IsSelectedProperty.PropertyName)
            {
                UpdateIsSelected();
            }

            if (e.PropertyName == RadioButton.TextProperty.PropertyName || e.PropertyName == RadioButton.TextColorProperty.PropertyName ||
                e.PropertyName == RadioButton.FontSizeProperty.PropertyName)
            {
                ApplyTextAndStyle();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        void ChangedEventHandler(object sender, EventArgs e)
        {
            _changedCallbackDepth++;
            Element.IsSelected = Control.GroupValue == 1 ? true : false;
            _changedCallbackDepth--;
        }

        void UpdateAll()
        {
            UpdateText();
            UpdateTextColor();
            UpdateFontSize();
            UpdateGroupName();
            UpdateIsSelected();
            ApplyTextAndStyle();
        }

        void UpdateText()
        {
            _span.Text = Element.Text;
        }

        void UpdateTextColor()
        {
            _span.ForegroundColor = Element.TextColor.ToNative();
        }

        void UpdateFontSize()
        {
            _span.FontSize = Element.FontSize;
        }

        void UpdateGroupName()
        {
            s_GroupManager.Value.PartGroup(Element);
            s_GroupManager.Value.JoinGroup(Element.GroupName, Element);
        }

        void UpdateIsSelected()
        {
            if (_changedCallbackDepth == 0)
            {
                Control.GroupValue = Element.IsSelected ? 1 : 0;
            }

            s_GroupManager.Value.UpdateChecked(Element.GroupName, Element);
        }

        void ApplyTextAndStyle()
        {
            SetInternalTextAndStyle(_span.GetDecoratedText(), _span.GetStyle());
        }

        void SetInternalTextAndStyle(string formattedText, string textStyle)
        {
            string emission = "elm,state,text,visible";
            if (string.IsNullOrEmpty(formattedText))
            {
                formattedText = null;
                textStyle = null;
                emission = "elm,state,text,hidden";
            }

            Control.Text = formattedText;

            var textblock = Control.EdjeObject["elm.text"];
            if (textblock != null)
            {
                textblock.TextStyle = textStyle;
            }

            Control.EdjeObject.EmitSignal(emission, "elm");
        }

        protected override Size MinimumSize()
        {
            return Measure(Control.MinimumWidth, Control.MinimumHeight).ToDP();
        }

        protected override ESize Measure(int availableWidth, int availableHeight)
        {
            var size = Control.Geometry;

            Control.Resize(availableWidth, size.Height);

            var formattedSize = Control.EdjeObject["elm.text"].TextBlockFormattedSize;

            Control.Resize(size.Width, size.Height);

            return new ESize()
            {
                Width = Control.MinimumWidth + formattedSize.Width,
                Height = Math.Max(Control.MinimumHeight, formattedSize.Height),
            };
        }
    }

    internal class RadioGroupManager
    {
        Dictionary<string, List<RadioButton>> _groupMap = new Dictionary<string, List<RadioButton>>();

        public void JoinGroup(string groupName, RadioButton button)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            if (!_groupMap.ContainsKey(groupName))
            {
                _groupMap.Add(groupName, new List<RadioButton>());
            }

            _groupMap[groupName].Add(button);
            UpdateChecked(groupName, button);
        }

        public void PartGroup(RadioButton button)
        {
            string groupName = null;
            foreach (var list in _groupMap)
            {
                if (list.Value.Contains(button))
                {
                    groupName = list.Key;
                }
            }

            PartGroup(groupName, button);
        }

        public void PartGroup(string groupName, RadioButton button)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            if (_groupMap.ContainsKey(groupName))
            {
                _groupMap[groupName].Remove(button);
            }
        }

        public void UpdateChecked(string groupName, RadioButton button)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            if (button.IsSelected)
            {
                foreach (var btn in _groupMap[groupName].Where(b => b.IsSelected && b != button))
                {
                    btn.IsSelected = false;
                }
            }
        }
    }
}