/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Threading.Tasks;
using VoiceMemo.Effects;
using VoiceMemo.Models;
using VoiceMemo.Services;
using VoiceMemo.ViewModels;
using Xamarin.Forms;

namespace VoiceMemo.Views
{
    /// <summary>
    /// RecordingPage class
    /// It provides voice recording, pausing, and canceling.
    /// </summary>
    public partial class RecordingPage : CirclePageEx
    {
        public RecordingPageModel ViewModel;
        double prevScale;
        double prevShadowScale;
        double prevAudioLevel;
        bool currentOpaque = true;
        double opacity;

        public static readonly BindableProperty RecordingEffectAnimationProperty = BindableProperty.Create(nameof(RecordingEffectAnimation), typeof(bool), typeof(RecordingPage), false, propertyChanged: OnAnimationPropertyChanged);

        private static async void OnAnimationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)oldValue == false && (bool)newValue == true)
            {
                await ((RecordingPage)(bindable)).StartVolumeEffectAnimation();
            }
        }

        public static readonly BindableProperty TimeFlickeringAnimationProperty = BindableProperty.Create(nameof(TimeFlickeringAnimation), typeof(bool), typeof(RecordingPage), false, propertyChanged: OnTimeFlickeringPropertyChanged);

        private static async void OnTimeFlickeringPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)oldValue == false && (bool)newValue == true)
            {
                await ((RecordingPage)(bindable)).Blink();
            }
        }

        public bool RecordingEffectAnimation
        {
            get { return (bool)GetValue(RecordingEffectAnimationProperty); }
            set
            {
                SetValue(RecordingEffectAnimationProperty, value);
            }
        }

        public bool TimeFlickeringAnimation
        {
            get { return (bool)GetValue(TimeFlickeringAnimationProperty); }
            set
            {
                SetValue(TimeFlickeringAnimationProperty, value);
            }
        }

        public RecordingPage(RecordingPageModel viewModel)
        {
            BindingContext = ViewModel = viewModel;
            InitializeComponent();
            UpdateColor();

            RegisterForEvents();

            RecordingEffectAnimation = false;
            TimeFlickeringAnimation = false;

            SetBinding(RecordingEffectAnimationProperty, new Binding("RecordingEffectOn", mode: BindingMode.Default));
            SetBinding(TimeFlickeringAnimationProperty, new Binding("TimeFlickeringOn", mode: BindingMode.Default));

            Init();
        }

        void RegisterForEvents()
        {
            //// Get notified when AudioRecordService's State is Recording
            //MessagingCenter.Subscribe<IAudioRecordService>(this, MessageKeys.ReadyToRecord, async (obj) =>
            //{
            //    Console.WriteLine("### MessagingCenter ## [RecordingPage] ReadyToRecord!! ");
            //});

            MessagingCenter.Subscribe<RecordingPageModel, Record>(this, MessageKeys.SaveVoiceMemo, async (obj, item) =>
            {
                var _DeviceInfoService = DependencyService.Get<IDeviceInformation>(DependencyFetchTarget.GlobalInstance);
                if (_DeviceInfoService.AppState == AppState.Terminated)
                {
                    //Toast.DisplayText("Memo " + item.ID + " saved.", 3000);
                    // TODO: show popup using app control
                    return;
                }

                await Navigation.PushAsync(PageFactory.GetInstance(Pages.Details, item/*new DetailsPageModel(item)*/));

                if (_DeviceInfoService.AppState == AppState.Background)
                {
                    Console.WriteLine(" ************   TO DO SOMETHING  *************");
                }
            });

            // Called when an error occurs while recording
            MessagingCenter.Subscribe<IMediaContentService, Exception>(this, MessageKeys.ErrorOccur, (obj, exception) =>
            {
                DisplayAlert("Recording", exception.Message, "OK");
            });

            MessagingCenter.Subscribe<RecordingPageModel>(this, MessageKeys.ForcePopRecordingPage, async (obj) =>
            {
                Console.WriteLine("### MessagingCenter ## [RecordingPage] just received ForcePopRecordingPage");
                await Navigation.PopAsync();
            });
        }

        /// <summary>
        /// Initialize Recording page
        /// </summary>
        public void Init()
        {
            RecordingTimeLabel.TextColor = Color.White;
            ImageAttributes.SetBlendColor(RecordingIconPauseImage, (Color)Application.Current.Resources["AO012L3"]);
            ((RecordingPageModel)ViewModel).Init();
        }

        /// <summary>
        /// Start volume effect animation
        /// </summary>
        private async Task StartVolumeEffectAnimation()
        {
            // Any background code that needs to update the user interface
            // interact with UI elements
            // call CheckVolumeAndResizeAnimationAsync method every 100ms
            while (((RecordingPageModel) BindingContext).RecordingEffectOn)
            {
                await Task.WhenAll(CheckVolumeAndResizeAnimationAsync(), Task.Delay(100));
            }
        }

        // Draw animation effects depending on volume level
        private async Task CheckVolumeAndResizeAnimationAsync()
        {
            RecordingPageModel recordingViewModel = (RecordingPageModel)BindingContext;

            if (recordingViewModel.AudioRecordingService.State == AudioRecordState.Recording)
            {
                double fScaleValue = 0, fShodowScaleValue = 0;
                double volume = recordingViewModel.AudioRecordingService.GetRecordingLevel(); //-300 ~ 0
                int uwLevel = 0;
                int uwShadowLevel = 0;
                int effectLevel = 0;
                double db = volume;
                const int VOLUME_LEVEL_MIN = -55;
                const int VOLUME_LEVEL_MAX = -10;
                const double LINEAR_VI_SCALE = 1.0 / 20.0;

                if (db < VOLUME_LEVEL_MIN)
                {
                    effectLevel = 0;
                }
                else if (db > VOLUME_LEVEL_MAX)
                {
                    effectLevel = VOLUME_LEVEL_MAX - VOLUME_LEVEL_MIN;
                }
                else
                {
                    effectLevel = (int)(db - VOLUME_LEVEL_MIN);
                }

                uwLevel = effectLevel;

                if (uwLevel > 1)
                {
                    uwShadowLevel = uwLevel / 4 + (new Random().Next() % (uwLevel / 2));
                }

                if (uwLevel > prevAudioLevel)
                {
                    fScaleValue = (30 + 70 * (uwLevel) / (VOLUME_LEVEL_MAX - VOLUME_LEVEL_MIN)) / 100.0;
                    fShodowScaleValue = (30 + 70 * (uwShadowLevel) / (VOLUME_LEVEL_MAX - VOLUME_LEVEL_MIN)) / 100.0;

                    if (fScaleValue < prevScale)
                    {
                        fScaleValue = prevScale - LINEAR_VI_SCALE;
                    }

                    if (fShodowScaleValue < prevShadowScale)
                    {
                        fShodowScaleValue = prevShadowScale - LINEAR_VI_SCALE;
                    }
                }
                else
                {
                    fScaleValue = prevScale - LINEAR_VI_SCALE;
                    fShodowScaleValue = prevShadowScale - LINEAR_VI_SCALE;
                }

                if (fScaleValue < 0.3)
                {
                    fScaleValue = 0.3;
                }
                else if (fScaleValue > 1.0)
                {
                    fScaleValue = 1.0;
                }

                if (fShodowScaleValue < 0.3)
                {
                    fShodowScaleValue = 0.3;
                }
                else if (fShodowScaleValue > 1.0)
                {
                    fShodowScaleValue = 1.0;
                }

                int actualSize = (int)(fScaleValue * 360);
                int acutalShadowSize = (int)(fShodowScaleValue * 360);

                await DrawVolumeEffectAnimationAsync(actualSize, acutalShadowSize);

                prevAudioLevel = uwLevel;
                prevScale = fScaleValue;
                prevShadowScale = fShodowScaleValue;
            }
        }

        // Draw animation depending on volume level while recording
        private Task DrawVolumeEffectAnimationAsync(int imageSize, int shadowImageSize)
        {
            double image2Ratio = (double)imageSize / 126;
            double imageRatio = (double)shadowImageSize / 126;
            return Task.WhenAll(
                VoiceRecorderBtnEffectImage2.ScaleTo(image2Ratio,50),
                VoiceRecorderBtnEffectImage.ScaleTo(imageRatio,50));
        }

        /// <summary>
        /// Invoked immediately prior to the Page becoming visible.
        /// </summary>
        protected override void OnAppearing()
        {
        }

        /// <summary>
        /// Invoked when this page disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
        }

        /// <summary>
        /// Invoked when backbutton is pressed
        /// As a result, CancelPage will be shown.
        /// </summary>
        /// <returns>bool</returns>
        protected override bool OnBackButtonPressed()
        {
            TryToCancel_Tapped(this, new EventArgs());
            return true;
        }

        // When it is no need to receive notifications anymore, unsubscribes subscribers from the specified messages 
        void UnregisterForEvents()
        {
            //MessagingCenter.Unsubscribe<IAudioRecordService>(this, MessageKeys.ReadyToRecord);
            MessagingCenter.Unsubscribe<RecordingPageModel, Record>(this, MessageKeys.SaveVoiceMemo);
            MessagingCenter.Unsubscribe<IMediaContentService, Exception>(this, MessageKeys.ErrorOccur);
            MessagingCenter.Unsubscribe<RecordingPageModel>(this, MessageKeys.ForcePopRecordingPage);
        }

        /// <summary>
        /// Dispose this page
        /// </summary>
        public void Dispose()
        {
            UnregisterForEvents();
            ((RecordingPageModel)BindingContext).Dispose();
        }

        /// <summary>
        /// Invoked when RecordingIconCancelImage is tapped
        /// Recording is temporarily paused.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">EventArgs</param>
        async void TryToCancel_Tapped(object sender, EventArgs e)
        {
            if (ViewModel.RecordingViewModelState == RecordingViewModelState.Recording)
            {
                ViewModel.RequestCommand.Execute(RecordingCommandType.PauseForCancelRequest);
            }

            await Navigation.PushAsync(PageFactory.GetInstance(Pages.TryCancel, ViewModel));
        }

        /// <summary>
        /// Invoked when RecordControllerPauseBgImage or RecordingIconPauseImage is tapped
        /// Recording is temporarily paused.
        /// paused and recording states are toggled every time this image button is pressed
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">EventArgs</param>
        void PauseRecording_Tapped(object sender, EventArgs e)
        {
            if (((RecordingPageModel)BindingContext).RecordingViewModelState == RecordingViewModelState.Paused)
            {
                ViewModel.RequestCommand.Execute(RecordingCommandType.Record);
            }
            else if (((RecordingPageModel)BindingContext).RecordingViewModelState == RecordingViewModelState.Recording)
            {
                ViewModel.RequestCommand.Execute(RecordingCommandType.Pause);
            }
        }

        private async Task Blink()
        {
            currentOpaque = true;
            
            while (((RecordingPageModel)BindingContext).TimeFlickeringOn)
            {
                await Task.WhenAll(BlinkInvoker(), Task.Delay(700));
            }
        }

        private async Task BlinkInvoker()
        {
            opacity = currentOpaque ? 0 : 1;
            await BlinkAnimation();
            currentOpaque = !currentOpaque;
        }

        private async Task BlinkAnimation()
        {
            if (!((RecordingPageModel)BindingContext).TimeFlickeringOn)
            {
                RecordingTimeLabel.Opacity = 1.0;
            }
            else
            {
                await Task.WhenAny(RecordingTimeLabel.FadeTo(opacity, 500, Easing.Linear));
            }
        }
            
        /// <summary>
        /// Invoked when VoiceRecorderBtnBgImage and VoiceRecorderBtnStopImage is tapped
        /// Audio recording will be done
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">EventArgs</param>
        void CompleteRecording_Tapped(object sender, EventArgs args)
        {
            Console.WriteLine("RecordingPage   ============   CompleteRecording_Tapped");
            ViewModel.RequestCommand.Execute(RecordingCommandType.Stop);
        }

        void UpdateColor()
        {
            // For case that STT is On, recording_stt_icon.png
            ImageAttributes.SetBlendColor(RecordingSttImage, (Color)Application.Current.Resources["AO0210"]);

            // Recorder
            ImageAttributes.SetBlendColor(VoiceRecorderBtnEffectImage, (Color)Application.Current.Resources["AO028"]);
            ImageAttributes.SetBlendColor(VoiceRecorderBtnEffectImage2, (Color)Application.Current.Resources["AO028"]);
            ImageAttributes.SetBlendColor(VoiceRecorderBtnBgImage, (Color)Application.Current.Resources["AO014L1"]);
            ImageAttributes.SetBlendColor(VoiceRecorderBtnStopImage, (Color)Application.Current.Resources["AO014L3"]);

            // Record Controller in Left
            ImageAttributes.SetBlendColor(RecordControllerCancelBgImage, (Color)Application.Current.Resources["AO012L1"]);
            ImageAttributes.SetBlendColor(RecordingIconCancelImage, (Color)Application.Current.Resources["AO012L3"]);

            // Record Controller in Right
            ImageAttributes.SetBlendColor(RecordControllerPauseBgImage, (Color)Application.Current.Resources["AO012L1"]);
            ImageAttributes.SetBlendColor(RecordingIconPauseImage, (Color)Application.Current.Resources["AO012L3"]);
        }
    }
}