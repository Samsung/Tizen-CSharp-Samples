using Clock.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Clock.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clock.Pages.Views;

namespace Clock.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmTonePage : ContentPage
    {
        private AlarmRecordViewModel source;

        public AlarmTonePage(AlarmRecordViewModel ar)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            source = ar;
            if (ar.AlarmToneType == AlarmToneTypes.AlarmMp3)
            {
                this.FindByName<AlarmRadioSwitch>("AlarmToneMp3").IsToggled = true;
            }
            else if (ar.AlarmToneType == AlarmToneTypes.RingtoneSdk)
            {
                this.FindByName<AlarmRadioSwitch>("AlarmToneSdk").IsToggled = true;
            }
            else
            {
                this.FindByName<AlarmRadioSwitch>("AlarmToneDefault").IsToggled = true;
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
            if (this.FindByName<AlarmRadioSwitch>("AlarmToneMp3").IsToggled == true)
            {
                source.AlarmToneType = AlarmToneTypes.AlarmMp3;
            }
            else if (this.FindByName<AlarmRadioSwitch>("AlarmToneSdk").IsToggled == true)
            {
                source.AlarmToneType = AlarmToneTypes.RingtoneSdk;
            }
            else
            {
                source.AlarmToneType = AlarmToneTypes.Default;
            }
            Navigation.PopAsync(false);
        }
    }
}
