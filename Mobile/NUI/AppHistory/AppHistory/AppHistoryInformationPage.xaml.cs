/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Collections.Generic;
using System.Linq;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.Security;

namespace AppHistory
{
    /// <summary>
    /// Class representing AppHistory Information Page
    /// </summary>
    public partial class AppHistoryInformationPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppHistoryInformationPage"/> class.
        /// </summary>
        /// <param name="list"> List of Informations displayed on page </param>
        public AppHistoryInformationPage(List<StatsInfoItem> list)
        {
            InitializeComponent();
            foreach (StatsInfoItem element in list)
            {
                AddElementToScroller(element);
            }
            
        }

        /// <summary>
        /// \metod for adding StatsInfoItem object to page scroller
        /// </summary>
        /// <param name="statsInfoItem"> Object containing information </param>
        public void AddElementToScroller(StatsInfoItem statsInfoItem)
        {
            Scroller.Add(new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Name = Scroller.ChildCount.ToString(),
                Layout = new FlexLayout
                {
                    Direction = FlexLayout.FlexDirection.Column,
                    Justification = FlexLayout.FlexJustification.SpaceBetween,
                }
            });
            Scroller.Children.Last().Add(new TextLabel
            {
                Text = statsInfoItem.Name,
                TextColor = Color.Black
            });
            Scroller.Children.Last().Add(new TextLabel
            {
                Text = statsInfoItem.Information,
                TextColor = Color.Gray
            });
        }
    }
}
