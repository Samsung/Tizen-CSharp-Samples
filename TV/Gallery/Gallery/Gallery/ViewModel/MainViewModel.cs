using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Gallery.Controls;
using Xamarin.Forms;

namespace Gallery.ViewModel
{
    public class MainViewModel : BindableObject
    {
        private IList<string> _imageSource = null;
        private bool _playing;
        private int _slideDirection = 1;
        private int currentIndex;

        public IList<string> ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public ICommand FullScreenShowCommand => new Command<string>(FullScreenShow);

        public ICommand PlaySlideShowCommand => new Command<Panel>(PlaySlideShow);

        public MainViewModel()
        {
            ImageSource = DependencyService.Get<IImageSearchService>().GetImagePathsAsync();
        }

        private async void FullScreenShow(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            currentIndex = ImageSource.IndexOf(path);

            var page = new ImageViewer(currentIndex)
            {
                BindingContext = this,
            };

            page.Disappearing += (s, e) =>
            {
                _playing = false;
                _slideDirection = 1;
                MessagingCenter.Unsubscribe<Page, Panel>(page, "NextImage");
                MessagingCenter.Unsubscribe<Page, Panel>(page, "PrevImage");
            };
            MessagingCenter.Subscribe<Page, Panel>(page, "NextImage", NextImage);
            MessagingCenter.Subscribe<Page, Panel>(page, "PrevImage", PrevImage);

            await Application.Current.MainPage.Navigation.PushAsync(page, false);
        }

        private void PlaySlideShow(Panel panel)
        {
            if (!_playing)
            {
                _playing = true;
                panel.Animate("SlideAnimation", new Animation(), length: 2000, finished: (v, c) => { if (_playing) InternalPlaySlideShow(panel); }, repeat: () => _playing);
            }
            else
            {
                _playing = false;
                panel.AbortAnimation("SlideAnimation");
            }
            OnPropertyChanged("PlayState");
        }

        private void InternalPlaySlideShow(Panel panel)
        {
            currentIndex += _slideDirection;

            if (currentIndex < 0 || currentIndex >= ImageSource.Count)
            {
                _slideDirection *= -1;

                currentIndex += (_slideDirection * 2);
            }

            Debug.WriteLine($"@@ Play Slide index : {currentIndex}");
            if (currentIndex >= 0 && currentIndex < ImageSource.Count)
            {
                panel.ScrollTo(ImageSource[currentIndex], ScrollToPosition.MakeVisible, true);
            }
        }

        private void NextImage(Page context, Panel panel)
        {
            if (currentIndex + 1 < ImageSource.Count)
            {
                panel.ScrollTo(ImageSource[currentIndex += 1], ScrollToPosition.MakeVisible, true);
            }
        }

        private void PrevImage(Page context, Panel panel)
        {
            if (currentIndex - 1 >= 0)
            {
                panel.ScrollTo(ImageSource[currentIndex -= 1], ScrollToPosition.MakeVisible, true);
            }
        }
    }
}
