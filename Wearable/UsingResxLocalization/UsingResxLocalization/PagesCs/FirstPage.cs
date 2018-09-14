using System;
using Xamarin.Forms;
using UsingResxLocalization.Resx;
using Tizen.Wearable.CircularUI.Forms;

namespace UsingResxLocalization
{
    /// <summary>
    /// FirstPage class
    /// </summary>
    public class FirstPage : CirclePage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FirstPage()
        {
            // Make this page have no navigation bar
            NavigationPage.SetHasNavigationBar(this, false);
            // create UI controls
            var myLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center
            };
            var myEntry = new Entry
            {
                HorizontalTextAlignment = TextAlignment.Center,
            };
            var myButton = new Button();
            var myPicker = new Picker();
            myPicker.Items.Add("0");
            myPicker.Items.Add("1");
            myPicker.Items.Add("2");
            myPicker.Items.Add("3");
            myPicker.Items.Add("4");

            // apply translated resources
            myLabel.Text = AppResources.NotesLabel;
            myEntry.Placeholder = AppResources.NotesPlaceholder;
            myPicker.Title = AppResources.PickerName;
            myButton.Text = AppResources.AddButton;

            var flag = new Image();
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    flag.Source = ImageSource.FromFile("Assets/Images/flag.png");
                    break;
                case Device.iOS:
                case Device.Android:
                case Device.Tizen:
                default:
                    flag.Source = ImageSource.FromFile("flag.png");
                    break;
            }

            // When myButton is pressed,
            // alert message is displayed.
            // This message will be localized according to the current system language.
            myButton.Clicked += async (sender, e) =>
            {
                var message = AppResources.AddMessageN;
                // According to the selected index of picker, message will be selected.
                if (myPicker.SelectedIndex <= 0)
                {
                    message = AppResources.AddMessage0;
                }
                else if (myPicker.SelectedIndex == 1)
                {
                    message = AppResources.AddMessage1;
                }
                else
                {
                    message = String.Format(message, myPicker.Items[myPicker.SelectedIndex]);
                }

                // Display an alert dialog
                await DisplayAlert(message, message, AppResources.CancelButton);
            };

            // add to screen
            CircleScrollView myScrollView = new CircleScrollView()
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        myLabel,
                        myEntry,
                        myPicker,
                        myButton,
                        flag
                    },
                }
            };
            // Now, can scroll the view using rotating the bezel
            RotaryFocusObject = myScrollView;
            Content = myScrollView;

            if (Device.RuntimePlatform == Device.Tizen)
            {
                MessagingCenter.Subscribe<App>(this, "UpdateUIByLanguageChanges", (obj) =>
                {
                    // apply translated resources
                    myLabel.Text = AppResources.NotesLabel;
                    myEntry.Placeholder = AppResources.NotesPlaceholder;
                    myPicker.Title = AppResources.PickerName;
                    myButton.Text = AppResources.AddButton;
                });
            }
        }
    }
}

