using System;
using Tizen.Wearable.CircularUI.Forms;
using UsingResxLocalization.Resx;
using Xamarin.Forms;

namespace UsingResxLocalization
{	
    /// <summary>
    /// FirstPageXaml class
    /// </summary>
	public partial class FirstPageXaml : CirclePage
	{	
        /// <summary>
        /// Constructor
        /// </summary>
		public FirstPageXaml()
		{
            // Make this page have no navigation bar
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
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

        /// <summary>
        /// Called when myButton is clicked.
        /// Alert message will be shown.
        /// The message will be localized based on the current system language.
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        async private void MyButton_Clicked(object sender, EventArgs e)
        {
            // Update texts
            var message = AppResources.AddMessageN;
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
        }
    }
}

