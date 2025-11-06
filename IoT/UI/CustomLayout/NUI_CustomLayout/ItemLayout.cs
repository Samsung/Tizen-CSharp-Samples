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

using Tizen.NUI;
using Tizen.NUI.BaseComponents;

/*
 * The Item Layout has two modes depending on the number of text labels inserted into it.
 * Structure is depicted below.
 *
 *   +------------------------------------------------------------------------+ ------
 *   |                                                                        |        -> Item Margin
 *   |  +------+   +--------------------------------------------------------+ | ------
 *   |  |      |   |                                                        | |
 *   |  | ICON |   |   TITLE                                                | |
 *   |  |      |   |                                                        | |
 *   |  +------+   +--------------------------------------------------------+ |
 *   +------------------------------------------------------------------------+
 *   |             |
 *   |             |
 *   |             |
 *   Text Left Margin
 *   |                                                                      |
 *                               Text Right Margin
 * 
 *   +------------------------------------------------------------------------------+-------
 *   |              +-------------------------------------------------------------+ |
 *   |  +-------+   | TITLE                                                       | |        Description Top Margin
 *   |  |       |   |                                                             | |  
 *   |  | ICON  |   +-------------------------------------------------------------+ |
 *   |  |       |   +-------------------------------------------------------------+ |-------
 *   |  +-------+   | DESCRIPTION                                                 | |
 *   |              |                                                             | |
 *   |              +-------------------------------------------------------------+ |
 *   +------------------------------------------------------------------------------+
*/

namespace SimpleLayout
{
    /// <summary>
    /// The custom layout sample implementation. This class creates layout for Item Object as it is depicted above.
    /// The custom layout must be derived from LayoutGroup and override the two methods, OnMeasure and OnLayout.
    /// OnMeasure and OnLayout methods must be extended and called during the measuring and layout phases respectively.
    /// </summary>
    internal class ItemLayout : LayoutGroup
    {
        /// <summary>
        /// Top margin of item contents.
        /// </summary>
        private const int ItemMargin = 5;

        /// <summary>
        /// Point where Description and Title begins.
        /// </summary>
        private const int TextLeftMargin = 120;
        
        /// <summary>
        /// Point where Description and Title ends.
        /// </summary>
        private const int TextRightMargin = 710;
        
        /// <summary>
        /// Vertical Description margin.
        /// </summary>
        private const int DescriptionTopMargin = 70;
        
        /// <summary>
        /// Height of a text object.
        /// </summary>
        private const int TextHeight = 50;

        /// <summary>
        /// Function calculates the layout size requirements using parent width and height measure specifications.
        /// </summary>
        /// <param name="widthMeasureSpec">Parent width measure specification</param>
        /// <param name="heightMeasureSpec">Parent height measure specification</param>
        protected override void OnMeasure(MeasureSpecification widthMeasureSpec, MeasureSpecification heightMeasureSpec)
        {
            var itemWidth = new LayoutLength(0);
            var itemHeight = new LayoutLength(0);
            var iconHeight = new LayoutLength(0);

            float labelMaxWidth = 0;

            //All layout items have to be measured to calculate Item width and height
            foreach (LayoutItem childLayout in LayoutChildren)
            {
                if (childLayout != null)
                {
                    //Set widthMEasureSpecification and HeightMeasureSpecification for children
                    MeasureChild(childLayout, widthMeasureSpec, heightMeasureSpec);
                    
                    //Item size depends of the content. If item contains descrpitoin height of the item is different.
                    if (childLayout.Owner is TextLabel)
                    {
                        itemHeight += childLayout.MeasuredHeight.Size;

                        if (childLayout.MeasuredWidth.Size.AsRoundedValue() > labelMaxWidth)
                        {
                            labelMaxWidth = childLayout.MeasuredWidth.Size.AsRoundedValue();
                        }
                    }
                    else
                    {
                        itemWidth += childLayout.MeasuredWidth.Size;
                        iconHeight = childLayout.MeasuredHeight.Size;
                    }

                    //update item width by maximum label widht
                    itemWidth += new LayoutLength(labelMaxWidth);
                }
            }

            if (iconHeight.AsRoundedValue() > itemHeight.AsRoundedValue())
            {
                itemHeight = iconHeight;
            }

            // Finally, call this method to set the dimensions we would like
            SetMeasuredDimensions(new MeasuredSize(itemWidth, MeasuredSize.StateType.MeasuredSizeOK),
                                  new MeasuredSize(itemHeight, MeasuredSize.StateType.MeasuredSizeOK));
        }

        /// <summary>
        /// Laying out and positioning the children within View itself using their measured sizes.
        /// </summary>
        /// <param name="changed">This is a new size or position for this layout.</param>
        /// <param name="left">Left position, relative to parent.</param>
        /// <param name="top"> Top position, relative to parent.</param>
        /// <param name="right">Right position, relative to parent.</param>
        /// <param name="bottom">Bottom position, relative to parent.</param>
        protected override void OnLayout(bool changed, LayoutLength left, LayoutLength top, LayoutLength right, LayoutLength bottom)
        {
            //Size have to be calculated for all childrens.
            foreach (LayoutItem childLayout in LayoutChildren)
            {
                //Layout owner name is used to set valid size values
                if (childLayout.Owner.Name == SimpleLayout.ItemContentNameIcon)
                {
                    LayoutLength width = childLayout.MeasuredWidth.Size;
                    LayoutLength height = childLayout.MeasuredHeight.Size;

                    childLayout.Layout(new LayoutLength(ItemMargin),
                                       new LayoutLength(ItemMargin),
                                       width + ItemMargin,
                                       height + ItemMargin);
                }
                else if (childLayout.Owner.Name == SimpleLayout.ItemContentNameTitle)
                {
                    childLayout.Layout(new LayoutLength(TextLeftMargin),
                                       new LayoutLength(ItemMargin),
                                       new LayoutLength(TextRightMargin),
                                       new LayoutLength(TextHeight + ItemMargin));
                }
                else if (childLayout.Owner.Name == SimpleLayout.ItemContentNameDescription)
                {
                    childLayout.Layout(new LayoutLength(TextLeftMargin),
                                       new LayoutLength(DescriptionTopMargin),
                                       new LayoutLength(TextRightMargin),
                                       new LayoutLength(DescriptionTopMargin + TextHeight + 3 * ItemMargin));
                }
            }
        }

    }
}
