
using System;
using System.Diagnostics;
using Tizen;
using Tizen.Network.Stc;
using Tizen.NUI;


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
        private const int MARGIN = 5;

        private const int TEXT_LEFT_MARGIN = 120;
        private const int TEXT_RIGTH_MARGIN = 710;
        private const int DESCRIPTION_TOP_MARGIN = 70;
        private const int TEXT_HEIGHT = 50;

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
                if (childLayout.Owner.Name == SimpleLayout.ITEM_CONTENT_NAME_ICON)
                {
                    LayoutLength width = childLayout.MeasuredWidth.Size;
                    LayoutLength height = childLayout.MeasuredHeight.Size;

                    childLayout.Layout(new LayoutLength(MARGIN),
                                       new LayoutLength(MARGIN),
                                       width + MARGIN,
                                       height + MARGIN);
                }
                else if (childLayout.Owner.Name == SimpleLayout.ITEM_CONTENT_NAME_TITLE)
                {
                    childLayout.Layout(new LayoutLength(TEXT_LEFT_MARGIN),
                                       new LayoutLength(MARGIN),
                                       new LayoutLength(TEXT_RIGTH_MARGIN),
                                       new LayoutLength(TEXT_HEIGHT + MARGIN));
                }
                else if (childLayout.Owner.Name == SimpleLayout.ITEM_CONTENT_NAME_DESCRIPTION)
                {
                    childLayout.Layout(new LayoutLength(TEXT_LEFT_MARGIN),
                                       new LayoutLength(DESCRIPTION_TOP_MARGIN),
                                       new LayoutLength(TEXT_RIGTH_MARGIN),
                                       new LayoutLength(DESCRIPTION_TOP_MARGIN + TEXT_HEIGHT + 3 * MARGIN));
                }
            }
        }
        
    }
}
