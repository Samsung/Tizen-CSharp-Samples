using System;
using System.Linq;
using System.Windows.Input;
using MusicPlayerUI.Page;
using Xamarin.Forms;

namespace MusicPlayerUI.View
{
    /// <summary>
    /// MusicBar
    /// </summary>
    public partial class MusicBar : ContentView
    {
        public static readonly BindableProperty IsPlayingProperty = BindableProperty.Create("IsPlaying", typeof(bool), typeof(MusicBar), false);
        public static readonly BindableProperty SongTitleProperty = BindableProperty.Create("SongTitle", typeof(string), typeof(MusicBar), "NoName");

        public ICommand PlayCommand
        {
            get
            {
                if (_playCommand == null)
                    _playCommand = new Command(PlayCommandExcute);
                return _playCommand;
            }
        }

        private ICommand _playCommand = null;
        private Label _lbSongTitle = null;
        private double _songValue = 0;
        private Xamarin.Forms.Page _modalPlayPage = null;

        /// <summary>
        /// Gets or sets a value that tells whether the music is currenty playing.
        /// </summary>
        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a title of the song.
        /// </summary>
        public string SongTitle
        {
            get { return (string)GetValue(SongTitleProperty); }
            set { SetValue(SongTitleProperty, value); }
        }

        public double SongValue
        {
            get { return _songValue; }
            set { _songValue = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MusicBar()
        {
            InitializeComponent();
            _lbSongTitle = this.FindByName<Label>("lbSongTitle");
        }

        /// <summary>
        /// Execute the play command.
        /// </summary>
        private void PlayCommandExcute()
        {
            if (_lbSongTitle == null)
                return;

            if (IsPlaying == false)
            {
                var transAni = new Animation(v => _lbSongTitle.TranslationX = (_lbSongTitle.Width * 2) - (v * (_lbSongTitle.Width * 2)), 0, 1, Easing.CubicOut);
                transAni.Commit(this, "TransLabelAnimation", 16, 4000, null, repeat: () => true);
                IsPlaying = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    if (TabItemPage.MusicBar.IsPlaying)
                    {
                        if (SongValue >= 1)
                            SongValue = 0;
                        SongValue += 0.001;
                    }
                    return IsPlaying;
                });
                OnPlayModalPage(null, null);
            }
            else
            {
                IsPlaying = false;
                this.AbortAnimation("TransLabelAnimation");
                _lbSongTitle.TranslationX = 0;
            }
        }

        private async void OnPlayModalPage(object sender, System.EventArgs e)
        {
            if (_modalPlayPage == null)
                _modalPlayPage = new ModalPlayPage();

            if (Navigation.ModalStack.Count(p => p is ModalPlayPage) == 0)
                await Navigation.PushModalAsync(_modalPlayPage);
        }
    }
}