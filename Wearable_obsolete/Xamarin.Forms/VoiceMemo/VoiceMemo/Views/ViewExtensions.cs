using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VoiceMemo.Views
{
    public static class ViewExtensions
    {
        public static void CancelAnimation(this VisualElement self)
        {
            self.AbortAnimation("ColorTo");
        }
    }
}
