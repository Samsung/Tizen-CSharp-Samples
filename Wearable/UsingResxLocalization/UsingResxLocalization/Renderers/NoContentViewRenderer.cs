using System;
using System.ComponentModel;
using UsingResxLocalization.ViewExtensions;
using UsingResxLocalization.Renderers;
using Xamarin.Forms.Platform.Tizen;
using ELayout = ElmSharp.Layout;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(NoContentView), typeof(NoContentViewRenderer))]

namespace UsingResxLocalization.Renderers
{
    /// <summary>
    /// Custom renderer class for NoContentView
    /// </summary>
    public class NoContentViewRenderer : ViewRenderer<NoContentView, ELayout>
    {
        ELayout _layout;

        public NoContentViewRenderer()
        {
        }

        /// <summary>
        /// Called when NoContentView element is changed
        /// </summary>
        /// <param name="e">ElementChangedEventArgs<NoContentView></param>
        protected override void OnElementChanged(ElementChangedEventArgs<NoContentView> e)
        {
            if (Control == null)
            {
                _layout = new ELayout(Forms.NativeParent);
                _layout.SetTheme("layout", "nocontents", "default");
                //var rect = new ElmSharp.Rectangle(_layout);
                //rect.Show();
                //_layout.SetPartContent("elm.swallow.icon", rect);
                //_layout.SetPartText("elm.text", "No Item");

                SetNativeControl(_layout);
            }
            //if (e.NewElement != null)
            //{
            //    UpdateTitle();
            //}
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("OnElementPropertyChanged:" + e.PropertyName);
            if (e.PropertyName == NoContentView.TitleProperty.PropertyName)
            {
                UpdateTitle();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        void UpdateTitle()
        {
            if (string.IsNullOrEmpty(Element.Title))
            {
                Console.WriteLine("UpdateTitle Title is null or empty");
                return;
            }

            Console.WriteLine($"UpdateTitle Title:{Element.Title}");
            _layout.SetPartText("elm.text.title", Element.Title);
            _layout.SignalEmit("elm,state,title,enable", "elm");
        }
    }
}
