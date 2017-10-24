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

namespace FirstScreen
{
    /// <summary>
    /// Class of constants define some const value.
    /// </summary>
    public class Constants
    {
        // Number of Poster ScrollContainers to be added on Top Container
        public const int TotalPostersContainers = 2;
        // Height Factor of stage height used for Top Container total height

        public const float TopContainerHeightFactor = 0.38f;
        // Position Factor of stage height used for Top Container position
        public const float TopContainerPositionFactor = 0.50f;
        // Height Factor of stage height used for Top Clip layer height
        public const float TopClipLayerHeightFactor = 0.34f;
        // Expanded Height Factor of stage height used for
        // Top Clip layer height (used when Bottom container is hidden)
        public const float TopClipLayerExpandHeightFactor = 0.36f;
        // Height Factor of stage height used for Poster ScrollContainers
        public const float PostersContainerHeightFactor = 0.32f;
        // Padding size used between items / images in Poster ScrollContainer
        public const float PostersContainerPadding = 2.0f;
        // Extra margin Padding size used between items / images
        // in Poster ScrollContainer when item / image is focused
        public const float PostersContainerMargin = 60.0f;
        // Position Factor of Poster item height used for Poster items / images position
        public const float PostersContainerOffsetYFactor = 0.17f;
        // Height Factor of stage height used for Bottom Container total height
        public const float BottomContainerHeightFactor = 0.16f;
        // Position Factor of stage height used for Bottom Container position when bottom container is hidden (not focused)
        public const float BottomContainerHidePositionFactor = 0.88f;
        // Position Factor of stage height used for Bottom Container position when bottom container is not hidden (focused)
        public const float BottomContainerShowPositionFactor = 0.84f;
        // Height Factor of stage height used for Bottom ScrollContainers
        public const float MenuContainerHeightFactor = 0.16f;
        // Height Factor of stage height used for Bottom Clip layer height
        public const float BottomClipLayerHeightFactor = 0.84f;
        // Padding size used between items / images in Menu ScrollContainer
        public const float MenuContainerPadding = 10.0f;
        // Extra margin Padding size used between items / images
        // in Menu ScrollContainer when item / image is focused
        public const float MenuContainerMargin = 25.0f;
        // Position Factor of Menu item height used for Menu items / images position
        public const float MenuContainerOffsetYFactor = 0.35f;
        // Width Factor (1/8) of stage Width used for Menu items / images Width
        public const float MenuItemWidthFactor = 0.125f;
        // Height Factor of stage height used for Menu items / images Height
        public const float MenuItemHeightFactor = 0.10f;
        // Number of Menu items / images used in a Menu ScrollContainer
        public const float MenuItemsCount = 20;
        // Width Factor (1/4) of stage Width used for Poster
        // items / images Width in a Poster ScrollContainer 0

        public const float Poster0ItemWidthFactor = 0.25f;
        // Width Factor of stage Width used for
        // Poster items / images Width in a Poster ScrollContainer 1
        public const float Poster1ItemWidthFactor = 0.20f;
        // Height Factor of stage height used for Poster items / images Height
        public const float PostersItemHeightFactor = 0.24f;
        // Number of Menu items / images used in a Poster ScrollContainer
        public const float PostersItemsCount = 24;
        // Width of Launcher. Launcher is used to
        // display three images on left of Menu ScrollContainer
        public const float LauncherWidth = 280.0f;
        //public const float LauncherWidth = 0.0f;
        // Extra Spaces to the left of first Launcher item / image
        public const float LauncherLeftMargin = 40.0f;
        // Extra area / space to the left of Menu ScrollContainer used for a speration shadow image
        public const float LauncherSeparatorWidth = 20.0f;
        // Total number of Launcher items / images
        public const float LauncherItemsCount = 3.0f;
        // Width of each Launcher item / image
        public const float LauncherIconWidth = (LauncherWidth - LauncherLeftMargin - LauncherSeparatorWidth) / LauncherItemsCount;
        // Duration of Spot Light Animation.
        public const int SpotLightDuration = 5000;
        // MilliSecond Duration of Focus Transition Animation.
        public const int FocusTransitionDuration = 350;
        // Duration of Focus Animation.
        public const int FocusDuration = 350;
        // Duration of Scroll Animation.
        public const int ScrollDuration = 350;
        // for target
        public const string ImageResourcePath = "/home/owner/apps_rw/org.tizen.example.FirstScreen/res/images/";

    }
}
