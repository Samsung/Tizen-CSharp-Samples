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
    /// Some already set constants
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Set to true if building for TV. TODO: Temporary workaround until style sheet is deployed on device.
        /// </summary>
        public const bool TVBuild = true;
        /// <summary>
        /// Set to true to create a transparent window without a background image
        /// </summary>
        public const bool TransparentWindow = false;
        /// <summary>
        /// Number of Poster ScrollContainers to be added on Top Container
        /// </summary>
        public const int TotalPosterMenus = 12;
        /// <summary>
        /// Height Factor of stage height used for Top Container total height
        /// </summary>
        public const float TopContainerHeightFactor = 0.38f;
        /// <summary>
        /// Position Factor of stage height used for Top Container position
        /// </summary>
        public const float TopContainerPositionFactor = 0.50f;
        /// <summary>
        /// Height Factor of stage height used for Top Clip layer height
        /// </summary>
        public const float TopClipLayerHeightFactor = 0.34f;
        /// <summary>
        /// Expanded Height Factor of stage height used for Top Clip layer height (used when Bottom container is hidden)
        /// </summary>
        public const float TopClipLayerExpandHeightFactor = 0.36f;
        /// <summary>
        /// Height Factor of stage height used for Poster ScrollContainers
        /// </summary>
        public const float PostersContainerHeightFactor = 0.32f;
        /// <summary>
        /// Padding size used between items / images in Poster ScrollContainer
        /// </summary>
        public const float PostersContainerPadding = 30.0f;
        /// <summary>
        /// Extra margin Padding size used between items / images in Poster ScrollContainer when item / image is focused
        /// </summary>
        public const float PostersContainerMargin = 0.0f;
        /// <summary>
        /// Extra margin to limit scrolling at each end of container
        /// </summary>
        public const float PostersContainerScrollEndMargin = 30.0f;
        /// <summary>
        /// Position Factor of Poster item height used for Poster items / images position
        /// </summary>
        public const float PostersContainerOffsetYFactor = 0.17f;
        /// <summary>
        /// Width Factor (1/8) of stage Width used for Menu items / images Width
        /// </summary>
        public const float MenuItemWidthFactor = 0.125f;
        /// <summary>
        /// Height Factor of stage height used for Menu items / images Height
        /// </summary>
        public const float MenuItemHeightFactor = 0.10f;
        /// <summary>
        /// Number of Menu items / images used in a Menu ScrollContainer
        /// </summary>
        public const float MenuItemsCount = 8;
        /// <summary>
        /// Height Factor of stage height used for Bottom Container total height
        /// </summary>
        public const float BottomContainerHeightFactor = MenuItemHeightFactor * 1.6f;
        /// <summary>
        /// Position Factor of stage height used for Bottom Container position when bottom container is hidden (not focused)
        /// </summary>
        public const float BottomContainerHidePositionFactor = 0.88f;
        /// <summary>
        /// Position Factor of stage height used for Bottom Container position when bottom container is not hidden (focused)
        /// </summary>
        public const float MenuContainerHideFactor = 0.29f;
        /// <summary>
        /// Height Factor of stage height used for Bottom ScrollContainers
        /// </summary>
        public const float MenuContainerHeightFactor = 0.16f;
        /// <summary>
        /// Padding size used between items / images in Menu ScrollContainer
        /// </summary>
        public const float SourceMenuPadding = 10.0f;
        /// <summary>
        /// Extra margin Padding size used between items / images in Menu ScrollContainer when item / image is focused
        /// </summary>
        public const float SourceMenuMargin = 38.0f;
        /// <summary>
        /// Extra margin to limit scrolling at each end of container
        /// </summary>
        public const float SourceMenuScrollEndMargin = 10.0f;
        /// <summary>
        /// Position Factor of Menu item height used for Menu items / images position
        /// </summary>
        public const float MenuContainerOffsetYFactor = 0.35f;
        /// <summary>
        /// Width Factor (1/4) of stage Width used for Poster items / images Width in a Poster ScrollContainer 0
        /// </summary>
        public const float Poster0ItemWidthFactor = 0.25f;
        /// <summary>
        /// Width Factor of stage Width used for Poster items / images Width in a Poster ScrollContainer 1
        /// </summary>
        public const float Poster1ItemWidthFactor = 0.20f;
        /// <summary>
        /// Height Factor of stage height used for Poster items / images Height
        /// </summary>
        public const float PostersItemHeightFactor = 0.24f;
        /// <summary>
        /// Number of Menu items / images used in a Poster ScrollContainer
        /// </summary>
        public const float PostersItemsCount = 24;
        /// <summary>
        /// Width of Launcher. Launcher is used to display three images on left of Menu ScrollContainer
        /// </summary>
        public const float SystemMenuWidth = 200.0f;
        /// <summary>
        /// Extra Spaces to the left of first Launcher item / image
        /// </summary>
        public const float LauncherLeftMargin = 40.0f;
        /// <summary>
        /// Extra area / space to the left of Menu ScrollContainer used for a separation shadow image
        /// </summary>
        public const float LauncherSeparatorWidth = 20.0f;
        /// <summary>
        /// Total number of Launcher items / images
        /// </summary>
        public const float SystemItemsCount = 3.0f;
        /// <summary>
        /// Width of each Launcher item / image
        /// </summary>
        public const float SystemIconWidth = (SystemMenuWidth - LauncherLeftMargin - LauncherSeparatorWidth) / SystemItemsCount;
        /// <summary>
        /// Duration of Spot Light Animation.
        /// </summary>
        public const int SpotLightDuration = 2500;
        public const int ShineDuration = 1000;
        public const int FocusDuration = 350;        
        public const int ScrollEndAnimationDuration = 400;
        public const int ActionReminderAnimationDuration = 2200;
        public const int ActionReminderTouchAnimationDuration = 800;
        public const uint ActionReminderTimePeriod = 5000;
        public const int ActionReminderRemoteShowTicks = 3;
        public const int ShowPosterDuration = 350;
        public const int ShowMenuDuration = 350;
        /// <summary>
        /// Duration of Scroll Animation.
        /// </summary>
        public const int ScrollDuration = 350;
        public const int SystemMenuAnimationDuration = 400;
        public const int SystemMenuSelectorAnimationDuration = 400;
        public const int PulseAnimationDuration = 400;
        public const float SourceFocusScale = 1.35f;
        public const float PosterFocusScale = 1.2f;
        //public const string ResourcePath = "./res/";                  // for Ubuntu
        public const string ResourcePath = "/home/owner/apps_rw/org.tizen.example.FirstScreen/res/"; // for target
    }
}
