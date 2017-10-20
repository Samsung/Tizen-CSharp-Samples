using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Clock.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clock.ViewModels;

namespace Clock.ViewModels
{
    public class AlarmListViewModel : INotifyPropertyChanged
    {
        public AlarmViewModelObservableCollection<AlarmRecordViewModel> Items { get; }

        public AlarmListViewModel()
        {
            Items = new AlarmViewModelObservableCollection<AlarmRecordViewModel>(AlarmRecordViewModel.GetItems());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
