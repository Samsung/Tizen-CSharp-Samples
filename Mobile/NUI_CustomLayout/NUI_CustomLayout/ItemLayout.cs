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
 *   +------------------------------------------------------------------------+
 *   |  +------+   +--------------------------------------------------------+ |
 *   |  |      |   |                                                        | |
 *   |  | ICON |   |   TITLE                                                | |
 *   |  |      |   |                                                        | |
 *   |  +------+   +--------------------------------------------------------+ |
 *   +------------------------------------------------------------------------+
 *
 *   +------------------------------------------------------------------------------+
 *   |              +-------------------------------------------------------------+ |
 *   |  +-------+   | TITLE                                                       | |
 *   |  |       |   |                                                             | |
 *   |  | ICON  |   +-------------------------------------------------------------+ |
 *   |  |       |   +-------------------------------------------------------------+ |
 *   |  +-------+   | DESCRIPTION                                                 | |
 *   |              |                                                             | |
 *   |              +-------------------------------------------------------------+ |
 *   +------------------------------------------------------------------------------+
*/

namespace SimpleLayout
{
    internal class ItemLayout : LayoutGroup
    {
        private const int ItemMargin = 5;
        private const int TextLeftMargin = 120;
        private const int TextRightMargin = 710;
        private const int DescriptionTopMargin = 70;
        private const int TextHeight = 50;

        protected override void OnMeasure(MeasureSpecification widthMeasureSpec, MeasureSpecification heightMeasureSpec)
        {
            var itemWidth = new LayoutLength(0);
            var itemHeight = new LayoutLength(0);
            var iconHeight = new LayoutLength(0);

            float labelMaxWidth = 0;

            foreach (LayoutItem childLayout in LayoutChildren)
            {
                if (childLayout != null)
                {
                    MeasureChild(childLayout, widthMeasureSpec, heightMeasureSpec);

                    if (childLayout.Owner is TextLabel)
                    {
                        MeasureChild(childLayout, widthMeasureSpec, heightMeasureSpec);
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

            if (iconHeight.AsRoundedValue() > itemHeight.AsRoundedValue()) itemHeight = iconHeight;

            // Finally, call this method to set the dimensions we would like
            SetMeasuredDimensions(new MeasuredSize(itemWidth, MeasuredSize.StateType.MeasuredSizeOK),
                                  new MeasuredSize(itemHeight, MeasuredSize.StateType.MeasuredSizeOK));
        }

        protected override void OnLayout(bool changed, LayoutLength left, LayoutLength top, LayoutLength right, LayoutLength bottom)
        {
            foreach (LayoutItem childLayout in LayoutChildren)
            {
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
