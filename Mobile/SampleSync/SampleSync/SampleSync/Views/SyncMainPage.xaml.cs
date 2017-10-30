/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using SampleSync.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleSync.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SyncMainPage : ContentPage
    {
        private static ISyncAPIs ISA;

        public SyncMainPage()
        {
            InitializeComponent();
            ISA = DependencyService.Get<ISyncAPIs>();

            // A listener to update date directly for On Demand sync job
            App.UpdateOnDemandDateListener += (s, e) =>
            {
                ondemanddate.Text = e.dateTime;
            };

            // A listener to update date directly for Periodic sync job
            App.UpdatePeriodicDateListener += (s, e) =>
            {
                periodicdate.Text = e.dateTime;
            };

            // A listener to update date directly for Calendar data
            App.UpdateCalendarDateListener += (s, e) =>
            {
                calendardate.Text = e.dateTime;
            };

            // A listener to update date directly for Contact data
            App.UpdateContactDateListener += (s, e) =>
            {
                contactdate.Text = e.dateTime;
            };

            // A listener to update date directly for Image data
            App.UpdateImageDateListener += (s, e) =>
            {
                imagedate.Text = e.dateTime;
            };

            // A listener to update date directly for Music data
            App.UpdateMusicDateListener += (s, e) =>
            {
                musicdate.Text = e.dateTime;
            };

            // A listener to update date directly for Sound data
            App.UpdateSoundDateListener += (s, e) =>
            {
                sounddate.Text = e.dateTime;
            };

            // A listener to update date directly for Video data
            App.UpdateVideoDateListener += (s, e) =>
            {
                videodate.Text = e.dateTime;
            };

            // A listener to notify alarm.set privilege directly to pop-up message
            App.AlarmSetPrivilegeListener += (s, e) =>
            {
                if (e.checkResult == "Allow")
                {
                    ExecutePeriodic();
                }
                else if (e.checkResult == "Ask")
                {
                    NotifyPeriodic();
                }
            };

            // A listener to notify calendar.read privilege directly to pop-up message
            App.CalendarReadPrivilegeListener += (s, e) =>
            {
                if (e.checkResult == "Allow")
                {
                    ExecuteCalendar();
                }
                else if (e.checkResult == "Ask")
                {
                    NotifyCalendar();
                }
            };

            // A listener to notify contact.read privilege directly to pop-up message
            App.ContactReadPrivilegeListener += (s, e) =>
            {
                if (e.checkResult == "Allow")
                {
                    ExecuteContact();
                }
                else if (e.checkResult == "Ask")
                {
                    NotifyContact();
                }
            };
        }

        ~SyncMainPage()
        {
        }

        /// <summary>
        /// A method will be called when the Sync button.
        /// </summary>
        void OnDemandClicked(object sender, EventArgs args)
        {
            ISA.OnDemand();
        }

        /// <summary>
        /// A method will be called when the ALLOW button on the Privilege notice.
        /// </summary>
        void ExecutePeriodic()
        {
            periodicbutton.Text = "Unset";
            ISA.AddPeriodic();
        }

        /// <summary>
        /// A method will notice privilege through its pop-up message.
        /// </summary>
        async void NotifyPeriodic()
        {
            var answer = await DisplayAlert("Privilege Notice", "Do you allow to set alarm for this app?", "ALLOW", "DENY");
            if (answer == true)
            {
                ISA.RequestAlarmSetPrivileges();
                ExecutePeriodic();
            }
            else
            {
                await DisplayAlert("Notice", "You can't use this feature unless allow the permission", "CLOSE");
            }
        }

        /// <summary>
        /// A method will be called when the Set/Unset button.
        /// </summary>
        void PeriodicClicked(object sender, EventArgs args)
        {
            // This button is toggled its text
            // Its function also changes
            if (periodicbutton.Text == "Set")
            {
                // Check Privacy Privilege for alarm.set
                ISA.CheckAlarmSetPrivileges();
            }
            else
            {
                periodicdate.Text = "";
                periodicbutton.Text = "Set";
                ISA.RemovePeriodic();
            }
        }

        /// <summary>
        /// A method will be called when the ALLOW button on the Privilege notice.
        /// </summary>
        void ExecuteCalendar()
        {
            calendarbutton.Text = "On";
            calendarbutton.BackgroundColor = Color.LightGreen;
            ISA.AddCalendarDataChange();
        }

        /// <summary>
        /// A method will notice privilege through its pop-up message.
        /// </summary>
        async void NotifyCalendar()
        {
            var answer = await DisplayAlert("Privilege Notice", "Do you allow to read calendar data for this app?", "ALLOW", "DENY");
            if (answer == true)
            {
                ISA.RequestCalendarReadPrivileges();
                ExecuteCalendar();
            }
            else
            {
                await DisplayAlert("Notice", "You can't use this feature unless allow the permission", "CLOSE");
            }
        }

        /// <summary>
        /// A method will be called when the On/Off button beside the Calendar.
        /// </summary>
        void CalendarClicked(object sender, EventArgs args)
        {
            // This button is toggled its text and color
            // Its function also changes
            if (calendarbutton.Text == "Off")
            {
                // Check Privacy Privilege for calendar.read
                ISA.CheckCalendarReadPrivileges();
            }
            else
            {
                calendardate.Text = "";
                calendarbutton.Text = "Off";
                calendarbutton.BackgroundColor = Color.Red;
                ISA.RemoveCalendarDataChange();
            }
        }

        /// <summary>
        /// A method will be called when the ALLOW button on the Privilege notice.
        /// </summary>
        void ExecuteContact()
        {
            contactbutton.Text = "On";
            contactbutton.BackgroundColor = Color.LightGreen;
            ISA.AddContactDataChange();
        }

        /// <summary>
        /// A method will notice privilege through its pop-up message.
        /// </summary>
        async void NotifyContact()
        {
            var answer = await DisplayAlert("Privilege Notice", "Do you allow to read contact data for this app?", "ALLOW", "DENY");
            if (answer == true)
            {
                ISA.RequestContactReadPrivileges();
                ExecuteContact();
            }
            else
            {
                await DisplayAlert("Notice", "You can't use this feature unless allow the permission", "CLOSE");
            }
        }

        /// <summary>
        /// A method will be called when the On/Off button beside the Contact.
        /// </summary>
        void ContactClicked(object sender, EventArgs args)
        {
            // This button is toggled its text and color
            // Its function also changes
            if (contactbutton.Text == "Off")
            {
                // Check Privacy Privilege for contact.read
                ISA.CheckContactReadPrivileges();
            }
            else
            {
                contactdate.Text = "";
                contactbutton.Text = "Off";
                contactbutton.BackgroundColor = Color.Red;
                ISA.RemoveContactDataChange();
            }
        }

        /// <summary>
        /// A method will be called when the On/Off button beside the Image.
        /// </summary>
        void ImageClicked(object sender, EventArgs args)
        {
            // This button is toggled its text and color
            // Its function also changes
            if (imagebutton.Text == "Off")
            {
                imagebutton.Text = "On";
                imagebutton.BackgroundColor = Color.LightGreen;
                ISA.AddImageDataChange();
            }
            else
            {
                imagedate.Text = "";
                imagebutton.Text = "Off";
                imagebutton.BackgroundColor = Color.Red;
                ISA.RemoveImageDataChange();
            }
        }

        /// <summary>
        /// A method will be called when the On/Off button beside the Music.
        /// </summary>
        void MusicClicked(object sender, EventArgs args)
        {
            // This button is toggled its text and color
            // Its function also changes
            if (musicbutton.Text == "Off")
            {
                musicbutton.Text = "On";
                musicbutton.BackgroundColor = Color.LightGreen;
                ISA.AddMusicDataChange();
            }
            else
            {
                musicdate.Text = "";
                musicbutton.Text = "Off";
                musicbutton.BackgroundColor = Color.Red;
                ISA.RemoveMusicDataChange();
            }
        }

        /// <summary>
        /// A method will be called when the On/Off button beside the Sound.
        /// </summary>
        void SoundClicked(object sender, EventArgs args)
        {
            // This button is toggled its text and color
            // Its function also changes
            if (soundbutton.Text == "Off")
            {
                soundbutton.Text = "On";
                soundbutton.BackgroundColor = Color.LightGreen;
                ISA.AddSoundDataChange();
            }
            else
            {
                sounddate.Text = "";
                soundbutton.Text = "Off";
                soundbutton.BackgroundColor = Color.Red;
                ISA.RemoveSoundDataChange();
            }
        }

        /// <summary>
        /// A method will be called when the On/Off button beside the Video.
        /// </summary>
        void VideoClicked(object sender, EventArgs args)
        {
            // This button is toggled its text and color
            // Its function also changes
            if (videobutton.Text == "Off")
            {
                videobutton.Text = "On";
                videobutton.BackgroundColor = Color.LightGreen;
                ISA.AddVideoDataChange();
            }
            else
            {
                videodate.Text = "";
                videobutton.Text = "Off";
                videobutton.BackgroundColor = Color.Red;
                ISA.RemoveVideoDataChange();
            }
        }
    }
}
