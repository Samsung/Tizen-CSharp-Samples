using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Tizen.NUI.Binding;

namespace Badges.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string counterText;
        private int counter;
        private bool flag;
        public ICommand ApplyBadges { get; private set; }
        public ICommand ResetBadges { get; private set; }
        public ICommand ReduceCounter { get; private set; }
        public ICommand IncreaseCounter { get; private set; }
        public ViewModel()
        {
            counterText = "0";
            counter = 0;
            flag = false;
            ApplyBadges = new Command(ExecuteApplyBadges);
            ResetBadges = new Command(ExecuteResetBadges);
            ReduceCounter = new Command(ExecuteReduceCounter);
            IncreaseCounter = new Command(ExecuteIncreaseCounter);
        }

        public bool Flag
        {
            get => flag;
            set 
            { 
                if(flag != value)
                {
                    flag = value;
                    RaisePropertyChanged();
                }    
            }
        }

        public string CounterText
        {
            get => counterText;
            set
            {
                if (counterText != value)
                {
                    counterText = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int Counter
        {
            get => counter;
            set
            {
                if (counter != value)
                {
                    counter = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void ExecuteApplyBadges()
        {
            Flag = true;
        }
        private void ExecuteResetBadges()
        {
            Flag = false;
        }
        private void ExecuteReduceCounter()
        {
            if(Counter > 0)
            {
                Counter--;
                CounterText = counter.ToString();
            }     
        }
        private void ExecuteIncreaseCounter()
        {
            Counter++;
            CounterText = counter.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
