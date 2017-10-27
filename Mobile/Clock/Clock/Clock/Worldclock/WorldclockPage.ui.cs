/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

using Clock.Controls;
using Clock.Data;
using Clock.Interfaces;
using Clock.Styles;
using Clock.Utils;
using System;
using System.Diagnostics;
using Tizen.Xamarin.Forms.Extension;
using Xamarin.Forms;

namespace Clock.Worldclock
{
    /// <summary>
    /// The world clock page, the class is defined in 2 files
    /// One is for UI part, one is for logical process,
    /// This one is for UI part
    /// </summary>
    public partial class WorldclockPage : ContentPage
    {
        private StackLayout mainLayout;
        // RelativeLayout for map area
        private RelativeLayout mapLayout;
        public WorldclockCityList cityList;

        const int DOT_IMAGE_SIZE = 7;
        const int RING_IMAGE_SIZE = 9;

        private Image[] dotImages;
        private Image[] ringImages;
        const int DOT_MAX_NUM = 8;

        private Image timezoneAreaImage;
        private Label timezoneGmtOffsetLabel;

        private StackLayout timezoneDetailFirstLineLayout;
        private Label timeLabel, amLabel, relativeToLocalLabel;
        private Label citiesLabel;

        // Dialog that is shown when an app user presses HW menu key
        private MoreMenuDialog dialog;

        /// <summary>
        /// Create the worldclock's main view,
        /// It includes map, timezone detail information, listview and one float button
        /// </summary>
        /// <returns> The StackLayout. </returns>
        public StackLayout CreateWorldClockPage()
        {
            CreateMapArea();
            cityList = new WorldclockCityList();

            mainLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 0,
                //BackgroundColor = Color.Red,
                Children =
                {
                    mapLayout,
                    cityList.emptyListAreaLayout,
                    cityList.cityListUI,
                }
            };

            OnMapViewUpdateRequest();

            foreach (Location l in App.ClockInfo.userLocations)
            {
                App.ClockInfo.AppendItemToCustomList(l);
            }

            return mainLayout;
        }

        /// <summary>
        /// Invoked when "Delete" more menu is choosed
        /// </summary>
        /// <param name="menu">string</param>
        private void ShowDeletePage(string menu)
        {
            // Make MoreMenuDialog invisible
            dialog.Hide();
            // Make WorldclockDeletePage visible
            Navigation.PushAsync(WorldclockDeletePage.GetInstance(App.ClockInfo), false);
        }

        /// <summary>
        /// Invoked when "Reorder" more menu is choosed
        /// </summary>
        /// <param name="menu">string</param>
        private void ShowReorderPage(string menu)
        {
            // Make MoreMenuDialog invisible
            dialog.Hide();
            Toast.DisplayText("[TODO] need to make WorldclockReorderPage visible.");
            // Temporarily added. Need to be delete when WorldclockReorderPage is created and shown.
            MenuKeyListener.Start(this, MenuKeyPressed);
        }

        /// <summary>
        /// Called when HW menu key button is pressed in World clock page.
        /// </summary>
        /// <param name="sender">IKeyEventSender</param>
        /// <param name="key">HW menu key("XF86Menu")</param>
        private void MenuKeyPressed(IKeyEventSender sender, string key)
        {
            // Ignore menu key handling when no data item is added to List
            // because there's no item to delete or reorder
            if (App.ClockInfo.CityRecordList.Count == 0)
            {
                return;
            }

            if (App.ClockInfo.CityRecordList.Count == 1)
            {
                // If one data item is added to a list, only "DELETE" more menu will be provided.
                dialog = new MoreMenuDialog(MORE_MENU_OPTION.MORE_MENU_DELETE, ShowDeletePage);
            }
            else
            {
                // If more than two data items are added, "DELETE" and "REORDER" more menu will be provided.
                dialog = new MoreMenuDialog(MORE_MENU_OPTION.MORE_MENU_DELETE_AND_REORDER, ShowDeletePage, ShowReorderPage);
            }

            dialog.BackButtonPressed += Dialog_Canceled;
            dialog.OutsideClicked += Dialog_Canceled;
            dialog.Shown += Dialog_Shown;
            // Make MoreMenuDialog visible
            dialog.Show();
        }

        /// <summary>
        /// Invokwed when MoreMenuDialog is shown.
        /// At that time, handling hw menu key should be ignored.
        /// </summary>
        /// <param name="sender">MoreMenuDialog</param>
        /// <param name="e">EventArgs</param>
        private void Dialog_Shown(object sender, EventArgs e)
        {
            // Disable handling HW menu key while MoreMenuDialog is shown
            MenuKeyListener.Stop(this);
        }

        /// <summary>
        /// Invoked when backbutton is pressed or the user clicks outside MoreMenuDialog area
        /// </summary>
        /// <param name="sender">MoreMenuDialog</param>
        /// <param name="e">EventArgs</param>
        private void Dialog_Canceled(object sender, EventArgs e)
        {
            // Make MoreMenuDialog hidden
            ((MoreMenuDialog)sender).Hide();
            // Restart listening HW Menu key and subscribing from messages about pressing HW meny key
            MenuKeyListener.Start(this, MenuKeyPressed);
        }

        /// <summary>
        /// Shows a floating button when the worldclock page is appearing
        /// </summary>
        protected override void OnAppearing()
        {
            // Make a floating button shown
            ((App)Application.Current).ShowFloatingButton(Title);
            // Start listening for key events
            MenuKeyListener.Start(this, MenuKeyPressed);
        }

        /// <summary>
        /// Hides a floating button when the worldclock page is disappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            // Stop listening for key events
            MenuKeyListener.Stop(this);
            // Make a floating button hidden
            ((App)Application.Current).HideFloatingButton(Title);
        }

        /// <summary>
        /// Creates image of world map and contents of timezone detail
        /// </summary>
        private void CreateMapArea()
        {
            CreateWorldMap();
            CreateTimezoenDetails();
        }

        /// <summary>
        /// Creates dot images for displaying city locations
        /// </summary>
        private void CreateDots()
        {
            try
            {
                dotImages = new Image[DOT_MAX_NUM];
                ringImages = new Image[DOT_MAX_NUM];
                for (int i = 0; i < DOT_MAX_NUM; i++)
                {
                    dotImages[i] = new Image();
                    dotImages[i].Source = "worldclock/clock_world_location_dot_b43.png";
                    dotImages[i].IsVisible = false;
                    mapLayout.Children.Add(dotImages[i],
                                  Constraint.RelativeToParent((parent) => { return 0; }),
                                  Constraint.RelativeToParent((parent) => { return 17; }));
                    ringImages[i] = new Image();
                    ringImages[i].Source = "worldclock/clock_world_location_ring.png";
                    ringImages[i].IsVisible = false;
                    mapLayout.Children.Add(ringImages[i],
                                  Constraint.RelativeToParent((parent) => { return 0; }),
                                  Constraint.RelativeToParent((parent) => { return 17; }));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[CreateDots] Exception Occurs : " + ex.Message);
            }
        }

        /// <summary>
        /// Creates area of world map
        /// </summary>
        private void CreateWorldMap()
        {
            mapLayout = new RelativeLayout
            {
                HeightRequest = 17 + 406 + 50 + 36 + 48 + 10 + 51 + 61, /*679*/
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                //BackgroundColor = Color.FromHex("#99dd00"),
            };

            Image worldMapImage = new Image
            {
                Source = "worldclock/clock_world_map_01.png",
                //BackgroundColor = Color.FromHex("#14CC88"),
            };
            mapLayout.Children.Add(worldMapImage,
              Constraint.RelativeToParent((parent) => { return 0; }),
              Constraint.RelativeToParent((parent) => { return 17; }));

            // Left arrow button
            Button arrowLeftImage = new Button
            {
                Image = "worldclock/clock_icon_world_clock_arrow_left.png",
                WidthRequest = 56,
                HeightRequest = 80,
                BackgroundColor = Color.FromHex("#00000000"),
                //BackgroundColor = Color.Firebrick,
            };

            mapLayout.Children.Add(arrowLeftImage,
              Constraint.RelativeToParent((parent) => { return 10; }),
              Constraint.RelativeToParent((parent) => { return 17 + 196; }));
            arrowLeftImage.Clicked += ArrowLeftImage_Clicked;

            // Right arrow button
            Button arrowRightImage = new Button
            {
                Image = "worldclock/clock_icon_world_clock_arrow_right.png",
                WidthRequest = 56,
                HeightRequest = 80,
                BackgroundColor = Color.FromHex("#00000000"),
            };

            mapLayout.Children.Add(arrowRightImage,
                          Constraint.RelativeToParent((parent) => { return 720 - 56 - 10; }),
                          Constraint.RelativeToParent((parent) => { return 17 + 196; }));
            arrowRightImage.Clicked += ArrowRightImage_Clicked;

            timezoneAreaImage = new Image
            {
                Source = "worldclock/clock_world_gmt_area_AO009_.png",
                Aspect = Aspect.Fill,
                WidthRequest = 15,
                HeightRequest = 320,
            };

            mapLayout.Children.Add(timezoneAreaImage,
              Constraint.RelativeToParent((parent) => { return 0; }),
              Constraint.RelativeToParent((parent) => { return 17 + 67; }));

            CreateDots();

            timezoneGmtOffsetLabel = new Label
            {
                FontSize = CommonStyle.GetDp(40),
                Text = "?",
                TextColor = Color.FromHex("#fafafa"),
                WidthRequest = 40 + 15 + 40,
                HeightRequest = 60 - 5,
                //BackgroundColor = Color.FromHex("#aa77cb"),
                HorizontalTextAlignment = TextAlignment.Center,
            };
            mapLayout.Children.Add(timezoneGmtOffsetLabel,
                    Constraint.RelativeToView(timezoneAreaImage, (parent, sibling) => { return sibling.X - 40; }),
                    Constraint.RelativeToView(timezoneAreaImage, (parent, sibling) => { return sibling.Y - 60; }));
        }

        /// <summary>
        /// Called when right arrow button has been clicked
        /// It updates Map view including time zone details according to GMT variation(current + 1)
        /// </summary>
        /// <param name="sender">arrowRightImage Button object</param>
        /// <seealso cref="System.object">
        /// <param name="e">Event argument for event of Button</param>
        /// <seealso cref="System.EventArgs">
        private void ArrowRightImage_Clicked(object sender, EventArgs e)
        {
            TimezoneUtility.MoveCurrentTimezone(Direction.RIGHT);
            UpdateMapAndTimezoneDetails(TimezoneUtility.GetCurrentTimezone());
        }

        /// <summary>
        /// Called when left arrow button has been clicked
        /// It updates Map view including time zone details according to GMT variation(current - 1)
        /// </summary>
        /// <param name="sender">arrowLeftImage Button object</param>
        /// <seealso cref="System.object">
        /// <param name="e">Event argument for event of Button</param>
        /// <seealso cref="System.EventArgs">
        private void ArrowLeftImage_Clicked(object sender, EventArgs e)
        {
            TimezoneUtility.MoveCurrentTimezone(Direction.LEFT);
            UpdateMapAndTimezoneDetails(TimezoneUtility.GetCurrentTimezone());
        }

        /// <summary>
        /// Updates map view and timezone details based on current timezone information
        /// </summary>
        public void OnMapViewUpdateRequest()
        {
            UpdateMapAndTimezoneDetails(TimezoneUtility.GetCurrentTimezone());
        }

        /// <summary>
        /// Updates timezone details based on the given timezone
        /// </summary>
        /// <param name="tz">The <see cref="Timezone"/> timezone for updating map view and timezone details </param>
        private void UpdateMapAndTimezoneDetails(Timezone tz)
        {
            UpdateTimezoneDots(tz);
            UpdateTimezoneArea(tz);
            UpdateGmtOffset(tz);
            UpdateTimezoneRelativeToLocal(tz.gmtOffset);
            UpdateTimezoneCitiesList(tz);
        }

        /// <summary>
        /// Draw dot and ring images at cities in the current timezone
        /// </summary>
        /// <param name="tz">The <see cref="Timezone"/> timezone to set for map </param>
        private void UpdateTimezoneDots(Timezone tz)
        {
            try
            {
                for (int i = 0; i < tz.places.Length; i++)
                {
                    dotImages[i].TranslationX = (tz.places[i].X - DOT_IMAGE_SIZE / 2);
                    ringImages[i].TranslationX = (tz.places[i].X - RING_IMAGE_SIZE / 2);
                    dotImages[i].TranslationY = (tz.places[i].Y - DOT_IMAGE_SIZE / 2);
                    ringImages[i].TranslationY = (tz.places[i].Y - RING_IMAGE_SIZE / 2);
                    dotImages[i].IsVisible = true;
                    ringImages[i].IsVisible = true;
                }

                for (int i = tz.places.Length; i < DOT_MAX_NUM; i++)
                {
                    dotImages[i].IsVisible = false;
                    ringImages[i].IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[UpdateTimezoneDots] Exception : " + ex.ToString() + ", " + ex.Message);
            }
        }

        /// <summary>
        /// Mark the current timezone area
        /// Image : timezoneAreaImage
        /// </summary>
        /// <param name="tz">The <see cref="Timezone"/> timezone to set for map </param>
        private void UpdateTimezoneArea(Timezone tz)
        {
            timezoneAreaImage.TranslationX = tz.xCoord + (tz.zoneWidth - timezoneAreaImage.Width) / 2;
            timezoneGmtOffsetLabel.TranslationX = tz.xCoord + (tz.zoneWidth - timezoneAreaImage.Width) / 2;
            timezoneAreaImage.IsVisible = true;
        }

        private string OffsetToString(int offset)
        {
            int abGmtOffset = Math.Abs(offset);
            int remainder = (abGmtOffset % 60) * 100 / 60;
            int remainder_str = (remainder % 10 != 0) ? remainder : remainder / 10;

            var updatedGmtOffset = String.Format("{0}{1}{2}{3}",
                (offset < 0) ? "" : "+",
                offset / 60,
                ((offset % 60 != 0) ? "." : ""),
                (remainder != 0) ? remainder_str.ToString() : "");

            return updatedGmtOffset;
        }

        /// <summary>
        /// Update current GMT offset information
        /// Label : timezoneGmtOffsetLabel
        /// </summary>
        /// <param name="tz">The <see cref="Timezone"/> timezone to set for map </param>
        private void UpdateGmtOffset(Timezone tz)
        {
            int gmtOffset = tz.gmtOffset;
            string updatedGmtOffset = OffsetToString(gmtOffset);
            timezoneGmtOffsetLabel.Text = updatedGmtOffset;
        }

        /// <summary>
        /// Update timezone related detail information
        /// </summary>
        /// <param name="offset">int</param>
        private void UpdateTimezoneRelativeToLocal(int offset)
        {
            CityRecordUtility.UpdateCityRecord(offset);
        }

        /// <summary>
        /// Update list of principal cities related to the given Timezone
        /// </summary>
        /// <param name="tz">Timezone</param>
        private void UpdateTimezoneCitiesList(Timezone tz)
        {
            string cities = tz.places[0].Name;
            for (int i = 1; i < tz.places.Length; i++)
            {
                cities += ", " + tz.places[i].Name;
            }

            CityRecordUtility.detail.Cities = cities;
        }

        /// <summary>
        /// Make UI for presenting details for timezone
        /// Time / AM or PM / relative time difference / principal cities
        /// </summary>
        private void CreateTimezoenDetails()
        {
            timeLabel = new Label
            {
                Text = "7:50",
                Style = Styles.WorldclockStyle.ATO017,
                WidthRequest = 164,
                HeightRequest = 94,
                HorizontalOptions = LayoutOptions.Center,
                //BackgroundColor = Color.Coral,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(timeLabel, FontWeight.Thin);
            timeLabel.SetBinding(Label.TextProperty, new Binding("CityTime", mode: BindingMode.OneWay, source: CityRecordUtility.detail));

            amLabel = new Label
            {
                Text = "a.m.",
                Style = Styles.WorldclockStyle.ATO018,
                WidthRequest = 70,
                HeightRequest = 48,
                HorizontalOptions = LayoutOptions.Center,
                //BackgroundColor = Color.BurlyWood,
                Margin = new Thickness(12, 36, 20, 10),
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(amLabel, FontWeight.Normal);
            amLabel.SetBinding(Label.TextProperty, new Binding("CityAmPm", mode: BindingMode.OneWay, source: CityRecordUtility.detail));

            relativeToLocalLabel = new Label
            {
                Text = "1h behind",
                Style = Styles.WorldclockStyle.ATO018,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 48,
                //BackgroundColor = Color.Pink,
                Margin = new Thickness(0, 36, 0, 10),
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(relativeToLocalLabel, FontWeight.Normal);
            relativeToLocalLabel.SetBinding(Label.TextProperty, new Binding("RelativeToLocalCountry", mode: BindingMode.OneWay, source: CityRecordUtility.detail));

            timezoneDetailFirstLineLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                HeightRequest = 94,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                //BackgroundColor = Color.FromHex("#22dd22"),
                Children =
                {
                    timeLabel,
                    amLabel,
                    relativeToLocalLabel
                }
            };
            StackLayout tmpStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                HeightRequest = 94,
                WidthRequest = 720,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                //BackgroundColor = Color.FromHex("#aa448b"),
                Children =
                {
                    timezoneDetailFirstLineLayout
                }
            };
            mapLayout.Children.Add(tmpStack,
              Constraint.RelativeToParent((parent) => { return 0; }),
              Constraint.RelativeToParent((parent) => { return 17 + 406 + 50; }));

            // principal cities in Timezone detail area
            citiesLabel = new Label
            {
                Text = "Beijing, Denpasar, Guangzhou, Haikou",
                Style = Styles.WorldclockStyle.ATO016,
                WidthRequest = 720 - 16 - 16,
                HeightRequest = 51,
                LineBreakMode = LineBreakMode.TailTruncation,
                //BackgroundColor = Color.Blue,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(citiesLabel, FontWeight.Normal);
            citiesLabel.SetBinding(Label.TextProperty, new Binding(path: "Cities", mode: BindingMode.OneWay, source: CityRecordUtility.detail));
            mapLayout.Children.Add(citiesLabel,
              Constraint.RelativeToParent((parent) => { return 16; }),
              Constraint.RelativeToParent((parent) => { return 17 + 406 + 50 + 94; }));
        }
    }
}
