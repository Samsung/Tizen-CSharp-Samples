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
using VoiceMemo.Models;
using VoiceMemo.ViewModels;
using Xamarin.Forms;

namespace VoiceMemo.Views
{
    public enum Pages
    {
        StandBy,
        Recording,
        TryCancel,
        Details,
        RecordList,
        PlayBack,
        Languages,
    }

    public static class PageFactory
    {
        static MainPage main;
        static RecordingPage record;
        static CancelPage cancel;
        static RecordListPage list;
        static DetailsPage detail;
        static PlayBackPage playback;
        static LanguageSelectionPage lang;

        public static Page GetInstance(Pages page, Object o = null)
        {
            switch (page)
            {
                case Pages.StandBy:
                    return PageFactory.main ?? (main = new MainPage());
                case Pages.Recording:
                    var sttOn = System.Convert.ToBoolean(o);
                    record?.Init(/*sttOn*/);
                    return PageFactory.record ?? (record = new RecordingPage(new RecordingPageModel(sttOn)));
                case Pages.TryCancel:
                    //VoiceRecordingPageModel vm = o as VoiceRecordingPageModel;
                    return PageFactory.cancel ?? (cancel = new CancelPage(o as RecordingPageModel));
                case Pages.Details:
                    if (detail != null)
                    {
                        //Console.WriteLine("<PageFactory> DetailsPageModel : " + ((DetailsPageModel)detail.BindingContext));
                        ((DetailsPageModel)detail.BindingContext).Init(o as Record);
                    }
                    //Console.WriteLine("<PageFactory> Return detail : " + detail);
                    return PageFactory.detail ?? (detail = new DetailsPage(new DetailsPageModel(o as Record)));
                case Pages.RecordList:
                    return PageFactory.list ?? (list = new RecordListPage((MainPageModel)o));

                case Pages.PlayBack:
                    if (playback != null)
                    {
                        ((PlayBackPageModel)playback.BindingContext).Init(o as Record);
                    }

                    return PageFactory.playback ?? (playback = new PlayBackPage(new PlayBackPageModel(o as Record)));
                case Pages.Languages:
                    Console.WriteLine("<PageFactory>                ++++++++++++   Languages");
                    lang?.Init();
                    return PageFactory.lang ?? (lang = new LanguageSelectionPage((MainPageModel)o));
                default:
                    return null;
            }
        }

        public static void DestoryPage()
        {
            //cancel?.Dispose();
            ////list?.Dispose();
            ////detail?.Dispose();
            ////lang?.Dispose();
            playback?.Dispose();
            record?.Dispose();
            main?.Dispose();
        }
    }
}

