/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Windows.Input;
using MusicPlayer.Models;
using MusicPlayer.Views;
using Xamarin.Forms;

namespace MusicPlayer.ViewModels
{
    /// <summary>
    /// The application's main view model class (abstraction of the view).
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Frequency of updating progress of the track.
        /// </summary>
        private const int UPDATE_PROGRESS_INTERVAL = 100;

        /// <summary>
        /// Reference to class handling navigation over views.
        /// </summary>
        private readonly IViewNavigation _navigation;

        /// <summary>
        /// Specifying progress of the track as value from 0 to 1.
        /// </summary>
        private double _trackProgress;

        /// <summary>
        /// Specifying the information about playback position of the track (in milliseconds).
        /// </summary>
        private int _trackPlaybackPosition;

        /// <summary>
        /// Current player model.
        /// </summary>
        private PlayerModel _musicPlayerModel;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets progress of the track as value from 0 to 1.
        /// </summary>
        public double TrackProgress
        {
            get => _trackProgress;
            set => SetProperty(ref _trackProgress, value);
        }

        /// <summary>
        /// Gets or sets information about playback position of the track (in milliseconds).
        /// </summary>
        public int TrackPlaybackPosition
        {
            get => _trackPlaybackPosition;
            set => SetProperty(ref _trackPlaybackPosition, value);
        }

        /// <summary>
        /// Gets current track.
        /// </summary>
        public Track CurrentTrack
        {
            get => _musicPlayerModel.CurrentTrack;
        }

        /// <summary>
        /// Gets list of loaded tracks.
        /// </summary>
        public List<Track> Tracks
        {
            get => _musicPlayerModel.Tracks;
        }

        /// <summary>
        /// Determines if track is paused.
        /// </summary>
        public bool IsPaused
        {
            get => !_musicPlayerModel.Playing;
        }

        /// <summary>
        /// Determines if there are any tracks available
        /// </summary>
        public bool TracksAvailable
        {
            get => _musicPlayerModel.Tracks.Count > 0;
        }

        /// <summary>
        /// Command which executes the procedure of changing to the previous track.
        /// </summary>
        public ICommand GoToPreviousTrackCommand { get; private set; }

        /// <summary>
        /// Command which executes the procedure of seeking backward.
        /// </summary>
        public ICommand RewindTrackCommand { get; private set; }

        /// <summary>
        /// Command which executes the procedure of pausing/playing the track.
        /// </summary>
        public ICommand PlayOrPauseTrackCommand { get; private set; }

        /// <summary>
        /// Command which executes the procedure of seeking forward.
        /// </summary>
        public ICommand ForwardTrackCommand { get; private set; }

        /// <summary>
        /// Command which executes the procedure of changing to the next track.
        /// </summary>
        public ICommand GoToNextTrackCommand { get; private set; }

        /// <summary>
        /// Command which shows actual application (after welcome screen).
        /// </summary>
        public ICommand GoToApplicationCommand { get; private set; }

        /// <summary>
        /// Command which shows the preview page.
        /// </summary>
        public ICommand GoToPreviewPageCommand { get; private set; }

        /// <summary>
        /// Command which sets as current track selected track.
        /// </summary>
        public ICommand SelectTrackCommand { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public MainViewModel()
        {
            TrackProgress = 0;
            _navigation = DependencyService.Get<IViewNavigation>();
            _musicPlayerModel = new PlayerModel();

            Device.StartTimer(TimeSpan.FromMilliseconds(UPDATE_PROGRESS_INTERVAL), OnProgressUpdateTimerTick);

            _musicPlayerModel.PlaybackStateChanged += OnPlaybackStateChanged;

            _musicPlayerModel.CurrentTrackChanged += OnCurrentTrackChanged;

            InitCommands();
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            GoToApplicationCommand = new Command(ExecuteGoToApplication);
            GoToPreviewPageCommand = new Command(ExecuteGoToPreview);
            GoToPreviousTrackCommand = new Command(GoToPreviousTrack);
            RewindTrackCommand = new Command(RewindTrack);
            PlayOrPauseTrackCommand = new Command(PlayOrPauseTrack);
            ForwardTrackCommand = new Command(ForwardTrack);
            GoToNextTrackCommand = new Command(GoToNextTrack);
            SelectTrackCommand = new Command<int>(ExecuteSelectTrack);
        }

        /// <summary>
        /// Handles the execution of "GoToPreviousTrackCommand"
        /// </summary>
        private void GoToPreviousTrack()
        {
            _musicPlayerModel.Previous();
        }

        /// <summary>
        /// Handles the execution of "RewindTrackCommand"
        /// </summary>
        private void RewindTrack()
        {
            _musicPlayerModel.Rewind();
        }

        /// <summary>
        /// Handles the execution of "PlayOrPauseTrackCommand"
        /// </summary>
        private void PlayOrPauseTrack()
        {
            _musicPlayerModel.PlayOrPause();
        }

        /// <summary>
        /// Handles the execution of "ForwardTrackCommand"
        /// </summary>
        private void ForwardTrack()
        {
            _musicPlayerModel.Forward();
        }

        /// <summary>
        /// Handles the execution of "GoToNextTrackCommand"
        /// </summary>
        private void GoToNextTrack()
        {
            _musicPlayerModel.Next();
        }

        /// <summary>
        /// Handles the execution of "SelectTrackCommand"
        /// </summary>
        /// <param name="index">Index of the selected track.</param>
        private void ExecuteSelectTrack(int index)
        {
            _musicPlayerModel.SetTrack(index, true);
        }

        /// <summary>
        /// Updates progress of the track.
        /// </summary>
        /// <returns>Flag indicating if timer should keep recurring.</returns>
        private bool OnProgressUpdateTimerTick()
        {
            if (_musicPlayerModel.CurrentTrack.Duration != 0)
            {
                TrackProgress = _musicPlayerModel.Progress;
                TrackPlaybackPosition = _musicPlayerModel.PlaybackPosition;
            }

            return true;
        }

        /// <summary>
        /// Handles execution of "GoToApplicationCommand".
        /// Navigates to page with soundtracks list (with clearing navigation history).
        /// </summary>
        private void ExecuteGoToApplication()
        {
            _navigation.GoToSoundtracksList(clearHistory: true);
        }

        /// <summary>
        /// Handles execution of "GoToPreviewPageCommand".
        /// Navigates to page with preview.
        /// </summary>
        private void ExecuteGoToPreview()
        {
            _navigation.GoToPreview();
        }

        /// <summary>
        /// Handles an event of changing the playback state of player.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void OnPlaybackStateChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged(nameof(IsPaused));
        }

        /// <summary>
        /// Handles an event of changing the current track.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void OnCurrentTrackChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged(nameof(CurrentTrack));
        }

        #endregion
    }
}
