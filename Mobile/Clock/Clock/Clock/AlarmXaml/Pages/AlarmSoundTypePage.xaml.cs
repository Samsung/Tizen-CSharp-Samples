using Clock.Pages.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Clock.Models;
using Clock.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clock.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmSoundTypePage : ContentPage
    {
        private AlarmRecordViewModel source;

        public AlarmSoundTypePage(AlarmRecordViewModel ar)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            source = ar;
            if (ar.AlarmType == AlarmTypes.Sound)
            {
                this.FindByName<AlarmRadioSwitch>("AlarmTypeSound").IsToggled = true;
            }
            else if (ar.AlarmType == AlarmTypes.Vibration)
            {
                this.FindByName<AlarmRadioSwitch>("AlarmTypeVibration").IsToggled = true;
            }
            else
            {
                this.FindByName<AlarmRadioSwitch>("AlarmTypeVibrationAndSound").IsToggled = true;
            }
        }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            AlarmRadioSwitch ars = (AlarmRadioSwitch)sender;
            if (e.Value == true)
            {
                ars.TurnOffOthers();
            }
        }

        private void TwoButtonPageHeader_OnRightButtonEvent(object sender, EventArgs e)
        {
            if (this.FindByName<AlarmRadioSwitch>("AlarmTypeSound").IsToggled == true)
            {
                source.AlarmType = AlarmTypes.Sound;
            }
            else if (this.FindByName<AlarmRadioSwitch>("AlarmTypeVibration").IsToggled == true)
            {
                source.AlarmType = AlarmTypes.Vibration;
            }
            else
            {
                source.AlarmType = AlarmTypes.SoundVibration;
            }
            Navigation.PopAsync(false);
        }
    }
}
