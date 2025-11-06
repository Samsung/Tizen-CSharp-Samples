/*
  * Copyright (c) 2016 Samsung Electronics Co., Ltd
  *
  * Licensed under the Flora License, Version 1.1 (the "License");
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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUI_CustomView
{
    public class ContactView : CustomView
    {
        private string name;
        private string resourceDirectory;

        //Indexes for registering Visuals in the CustomView
        private const int BaseIndex = 10000;
        private const int BackgroundVisualIndex = BaseIndex + 1;
        private const int LabelVisualIndex = BaseIndex + 2;
        private const int ContactBgIconIndex = BaseIndex + 3;
        private const int ContactIconIndex = BaseIndex + 4;
        private const int ContactEditIndex = BaseIndex + 5;
        private const int ContactFavoriteIndex = BaseIndex + 6;
        private const int ContactDeleteIndex = BaseIndex + 7;

        static ContactView()
        {
            //Each custom view must have its static constructor to register its type.
            CustomViewRegistry.Instance.Register(CreateInstance, typeof(ContactView));
        }

        static CustomView CreateInstance()
        {
            //Create and return valid custom view object. In this case ContactView is created.
            return new ContactView(null, null);
        }

        /// <summary>
        /// Creates and register background color visual.
        /// </summary>
        /// <param name="color">RGBA color vector</param>
        private void CreateBackground(Vector4 color)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Color))
               .Add(ColorVisualProperty.MixColor, new PropertyValue(color));

            VisualBase background = VisualFactory.Instance.CreateVisual(map);
            RegisterVisual(BackgroundVisualIndex, background);
            background.DepthIndex = BackgroundVisualIndex;
        }

        /// <summary>
        /// Creates and register label visual.
        /// </summary>
        /// <param name="text">String viewed by created label</param>
        private void CreateLabel(string text)
        {
            PropertyMap textVisual = new PropertyMap();
            textVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text))
                      .Add(TextVisualProperty.Text, new PropertyValue(text))
                      .Add(TextVisualProperty.TextColor, new PropertyValue(Color.Black))
                      .Add(TextVisualProperty.PointSize, new PropertyValue(12))
                      .Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"))
                      .Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));

            VisualBase label = VisualFactory.Instance.CreateVisual(textVisual);
            RegisterVisual(LabelVisualIndex, label);
            label.DepthIndex = LabelVisualIndex;

            PropertyMap imageVisualTransform = new PropertyMap();
            imageVisualTransform.Add((int)VisualTransformPropertyType.Offset, new PropertyValue(new Vector2(30, 5)))
                                .Add((int)VisualTransformPropertyType.OffsetPolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                                .Add((int)VisualTransformPropertyType.SizePolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                                .Add((int)VisualTransformPropertyType.Size, new PropertyValue(new Vector2(350, 100)));

            label.SetTransformAndSize(imageVisualTransform, new Vector2(this.SizeWidth, this.SizeHeight));
        }

        /// <summary>
        /// Creates and register icon image.
        /// </summary>
        /// <param name="url">Icon absolute path</param>
        /// <param name="x">x icon position</param>
        /// <param name="y">y icon position</param>
        /// <param name="w">icon width</param>
        /// <param name="h">icon height</param>
        /// <param name="index">visuals registration index</param>
        private void CreateIcon(string url, float x, float y, float w, float h, int index)
        {
            PropertyMap map = new PropertyMap();
            PropertyMap transformMap = new PropertyMap();

            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
               .Add(ImageVisualProperty.URL, new PropertyValue(url));

            VisualBase icon = VisualFactory.Instance.CreateVisual(map);

            PropertyMap imageVisualTransform = new PropertyMap();
            imageVisualTransform.Add((int)VisualTransformPropertyType.Offset, new PropertyValue(new Vector2(x, y)))
                                .Add((int)VisualTransformPropertyType.OffsetPolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                                .Add((int)VisualTransformPropertyType.SizePolicy, new PropertyValue(new Vector2((int)VisualTransformPolicyType.Absolute, (int)VisualTransformPolicyType.Absolute)))
                                .Add((int)VisualTransformPropertyType.Size, new PropertyValue(new Vector2(w, h)))
                                .Add((int)VisualTransformPropertyType.Origin, new PropertyValue((int)Visual.AlignType.CenterBegin))
                                .Add((int)VisualTransformPropertyType.AnchorPoint, new PropertyValue((int)Visual.AlignType.CenterBegin));

            icon.SetTransformAndSize(imageVisualTransform, new Vector2(this.SizeWidth, this.SizeHeight));

            RegisterVisual(index, icon);
            icon.DepthIndex = index;
        }

        /// <summary>
        /// Contact View constructor
        /// </summary>
        /// <param name="name">name viewed by CustomView object</param>
        /// <param name="resourceDirectory">resource directory path used to create absolute paths to icons</param>
        /// <returns></returns>
        public ContactView(string name, string resourceDirectory) : base(typeof(ContactView).Name, CustomViewBehaviour.ViewBehaviourDefault)
        {
            this.name = name;
            this.resourceDirectory = resourceDirectory;

            //Add background to contact view
            CreateBackground(new Vector4(1.0f, 1.0f, 1.0f, 1.0f));

            //Append icons using absolute path and icons positions parameters.
            CreateIcon(resourceDirectory + "/images/cbg.png", 10.0f, 5.0f, 100.0f, 100.0f, ContactBgIconIndex);
            CreateIcon(resourceDirectory + "/images/contact.png", 10.0f, 5.0f, 100.0f, 100.0f, ContactIconIndex);
            CreateIcon(resourceDirectory + "/images/edit.png", 130.0f, 40.0f, 50.0f, 50.0f, ContactEditIndex);
            CreateIcon(resourceDirectory + "/images/favorite.png", 180.0f, 40.0f, 50.0f, 50.0f, ContactFavoriteIndex);
            CreateIcon(resourceDirectory + "/images/delete.png", 640.0f, 40.0f, 50.0f, 50.0f, ContactDeleteIndex);

            //Append title
            CreateLabel(name);
        }
    }
}
