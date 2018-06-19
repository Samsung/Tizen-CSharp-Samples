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

using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Tizen.NUI.Constants;

namespace FirstScreen
{
    public class PosterMenu
    {
        private bool _use3D = false;
        private ScrollMenu _menu;
        private Effect[] _showHideMenuEffect;

        /// <summary>
        /// Set or get show Menu effect
        /// </summary>
        public Effect ShowMenuEffect
        {
            get
            {
                return _showHideMenuEffect[0];
            }

            set
            {
                _showHideMenuEffect[0] = value;
            }
        }

        /// <summary>
        /// Set or get HideMenuEffect
        /// </summary>
        public Effect HideMenuEffect
        {
            get
            {
                return _showHideMenuEffect[1];
            }

            set
            {
                _showHideMenuEffect[1] = value;
            }
        }

        /// <summary>
        /// Create Items for Poster ScrollContainer
        /// </summary>
        /// <param name="height">height</param>
        /// <param name="imagePath">image path</param>
        /// <param name="menuDefinition">menu definition</param>
        public PosterMenu(int height, String imagePath, TVConfiguration.MenuDefinition menuDefinition)
        {
            _showHideMenuEffect = new Effect[2];
            
            _menu = new ScrollMenu();
            _menu.PositionUsesPivotPoint = true;
            _menu.ParentOrigin = ParentOrigin.TopCenter;
            _menu.PivotPoint = PivotPoint.TopCenter;
            _menu.WidthResizePolicy = ResizePolicyType.FillToParent;
            _menu.HeightResizePolicy = ResizePolicyType.Fixed;
            _menu.Size2D = new Size2D(0, height);
            _menu.StartActive = false;
            _menu.Gap = Constants.PostersContainerPadding;
            _menu.Margin = Constants.PostersContainerMargin;
            _menu.ScrollEndMargin = Constants.PostersContainerScrollEndMargin;
            _menu.ItemDimensions = menuDefinition.posterDimensions;
            _menu.FocusScale = Constants.PosterFocusScale;

            for (int i = 0; i < menuDefinition.posterTitles.Count; i++)
            {
                ScrollMenu.ScrollItem scrollItem = new ScrollMenu.ScrollItem();

                TextLabel titleText = new TextLabel(menuDefinition.posterTitles[i]);
                titleText.Name = "poster-title_" + (i + 1);
                titleText.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
                titleText.PositionUsesPivotPoint = true;
                titleText.ParentOrigin = new Position(0.5f, 0.04f, 0.5f);
                titleText.PivotPoint = PivotPoint.TopCenter;
                //titleText.PointSize = Constants.TVBuild ? 27.0f : 19.0f; //TODO: Set with style sheet
                //titleText.PointSize = Constants.TVBuild ? 7.0f : 19.0f;
                titleText.PointSize = DeviceCheck.PointSize7;
                if (menuDefinition.whiteText)
                {
                    titleText.TextColor = Color.White;
                    titleText.ShadowColor = Color.Black; // Note: This also enables shadows
                    titleText.ShadowOffset = new Vector2(2.0f, 2.0f);
                }
                else
                {
                    titleText.TextColor = Color.Black;
                }

                scrollItem.title = titleText;

                scrollItem.shadow = new ImageView(Constants.ResourcePath + "/images/Effect/thumbnail_cast_shadow.png");
                scrollItem.shadow.ParentOrigin = ParentOrigin.BottomCenter;
                scrollItem.shadow.PivotPoint = PivotPoint.TopCenter;
                scrollItem.shadow.PositionUsesPivotPoint = true;
                scrollItem.shadow.WidthResizePolicy = ResizePolicyType.SizeRelativeToParent;
                scrollItem.shadow.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
                scrollItem.shadow.SizeModeFactor = new Vector3(1.1f, 0.4f, 0.0f);
                scrollItem.shadow.Position = new Position(0.0f, 0.0f, 0.0f);
                scrollItem.shadow.Opacity = 0.7f;

                if (_use3D)
                {
                    VisualView item = new VisualView();
                    item.WidthResizePolicy = ResizePolicyType.Fixed;
                    item.HeightResizePolicy = ResizePolicyType.Fixed;
                    item.Size2D = new Size2D(400, 400);
                    item.Position = new Position(0.0f, 0.0f, 0.0f);
                    item.PositionUsesPivotPoint = true;
                    item.Focusable = true;
                    item.ParentOrigin = ParentOrigin.Center;
                    item.PivotPoint = PivotPoint.Center;

                    MeshVisual meshVisualMap1 = new MeshVisual();
                    meshVisualMap1.ObjectURL = Constants.ResourcePath + "/models/roundedcube-half3.obj";
                    if ((i % 3) == 0)
                    {
                        meshVisualMap1.MaterialtURL = Constants.ResourcePath + "/models/roundedcube-half2.mtl";
                    }
                    else if ((i % 3) == 1)
                    {
                        meshVisualMap1.MaterialtURL = Constants.ResourcePath + "/models/roundedcube-half3.mtl";
                    }
                    else
                    {
                        meshVisualMap1.MaterialtURL = Constants.ResourcePath + "/models/roundedcube-half4.mtl";
                    }

                    meshVisualMap1.TexturesPath = Constants.ResourcePath + "/images/";
                    meshVisualMap1.ShadingMode = MeshVisualShadingModeValue.TexturedWithSpecularLighting;
                    meshVisualMap1.Size = new Size2D(445, 445);
                    meshVisualMap1.Position = new Position2D(0 ,0);
                    meshVisualMap1.PositionPolicy = VisualTransformPolicyType.Absolute;
                    meshVisualMap1.SizePolicy = VisualTransformPolicyType.Absolute;
                    meshVisualMap1.Origin = Visual.AlignType.Center;
                    meshVisualMap1.AnchorPoint = Visual.AlignType.Center;
                    item.AddVisual("meshVisual1", meshVisualMap1);
                    item.Name = "poster-item_" + (i + 1);
                    scrollItem.item = item;

                    _menu.AddItem(scrollItem);
                }
                else
                {
/*                  string itemURL = _imagePath + "/poster" + j + "/" + (i % 14) + ".jpg";
                    View item = new ImageView(itemURL);
                    View itemReflection = new ImageView(itemURL);
                    item.Name = ("poster-item-" + _postersContainer[j].ItemCount);
                    _postersContainer[j].AddItem(item, shadowBorder, itemReflection);
*/
                    string itemURL = imagePath + "/mi" + i + ".png";
                    scrollItem.item = new ImageView(itemURL);
                    scrollItem.item.Name = "poster-item_" + (i + 1);
                    _menu.AddItem(scrollItem);
                }
            }
        }

        /// <summary>
        /// Get scrollMenu
        /// </summary>
        /// <returns>scroll menu</returns>
        public ScrollMenu ScrollMenu()
        {
            return _menu;
        }

        /// <summary>
        /// Set focus and get focused view
        /// </summary>
        /// <param name="focus">Whether is focused</param>
        /// <returns>
        /// If focus,return current focused item
        /// else return a new View
        /// </returns>
        public View Focus(bool focus)
        {
            _menu.SetActive(focus);
            if (focus)
            {
                return _menu.GetCurrentFocusedItem();
            }
            else
            {
                return new View();
            }
        }

        /// <summary>
        /// Show hide effect
        /// </summary>
        /// <param name="visible">Is visible or not</param>
        public void Show(bool visible)
        {
            _showHideMenuEffect[visible ? 0 : 1].Play();
        }

        /// <summary>
        /// Finish hide effect
        /// </summary>
        public void ForceHide()
        {
            _showHideMenuEffect[1].Finish();
        }

    }
}
