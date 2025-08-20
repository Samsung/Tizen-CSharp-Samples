using System;
using Gallery.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageViewer : ContentPage, IRemoteController
    {
        private bool _playing = false;

        public ImageViewer(int selectedIndex)
        {
            InitializeComponent();

            Appearing += (s, e) =>
            {
                // TODO. Scroll is not applied to ScrollView at the time of Appearing call in UWP.
                if (Device.RuntimePlatform == Device.UWP)
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(20), () =>
                    {
                        panel.ScrollTo(selectedIndex * Width, 0, false);

                        return false;
                    });
                }
                else
                {
                    panel.ScrollTo(selectedIndex * Width, 0, false);
                }
            };
        }

        public void SendLeftButtonDown()
        {
            MessagingCenter.Send<Page, Panel>(this, "PrevImage", panel);
        }

        public void SendRightButtonDown()
        {
            MessagingCenter.Send<Page, Panel>(this, "NextImage", panel);
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var btn = sender as Button;
            if (!_playing)
            {
                btn.Text = "Stop Slide Show";
                NavigationPage.SetHasNavigationBar(this, false);
            }
            else
            {
                btn.Text = "Play Slide Show";
                NavigationPage.SetHasNavigationBar(this, true);
            }
            _playing = !_playing;
        }
    }
}