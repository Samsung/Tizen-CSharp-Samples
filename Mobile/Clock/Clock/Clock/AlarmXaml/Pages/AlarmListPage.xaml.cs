//#define ADD_TIZEN_FLOATING_BUTTON
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clock.ViewModels;
using Tizen.Xamarin.Forms.Extension;
using Clock.Tizen;
using Clock.Alarm;
using TizenClock.Tizen.AlarmXaml.Pages;

namespace Clock.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmListPage : ContentPage
    {
        public AlarmListPage()
        {
            BindingContext = new AlarmListViewModel();
            InitializeComponent();
            /// Title for this page
            Title = "Alarm";
            /// Icon to be shown in the main tab
            Icon = "maintabbed/clock_tabs_ic_alarm.png";
        }

        async public void OnFloatingButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(AlarmXamlPageController.GetInstance(TizenClock.Tizen.AlarmXaml.Pages.AlarmPages.EditPageXaml, BindingContext));
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(AlarmXamlPageController.GetInstance(TizenClock.Tizen.AlarmXaml.Pages.AlarmPages.EditPageXaml, BindingContext));
        }

        private void AlarmListOnOff_OnToggled(object sender, ToggledEventArgs e)
        {
            if (sender == null) return;
            Switch sw = (Switch)sender;
            if (sw.BindingContext is AlarmRecordViewModel)
            {
                AlarmRecordViewModel ar = (AlarmRecordViewModel)sw.BindingContext;
                ar.UpdateOnOffToDb();
            }
        }

        protected override void OnAppearing()
        {
            // When this page is shown, a floating button should be visible for an app user to add a new alarm
            ((App)Application.Current).ShowFloatingButton(Title);
        }

        /// <summary>
        /// Each time this page is disappearing, a floating button should be hidden.
        /// </summary>
        protected override void OnDisappearing()
        {
            ((App)Application.Current).HideFloatingButton(Title);
        }
    }
}
