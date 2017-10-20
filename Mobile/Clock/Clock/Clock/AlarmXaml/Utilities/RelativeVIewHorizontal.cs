using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Clock.Utilities
{
    public class RelativeViewHorizontal : RelativeToView
    {
        protected override double DetermineExtent(VisualElement view)
        {
            return view.Width;
        }

        protected override double DetermineStart(VisualElement view)
        {
            return view.X;
        }
    }
}