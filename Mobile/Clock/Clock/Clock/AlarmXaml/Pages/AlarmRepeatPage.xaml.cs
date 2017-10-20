using System;
using Clock.Models;
using Clock.Pages.Views;
using Clock.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Label = Xamarin.Forms.Label;

namespace Clock.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmRepeatPage : ContentPage
    {
        private AlarmRecordViewModel source;

        public AlarmRepeatPage(AlarmRecordViewModel ar)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            source = ar;
            //if (ar.AlarmType == AlarmTypes.Sound)
            //{
            //    this.FindByName<AlarmRadioSwitch>("AlarmTypeSound").IsToggled = true;
            //}
            //else if (ar.AlarmType == AlarmTypes.Vibration)
            //{
            //    this.FindByName<AlarmRadioSwitch>("AlarmTypeVibration").IsToggled = true;
            //}
            //else
            //{
            //    this.FindByName<AlarmRadioSwitch>("AlarmTypeVibrationAndSound").IsToggled = true;
            //}
            /// Checks whether this weekFlag indicates this CheckBox should turn on or not
            if (ar.WeekFlag == AlarmWeekFlag.AllDays)
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = true;
            }
            if ((ar.WeekFlag & AlarmWeekFlag.Monday) != 0)
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatMonday").IsChecked = true;
            }
            if ((ar.WeekFlag & AlarmWeekFlag.Tuesday) != 0)
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatTuesday").IsChecked = true;
            }
            if ((ar.WeekFlag & AlarmWeekFlag.Wednesday) != 0)
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatWednesday").IsChecked = true;
            }
            if ((ar.WeekFlag & AlarmWeekFlag.Thursday) != 0)
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatThursday").IsChecked = true;
            }
            if ((ar.WeekFlag & AlarmWeekFlag.Friday) != 0)
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatFriday").IsChecked = true;
            }
            if ((ar.WeekFlag & AlarmWeekFlag.Saturday) != 0)
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatSaturday").IsChecked = true;
            }
            if ((ar.WeekFlag & AlarmWeekFlag.Sunday) != 0)
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatSunday").IsChecked = true;
            }
        }

        private void TwoButtonPageHeader_OnRightButtonEvent(object sender, EventArgs e)
        {
            source.WeekFlag = 0;

            if (this.FindByName<AlarmCheckbox>("AlarmRepeatSunday").IsChecked == true)
            {
                source.WeekFlag = source.WeekFlag | AlarmWeekFlag.Sunday;
            }
            if (this.FindByName<AlarmCheckbox>("AlarmRepeatMonday").IsChecked == true)
            {
                source.WeekFlag = source.WeekFlag | AlarmWeekFlag.Monday;
            }
            if (this.FindByName<AlarmCheckbox>("AlarmRepeatTuesday").IsChecked == true)
            {
                source.WeekFlag = source.WeekFlag | AlarmWeekFlag.Tuesday;
            }
            if (this.FindByName<AlarmCheckbox>("AlarmRepeatWednesday").IsChecked == true)
            {
                source.WeekFlag = source.WeekFlag | AlarmWeekFlag.Wednesday;
            }
            if (this.FindByName<AlarmCheckbox>("AlarmRepeatThursday").IsChecked == true)
            {
                source.WeekFlag = source.WeekFlag | AlarmWeekFlag.Thursday;
            }
            if (this.FindByName<AlarmCheckbox>("AlarmRepeatFriday").IsChecked == true)
            {
                source.WeekFlag = source.WeekFlag | AlarmWeekFlag.Friday;
            }
            if (this.FindByName<AlarmCheckbox>("AlarmRepeatSaturday").IsChecked == true)
            {
                source.WeekFlag = source.WeekFlag | AlarmWeekFlag.Saturday;
            }
            Navigation.PopAsync(false);
        }

        private void AlarmRepeatEveryday_OnChecked(object sender, CheckedEventArgs e)
        {
            // if it is checked, all must be turned on
            if (e.Value == true)
            {
                TurnOnEveryday();
            }
        }

        private bool IsAllChecked()
        {
            if (this.FindByName<AlarmCheckbox>("AlarmRepeatMonday").IsChecked == true &&
                this.FindByName<AlarmCheckbox>("AlarmRepeatTuesday").IsChecked == true &&
                this.FindByName<AlarmCheckbox>("AlarmRepeatWednesday").IsChecked == true &&
                this.FindByName<AlarmCheckbox>("AlarmRepeatThursday").IsChecked == true &&
                this.FindByName<AlarmCheckbox>("AlarmRepeatFriday").IsChecked == true &&
                this.FindByName<AlarmCheckbox>("AlarmRepeatSaturday").IsChecked == true &&
                this.FindByName<AlarmCheckbox>("AlarmRepeatSunday").IsChecked == true) return true;
            else return false;
        }

        private void TurnOnEveryday()
        {
            this.FindByName<AlarmCheckbox>("AlarmRepeatMonday").IsChecked = true;
            this.FindByName<AlarmCheckbox>("AlarmRepeatTuesday").IsChecked = true;
            this.FindByName<AlarmCheckbox>("AlarmRepeatWednesday").IsChecked = true;
            this.FindByName<AlarmCheckbox>("AlarmRepeatThursday").IsChecked = true;
            this.FindByName<AlarmCheckbox>("AlarmRepeatFriday").IsChecked = true;
            this.FindByName<AlarmCheckbox>("AlarmRepeatSaturday").IsChecked = true;
            this.FindByName<AlarmCheckbox>("AlarmRepeatSunday").IsChecked = true;
        }

        private void AlarmRepeatMonday_OnChecked(object sender, CheckedEventArgs e)
        {
            if (e.Value == true)
            {
                if (IsAllChecked()) this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = true;
            }
            else
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = false;
            }
        }

        private void AlarmRepeatTuesday_OnChecked(object sender, CheckedEventArgs e)
        {
            if (e.Value == true)
            {
                if (IsAllChecked()) this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = true;
            }
            else
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = false;
            }
        }

        private void AlarmRepeatWednesday_OnChecked(object sender, CheckedEventArgs e)
        {
            if (e.Value == true)
            {
                if (IsAllChecked()) this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = true;
            }
            else
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = false;
            }
        }

        private void AlarmRepeatThursday_OnChecked(object sender, CheckedEventArgs e)
        {
            if (e.Value == true)
            {
                if (IsAllChecked()) this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = true;
            }
            else
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = false;
            }
        }

        private void AlarmRepeatFriday_OnChecked(object sender, CheckedEventArgs e)
        {
            if (e.Value == true)
            {
                if (IsAllChecked()) this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = true;
            }
            else
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = false;
            }
        }

        private void AlarmRepeatSaturday_OnChecked(object sender, CheckedEventArgs e)
        {
            if (e.Value == true)
            {
                if (IsAllChecked()) this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = true;
            }
            else
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = false;
            }
        }

        private void AlarmRepeatSunday_OnChecked(object sender, CheckedEventArgs e)
        {
            if (e.Value == true)
            {
                if (IsAllChecked()) this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = true;
            }
            else
            {
                this.FindByName<AlarmCheckbox>("AlarmRepeatEveryday").IsChecked = false;
            }
        }
    }
}