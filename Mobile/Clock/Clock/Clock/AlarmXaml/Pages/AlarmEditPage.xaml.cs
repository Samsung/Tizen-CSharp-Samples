using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clock.Models;
using Clock.Pages.Customs;
using Clock.Pages.Views;
using Clock.ViewModels;
using Xamarin.Forms;
using Tizen.Xamarin.Forms.Extension;

namespace Clock.Pages
{
    public partial class AlarmEditPage : ContentPage
    {
        private AlarmRecordViewModel originalAlarmViewModel;
        private AlarmListViewModel originalAlarmListViewModel;
        private AlarmRecordViewModel newAlarmViewModel;
        public AlarmEditPage(AlarmListViewModel alvm)
        {
            originalAlarmListViewModel = alvm;
            newAlarmViewModel = new AlarmRecordViewModel(NewAlarmRecordFactory.GetNewAlarm());
            BindingContext = newAlarmViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        public AlarmEditPage(AlarmRecordViewModel avm, AlarmListViewModel alvm)
        {
            originalAlarmViewModel = avm;
            originalAlarmListViewModel = alvm;
            newAlarmViewModel = new AlarmRecordViewModel(avm); // deep copy only info
            BindingContext = newAlarmViewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            this.FindByName<TwoButtonPageHeader>("ViewHeader").CenterTitleText = "Edit";
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            DisplayActionSheet("Title", "Cancel", "Destroy");
        }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            if (sender == null || e == null) return;
            if (e.Value == true)
            {
            }
            else
            {
            }
        }

        async private void Button_OnClicked2(object sender, EventArgs e)
        {
            // save this record to DB
            AlarmRecordViewModelEnum result = ((AlarmRecordViewModel)BindingContext).SaveToDb();
            if (result == AlarmRecordViewModelEnum.FailureNew)
            {
                Toast.DisplayText("Failed to add a new alarm (existing).");
            }
            else if (result == AlarmRecordViewModelEnum.SuccessCancelAndUpdateExisting)
            {
                originalAlarmListViewModel.Items.Remove(originalAlarmViewModel);
                Toast.DisplayText("The time is already existing so delete existing one");
            }
            else if (result == AlarmRecordViewModelEnum.SuccessUpdateExceptTime)
            {
                Toast.DisplayText("Success. In this row, change except time");
                originalAlarmViewModel.DeepCopy(newAlarmViewModel);
            }
            else if (result == AlarmRecordViewModelEnum.SuccessUpdateNonexisting)
            {
                Toast.DisplayText("Success. In this row, change time");
                originalAlarmViewModel.DeepCopy(newAlarmViewModel);
            }
            else if (result == AlarmRecordViewModelEnum.SuccessNew)
            {
                originalAlarmListViewModel.Items.Add((AlarmRecordViewModel)BindingContext);
                Toast.DisplayText("Added a new alarm successfully");
            }
            await Navigation.PopAsync(false);
        }

        protected override void OnAppearing()
        {

        }

        protected override void OnDisappearing()
        {

        }

        private void TapGestureRecognizer_SoundOnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AlarmSoundTypePage((AlarmRecordViewModel)BindingContext), false);
        }

        private void TapGestureRecognizer_ToneTypeOnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AlarmTonePage((AlarmRecordViewModel)BindingContext), false);
        }

        private void TapGestureRecognizer_RepeatTypeOnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AlarmRepeatPage((AlarmRecordViewModel)BindingContext), false);
        }
    }
}
